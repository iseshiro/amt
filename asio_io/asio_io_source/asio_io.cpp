#using <mscorlib.dll>
#include <windows.h>
#include <string>
#include "asiodrivers.h"
#include "asio_io.h"
#include <gcroot.h> 
#include "soundtools.h"


//----------------------------------------------------------------------------------
// some external references
extern AsioDrivers* asioDrivers;
extern DriverInfo asioDriverInfo;
extern ASIOCallbacks asioCallbacks;

extern bool loadAsioDriver(char *name);
extern ASIOTime *bufferSwitchTimeInfo_d(ASIOTime *timeInfo, long index, ASIOBool processNow);
extern void bufferSwitch(long index, ASIOBool processNow);
extern ASIOTime *bufferSwitchTimeInfo_binary(ASIOTime *timeInfo, long index, ASIOBool processNow);
extern void sampleRateChanged(ASIOSampleRate sRate);
extern long asioMessages(long selector, long value, void* message, double* opt);
extern bool getstop();
extern void setstop(bool is_stop);
extern void init_timer();
extern double asio_time_stdev();
extern unsigned int *time_record;
extern int g_processedSamples_binary;

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::IO;

int g_binary_leng = 0;
int g_preferredSize;
int g_sfreq;
//int g_da_delay = 4096;

SYSTEM_INFO  siSysInfo;
int g_temporary_outch;
int *g_asio_binary_buf;
ERROR_STATUS g_error = 0;
bool g_use_convolution = false;

#define AmpGainSize 8192
int g_amp_gain_val[AmpGainSize];
int g_amp_gain_cur = 0;
int g_amp_gain_next = 0;
int g_amp_gain_max = AmpGainSize;
bool g_amp_gain_enable = false;

extern fp bufferSwitchTimeInfo;

#define DriverListMax 20
#define ADDRESS "127.0.0.1"
#define PORT 7000
#define OUTPUT_BUFFER_SIZE 1024

