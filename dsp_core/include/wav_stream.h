#pragma once
#include <windows.h>
#include "dsp_common.h"
// oneMKL opaque handle (same typedef as mkl_dfti.h; avoids a dependency on oneMKL headers)
typedef struct DFTI_DESCRIPTOR *DFTI_DESCRIPTOR_HANDLE;

//typedef struct wavHeader {
//	unsigned char	riff[4];			// WAVの場合は必ず 'RIFF'
//	unsigned int	file_size;			// RIFFヘッダの後のファイルサイズ　つまりファイルサイズ－8
//	unsigned char	wav[4];				// WAVの場合は必ず 'WAVE'
//	unsigned char	fmt[4];				// WAVの場合は必ず 'fmt ' ←スペース重要
//	unsigned int	fmt_size;			// fmtヘッダの後のファイルサイズ
//	unsigned short  format_id;			// WAVファイルの形式
//	unsigned short  channel_num;		// チャンネル数（1:モノラル，2:ステレオ）
//	unsigned int    sampling_rate;		// サンプリング周波数（11025、22050、44100）
//	unsigned int    bytes_per_second;	// １秒あたりの音声データのバイト数
//	unsigned short  bytes_per_sample;	// １サンプルあたりのバイト数
//	unsigned short  bits_per_sample;	// 量子化ビット数（8、16）
//	unsigned char	data[4];			// WAVの場合は必ず 'data'
//	unsigned int	data_size;			// 本ヘッダの後のファイルサイズ
//} WavHeader;

typedef struct wavHeaderRiff {
	unsigned char	riff[4];			// WAVの場合は必ず 'RIFF'
	unsigned int	file_size;			// RIFFヘッダの後のファイルサイズ　つまりファイルサイズ－8
	unsigned char	wave[4];				// WAVの場合は必ず 'WAVE'
} WavHeaderRiff;

typedef struct wavHeaderFmt {
	//unsigned char	fmt[4];				// WAVの場合は必ず 'fmt ' ←スペース重要
	//unsigned int	fmt_size;			// fmtヘッダの後のファイルサイズ
	unsigned short  format_id;			// WAVファイルの形式
	unsigned short  channel_num;		// チャンネル数（1:モノラル，2:ステレオ）
	unsigned int    sampling_rate;		// サンプリング周波数（11025、22050、44100）
	unsigned int    bytes_per_second;	// １秒あたりの音声データのバイト数
	unsigned short  bytes_per_sample;	// １サンプルあたりのバイト数
	unsigned short  bits_per_sample;	// 量子化ビット数（8、16）
} WavHeaderFmt;

typedef struct wavHeaderData {
	unsigned char	data[4];			// WAVの場合は必ず 'data'
	unsigned int	data_size;			// 本ヘッダの後のファイルサイズ
} WavHeaderData;

typedef union data16 {
	unsigned char ddd[2];
	short iii;
} Data16;

typedef union data32 {
	unsigned char ddd[4];
	int iii;
} Data32;

class WavStream
{
private:
	WavHeaderRiff wavheaderRiff;
	WavHeaderFmt wavheaderFmt;
	WavHeaderData wavheaderData;
	void *trns_data;
	DWORD data_head_point;
	int trns_size;
	bool on_memory;
	float * on_mem_buf;
	HANDLE fs_hndl;
	int stream_word_count;
	int file_start_point;
	int file_stop_point;
	int file_current_point;
	int to_write_count;
	int sample_freq;
	bool isOpen;
//	void stream_setfptr(int file_point);
	int read_mch(float* buf, int data_point, int to_read_count, int ch_no, bool add);
	bool resample_fft_fir(float* win, DFTI_DESCRIPTOR_HANDLE fft_hndl, int fs1, int kk1, int win_leng, int fft_point);
	bool resample_sinc_fir(float* win, DFTI_DESCRIPTOR_HANDLE fft_hndl, int fs1, int kk1, int win_leng, int fft_point);

public:
	//ERROR_CODE error_code;
	char* filename;
	FILE_FORMAT file_format;

	WavStream();
	~WavStream();
	int Open(wchar_t *src_file, int Bufsize, bool OnMemory, int OnMemResampleSfreq);  // return stream word count
	void Close();
	//int stream_read_1ch(float *buf_ss, bool add);
	int stream_read_mch(float *buf, int data_point, int to_read_count, int ch_no, bool repeat, bool add);
	int file_start_byte();
	int Open_write(wchar_t *file_name, FILE_FORMAT file_format);
	void write_wavheader(int word_cnt, int sample_freq, int ch);
	bool read_wavheader();
	int effective_ir_length(double rate);
	bool has_stream();
	//int stream_write_short(float *buf_i, int to_write_count);
	int stream_write_float(float *buf_i, int to_write_count);
	void stream_attach(int start, int stop);
	int correct_header();
	int get_file_size();
	void set_start_point(int start_point);
	void set_stop_point(int stop_point);
	void set_current_point(int current_point, bool from_file_start_point);
	int get_file_length();
	int get_sample_freq();
	float get_data_onmem(int pnt);
	int onmem_to_file(wchar_t* dst_file);
	void resample(int OnMemResampleSfreq, bool use_sinc_func);
	int resample(float* src_data, int src_leng, wchar_t* dst_file, int src_sfreq, int dst_sfreq, bool use_sinc_func);
};
