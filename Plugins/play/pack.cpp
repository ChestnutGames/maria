#include "stdafx.h"
#include "pack.h"


void WriteByte(char *ptr, int ofs, uint8_t val) {
	ptr[ofs] = val;
}

void WriteInt16(char *ptr, int ofs, int16_t val) {
	for (size_t i = 0; i < 2; i++) {
		ptr[0] = (val >> 8 * (1 - i)) & 0xff;
	}
}

void WriteInt32(char *ptr, int ofs, int32_t val) {
	for (size_t i = 0; i < 4; i++) {
		ptr[0] = (val >> 8 * (1 - i)) & 0xff;
	}
}

void WriteInt64(char *ptr, int ofs, int64_t val) {
	for (size_t i = 0; i < 8; i++) {
		ptr[0] = (val >> 8 * (1 - i)) & 0xff;
	}
}