namespace Asio_io{
	extern "C" {
		public ref class asio_io
		{
		private:
			ASIOError create_asio_buffers(DriverInfo *asioDriverInfo);
			bool ValidDataType();
			~asio_io();
			//bool without_driver;
			void getlist();
			float **src_ptr;

		public:
			//int in_ch_cnt;
			//int out_ch_cnt;
			//int grp_ch_cnt;
			int driver_cnt;
			char *Driver_Name;
			char **DriversList;
			ASIOstatus status;

			asio_io();
			int infoget(wchar_t *driver_name);
			int change_bufsize(int new_size);
			int initialize();
			//int init_buf_only(int inch, int outch, int Sfreq);
			int exit();
			int create_buffers();
			int dispose_buffers();
			int start();
			int stop();
			//int forceStop();
			void push_asio_mode(ASIOmode asio_mode) { SoundTool::SoundTools::push_asio_mode(asio_mode); }
			bool asio_binarymode_init(int leng);
			int stream_word_count() { return SoundTool::SoundTools::stream_word_count(); }
			int GetByteSizePerData(int in_ch_cnt, int out_ch_cnt);
			int GetPreferredSize() { return g_preferredSize; }
			int GetAsioInputChannels() { return asioDriverInfo.inputChannels; }
			int GetAsioOutputChannels() { return asioDriverInfo.outputChannels; }
			void makeTSP(int fftpnt, BTN_TYPE type, int btn_no, double amp);
			double Convolution(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out, int fftpoint, double scale, double freq_st, double freq_ed);
			double Deconvolution(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out, int fftpoint, double scale, double freq_st, double freq_ed);
			bool get_stop() { return getstop(); };
			void set_stop(bool is_stop) { setstop(is_stop); }
			int getADpoint() { return SoundTool::SoundTools::getADpoint(); }
			bool set_level_meter(bool is_input, int ch, int mode);
			double get_peak_rms() { return SoundTool::SoundTools::get_peak_rms(); }
			int get_adbuf_point() { return SoundTool::SoundTools::get_adbuf_point(); }
			void setADpoint(int cnt) { SoundTool::SoundTools::setADpoint(cnt); }
			bool can_sfreq(int sfreq1);
			int get_sfreq();
			int set_sfreq(int sfreq1);
			void set_repeat_mode(bool repeat);
			double get_amp16_1() { return amp16_1; }
			double get_amp16() { return amp16; }
			double get_amp32_1() { return amp32_1; }
			double get_amp32() { return amp32; }
			double asio_zitter();
			//void reset_error_cnt();
			void rtconv_set(int fft_level, float *imp, int in_ch, int out_ch, int word_cnt_max, bool is_last);
			void rtconv_fpga_set(int fpga_ch, float *imp, int out_ch, int fir_tap);
			int rtconv_total_error();
			int rtconv_error_cnt(int no);
			int rt_fft_level() { return SoundTool::SoundTools::rt_fft_level(); }
			int core_count() { return siSysInfo.dwNumberOfProcessors; }
			//void rtconv_start();
			//void rtconv_stop();
			bool micamp_gain_set(int madi_no, int board_no, int ch_no, int ctrl_val, int gpo_val, int gain_val);
			char *error_message();
			int check_error();
			//void rtconv_imp_updating(bool is_updating);
			void damp_volume_set(int amp_val);
			bool has_damp_volume();
			int rtmove_add_src(wchar_t* argv, wchar_t* src_file);
			int rtmove_add_ue_src(wchar_t* src_file, int sfreq);
			int rtmove_add_vr_src(int input_count);
			void init_gpara();

			double getScale(double maxval);
			void makePureTone(double amp, double freq, BTN_TYPE type, int btn_no);
			void makeWhiteNoise(double amp, unsigned long seed, BTN_TYPE type, int btn_no);
			void makeTSP(int fftpnt, BTN_TYPE type, int btn_no, double amp, int coef_m_numer, int coef_m_denom, double coef_shift, int flag);
			void makePinkNoise_Pole(double amp, unsigned long seed, BTN_TYPE type, int btn_no);
			void makePinkNoise_Voss(double amp, unsigned long seed, BTN_TYPE type, int btn_no);
			void makeFilter(int fftpnt, BTN_TYPE type, int btn_no, int freq_from, int freq_to, int level_from, int level_to);
			void makeSaw(BTN_TYPE type, int btn_no, double max_val, double min_val, int period_point, bool max2min);

			double IIR_LPF(int filter_order, double cut_freq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
			double IIR_HPF(int filter_order, double cut_freq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
			double IIR_BPF(int filter_order, double lcutfreq, double hcutfreq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
			double IIR_OCTBPF(int filter_order, double centerfreq, double bandwidth, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);

			bool rtconv_check() { return SoundTool::SoundTools::rtconv_check(); }
			bool rtconv_init(int fft_level, int in_ch, int out_ch, bool is_single) { return SoundTool::SoundTools::rtconv_init(fft_level, in_ch, out_ch, is_single); }
			bool fconv_init(int fft_level, int in_ch, int out_ch, int pref_size, int fifo_size, int fft_point);
			bool fconv_set_src(int in_ch, wchar_t* src_file, int pref_size);
			int fconv_file_length(int in_ch);
			void fconv_close_src(int in_ch);
			int get_fconv_format(int in_ch);
			//void fconv_set_filt(int in_ch, int out_ch, float *data, int conv_leng, bool is_last);
			bool fconv_create_mid(wchar_t *mid_file);
			float fconv_close_mid();
			void fconv_convolution(int in_ch, int out_ch, int pref_size, int write_size, int processedSamples);

			void fmove_init(int Mic_cnt, int Shift_size, int Sfreq);
			void fmove_src_cnt(int Src_cnt);
			void fmove_add_src(int src_no, double play_time, double amp, double start_time, double stop_time, int seq_cnt);
			void fmove_set_fileinfo(int src_no, wchar_t *file_path);
			bool fmove_add_seq(int src_no, int shift_no, double posi_x, double posi_y, double posi_z, bool stay);
			int fmove_set_outfile1(int mic_no, wchar_t *file_path);
			bool fmove_move(int shift_no);
			bool fmove_sequence_update(int src_no);
			double fmove_current_posi(int src_no, int xyz);
			int fmove_close1(int mic_no, wchar_t *file_path);
			float fmove_maxval();

			//--- channel_info
			//void group_channel_in(int btn_no, int ch_cnt, int ch_no);
			//void group_channel_out(int btn_no, int ch_cnt, int ch_no);
			double GetMaxVal(BTN_TYPE type, int btn_no);
			double GetMaxValClear(BTN_TYPE type, int btn_no);
			int GetOffset(BTN_TYPE type, int btn_no);
			int GetMaxID(BTN_TYPE type, int btn_no);
			void SetMaxVal(BTN_TYPE type, int btn_no);
			void SetOffset(BTN_TYPE type, int btn_no, int st_point, int ed_point);
			void CorrectOffset(BTN_TYPE type, int btn_no);
			void data_clear(BTN_TYPE type, int btn_no);
			void data_clear1(BTN_TYPE type, int btn_no, int st_point, int ed_point);
			void data_clear2(BTN_TYPE type, int btn_no, int st_point, int ed_point);
			double GetPower_t(BTN_TYPE type, int btn_no, int fftpoint);
			void data_dispose(BTN_TYPE type, int btn_no);
			void CopyData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
			void SubData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
			double Verify(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
			double verify_data(BTN_TYPE type, int btn_no, float* src_dat, int offset, int length);
			int data_create(BTN_TYPE type, int btn_no, STREAM_TYPE st_type);
			void get_data(BTN_TYPE type, int btn_no, float* dst_dat, int length, int offset);
			void load_data(BTN_TYPE type, int btn_no, float* src_dat, int length, int ch_cnt, int load_ch);
			void load_data1(BTN_TYPE type, int btn_no, float* src_dat, int offset, int length);
			//void clear_buf();
			void move_forward(BTN_TYPE type, int btn_no, int position);
			void ClearBothSides(BTN_TYPE type, int btn_no, int start, int end);
			//void ClearAll(bool dispose);
			bool have_buffer(BTN_TYPE type, int btn_no);
			bool have_signal(BTN_TYPE type, int btn_no);
			bool SetDataLength(int leng, bool without_asio);
			int GetDataLength();
			void Add(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out);
			void Multiply(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out);
			void MultiplyValue(BTN_TYPE type_in1, int btn_no_in1, double val);
			void makePulse(double amp, int delay, int width, BTN_TYPE type, int btn_no);
			void makeDC(double amp, BTN_TYPE type, int btn_no);
			double data(int cnt, BTN_TYPE type, int btn_no);
			void getminmax(int stpnt, int edpnt, BTN_TYPE type, int btn_no, double *rmin, double *rmax);
			void getminmax_famp(int stpnt, int edpnt, BTN_TYPE type, int btn_no, double *rmin, double *rmax);
			double GetTimeSigPower(BTN_TYPE type, int btn_no, int st_point, int ed_point);
			void HanningWindow(int start_pnt, int end_pnt, BTN_TYPE type, int btn_no);
			void HanningWindow2(int end_pnt, BTN_TYPE type, int btn_no);
			void Delay(int delay, BTN_TYPE type, int btn_no);
			//void AdaptiveFIR(int err_ch, int imp2_ch, int fir_ch, double mu, int firtap);
			//void AdaptiveFIR_Inv(int err_ch, int imp2_ch, int fir_ch, double mu, int firtap);
			//void update_info1();
			void create_chinfo(int in_ch_cnt, int out_ch_cnt, int work_ch_cnt);
			void assign_ch(int in_ch_cnt, int *asio_inch_list, int out_ch_cnt, int *asio_outch_list);
			//int find_asio_ch(BTN_TYPE btn_type, int btn_no);
			void spamp_gain_enable(bool is_enable) { g_amp_gain_enable = is_enable; }
			void spamp_gain_push(int amp_val);

			//--- fft
			double famp(int cnt) { return SoundTool::SoundTools::famp(cnt); }
			double GetPower_f(int fftpoint) { return SoundTool::SoundTools::GetPower_f(fftpoint); }
			void freq_range_clear(int st_freq, int ed_freq, int fftpoint) { SoundTool::SoundTools::freq_range_clear(st_freq, ed_freq, fftpoint); }
			void DivData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out, int fftpnt) { SoundTool::SoundTools::DivData(type_in, btn_no_in, type_out, btn_no_out, fftpnt); }
			//double MakeConvolution(int in1, int in2, int out, int fftpnt, double scale, int freq_st, int freq_ed);
			//double MakeDeconvolution(int in1, int in2, int out, int fftpnt, double scale, int freq_st, int freq_ed);
			void makeFFT(int fftpnt, int start_pnt, int end_pnt, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makeFFT(fftpnt, start_pnt, end_pnt, type, btn_no); }
			void FFT(float* dre, float* dim, int fftpnt);

			//--- rtmove
			bool rtmove_init(int mic_mode, int frame_size, int max_src_cnt, int input_count);
			double rtmove_mic_radius();
			void rtmove_stop(int stop_src_no);
			int rtmove_chg_src(int src_no, wchar_t *argv, wchar_t *src_file);
			void rtmove_chg_amp(int src_no, double new_amp);
			void rtmove_play_mode(int src_no, int mode, double playback_start, double playback_delay, double time_to_que);
			void rtmove_move(int src_no, double posi_x, double posi_y, double posi_z);
			void rtmove_move_polar(int src_no, double rr, double theta, double phi);
			int rtmove_maxmic(int src_no);
			int rtmove_save_json(wchar_t *name);
			int rtmove_save_data(wchar_t *name);
			int rtmove_get_wavfile_no(int src_no);
			int rtmove_src_leng(int src_no);

			//--- wav_stream
			int stream_open_mch_in(wchar_t *src_file, int file_ch, int ch);
			int stream_open_mch_out(wchar_t *src_file, int file_ch, int ch);
			//int stream_open_read_wav1ch(wchar_t *file_name, int ch, double amp);
			int stream_open_write(wchar_t *file_name, BTN_TYPE type, int btn_no, FILE_FORMAT file_format) {	return SoundTool::SoundTools::stream_open_write(file_name, type, btn_no, file_format); }
			void stream_close(BTN_TYPE type, int btn_no) { SoundTool::SoundTools::stream_close(type, btn_no); }
			//bool has_stream();
			int stream_file_point();
			bool is_stream_audio_out();
			float udp_max_val();
			float udp_min_val();
			int effective_ir_length(wchar_t *src_file, BTN_TYPE type, int btn_no, bool is_double) {	return SoundTool::SoundTools::effective_ir_length(src_file, type, btn_no, is_double); }
			void stream_set_leng(int start_ch, int end_ch, int leng) { SoundTool::SoundTools::stream_set_leng(start_ch, end_ch, leng); }

			//--- make_inv
			void makeinv_init(int sp_count, int mic_count, int fftpoint);
			void makeinv_dataset(int data_leng, int sp_no, int mic_no, float *data);
			void makeinv_calc();

			//--udp_client
			bool create_udp_sender(wchar_t *ip_address, int port_no, int asio_ch, bool send_sin1k);
			void dispose_udp_sender();
			bool create_udp_receiver(int port_no, int delay);
			void dispose_udp_receiver();
			int udp_receive();
			void attach_song(int id);

			//--play_list
			int playlist_stream_attach(int id, int start, int stop, bool is_single);
			bool play_list_apend(wchar_t *path, int ch_cnt, double start, double stop);
			void play_list_remove(int id);

			//--resample
			int resample(wchar_t* src_file, wchar_t* dst_file, int src_sfreq, int dst_sfreq, bool use_sinc_func) { return SoundTool::SoundTools::resample(src_file, dst_file, src_sfreq, dst_sfreq, use_sinc_func); }
			int resample(float* src_data, int src_leng, wchar_t* dst_file, int src_sfreq, int dst_sfreq, bool use_sinc_func) { return SoundTool::SoundTools::resample(src_data, src_leng, dst_file, src_sfreq, dst_sfreq, use_sinc_func); }
		};
	}

//---------------------------------------------------------------------------------
asio_io::asio_io()
{
	//DebugLog("asio_io 1: status = ", status);
	status=kBeginning;

	//without_driver = true;
	::GetSystemInfo((LPSYSTEM_INFO)&siSysInfo);
	getlist();
	time_record = 0;
	//DebugLog("asio_io 2: status = ", status);

#ifdef LogDebug
	SoundTool::SoundTools::SoundTools_debug_init();
#endif
}

asio_io::~asio_io()
{
	//DebugLog("~asio_io 1: status = ", status);
	if(status == kStarted)
	{
		ASIOStop();
		status = kPrepared;
	}
	if(status == kPrepared)
	{
		ASIODisposeBuffers();
		status = kInitilized;
	}
	if(status == kInitilized)
	{
		ASIOExit();
		status = kDriverLoaded;
	}

	if(DriversList!=0) {
		for(int i=0;i<DriverListMax;i++) free(DriversList[i]);
		free(DriversList);
	}

	SoundTool::SoundTools::SoundTools_exit();  // This include SoundTools_debug_exit()
	if(time_record != 0) free(time_record);
}

void asio_io::init_gpara() { SoundTool::SoundTools::init_gpara(); }
double asio_io::getScale(double maxval) { return SoundTool::SoundTools::getScale(maxval); }
void asio_io::makePureTone(double amp, double freq, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makePureTone(amp, freq, type, btn_no); }
void asio_io::makeWhiteNoise(double amp, unsigned long seed, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makeWhiteNoise(amp, seed, type, btn_no); }
void asio_io::makeTSP(int fftpnt, BTN_TYPE type, int btn_no, double amp, int coef_m_numer, int coef_m_denom, double coef_shift, int flag) { SoundTool::SoundTools::makeTSP(fftpnt, type, btn_no, amp, coef_m_numer, coef_m_denom, coef_shift, flag); }
void asio_io::makePinkNoise_Pole(double amp, unsigned long seed, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makePinkNoise_Pole(amp, seed, type, btn_no); }
void asio_io::makePinkNoise_Voss(double amp, unsigned long seed, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makePinkNoise_Voss(amp, seed, type, btn_no); }
void asio_io::makeFilter(int fftpnt, BTN_TYPE type, int btn_no, int freq_from, int freq_to, int level_from, int level_to) { SoundTool::SoundTools::makeFilter(fftpnt, type, btn_no, freq_from, freq_to, level_from, level_to); }
void asio_io::makeSaw(BTN_TYPE type, int btn_no, double max_val, double min_val, int period_point, bool max2min) { SoundTool::SoundTools::makeSaw(type, btn_no, max_val, min_val, period_point, max2min); }
double asio_io::IIR_LPF(int filter_order, double cut_freq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { return SoundTool::SoundTools::IIR_LPF(filter_order, cut_freq, ripple, scale, type_in, btn_no_in, type_out, btn_no_out); }
double asio_io::IIR_HPF(int filter_order, double cut_freq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { return SoundTool::SoundTools::IIR_HPF(filter_order, cut_freq, ripple, scale, type_in, btn_no_in, type_out, btn_no_out); }
double asio_io::IIR_BPF(int filter_order, double lcutfreq, double hcutfreq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { return SoundTool::SoundTools::IIR_BPF(filter_order, lcutfreq, hcutfreq, ripple, scale, type_in, btn_no_in, type_out, btn_no_out); }
double asio_io::IIR_OCTBPF(int filter_order, double centerfreq, double bandwidth, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { return SoundTool::SoundTools::IIR_OCTBPF(filter_order, centerfreq, bandwidth, ripple, scale, type_in, btn_no_in, type_out, btn_no_out); }
bool asio_io::fconv_init(int fft_level, int in_ch, int out_ch, int pref_size, int fifo_size, int fft_point) { return SoundTool::SoundTools::fconv_init(fft_level, in_ch, out_ch, pref_size, fifo_size, fft_point); }
bool asio_io::fconv_set_src(int in_ch, wchar_t* src_file, int pref_size) { return SoundTool::SoundTools::fconv_set_src(in_ch, src_file, pref_size); }
int asio_io::fconv_file_length(int in_ch) { return SoundTool::SoundTools::fconv_file_length(in_ch); }
void asio_io::fconv_close_src(int in_ch) { SoundTool::SoundTools::fconv_close_src(in_ch); }
int asio_io::get_fconv_format(int in_ch) { return SoundTool::SoundTools::get_fconv_format(in_ch); }
//void asio_io::fconv_set_filt(int in_ch, int out_ch, float *data, int conv_leng, bool is_last) { SoundTool::SoundTools::fconv_set_filt(in_ch, out_ch, data, conv_leng, is_last); }
bool asio_io::fconv_create_mid(wchar_t *mid_file) { return SoundTool::SoundTools::fconv_create_mid(mid_file); }
float asio_io::fconv_close_mid() { return SoundTool::SoundTools::fconv_close_mid(); }
void asio_io::fconv_convolution(int in_ch, int out_ch, int pref_size, int write_size, int processedSamples) { SoundTool::SoundTools::fconv_convolution(in_ch, out_ch, pref_size, write_size, processedSamples); }
void asio_io::fmove_init(int Mic_cnt, int Shift_size, int Sfreq) { SoundTool::SoundTools::fmove_init(Mic_cnt, Shift_size, Sfreq); }
void asio_io::fmove_src_cnt(int Src_cnt) { SoundTool::SoundTools::fmove_src_cnt(Src_cnt); }
void asio_io::fmove_add_src(int src_no, double play_time, double amp, double start_time, double stop_time, int seq_cnt) { SoundTool::SoundTools::fmove_add_src(src_no, play_time, amp, start_time, stop_time, seq_cnt); }
void asio_io::fmove_set_fileinfo(int src_no, wchar_t *file_path) { SoundTool::SoundTools::fmove_set_fileinfo(src_no, file_path); }
bool asio_io::fmove_add_seq(int src_no, int shift_no, double posi_x, double posi_y, double posi_z, bool stay) { return SoundTool::SoundTools::fmove_add_seq(src_no, shift_no, posi_x, posi_y, posi_z, stay); }
int asio_io::fmove_set_outfile1(int mic_no, wchar_t *file_path) { return SoundTool::SoundTools::fmove_set_outfile1(mic_no, file_path); }
bool asio_io::fmove_move(int shift_no) { return SoundTool::SoundTools::fmove_move(shift_no); }
bool asio_io::fmove_sequence_update(int src_no) { return SoundTool::SoundTools::fmove_sequence_update(src_no); }
double asio_io::fmove_current_posi(int src_no, int xyz) { return SoundTool::SoundTools::fmove_current_posi(src_no, xyz); }
int asio_io::fmove_close1(int mic_no, wchar_t *file_path) { return SoundTool::SoundTools::fmove_close1(mic_no, file_path); }
float asio_io::fmove_maxval() { return SoundTool::SoundTools::fmove_maxval(); }
//void asio_io::group_channel_in(int btn_no, int ch_cnt, int ch_no) { SoundTool::SoundTools::group_channel_in(btn_no, ch_cnt, ch_no); }
//void asio_io::group_channel_out(int btn_no, int ch_cnt, int ch_no) { SoundTool::SoundTools::group_channel_out(btn_no, ch_cnt, ch_no); }
double asio_io::GetMaxVal(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::GetMaxVal(type, btn_no); }
double asio_io::GetMaxValClear(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::GetMaxValClear(type, btn_no); }
int asio_io::GetOffset(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::GetOffset(type, btn_no); }
int asio_io::GetMaxID(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::GetMaxID(type, btn_no); }
void asio_io::SetMaxVal(BTN_TYPE type, int btn_no) { SoundTool::SoundTools::SetMaxVal(type, btn_no); }
void asio_io::SetOffset(BTN_TYPE type, int btn_no, int st_point, int ed_point) { SoundTool::SoundTools::SetOffset(type, btn_no, st_point, ed_point); }
void asio_io::CorrectOffset(BTN_TYPE type, int btn_no) { SoundTool::SoundTools::CorrectOffset(type, btn_no); }
void asio_io::data_clear(BTN_TYPE type, int btn_no) { SoundTool::SoundTools::data_clear(type, btn_no); }
void asio_io::data_clear1(BTN_TYPE type, int btn_no, int st_point, int ed_point) { SoundTool::SoundTools::data_clear1(type, btn_no, st_point, ed_point); }
void asio_io::data_clear2(BTN_TYPE type, int btn_no, int st_point, int ed_point) { SoundTool::SoundTools::data_clear2(type, btn_no, st_point, ed_point); }
//void asio_io::SetAmp(BTN_TYPE type, int btn_no, double amp) { SoundTool::SoundTools::SetAmp(type, btn_no, amp); }
//float asio_io::*get_data_ptr(BTN_TYPE type, int btn_no, int offset) { return SoundTool::SoundTools::*get_data_ptr(type, btn_no, offset); }
//float asio_io::*get_data_ptr0(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::*get_data_ptr0(type, btn_no); }
double asio_io::GetPower_t(BTN_TYPE type, int btn_no, int fftpoint) { return SoundTool::SoundTools::GetPower_t(type, btn_no, fftpoint); }
void asio_io::data_dispose(BTN_TYPE type, int btn_no) { SoundTool::SoundTools::data_dispose(type, btn_no); }
void asio_io::CopyData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { SoundTool::SoundTools::CopyData(type_in, btn_no_in, type_out, btn_no_out); }
void asio_io::SubData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { SoundTool::SoundTools::SubData(type_in, btn_no_in, type_out, btn_no_out); }
double asio_io::Verify(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out) { return SoundTool::SoundTools::Verify(type_in, btn_no_in, type_out, btn_no_out); }
double asio_io::verify_data(BTN_TYPE type, int btn_no, float* src_dat, int offset, int length) { return SoundTool::SoundTools::verify_data(type, btn_no, src_dat, offset, length); }
int asio_io::data_create(BTN_TYPE type, int btn_no, STREAM_TYPE st_type) { return SoundTool::SoundTools::data_create(type, btn_no, st_type); }
void asio_io::get_data(BTN_TYPE type, int btn_no, float* dst_dat, int length, int offset) { SoundTool::SoundTools::get_data(type, btn_no, dst_dat, length, offset); }
void asio_io::load_data(BTN_TYPE type, int btn_no, float* src_dat, int length, int ch_cnt, int load_ch) { SoundTool::SoundTools::load_data(type, btn_no, src_dat, length, ch_cnt, load_ch); }
void asio_io::load_data1(BTN_TYPE type, int btn_no, float* src_dat, int offset, int length) { SoundTool::SoundTools::load_data1(type, btn_no, src_dat, offset, length); }
void asio_io::move_forward(BTN_TYPE type, int btn_no, int position) { SoundTool::SoundTools::move_forward(type, btn_no, position); }
void asio_io::ClearBothSides(BTN_TYPE type, int btn_no, int start, int end) { SoundTool::SoundTools::ClearBothSides(type, btn_no, start, end); }
//void asio_io::ClearAll(bool dispose) { SoundTool::SoundTools::ClearAll(dispose); }
bool asio_io::have_buffer(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::have_buffer(type, btn_no); }
bool asio_io::have_signal(BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::have_signal(type, btn_no); }
bool asio_io::SetDataLength(int leng, bool without_asio) { return SoundTool::SoundTools::SetDataLength(leng, without_asio); }
int asio_io::GetDataLength() { return SoundTool::SoundTools::GetDataLength(); }
void asio_io::Add(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out) { SoundTool::SoundTools::Add(type_in1, btn_no_in1, type_in2, btn_no_in2, type_out, btn_no_out); }
void asio_io::Multiply(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out) { SoundTool::SoundTools::Multiply(type_in1, btn_no_in1, type_in2, btn_no_in2, type_out, btn_no_out); }
void asio_io::MultiplyValue(BTN_TYPE type_in1, int btn_no_in1, double val) { SoundTool::SoundTools::MultiplyValue(type_in1, btn_no_in1, val); }
void asio_io::makePulse(double amp, int delay, int width, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makePulse(amp, delay, width, type, btn_no); }
void asio_io::makeDC(double amp, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::makeDC(amp, type, btn_no); }
double asio_io::data(int cnt, BTN_TYPE type, int btn_no) { return SoundTool::SoundTools::data(cnt, type, btn_no); }
void asio_io::getminmax(int stpnt, int edpnt, BTN_TYPE type, int btn_no, double *rmin, double *rmax) { SoundTool::SoundTools::getminmax(stpnt, edpnt, type, btn_no, rmin, rmax); }
void asio_io::getminmax_famp(int stpnt, int edpnt, BTN_TYPE type, int btn_no, double *rmin, double *rmax) { SoundTool::SoundTools::getminmax_famp(stpnt, edpnt, type, btn_no, rmin, rmax); }
double asio_io::GetTimeSigPower(BTN_TYPE type, int btn_no, int st_point, int ed_point) { return SoundTool::SoundTools::GetTimeSigPower(type, btn_no, st_point, ed_point); }
void asio_io::HanningWindow(int start_pnt, int end_pnt, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::HanningWindow(start_pnt, end_pnt, type, btn_no); }
void asio_io::HanningWindow2(int end_pnt, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::HanningWindow2(end_pnt, type, btn_no); }
void asio_io::Delay(int delay, BTN_TYPE type, int btn_no) { SoundTool::SoundTools::Delay(delay, type, btn_no); }
void asio_io::FFT(float* dre, float* dim, int fftpnt) { SoundTool::SoundTools::fft_safe(dre, dim, fftpnt); }
bool asio_io::rtmove_init(int mic_mode, int frame_size, int max_src_cnt, int input_count) { return SoundTool::SoundTools::rtmove_init(mic_mode, frame_size, max_src_cnt, input_count); }
bool asio_io::set_level_meter(bool is_input, int ch, int mode) { return SoundTool::SoundTools::set_level_meter(is_input, ch, mode); }
double asio_io::rtmove_mic_radius() { return SoundTool::SoundTools::rtmove_mic_radius(); }
void asio_io::rtmove_stop(int stop_src_no) { SoundTool::SoundTools::rtmove_stop(stop_src_no); }
int asio_io::rtmove_add_src(wchar_t* argv, wchar_t* src_file) { return SoundTool::SoundTools::rtmove_add_src(argv, src_file); }
int asio_io::rtmove_add_ue_src(wchar_t* src_file, int sfreq) { return SoundTool::SoundTools::rtmove_add_ue_src(src_file, sfreq); }
int asio_io::rtmove_add_vr_src(int input_count) { return SoundTool::SoundTools::rtmove_add_vr_src(input_count); }
int asio_io::rtmove_chg_src(int src_no, wchar_t *argv, wchar_t *src_file) { return SoundTool::SoundTools::rtmove_chg_src(src_no, argv, src_file); }
void asio_io::rtmove_chg_amp(int src_no, double new_amp) { SoundTool::SoundTools::rtmove_chg_amp(src_no, new_amp); }
void asio_io::rtmove_play_mode(int src_no, int mode, double playback_start, double playback_delay, double time_to_que) { SoundTool::SoundTools::rtmove_play_mode(src_no, mode, playback_start, playback_delay, time_to_que); }
void asio_io::rtmove_move(int src_no, double posi_x, double posi_y, double posi_z) { SoundTool::SoundTools::rtmove_move(src_no, posi_x, posi_y, posi_z); }
void asio_io::rtmove_move_polar(int src_no, double rr, double theta, double phi) { SoundTool::SoundTools::rtmove_move_polar(src_no, rr, theta, phi); }
int asio_io::rtmove_maxmic(int src_no) { return SoundTool::SoundTools::rtmove_maxmic(src_no); }
int asio_io::rtmove_save_json(wchar_t *name) { return SoundTool::SoundTools::rtmove_save_json(name); }
int asio_io::rtmove_save_data(wchar_t *name) { return SoundTool::SoundTools::rtmove_save_data(name); }
int asio_io::rtmove_get_wavfile_no(int src_no) { return SoundTool::SoundTools::rtmove_get_wavfile_no(src_no); }
int asio_io::rtmove_src_leng(int src_no) { return SoundTool::SoundTools::rtmove_src_leng(src_no); }
int asio_io::stream_open_mch_in(wchar_t *src_file, int file_ch, int btn_no) { return SoundTool::SoundTools::stream_open_mch_in(src_file, file_ch, btn_no); }
int asio_io::stream_open_mch_out(wchar_t *src_file, int file_ch, int btn_no) { return SoundTool::SoundTools::stream_open_mch_out(src_file, file_ch, btn_no); }
//int asio_io::stream_open_read_wav1ch(wchar_t *file_name, int ch, double amp) { return SoundTool::SoundTools::stream_open_read_wav1ch(file_name, ch, amp); }
//int asio_io::stream_open_write(wchar_t *file_name, int ch, FILE_FORMAT file_format) { return SoundTool::SoundTools::stream_open_write(file_name, ch, file_format); }
//bool asio_io::has_stream() { return SoundTool::SoundTools::has_stream(); }
int asio_io::playlist_stream_attach(int id, int start, int stop, bool is_single) { return SoundTool::SoundTools::playlist_stream_attach(id, start, stop, is_single); }
int asio_io::stream_file_point() { return SoundTool::SoundTools::stream_file_point(); }
bool asio_io::is_stream_audio_out() { return SoundTool::SoundTools::is_stream_audio_out(); }
float asio_io::udp_max_val() { return SoundTool::SoundTools::udp_max_val(); }
float asio_io::udp_min_val() { return SoundTool::SoundTools::udp_min_val(); }
void asio_io::makeinv_init(int sp_count, int mic_count, int fftpoint) { SoundTool::SoundTools::makeinv_init(sp_count, mic_count, fftpoint); }
void asio_io::makeinv_dataset(int data_leng, int sp_no, int mic_no, float *data) { SoundTool::SoundTools::makeinv_dataset(data_leng, sp_no, mic_no, data); }
void asio_io::makeinv_calc() { SoundTool::SoundTools::makeinv_calc(); }
void asio_io::dispose_udp_sender() { SoundTool::SoundTools::dispose_udp_sender(); }
bool asio_io::create_udp_receiver(int port_no, int delay) { return SoundTool::SoundTools::create_udp_receiver(port_no, delay); }
void asio_io::dispose_udp_receiver() { SoundTool::SoundTools::dispose_udp_receiver(); }
int asio_io::udp_receive() { return SoundTool::SoundTools::udp_receive(); }
void asio_io::attach_song(int id) { SoundTool::SoundTools::attach_song(id); }

void asio_io::create_chinfo(int in_ch_cnt, int out_ch_cnt, int work_ch_cnt) {
	SoundTool::SoundTools::SoundTools_alloc(in_ch_cnt, out_ch_cnt, work_ch_cnt);
	for (int i = 0; i < in_ch_cnt; i++)
	{
		//SoundTool::SoundTools::DebugLog("assign_ch create_chinfo: type, btn_no", btn_type_in, i + 1);
		SoundTool::SoundTools::create_chinfo(btn_type_in, i + 1);
	}
	for (int i = 0; i < out_ch_cnt; i++)
	{
		//SoundTool::SoundTools::DebugLog("assign_ch create_chinfo: type, btn_no", btn_type_out, i + 1);
		SoundTool::SoundTools::create_chinfo(btn_type_out, i + 1);
	}
}

void asio_io::assign_ch(int in_ch_cnt, int *asio_inch_list, int out_ch_cnt, int *asio_outch_list) {
	for (int i = 0; i < in_ch_cnt; i++)
	{
		//SoundTool::SoundTools::DebugLog("assign_ch create_chinfo: type, btn_no, asio_ch", btn_type_in, i + 1, asio_inch_list[i]);
		//SoundTool::SoundTools::DebugLog("assign_ch asio ptr: ", (int)asioDriverInfo.bufferInfos[asio_inch_list[i] - 1].buffers);
		SoundTool::SoundTools::assign_ch(btn_type_in, i + 1, asio_inch_list[i], asioDriverInfo.bufferInfos[asio_inch_list[i] - 1].buffers);
	}
	for (int i = 0; i < out_ch_cnt; i++)
	{
		//SoundTool::SoundTools::DebugLog("assign_ch create_chinfo: type, btn_no, asio_ch", btn_type_out, i + 1, asio_outch_list[i]);
		//SoundTool::SoundTools::DebugLog("assign_ch asio ptr: ", (int)asioDriverInfo.bufferInfos[asioDriverInfo.inputBuffers + asio_outch_list[i] - 1].buffers);
		SoundTool::SoundTools::assign_ch(btn_type_out, i + 1, asio_outch_list[i], asioDriverInfo.bufferInfos[asioDriverInfo.inputBuffers + asio_outch_list[i] - 1].buffers);
	}
}
//int asio_io::find_asio_ch(BTN_TYPE btn_type, int btn_no) {
//	int asio_ch;
//	switch (btn_type)
//	{
//	case btn_type_in:
//		asio_ch = g_assign_in[btn_no - 1] + 1;
//		break;
//	case btn_type_out:
//		asio_ch = g_assign_out[btn_no - 1] + 1;
//		break;
//	case btn_type_work:
//		asio_ch = g_assign_work[btn_no - 1] + 1;
//		break;
//	}
//	return asio_ch;
//}

bool asio_io::create_udp_sender(wchar_t *ip_address, int port_no, int asio_ch, bool send_sin1k)
{ return SoundTool::SoundTools::create_udp_sender(ip_address, port_no, asioDriverInfo.bufferInfos[asio_ch - 1].buffers, send_sin1k); }

bool asio_io::play_list_apend(wchar_t *path, int ch_cnt, double start, double stop)
{
	return SoundTool::SoundTools::playlist_apend(path, ch_cnt, start, stop);
}

void asio_io::play_list_remove(int id)
{
	SoundTool::SoundTools::playlist_remove(id);
}

//---------------------------------------------------------------------------------
int asio_io::GetByteSizePerData(int in_ch_cnt, int out_ch_cnt)
{
	int bytesize_per_data = 0;
	bytesize_per_data = 4;
	int ch_cnt = in_ch_cnt + out_ch_cnt;
	return ch_cnt*bytesize_per_data;
}

//----------------------------------------------------------------------------------
double asio_io::Convolution(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out, int fftpoint, double scale, double freq_st, double freq_ed)
{
	double new_scale = 0.0;
    int freq_st_i = (int)(freq_st * (double)fftpoint / (double)g_sfreq);
    int freq_ed_i = (int)(freq_ed * (double)fftpoint / (double)g_sfreq);
	new_scale = SoundTool::SoundTools::MakeConvolution(type_in1, btn_no_in1, type_in2, btn_no_in2, type_out, btn_no_out,fftpoint,scale,freq_st_i,freq_ed_i);
	return new_scale;
}
//----------------------------------------------------------------------------------
double asio_io::Deconvolution(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out, int fftpoint, double scale, double freq_st, double freq_ed)
{
	double new_scale = 0.0;
    int freq_st_i = (int)(freq_st * (double)fftpoint / (double)g_sfreq);
    int freq_ed_i = (int)(freq_ed * (double)fftpoint / (double)g_sfreq);
	new_scale = SoundTool::SoundTools::MakeDeconvolution(type_in1, btn_no_in1, type_in2, btn_no_in2, type_out, btn_no_out,fftpoint,scale,freq_st_i,freq_ed_i);
	return new_scale;
}

//----------------------------------------------------------------------------------
void asio_io::makeTSP(int fftpnt, BTN_TYPE type, int btn_no, double amp)
{
	makeTSP(fftpnt, type, btn_no, amp, 3, 8, 0.125, -1);
}
//bool asio_io::have_out_buffer(int ch)
//{
//	if( ch <= 0) return false;
//	if( ch > asioDriverInfo.outputBuffers ) return false;
//	int ch_no = ch + asioDriverInfo.inputBuffers;
//	if(SoundTool::SoundTools::get_data_ptr(ch_no, 0)==0) return false;
//	return true;
//}

//----------------------------------------------------------------------------------
bool asio_io::can_sfreq(int sfreq1)
{
	double sfreq_d = (double)sfreq1;
	int ret_val = ASIOCanSampleRate(sfreq_d);
	return (ret_val==0);
}

//----------------------------------------------------------------------------------
int asio_io::get_sfreq()
{
	return g_sfreq;
}

//----------------------------------------------------------------------------------
int asio_io::set_sfreq(int sfreq1)
{
	if (sfreq1 == g_sfreq) return g_sfreq;
	double sfreq_d = (double)sfreq1;
	int ret_val = ASIOSetSampleRate(sfreq_d);
	if (ASIOGetSampleRate(&asioDriverInfo.sampleRate) != ASE_OK)
	{
		g_error = k_initialize_error_4;
		return g_error;
	}
	g_sfreq = (int)asioDriverInfo.sampleRate;
	SoundTool::SoundTools::set_sfreq(g_sfreq);
	return g_sfreq;
}

//----------------------------------------------------------------------------------
int asio_io::check_error()
{
	return g_error;
}

//----------------------------------------------------------------------------------
char *asio_io::error_message()
{
	switch (g_error)
	{
	case k_invalid_protocol:
		return "Invalid protocol";
	case k_no_asio_driver:
		return "ASIO driver not exist";
	case k_load_driver_error:
		return "Can not load driver";
	case k_initialize_error_1:
		return "Can not initialize";
	case k_initialize_error_2:
		return "Can not get channels";
	case k_initialize_error_3:
		return "Can not get buffer size";
	case k_initialize_error_4:
		return "Can not sample rate";
	case k_create_buffers_error_1:
		return "Can not create buffers";
	case k_create_buffers_error_2:
		return "Unsupported data type";
	case k_start_error_1:
		return "ASIO can not start";
	case k_stop_error_1:
		return "ASIO stop without start";
	default:
		return SoundTool::SoundTools::getErrorMessage();
	}
}


//----------------------------------------------------------------------------------
int asio_io::initialize()
{
	//DebugLog("initialize 1: status = ", status);
	if (status == kInitilized)
	{
		return 0;
	}
	else if (status != kDriverLoaded)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}

	// initialize the driver
	if (ASIOInit(&asioDriverInfo.driverInfo) != ASE_OK)
	{
		g_error = k_initialize_error_1;
		return g_error;
	}

	// get the number of available channels
	if (ASIOGetChannels(&asioDriverInfo.inputChannels, &asioDriverInfo.outputChannels) != ASE_OK)
	{
		g_error = k_initialize_error_2;
		return g_error;
	}

	//in_ch_cnt = asioDriverInfo.inputChannels;
	//if (in_ch_cnt == 388)
	//{
	//	in_ch_cnt = 256;
	//}
	//out_ch_cnt = asioDriverInfo.outputChannels;
	// get the usable buffer sizes
	if (ASIOGetBufferSize(&asioDriverInfo.minSize, &asioDriverInfo.maxSize, &asioDriverInfo.preferredSize, &asioDriverInfo.granularity) != ASE_OK)
	{
		g_error = k_initialize_error_3;
		return g_error;
	}

	// get the currently selected sample rate
	if (ASIOGetSampleRate(&asioDriverInfo.sampleRate) != ASE_OK)
	{
		g_error = k_initialize_error_4;
		return g_error;
	}

	g_sfreq=(int)asioDriverInfo.sampleRate;

	// check wether the driver requires the ASIOOutputReady() optimization
	// (can be used by the driver to reduce output latency by one block)
	if(ASIOOutputReady() == ASE_OK)
		asioDriverInfo.postOutput = true;
	else
		asioDriverInfo.postOutput = false;

	//without_driver=false;
	//console_msg(S"Status: Initialized\r\n");
	status = kInitilized;
	g_error = k_no_error;
	//DebugLog("initialize 2: status = ", status);
	return g_error;
}

int asio_io::exit()
{
	if (status == kDriverLoaded)
	{
		return 0;
	}
	else if (status != kInitilized)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}

	ASIOExit();
	status = kDriverLoaded;
	//DebugLog("exit 2: status = ", status);
	return 0;
}

//---------------------------------------------------------------------------------
//---------------------------------------------------------------------------------
bool asio_io::asio_binarymode_init(int leng)
{
	SoundTool::SoundTools::push_asio_mode(asiomode_binary_out);
	g_binary_leng = leng;
	g_asio_binary_buf = (int *)calloc(g_binary_leng, sizeof(int));
	g_processedSamples_binary = 0;
	return true;
}

//---------------------------------------------------------------------------------
void asio_binarymode_exit()
{
	SoundTool::SoundTools::pop_asio_mode();
	free(g_asio_binary_buf);
}

int asio_io::create_buffers()
{
	//DebugLog("create_buffers 1: status = ", status);
	if (status == kPrepared)
	{
		return 0;
	}
	else if (status != kInitilized)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}
	//if(without_driver)
	//{
	//	status = kPrepared;
	//	return 0;
	//}

	asioCallbacks.sampleRateDidChange = &sampleRateChanged;
	asioCallbacks.asioMessage = &asioMessages;

	asioCallbacks.bufferSwitch = &bufferSwitch;

	if(SoundTool::SoundTools::is_binaryout())
	{
		asioCallbacks.bufferSwitchTimeInfo = &bufferSwitchTimeInfo_binary;
	}
	else
	{
		asioCallbacks.bufferSwitchTimeInfo = &bufferSwitchTimeInfo_d;
	}

	bufferSwitchTimeInfo = asioCallbacks.bufferSwitchTimeInfo;
	int result = create_asio_buffers (&asioDriverInfo);
	if (result != ASE_OK)
	{
		g_error = k_create_buffers_error_1;
		return g_error;
	}

	bool ok = ValidDataType();
	if (!ok)
	{
		g_error = k_create_buffers_error_2;
		return g_error;
	}

	status = kPrepared;
	//DebugLog("create_buffers 2: status = ", status);
	setstop(true);

	if (g_use_convolution)
	{
		//SoundTool::SoundTools::clear_buf();
		SoundTool::SoundTools::rtconv_restart();
	}
	return 0;
}

int asio_io::dispose_buffers()
{
	//DebugLog("dispose_buffers 1: status = ", status);
	if (g_use_convolution)
	{
		SoundTool::SoundTools::rtconv_stop();
	}

	if (status == kInitilized)
	{
		return 0;
	}
	else if (status != kPrepared)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}

	ASIODisposeBuffers();

	status = kInitilized;
	//console_msg(S"Status: Initialized\r\n");
	//DebugLog("dispose_buffers 2: status = ", status);
	return 0;
}

int asio_io::start()
{
	//DebugLog("start 1: status = ", status);
	init_timer();
	if (status == kStarted)
	{
		return 0;
	}
	else if (status != kPrepared)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}
	setstop(false);
	SoundTool::SoundTools::init_gpara();
	//SoundTool::SoundTools::init_gpara(g_da_delay);
	//g_power = 0.0;

	if (ASIOStart() != ASE_OK)
	{
		g_error = k_start_error_1;
	}
	else
	{
		status = kStarted;
		//console_msg(S"Status: Started\r\n");
	}
	//DebugLog("start 2: status = ", status);
	return g_error;
}

int asio_io::stop()
{
	//DebugLog("stop 1: status = ", status);
	if (status == kPrepared)
	{
		return 0;
	}
	else if (status != kStarted)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}

	while (!getstop())
	{
		Sleep(50);	// goto sleep for 100 milliseconds
	}
	ASIOStop();
	status = kPrepared;
	//console_msg(S"Status: Prepared\r\n");
	//DebugLog("stop 2: status = ", status);
	return 0;
}
//
//int asio_io::forceStop()
//{
//	DebugLog("stop 1: status = ", status);
//	if (status == kPrepared)
//	{
//		return 0;
//	}
//	else if (status != kStarted)
//	{
//		g_error = k_invalid_protocol;
//		return g_error;
//	}
//
//	if(!getstop())
//	{
//		Sleep(50);	// goto sleep for 50 milliseconds
//	}
//	ASIOStop();
//	setstop(true);
//	dispose_buffers();
//	status = kPrepared;
//	return 0;
//}

bool asio_io::ValidDataType()
{
	int type = asioDriverInfo.channelInfos[0].type;

	if (type != ASIOSTInt32LSB) return false;
	for (int i=1; i < asioDriverInfo.inputBuffers + asioDriverInfo.outputBuffers; i++)
	{
		if (type != asioDriverInfo.channelInfos[i].type) return false;
	}
	return true;
}

//----------------------------------------------------------------------------------
ASIOError asio_io::create_asio_buffers (DriverInfo *asioDriverInfo)
{	// create buffers for all inputs and outputs of the card with the 
	// preferredSize from ASIOGetBufferSize() as buffer size
	long i;
	ASIOError result;

	// fill the bufferInfos from the start without a gap
	ASIOBufferInfo *info = asioDriverInfo->bufferInfos;

	// prepare inputs (Though this is not necessaily required, no opened inputs will work, too
	if (asioDriverInfo->inputChannels > kMaxInputChannels)
		asioDriverInfo->inputBuffers = kMaxInputChannels;
	else
		asioDriverInfo->inputBuffers = asioDriverInfo->inputChannels;
	for(i = 0; i < asioDriverInfo->inputBuffers; i++, info++)
	{
		info->isInput = ASIOTrue;
		info->channelNum = i;
		info->buffers[0] = info->buffers[1] = 0;
	}

	// prepare outputs
	if (asioDriverInfo->outputChannels > kMaxOutputChannels)
		asioDriverInfo->outputBuffers = kMaxOutputChannels;
	else
		asioDriverInfo->outputBuffers = asioDriverInfo->outputChannels;
	for(i = 0; i < asioDriverInfo->outputBuffers; i++, info++)
	{
		info->isInput = ASIOFalse;
		info->channelNum = i;
		info->buffers[0] = info->buffers[1] = 0;
	}

	// create and activate buffers
	result = ASIOCreateBuffers(asioDriverInfo->bufferInfos,
		asioDriverInfo->inputBuffers + asioDriverInfo->outputBuffers,
		g_preferredSize, &asioCallbacks);
	if (result == ASE_OK)
	{
		// now get all the buffer details, sample word length, name, word clock group and activation
		for (i = 0; i < asioDriverInfo->inputBuffers + asioDriverInfo->outputBuffers; i++)
		{
			asioDriverInfo->channelInfos[i].channel = asioDriverInfo->bufferInfos[i].channelNum;
			asioDriverInfo->channelInfos[i].isInput = asioDriverInfo->bufferInfos[i].isInput;
			result = ASIOGetChannelInfo(&asioDriverInfo->channelInfos[i]);
			if (result != ASE_OK)
				break;
		}

		if (result == ASE_OK)
		{
			// get the input and output latencies
			// Latencies often are only valid after ASIOCreateBuffers()
			// (input latency is the age of the first sample in the currently returned audio block)
			// (output latency is the time the first sample in the currently returned audio block requires to get to the output)
			result = ASIOGetLatencies(&asioDriverInfo->inputLatency, &asioDriverInfo->outputLatency);
			//if (result == ASE_OK)
			//{
			//	String *tt;
			//	tt=String::Concat(   "ASIOGetLatencies (input: ",asioDriverInfo->inputLatency.ToString());
			//	tt=String::Concat(tt,", output: ",asioDriverInfo->outputLatency.ToString(),");\r\n");
			//	console_msg(tt);
			//}
		}
	}
	return result;
}

//----------------------------------------------------------------------------------
int asio_io::infoget(wchar_t *driver_name)
{
	//DebugLog("infoget 1: status = ", status);
	int ret = 0;
	if (status == kInitilized)
	{
		if ((ret = exit()) != 0) return ret;
	}
	else if (status != kBeginning && status != kDriverLoaded)
	{
		g_error = k_invalid_protocol;
		return g_error;
	}

	if (driver_cnt == 0)
	{
		g_error = k_no_asio_driver;
		return g_error;
	}
	int size1 = 256;
	DWORD read_count = 0;
	char *fname = new char[size1];
	size_t pReturnValue;
	setlocale(LC_ALL, "japanese");
	wcstombs_s(&pReturnValue, fname, size1, driver_name, _TRUNCATE);

	if (loadAsioDriver(fname))
	{
		status = kDriverLoaded;
	}
	else
	{
		g_error = k_load_driver_error;
		return g_error;
	}

	if((ret=initialize())!=0) return ret;
	g_preferredSize = asioDriverInfo.preferredSize;
	//g_da_delay = g_preferredSize * 2;

	if((ret=create_buffers())!=0) return ret;
	if((ret=dispose_buffers())!=0) return ret;

	SoundTool::SoundTools::SoundTools_init(g_preferredSize, g_sfreq);
	g_error = k_no_error;
	//DebugLog("infoget 2: status = ", status);
	//if (g_assign_in != 0) free(g_assign_in);
	//if (g_assign_out != 0) free(g_assign_out);
	//if (g_assign_work != 0) free(g_assign_work);

	//g_assign_in = (int *)calloc(in_ch_cnt, sizeof(int));   // check memory leak: OK
	//g_assign_out = (int *)calloc(out_ch_cnt, sizeof(int));   // check memory leak: OK
	//g_assign_work = (int *)calloc(WORK_BUF_CNT, sizeof(int));   // check memory leak: OK

	return g_error;
}

//----------------------------------------------------------------------------------
int asio_io::change_bufsize(int new_size)
{
	int backup_size = asioDriverInfo.preferredSize;
	g_preferredSize = asioDriverInfo.preferredSize = new_size;
	int ret;
	if((ret=create_buffers())!=0)
	{
		g_preferredSize = asioDriverInfo.preferredSize = backup_size;
		if((ret=create_buffers())!=0) return 0;
	}
	if((ret=dispose_buffers())!=0) return 0;
	return g_preferredSize;
}

//----------------------------------------------------------------------------------

//----------------------------------------------------------------------------------
void asio_io::getlist()
{
	//if (!loadAsioDriver (ASIO_DRIVER_NAME))
	if(!asioDrivers)
		asioDrivers = new AsioDrivers();

	if(DriversList)
	{
		for(int i=0;i<DriverListMax;i++) free(DriversList[i]);
		free(DriversList);
	}
	DriversList = (char **)malloc(DriverListMax*sizeof(char *));
	if(DriversList)
	{
		for (int i = 0; i < DriverListMax; i++)
		{
			DriversList[i] = (char*)malloc(32);
		}
	}

	if(asioDrivers)
		driver_cnt = asioDrivers->getDriverNames(DriversList,DriverListMax);
	else
		driver_cnt = 0;
}

//---------------------------------------------------------------------------------
//void asio_io::reset_error_cnt()
//{
//	SoundTool::SoundTools::reset_error_cnt();
//}

//---------------------------------------------------------------------------------
//void asio_io::rtconv_imp_updating(bool is_updating)
//{
//	SoundTool::SoundTools::rtconv_imp_updating(is_updating);
//}

//---------------------------------------------------------------------------------
void asio_io::rtconv_set(int fft_level, float *imp, int in_ch, int out_ch, int word_cnt_max, bool is_last)
{
	g_use_convolution = true;
	SoundTool::SoundTools::rtconv_set(fft_level - 1, imp, in_ch, out_ch, word_cnt_max);
	if (is_last) SoundTool::SoundTools::rtconv_fft_imp();
}

//---------------------------------------------------------------------------------
void asio_io::rtconv_fpga_set(int fpga_ch, float *imp, int out_ch, int fir_tap)
{
	if (out_ch == 0) return;
	asio_binarymode_init(out_ch * (fir_tap + 3));

	for (int ch = 0; ch < out_ch; ch++)
	{
		int point = ch * (fir_tap + 3);
		g_asio_binary_buf[point + 0] = 0xa5a5a5a5;
		g_asio_binary_buf[point + 1] = 0x5a5a5a5a;
		g_asio_binary_buf[point + 2] = ch << 16;
		for (int i = 0; i < fir_tap; i++)
		{
			g_asio_binary_buf[point + 3 + i] = (int)((double)imp[ch * fir_tap+ i] * amp32);
		}
	}
	// Create int buffer for g_preferedsize(minimum)
	g_temporary_outch = fpga_ch;

	create_buffers();
	start();
	stop();
	dispose_buffers();
	asio_binarymode_exit();
}

//---------------------------------------------------------------------------------
bool asio_io::has_damp_volume()
{
	return asioDriverInfo.outputBuffers >= AmpGainChannel;
}

//---------------------------------------------------------------------------------
void asio_io::damp_volume_set(int amp_val)
{
	asio_binarymode_init(g_preferredSize);
	for (g_temporary_outch = AmpGainChannel; g_temporary_outch <= asioDriverInfo.outputBuffers; g_temporary_outch += 64)
	{
		g_asio_binary_buf[0] = amp_val << 16;
		g_processedSamples_binary = 0;
		create_buffers();
		start();
		stop();
		dispose_buffers();

		g_asio_binary_buf[0] = 0;
		g_processedSamples_binary = 0;
		create_buffers();
		start();
		stop();
		dispose_buffers();
	}
	asio_binarymode_exit();
}

//---------------------------------------------------------------------------------
int asio_io::rtconv_total_error()
{
	return SoundTool::SoundTools::rtconv_total_error();
}

//---------------------------------------------------------------------------------
int asio_io::rtconv_error_cnt(int no)
{
	return SoundTool::SoundTools::rtconv_error_cnt(no);
}

void asio_io::set_repeat_mode(bool repeat)
{
	SoundTool::SoundTools::setRepeatMode(repeat);
}

//---------------------------------------------------------------------------------
double asio_io::asio_zitter()
{
	return asio_time_stdev();
}

//
//void asio_io::rtconv_start(){
//	SoundTool::SoundTools::clear_buf();
//	SoundTool::SoundTools::rtconv_restart();
//}
//
//void asio_io::rtconv_stop(){
//	SoundTool::SoundTools::rtconv_stop();
//	SoundTool::SoundTools::rtconv_clearbuf_all();
//}

int gain_code(int board_no, int ch_no, int ctrl_val, int gpo_val, int gain_val)
{
	return 0xA8000000 + ((board_no & 0x7) << 24) + ((ch_no & 0x7) << 21) + ((ctrl_val & 0x7) << 18) + ((gpo_val & 0xF) << 14) + ((gain_val & 0x3F) << 8);
}

//---------------------------------------------------------------------------------
bool asio_io::micamp_gain_set(int madi_no, int board_no, int ch_no, int ctrl_val, int gpo_val, int gain_val)
{
	asio_binarymode_init(1);  // micamp_gain_set_buf_size = 1;
	g_temporary_outch = 64 * (madi_no + 1);
	g_asio_binary_buf[0] = gain_code(board_no, ch_no, ctrl_val, gpo_val, gain_val);

	// Start asio temporary out
	create_buffers();
	start();
	// Stop asio
	stop();
	dispose_buffers();
	asio_binarymode_exit();

	return true;
}
//---------------------------------------------------------------------------------
void asio_io::spamp_gain_push(int amp_val)
{
	if (getstop())
	{
		g_amp_gain_cur = 0;
		g_amp_gain_next = 1;
		g_amp_gain_val[0] = amp_val;
	}
	else
	{
		g_amp_gain_val[g_amp_gain_next++] = amp_val;
		if (g_amp_gain_next == g_amp_gain_max) g_amp_gain_next = 0;
	}
}
};
