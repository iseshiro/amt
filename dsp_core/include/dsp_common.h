#pragma once

#include <malloc.h>
#include <windows.h>
#include <string.h>
#include <locale.h>

#define amp32 2147483648.0
#define amp24 8388608.0
#define amp16 32768.0
#define amp32_1 4.656612873077393e-10
#define amp24_1 1.192092895507813e-7
#define amp16_1 0.000030517578125
//#define LogDebug
#define WORK_BUF_CNT 6

typedef int BTN_TYPE;
enum {
	btn_type_in = 1,
	btn_type_out = 2,
	btn_type_work = 3
};

typedef int DATA_TYPE;
enum {
	data_type_unknown = 0,
	data_type_float = 1,
	data_type_int32 = 2
};

typedef int STREAM_TYPE;
enum {
	stream_type_no_io = 0,
	stream_type_asioin = 1,
	stream_type_asioout = 2,
	stream_type_wavread = 3,
	stream_type_wavread_add = 4,
	stream_type_wavwrite = 5,
	stream_type_wavread_asioout = 6,
	stream_type_asioin_wavwrite = 7
};

//#ifndef __Asio_IO__
//#define __Asio_IO__
typedef int ST_ERROR_STATUS;
enum {
//	k_has_no_error = 0,
	k_rtmove_buffer_size_short = -1,
	k_network_init_host_name = -2,
	k_network_init_bind_socket = -3,
	k_network_recv_data = -4,
	k_network_windows_connect = -5,
	k_udp_create_socket = -6,
	k_network_udp_send = -7,
	k_stream_read_count = -8,
	k_error_wav_headder = -9,
	k_error_file_open = -10,
	k_error_buf_error = -11,
	k_error_ch_info = -12,
	k_dfti_verify_error = -13,
	k_dfti_config_error = -14
};
//#endif

typedef int FILE_FORMAT;
enum {
	header_not_read = -2,
	format_error = -1,
	wav_int16 = 0,
	wav_int24 = 1,
	wav_float32 = 2,
	bin_double64 = 3
};

//typedef int ERROR_CODE;
//enum {
//	error_wav_headder = -4,
//	error_file_open = -3,
//	error_buf_error = -2,
//	error_ch_info = -1,
//	no_error = 0
//};

#define AmpGainChannel 64

typedef int ASIOmode;
enum {
	asiomode_normal = 0,
	//	asiomode_wav_stream_read = 1,
	asiomode_stream_out = 2,
	//	asiomode_double_stream_read = 3,
	asiomode_rtconv_io = 4,
	asiomode_stream_in_rtconv_out = 5,
	asiomode_rtmove = 6,
	asiomode_binary_out = 7,
	asiomode_udp = 8,
	asiomode_test = 9
};
