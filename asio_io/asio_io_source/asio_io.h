#ifndef __Asio_IO__
#define __Asio_IO__

#include "asio.h"

enum {
	// number of input and outputs supported by the host application
	// you can change these to higher or lower values
	kMaxInputChannels = 256,
	kMaxOutputChannels = 256
};

typedef int ASIOstatus;
enum {
	kBeginning = 0,
	kDriverLoaded = 1,
	kInitilized = 2,
	kPrepared = 3,
	kStarted = 4
};

typedef int ERROR_STATUS;
enum {
	k_no_error = 0,
	k_invalid_protocol = -1,
	k_no_asio_driver = -2,
	k_load_driver_error = -4,
	k_initialize_error_1 = -5,
	k_initialize_error_2 = -6,
	k_initialize_error_3 = -7,
	k_initialize_error_4 = -8,
	k_create_buffers_error_1 = -9,
	k_create_buffers_error_2 = -10,
	k_start_error_1 = -11,
	k_stop_error_1 = -14,
};

// internal data storage
typedef struct DriverInfo
{
	// ASIOInit()
	ASIODriverInfo driverInfo;

	// ASIOGetChannels()
	long           inputChannels;
	long           outputChannels;

	// ASIOGetBufferSize()
	long           minSize;
	long           maxSize;
	long           preferredSize;
	long           granularity;

	// ASIOGetSampleRate()
	ASIOSampleRate sampleRate;

	// ASIOOutputReady()
	bool           postOutput;

	// ASIOGetLatencies ()
	long           inputLatency;
	long           outputLatency;

	// ASIOCreateBuffers ()
	long inputBuffers;	// becomes number of actual created input buffers
	long outputBuffers;	// becomes number of actual created output buffers
	ASIOBufferInfo bufferInfos[kMaxInputChannels + kMaxOutputChannels]; // buffer info's

	// ASIOGetChannelInfo()
	ASIOChannelInfo channelInfos[kMaxInputChannels + kMaxOutputChannels]; // channel info's
	// The above two arrays share the same indexing, as the data in them are linked together

	// Information from ASIOGetSamplePosition()
	// data is converted to double floats for easier use, however 64 bit integer can be used, too
	double         nanoSeconds;
	double         samples;
	double         tcSamples;	// time code samples

	// bufferSwitchTimeInfo()
	ASIOTime       tInfo;			// time info state
	unsigned long  sysRefTime;      // system reference time, when bufferSwitch() was called

	// Signal the end of processing in this example
	bool           stopped;
} DriverInfo;

typedef ASIOTime *(*fp)(ASIOTime *timeInfo, long index, ASIOBool processNow); // function returning function pointer);

#endif
