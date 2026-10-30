#using <mscorlib.dll>

#include <stdio.h>
#include <string.h>
#include "asiosys.h"
#include "asio.h"
#include "asiodrivers.h"
#include "asio_io.h"
#include "soundtools.h"

ASIOCallbacks asioCallbacks;
DriverInfo asioDriverInfo = {0};

//----------------------------------------------------------------------------------
// some external references
extern AsioDrivers* asioDrivers;
extern int g_temporary_outch;
extern int *g_asio_binary_buf;
extern int g_preferredSize;
extern int g_binary_leng;

extern int g_amp_gain_val[];
extern int g_amp_gain_cur;
extern int g_amp_gain_next;
extern int g_amp_gain_max;
extern bool g_amp_gain_enable;
extern ERROR_STATUS g_error;
ST_ERROR_STATUS st_error;

//int amp_gain_val_size;
int g_gain_ch = (AmpGainChannel - 1);
int g_processedSamples_binary = 0;
bool getstop();
void setstop(bool is_stop);
void damp_out(int index);

// callback prototypes
void sampleRateChanged(ASIOSampleRate sRate);
long asioMessages(long selector, long value, void* message, double* opt);
unsigned long get_sys_reference_time();

#define TIME_RECORD 100
int time_record_counter = 0;
bool time_record_full = false;
LARGE_INTEGER current;
unsigned int current_time;
unsigned int prev_time;
unsigned int *time_record;
void set_zitter_intvl();

fp bufferSwitchTimeInfo;

//----------------------------------------------------------------------------------
// conversion from 64 bit ASIOSample/ASIOTimeStamp to double float
#if NATIVE_INT64
	#define ASIO64toDouble(a)  (a)
#else
	const double twoRaisedTo32 = 4294967296.;
	#define ASIO64toDouble(a)  ((a).lo + (a).hi * twoRaisedTo32)
#endif

unsigned long get_sys_reference_time()
{	// get the system reference time
#if WINDOWS
	return timeGetTime();
#elif MAC
static const double twoRaisedTo32 = 4294967296.;
	UnsignedWide ys;
	Microseconds(&ys);
	double r = ((double)ys.hi * twoRaisedTo32 + (double)ys.lo);
	return (unsigned long)(r / 1000.);
#endif
}

ASIOTime *bufferSwitchTimeInfo_d(ASIOTime *timeInfo, long index, ASIOBool processNow)
{	// the actual processing callback.
	// Beware that this is normally in a seperate thread, hence be sure that you take care
	// about thread synchronization. This is omitted here for simplicity.

	// store the timeInfo for later use
	asioDriverInfo.tInfo = *timeInfo;

	// get the time stamp of the buffer, not necessary if no
	// synchronization to other media is required
	if (timeInfo->timeInfo.flags & kSystemTimeValid)
		asioDriverInfo.nanoSeconds = ASIO64toDouble(timeInfo->timeInfo.systemTime);
	else
		asioDriverInfo.nanoSeconds = 0;

	if (timeInfo->timeInfo.flags & kSamplePositionValid)
		asioDriverInfo.samples = ASIO64toDouble(timeInfo->timeInfo.samplePosition);
	else
		asioDriverInfo.samples = 0;

	if (timeInfo->timeCode.flags & kTcValid)
		asioDriverInfo.tcSamples = ASIO64toDouble(timeInfo->timeCode.timeCodeSamples);
	else
		asioDriverInfo.tcSamples = 0;

	// get the system reference time
	asioDriverInfo.sysRefTime = get_sys_reference_time();
	if (SoundTool::SoundTools::asio_play(getstop(), index))
	{
		st_error = SoundTool::SoundTools::getErrorStatus();
		setstop(true);
	}
	if (g_amp_gain_enable) damp_out(index);
	set_zitter_intvl();

	if (asioDriverInfo.postOutput) ASIOOutputReady();

	return 0L;
}

//

