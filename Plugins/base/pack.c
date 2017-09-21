#include "pack.h"

union if32 {
	int32_t i;
	float   f;
};

union if64 {
	int64_t i;
	double  f;
};

void WriteUInt8(char *ptr, int ofs, uint8_t val) {
	ptr[ofs] = val;
}

void WriteInt16(char *ptr, int ofs, int16_t val) {
	for (size_t i = 0; i < 2; i++) {
		ptr[ofs + i] = (val >> (8 * i)) & 0xff;
	}
}

void WriteInt32(char *ptr, int ofs, int32_t val) {
	for (size_t i = 0; i < 4; i++) {
		ptr[ofs + i] = (val >> (8 * i)) & 0xff;
	}
}

void WriteInt64(char *ptr, int ofs, int64_t val) {
	for (size_t i = 0; i < 8; i++) {
		ptr[ofs + i] = (val >> (8 * i)) & 0xff;
	}
}

void WriteFnt32(char *ptr, int ofs, float val) {
	union if32 x;
	x.f = val;
	for (size_t i = 0; i < 4; i++) {
		ptr[ofs + i] = (x.i >> (8 * i)) & 0xff;
	}
}

void WriteFnt64(char *ptr, int ofs, double val) {
	union if64 x;
	x.f = val;
	for (size_t i = 0; i < 8; i++) {
		ptr[ofs + i] = (x.i >> (8 * i)) & 0xff;
	}
}