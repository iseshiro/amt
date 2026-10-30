#pragma once

#include <malloc.h>
#include <windows.h>
#include <string>

const double amp32((double)0x7fffffff);
const double amp24((double)0x7fffff);
const double amp16((double)0x7fff);
const double amp32_1(1.0 / (double)0x7fffffff);
const double amp24_1(1.0 / (double)0x7fffff);
const double amp16_1(1.0 / (double)0x7fff);
//#define LogDebug

typedef int BTN_TYPE;
enum {
	btn_type_in = 1,
	btn_type_out = 2,
	btn_type_work = 3
};

typedef int IO_TYPE;
enum {
	io_type_short = 1,
	io_type_int = 2
};

typedef int DATA_TYPE;
enum {
	data_type_unknown = 0,
	data_type_float = 1,
	data_type_int32 = 2
};

//extern void DebugLog(char *message, int val1, int val2);
//extern void DebugLog_f(char *message, int i1, int i2, float val1, float val2);
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

#define AmpGainChannel 64
