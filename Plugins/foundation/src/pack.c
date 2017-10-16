#include "pack.h"

package_t *
package_alloc(char *src, int size) {
	package_t *inst = malloc(sizeof(package_t) + size);
	memcpy(inst->src, src, size);
	return inst;
}

int
package_size(package_t *self) {
	return self->size;
}

void
package_memcpy(package_t *self, char *dst, int len) {
	assert(self->size <= len);
	memcpy(dst, self->src, self->size);
}

void
package_free(package_t *self) {
	free(self);
}

union if32 {
	int32_t i;
	float   f;
};

union if64 {
	int64_t i;
	double  f;
};

int WriteUInt8(char *ptr, int ofs, uint8_t val) {
	ptr[ofs] = val;
	return (ofs + 1);
}

int WriteInt16(char *ptr, int ofs, int16_t val) {
	for (size_t i = 0; i < 2; i++) {
		ptr[ofs + i] = (val >> (8 * i)) & 0xff;
	}
	return (ofs + 2);
}

int WriteInt32(char *ptr, int ofs, int32_t val) {
	for (size_t i = 0; i < 4; i++) {
		ptr[ofs + i] = (val >> (8 * i)) & 0xff;
	}
	return (ofs + 4);
}

int WriteInt64(char *ptr, int ofs, int64_t val) {
	for (size_t i = 0; i < 8; i++) {
		ptr[ofs + i] = (val >> (8 * i)) & 0xff;
	}
	return (ofs + 8);
}

int WriteFnt32(char *ptr, int ofs, float val) {
	union if32 x;
	x.f = val;
	for (size_t i = 0; i < 4; i++) {
		ptr[ofs + i] = (x.i >> (8 * i)) & 0xff;
	}
	return (ofs + 4);
}

int WriteFnt64(char *ptr, int ofs, double val) {
	union if64 x;
	x.f = val;
	for (size_t i = 0; i < 8; i++) {
		ptr[ofs + i] = (x.i >> (8 * i)) & 0xff;
	}
	return (ofs + 8);
}