//----------------------------------------------------------------------------------
void bufferSwitch(long index, ASIOBool processNow)
{	// the actual processing callback.
	// Beware that this is normally in a seperate thread, hence be sure that you take care
	// about thread synchronization. This is omitted here for simplicity.

	// as this is a "back door" into the bufferSwitchTimeInfo a timeInfo needs to be created
	// though it will only set the timeInfo.samplePosition and timeInfo.systemTime fields and the according flags
	ASIOTime  timeInfo;
	memset (&timeInfo, 0, sizeof (timeInfo));

	// get the time stamp of the buffer, not necessary if no
	// synchronization to other media is required
	if(ASIOGetSamplePosition(&timeInfo.timeInfo.samplePosition, &timeInfo.timeInfo.systemTime) == ASE_OK)
		timeInfo.timeInfo.flags = kSystemTimeValid | kSamplePositionValid;

	bufferSwitchTimeInfo(&timeInfo, index, processNow);
}


void damp_out(int index)
{
	if (g_amp_gain_next != g_amp_gain_cur && asioDriverInfo.outputBuffers >= g_gain_ch)
	{
		for (int ch = g_gain_ch; ch < asioDriverInfo.outputBuffers; ch += 64)
		{
			int *iptr = (int *)asioDriverInfo.bufferInfos[asioDriverInfo.inputBuffers + ch].buffers[index];
			*iptr = g_amp_gain_val[g_amp_gain_cur] << 16;
		}
		g_amp_gain_cur++;
		if (g_amp_gain_cur == g_amp_gain_max) g_amp_gain_cur = 0;
	}
	else
	{
		for (int ch = g_gain_ch; ch < asioDriverInfo.outputBuffers; ch += 64)
		{
			int *iptr = (int *)asioDriverInfo.bufferInfos[asioDriverInfo.inputBuffers + ch].buffers[index];
			*iptr = 0;
		}
	}
}

ASIOTime *bufferSwitchTimeInfo_binary(ASIOTime *timeInfo, long index, ASIOBool processNow)
{
	asioDriverInfo.tInfo = *timeInfo;
	if (timeInfo->timeInfo.flags & kSystemTimeValid)
		asioDriverInfo.nanoSeconds = ASIO64toDouble(timeInfo->timeInfo.systemTime);
	else
		asioDriverInfo.nanoSeconds = 0;

	if (timeInfo->timeInfo.flags & kSamplePositionValid)
		asioDriverInfo.samples = ASIO64toDouble(timeInfo->timeInfo.samplePosition);
	else
		asioDriverInfo.samples = 0;

	if (timeInfo->timeCode.flags & kTcValid)
		asioDriverInfo.tcSamples = ASIO64toDouble(timeInfo->timeCode.timeCodeSamples);
	else
		asioDriverInfo.tcSamples = 0;

	// get the system reference time
	asioDriverInfo.sysRefTime = get_sys_reference_time();

	// buffer size in samples
	long buffSize = g_preferredSize;

	int remain = g_binary_leng - g_processedSamples_binary;
	if (remain <= 0)
	{
		setstop(true);
	}
	if (getstop()) return 0L;
	if (buffSize >= remain)
	{
		buffSize = remain;
	}

	// perform the processing
	int ch_no = asioDriverInfo.inputBuffers + g_temporary_outch - 1;
	//DebugLog("g_asio_binary_out : outch = ", g_temporary_outch);
	//DebugLog("g_asio_binary_out : val = ", g_asio_binary_buf[g_processedSamples_binary]);
	memcpy(asioDriverInfo.bufferInfos[ch_no].buffers[index], &g_asio_binary_buf[g_processedSamples_binary], buffSize * 4);

	// finally if the driver supports the ASIOOutputReady() optimization, do it here, all data are in place
	if (asioDriverInfo.postOutput)
		ASIOOutputReady();

	g_processedSamples_binary = g_processedSamples_binary + buffSize;

	return 0L;
}

//----------------------------------------------------------------------------------
void sampleRateChanged(ASIOSampleRate sRate)
{
	// do whatever you need to do if the sample rate changed
	// usually this only happens during external sync.
	// Audio processing is not stopped by the driver, actual sample rate
	// might not have even changed, maybe only the sample rate status of an
	// AES/EBU or S/PDIF digital input at the audio device.
	// You might have to update time/sample related conversion routines, etc.
}

