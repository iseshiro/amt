#include "stdio.h"
#include "windows.h"
#include "process.h"
#include "dsp_common.h"
#include "channel_info.h"

namespace SoundTool
{
	class SoundTools
	{
	public:
		static __declspec(dllexport) void SoundTools_init(int preferredSize, int sfreq);
		static __declspec(dllexport) void SoundTools_debug_init();
		static __declspec(dllexport) void SoundTools_alloc(int In_ch_cnt, int Out_ch_cnt, int Work_ch_cnt);
		static __declspec(dllexport) void push_asio_mode(ASIOmode asio_mode);
		static __declspec(dllexport) void pop_asio_mode();
		static __declspec(dllexport) bool is_binaryout();
		static __declspec(dllexport) void set_sfreq(int sfreq);
		static __declspec(dllexport) void SoundTools_exit();
		static __declspec(dllexport) void create_chinfo(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void assign_ch(BTN_TYPE type, int btn_no, int asio_ch, void **buf_ptr);
		static __declspec(dllexport) void init_gpara();
		//static __declspec(dllexport) void init_gpara(int delay);
		static __declspec(dllexport) int getADpoint();
		static __declspec(dllexport) bool set_level_meter(bool is_input, int ch, int mode);
		static __declspec(dllexport) double get_peak_rms();
		static __declspec(dllexport) int get_adbuf_point();
		static __declspec(dllexport) void setADpoint(int cnt);
		static __declspec(dllexport) void setRepeatMode(bool repeat_mode);
		static __declspec(dllexport) int stream_word_count();
		static __declspec(dllexport) bool is_stream_audio_out();
		static __declspec(dllexport) char *getErrorMessage();
		static __declspec(dllexport) int getErrorStatus();

		static __declspec(dllexport) double getScale(double maxval);
		static __declspec(dllexport) void makePureTone(double amp, double freq, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void makeWhiteNoise(double amp, unsigned long seed, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void makeTSP(int fftpnt, BTN_TYPE type, int btn_no, double amp, int coef_m_numer, int coef_m_denom, double coef_shift, int flag);
		static __declspec(dllexport) void makePinkNoise_Pole(double amp, unsigned long seed, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void makePinkNoise_Voss(double amp, unsigned long seed, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void makeFilter(int fftpnt, BTN_TYPE type, int btn_no, int freq_from, int freq_to, int level_from, int level_to);
		static __declspec(dllexport) void makeSaw(BTN_TYPE type, int btn_no, double max_val, double min_val, int period_point, bool max2min);

		static __declspec(dllexport) double IIR_LPF(int filter_order, double cut_freq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) double IIR_HPF(int filter_order, double cut_freq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) double IIR_BPF(int filter_order, double lcutfreq, double hcutfreq, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) double IIR_OCTBPF(int filter_order, double centerfreq, double bandwidth, double ripple, double scale, BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);

		static __declspec(dllexport) bool fconv_init(int fft_level, int ch_in, int ch_out, int pref_size, int fifo_size, int fft_point);
		static __declspec(dllexport) bool fconv_set_src(int ch_in, wchar_t* src_file, int pref_size);
		static __declspec(dllexport) int fconv_file_length(int ch_in);
		static __declspec(dllexport) void fconv_close_src(int ch_in);
		static __declspec(dllexport) int get_fconv_format(int ch_in);
		static __declspec(dllexport) bool fconv_create_mid(wchar_t *mid_file);
		static __declspec(dllexport) float fconv_close_mid();
		static __declspec(dllexport) void fconv_convolution(int ch_in, int ch_out, int pref_size, int write_size, int processedSamples);

		static __declspec(dllexport) void fmove_init(int Mic_cnt, int Shift_size, int Sfreq);
		static __declspec(dllexport) void fmove_src_cnt(int Src_cnt);
		static __declspec(dllexport) void fmove_add_src(int src_no, double play_time, double amp, double start_time, double stop_time, int seq_cnt);
		static __declspec(dllexport) void fmove_set_fileinfo(int src_no, wchar_t *file_path);
		static __declspec(dllexport) bool fmove_add_seq(int src_no, int shift_no, double posi_x, double posi_y, double posi_z, bool stay);
		static __declspec(dllexport) int fmove_set_outfile1(int mic_no, wchar_t *file_path);
		static __declspec(dllexport) bool fmove_move(int shift_no);
		static __declspec(dllexport) bool fmove_sequence_update(int src_no);
		static __declspec(dllexport) double fmove_current_posi(int src_no, int xyz);
		static __declspec(dllexport) int fmove_close1(int mic_no, wchar_t *file_path);
		static __declspec(dllexport) float fmove_maxval();

		static __declspec(dllexport) bool asio_play(bool is_stop, int index);
		static __declspec(dllexport) bool asio_play_d(bool is_stop, int index);
		static __declspec(dllexport) bool asio_play_r(bool is_stop, int index);
		static __declspec(dllexport) bool asio_play_s(bool is_stop, int index);
		static __declspec(dllexport) bool asio_play_m(bool is_stop, int index);
		static __declspec(dllexport) bool asio_play_u(bool is_stop, int index);
		static __declspec(dllexport) bool asio_play_t(bool is_stop, int index);
		static __declspec(dllexport) void attach_song(int id);
		static __declspec(dllexport) void next_song();

		//--- channel_info
		static __declspec(dllexport)ChannelInfo * chnl_info(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) double GetMaxVal(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) double GetMaxValClear(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) int GetOffset(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) int GetMaxID(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void SetMaxVal(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void SetOffset(BTN_TYPE type, int btn_no, int st_point, int ed_point);
		static __declspec(dllexport) void CorrectOffset(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void data_clear(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void data_clear1(BTN_TYPE type, int btn_no, int st_point, int ed_point);
		static __declspec(dllexport) void data_clear2(BTN_TYPE type, int btn_no, int st_point, int ed_point);
		static __declspec(dllexport) float *get_data_ptr0(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) double GetPower_t(BTN_TYPE type, int btn_no, int fftpoint);
		static __declspec(dllexport) void data_dispose(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void CopyData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) void SubData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) double Verify(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) double verify_data(BTN_TYPE type, int btn_no, float* src_dat, int offset, int length);
		static __declspec(dllexport) int data_create(BTN_TYPE type, int btn_no, STREAM_TYPE st_type);
		static __declspec(dllexport) void get_data(BTN_TYPE type, int btn_no, float* dst_dat, int length, int offset);
		static __declspec(dllexport) void load_data(BTN_TYPE type, int btn_no, float* src_dat, int length, int ch_cnt, int load_ch);
		static __declspec(dllexport) void load_data1(BTN_TYPE type, int btn_no, float* src_dat, int offset, int length);
		static __declspec(dllexport) void move_forward(BTN_TYPE type, int btn_no, int position);
		static __declspec(dllexport) void ClearBothSides(BTN_TYPE type, int btn_no, int start, int end);
		static __declspec(dllexport) bool have_buffer(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) bool have_signal(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) bool SetDataLength(int leng, bool without_asio);
		static __declspec(dllexport) int GetDataLength();
		static __declspec(dllexport) void Add(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) void Multiply(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out);
		static __declspec(dllexport) void MultiplyValue(BTN_TYPE type_in1, int btn_no_in1, double val);
		static __declspec(dllexport) void makePulse(double amp, int delay, int width, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void makeDC(double amp, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) double data(int cnt, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void getminmax(int stpnt, int edpnt, BTN_TYPE type, int btn_no, double *rmin, double *rmax);
		static __declspec(dllexport) void getminmax_famp(int stpnt, int edpnt, BTN_TYPE type, int btn_no, double *rmin, double *rmax);
		static __declspec(dllexport) double GetTimeSigPower(BTN_TYPE type, int btn_no, int st_point, int ed_point);
		static __declspec(dllexport) void HanningWindow(int start_pnt, int end_pnt, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void HanningWindow2(int end_pnt, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void Delay(int delay, BTN_TYPE type, int btn_no);

		//--- fft
		static __declspec(dllexport) double famp(int cnt);
		static __declspec(dllexport) double GetPower_f(int fftpoint);
		static __declspec(dllexport) void freq_range_clear(int st_freq, int ed_freq, int fftpoint);
		static __declspec(dllexport) void DivData(BTN_TYPE type_in, int btn_no_in, BTN_TYPE type_out, int btn_no_out, int fftpnt);
		static __declspec(dllexport) double MakeConvolution(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out, int fftpnt, double scale, int freq_st, int freq_ed);
		static __declspec(dllexport) double MakeDeconvolution(BTN_TYPE type_in1, int btn_no_in1, BTN_TYPE type_in2, int btn_no_in2, BTN_TYPE type_out, int btn_no_out, int fftpnt, double scale, int freq_st, int freq_ed);
		static __declspec(dllexport) void makeFFT(int fftpnt, int start_pnt, int end_pnt, BTN_TYPE type, int btn_no);
		static __declspec(dllexport) void fft_safe(float* dre, float* dim, int fftpnt);

		//--- rtmove
		static __declspec(dllexport) bool rtmove_init(int mic_mode, int frame_size, int max_wav_cnt, int input_count);
		static __declspec(dllexport) double rtmove_mic_radius();
		static __declspec(dllexport) void rtmove_stop(int stop_src_no);
		static __declspec(dllexport) int rtmove_add_src(wchar_t* argv, wchar_t* src_file);
		static __declspec(dllexport) int rtmove_add_ue_src(wchar_t* src_file, int sfreq);
		static __declspec(dllexport) int rtmove_add_vr_src(int input_count);
		static __declspec(dllexport) int rtmove_chg_src(int src_no, wchar_t *argv, wchar_t *src_file);
		static __declspec(dllexport) void rtmove_chg_amp(int src_no, double new_amp);
		static __declspec(dllexport) void rtmove_play_mode(int src_no, int mode, double playback_start, double playback_delay, double time_to_que);
		static __declspec(dllexport) void rtmove_move(int src_no, double posi_x, double posi_y, double posi_z);
		static __declspec(dllexport) void rtmove_move_polar(int src_no, double rr, double theta, double phi);
		static __declspec(dllexport) int rtmove_maxmic(int src_no);
		static __declspec(dllexport) int rtmove_save_json(wchar_t *name);
		static __declspec(dllexport) int rtmove_save_data(wchar_t *name);
		static __declspec(dllexport) int rtmove_get_wavfile_no(int src_no);
		static __declspec(dllexport) int rtmove_src_leng(int src_no);

		//--- wav_stream
		static __declspec(dllexport) int stream_open_mch_in(wchar_t *src_file, int file_ch, int btn_no);
		static __declspec(dllexport) int stream_open_mch_out(wchar_t *src_file, int file_ch, int btn_no);
		static __declspec(dllexport) int stream_open_write(wchar_t *file_name, BTN_TYPE type, int btn_no, FILE_FORMAT file_format);
		static __declspec(dllexport) void stream_close(BTN_TYPE type, int btn_no);
		static __declspec(dllexport) int playlist_stream_attach(int id, int start, int stop, bool is_single);
		static __declspec(dllexport) int stream_file_point();
		static __declspec(dllexport) float udp_max_val();
		static __declspec(dllexport) float udp_min_val();
		static __declspec(dllexport) int effective_ir_length(wchar_t* src_file, BTN_TYPE type, int btn_no, bool is_double);
		static __declspec(dllexport) void stream_set_leng(int start_ch, int end_ch, int leng);

		//--- make_inv
		static __declspec(dllexport) void makeinv_init(int sp_count, int mic_count, int fftpoint);
		static __declspec(dllexport) void makeinv_dataset(int data_leng, int sp_no, int mic_no, float *data);
		static __declspec(dllexport) void makeinv_calc();

		//--udp_client
		static __declspec(dllexport) bool create_udp_sender(wchar_t *ip_address, int port_no, void **buf_ptr, bool send_sin1k);
		static __declspec(dllexport) void dispose_udp_sender();
		static __declspec(dllexport) bool create_udp_receiver(int port_no, int delay);
		static __declspec(dllexport) void dispose_udp_receiver();
		static __declspec(dllexport) int udp_receive();

		static __declspec(dllexport) void DebugLog(int data1);
		static __declspec(dllexport) void DebugLog(float data1);
		static __declspec(dllexport) void DebugLog(char *message);
		static __declspec(dllexport) void DebugLog(wchar_t *message);
		static __declspec(dllexport) void DebugLog(char ss);
		static __declspec(dllexport) void DebugLog(char *message, int d1);
		static __declspec(dllexport) void DebugLog(char *message, int d1, long long int d2);
		static __declspec(dllexport) void DebugLog(char *message, int d1, int d2, int d3);
		static __declspec(dllexport) void DebugLog(char *message, float d1, float d2, float d3);
		static __declspec(dllexport) void DebugLog(char* message, int d1, float d2);
		static __declspec(dllexport) void DebugLog(char* message, int d1, int d2, float d3);
		static __declspec(dllexport) void DebugLog(char* message, int d1, double d2, double d3);
		static __declspec(dllexport) void DebugLog_bin_init(int Log_buf_size);
		static __declspec(dllexport) void DebugLog_bin_write(float* data1, float* data2, int leng);
		static __declspec(dllexport) void DebugLog_bin_exit();
		static __declspec(dllexport) void DebugLogFloatMax(char* message, float* buf, int leng);


		//--realtime_conv
		static __declspec(dllexport) int rt_fft_level();
		static __declspec(dllexport) bool rtconv_check();
		static __declspec(dllexport) bool rtconv_init(int fft_level, int in_ch, int out_ch, bool is_single);
		static __declspec(dllexport) void rtconv_set(int no, float *imp, int ich, int och, int word_cnt_max);
		static __declspec(dllexport) void rtconv_fft_imp();
		static __declspec(dllexport) int rtconv_total_error();
		static __declspec(dllexport) int rtconv_error_cnt(int no);
		static __declspec(dllexport) void rtconv_restart();
		static __declspec(dllexport) void rtconv_stop();

		//--playlist
		static __declspec(dllexport) bool playlist_apend(wchar_t *path, int ch_cnt, double start, double stop);
		static __declspec(dllexport) void playlist_remove(int id);

		//--resample
		static __declspec(dllexport) int resample(wchar_t* src_file, wchar_t* dst_file, int src_sfreq, int dst_sfreq, bool use_sinc_func);
		static __declspec(dllexport) int resample(float* src_data, int src_leng, wchar_t* dst_file, int src_sfreq, int dst_sfreq, bool use_sinc_func);
	};
	void init_by_array(unsigned long* init_key, unsigned long key_length);
	unsigned long genrand_int32();
	float maxfloat(float *dat, int leng);
	void tsp(float *xRe, float *xIm, int n, int coef_m_numer, int coef_m_denom, double coef_shift, int flag);
	void Filter(float *xRe, float *xIm, int n, int freq_from, int freq_to, int level_from, int level_to);
}
