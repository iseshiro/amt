#pragma once
#include "wav_stream.h"

class ChannelInfo
{
private:
	float* sdata;
	//float** sdata_list;
	double amp;
	double maxval;
	int maxid;
	int offset;
	int length;
	int wav_file_ch;
	int asio_ch;
	int read_count;
	double power;
	void **asio_bufptr;
	int stream_length;
	int total_read_count;
	float* peak_pwr_list = 0;
	int peak_pwr_leng = 0;
	int peak_pwr_pnt = 0;
	double pwr_sum_leng_1 = 0.0;
	int level_meter_mode = 0; // 0:off, 1:peak, 2:rms

	int trans_asio_in(int offset, int leng, int index);
	int trans_asio_out(int offset, int leng, int index, bool clear);
	int stream_read(int to_read_cnt, int data_point, bool add);
	int trans_wavread_asioout(int offset, int leng, int index, bool clear);

public:
//	BTN_TYPE btn_type;
	WavStream *wav_stream;
	bool has_data;
//	ChannelInfo *next_channel;
	STREAM_TYPE stream_type;

	ChannelInfo();
	~ChannelInfo();
	//void set_asio_buf_ptr(void **buf_ptr);
	void assign_ch(int asio_chnl, void **buf_ptr);
	float *get_data(int offset);
	float *get_data0();
	void data_clear();
	void data_clear1(int st_point, int ed_point);
	void data_clear2(int st_point, int ed_point);
	int data_create(int leng, STREAM_TYPE type);
	//void group_channel(int ch_cnt, int ch_no);
	void data_dispose();
	void CopyData(ChannelInfo *src);
	void SubData(ChannelInfo *src);
	double verify_data(ChannelInfo *src);
	double get_maxval(bool clear_flag);
	int get_maxid();
	int get_offset();
	void set_maxval();
	bool set_level_meter(int mode, int bufsize);
	double get_peak_rms();
	void set_offset(int st_point, int ed_point);
	void correct_offset();
	void update_maxval(int datapnt, int wcnt);
	//int stream_open_read(wchar_t *src_file, int Bufsize);
	int stream_open_read_mch(wchar_t *src_file, int file_ch, int Bufsize);
	//int stream_open_read_wav1ch(wchar_t *src_file, int Bufsize, double Amp);
	int stream_open_write(wchar_t *file_name, FILE_FORMAT file_format);
	void write_wavheader(int word_cnt, int sfreq, int ch_cnt);
	int effective_ir_length(double rate);
	int effective_ir_length_double(wchar_t *src_file, double rate);
	bool has_stream();
	void stream_close();
	void stream_set_leng(int leng);;
	//void stream_setamp(double amp);
	//int stream_write_short(int to_write_count);
	void stream_attach(int start, int stop);
	void calc_power();
	void trans_asio_clear_int(int leng);
	int trans_data(int offset, int leng, int index, bool clear);
	void calc_peak_pwr(int offset, int leng);
	//void set_stream_type(STREAM_TYPE type);
};