//----------------------------------------------------------------------------------
long asioMessages(long selector, long value, void* message, double* opt)
{
	// currently the parameters "value", "message" and "opt" are not used.
	long ret = 0;
	switch(selector)
	{
		case kAsioSelectorSupported:
			if(value == kAsioResetRequest
			|| value == kAsioEngineVersion
			|| value == kAsioResyncRequest
			|| value == kAsioLatenciesChanged
			// the following three were added for ASIO 2.0, you don't necessarily have to support them
			|| value == kAsioSupportsTimeInfo
			|| value == kAsioSupportsTimeCode
			|| value == kAsioSupportsInputMonitor)
				ret = 1L;
			break;
		case kAsioResetRequest:
			// defer the task and perform the reset of the driver during the next "safe" situation
			// You cannot reset the driver right now, as this code is called from the driver.
			// Reset the driver is done by completely destruct is. I.e. ASIOStop(), ASIODisposeBuffers(), Destruction
			// Afterwards you initialize the driver again.
			setstop(true);  // In this sample the processing will just stop
			ret = 1L;
			break;
		case kAsioResyncRequest:
			// This informs the application, that the driver encountered some non fatal data loss.
			// It is used for synchronization purposes of different media.
			// Added mainly to work around the Win16Mutex problems in Windows 95/98 with the
			// Windows Multimedia system, which could loose data because the Mutex was hold too long
			// by another thread.
			// However a driver can issue it in other situations, too.
			ret = 1L;
			break;
		case kAsioLatenciesChanged:
			// This will inform the host application that the drivers were latencies changed.
			// Beware, it this does not mean that the buffer sizes have changed!
			// You might need to update internal delay data.
			ret = 1L;
			break;
		case kAsioEngineVersion:
			// return the supported ASIO version of the host application
			// If a host applications does not implement this selector, ASIO 1.0 is assumed
			// by the driver
			ret = 2L;
			break;
		case kAsioSupportsTimeInfo:
			// informs the driver wether the asioCallbacks.bufferSwitchTimeInfo() callback
			// is supported.
			// For compatibility with ASIO 1.0 drivers the host application should always support
			// the "old" bufferSwitch method, too.
			ret = 1;
			break;
		case kAsioSupportsTimeCode:
			// informs the driver wether application is interested in time code info.
			// If an application does not need to know about time code, the driver has less work
			// to do.
			ret = 0;
			break;
	}
	return ret;
}

bool getstop()
{
	return asioDriverInfo.stopped;
}

void setstop(bool is_stop)
{
	asioDriverInfo.stopped = is_stop;
}

void init_timer()
{
	time_record_counter = 0;
	time_record_full = false;
	QueryPerformanceCounter(&current);
	prev_time = current.LowPart;
	if (time_record == 0)
	{
		time_record = (unsigned int *)calloc(TIME_RECORD, sizeof(unsigned int));
	}
}

void set_zitter_intvl()
{
	QueryPerformanceCounter(&current);
	int time_intvl = (int)(current.LowPart - prev_time);
	time_record[time_record_counter++] = time_intvl;
	if (time_record_counter == TIME_RECORD)
	{
		time_record_full = true;
		time_record_counter = 0;
	}
	prev_time = current.LowPart;
}

//---------------------------------------------------------------------------------
double asio_time_stdev() {
	double ave = 0.0;
	int rec_leng = TIME_RECORD;
	if (time_record_full == false) return -1.0;
	for (int i = 0; i < rec_leng; i++)
	{
		ave += time_record[i];
	}
	ave /= rec_leng;
	double stdev = 0.0;
	for (int i = 0; i < rec_leng; i++)
	{
		double tt = time_record[i] - ave;
		stdev += tt * tt;
	}
	stdev /= rec_leng;
	stdev = sqrt(stdev) / ave;
	return stdev;
}