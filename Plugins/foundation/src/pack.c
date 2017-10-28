#include "pack.h"

package_t *
package_alloc(char *src, int size) {
	package_t *inst = malloc(sizeof(package_t) + size);
	memcpy(inst->src, src, size);
	inst->size = size;
	return inst;
}

package_t *
package_alloci(int size) {
	package_t *inst = malloc(sizeof(package_t) + size);
	memset(inst, 0, sizeof(package_t) + size);
	inst->size = size;
	return inst;
}

int
package_size(package_t *self) {
	return self->size;
}

char *
package_buffer(package_t *self) {
	return self->src;
}

void
package_memcpy(package_t *self, char *dst, int len) {
	assert(self->size <= len);
	memcpy(dst, self->src, self->size);
}

void
package_free(package_t *self) {
	if (self != NULL) {
		free(self);
	}
}

union if32 {
	int32_t i;
	float   f;
};

union if64 {
	int64_t i;
	double  f;
};

int WriteUInt8(char *ptr, int ofs, uint8_t val, int n) {
#if defined(_DEBUG)
	assert((ofs + 1) < n);
#endif
	if ((ofs + 1) < n) {
		ptr[ofs] = val;
		return (ofs + 1);
	} else {
		return ofs;
	}
}

int WriteInt16(char *ptr, int ofs, int16_t val, int n) {
#if defined(_DEBUG)
	assert((ofs + 2) < n);
#endif
	if ((ofs + 2) < n) {
		for (size_t i = 0; i < 2; i++) {
			ptr[ofs + i] = (val >> (8 * i)) & 0xff;
		}
		return (ofs + 2);
	} else {
		return ofs;
	}
}

int WriteInt32(char *ptr, int ofs, int32_t val, int n) {
#if defined(_DEBUG)
	assert((ofs + 4) < n);
#endif
	if ((ofs + 4) < n) {
		for (size_t i = 0; i < 4; i++) {
			ptr[ofs + i] = (val >> (8 * i)) & 0xff;
		}
		return (ofs + 4);
	} else {
		return ofs;
	}
}

int WriteInt64(char *ptr, int ofs, int64_t val, int n) {
#if defined(_DEBUG)
	assert((ofs + 8) < n);
#endif
	if ((ofs + 8) < n) {
		for (size_t i = 0; i < 8; i++) {
			ptr[ofs + i] = (val >> (8 * i)) & 0xff;
		}
		return (ofs + 8);
	} else {
		return ofs;
	}
}

int WriteFnt32(char *ptr, int ofs, float val, int n) {
#if defined(_DEBUG)
	assert((ofs + 4) < n);
#endif
	if ((ofs + 4) < n) {
		union if32 x;
		x.f = val;
		for (size_t i = 0; i < 4; i++) {
			ptr[ofs + i] = (x.i >> (8 * i)) & 0xff;
		}
		return (ofs + 4);
	} else {
		return ofs;
	}
}

int WriteFnt64(char *ptr, int ofs, double val, int n) {
#if defined(_DEBUG)
	assert((ofs + 8) < n);
#endif
	if ((ofs + 8) < n) {
		union if64 x;
		x.f = val;
		for (size_t i = 0; i < 8; i++) {
			ptr[ofs + i] = (x.i >> (8 * i)) & 0xff;
		}
		return (ofs + 8);
	} else {
		return ofs;
	}
}

int WriteString(char *ptr, int ofs, const char *src, int len, int n) {
#if defined(_DEBUG)
	assert((ofs + 4 + len) < n);
#endif
	if ((ofs + 4 + len) < n) {
		ofs = WriteInt32(ptr, ofs, len, n);
		memcpy(ptr + ofs, src, len);
		return (ofs + len);
	} else {
		return ofs;
	}
}

int
ReadUInt8(char *ptr, int ofs, uint8_t *val, int n) {
#if defined(_DEBUG)
	assert((ofs + 1) < n);
#endif
	size_t len = 1;
	int16_t res = 0;
	for (size_t i = 0; i < len; i++) {
		res |= ptr[ofs + i] << (8 * i);
	}
	*val = res;
	return (ofs + len);
}

int
ReadInt16(char *ptr, int ofs, int16_t *val, int n) {
#if defined(_DEBUG)
	assert((ofs + 2) < n);
#endif
	if ((ofs + 2) < n) {
		size_t len = 2;
		int16_t res = 0;
		for (size_t i = 0; i < len; i++) {
			res |= ptr[ofs + i] << (8 * i);
		}
		*val = res;
		return (ofs + len);
	} else {
		return ofs;
	}
}

int
ReadInt32(char *ptr, int ofs, int32_t *val, int n) {
#if defined(_DEBUG)
	assert((ofs + 4) < n);
#endif
	if ((ofs + 4) < n) {
		size_t len = 4;
		int32_t res = 0;
		for (size_t i = 0; i < len; i++) {
			res |= ptr[ofs + i] << (8 * i);
		}
		*val = res;
		return (ofs + len);
	} else {
		return ofs;
	}
}

int
ReadInt64(char *ptr, int ofs, int64_t *val, int n) {
#if defined(_DEBUG)
	assert((ofs + 8) < n);
#endif
	if ((ofs + 8) < n) {
		size_t len = 8;
		int64_t res = 0;
		for (size_t i = 0; i < len; i++) {
			res |= ptr[ofs + i] << (8 * i);
		}
		*val = res;
		return (ofs + len);
	} else {
		return ofs;
	}
}

int
ReadFnt32(char *ptr, int ofs, float *val, int n) {
#if defined(_DEBUG)
	assert((ofs + 8) < n);
#endif
	size_t len = 4;
	int32_t res = 0;
	for (size_t i = 0; i < len; i++) {
		res |= ptr[ofs + i] << (8 * i);
	}

	union if32 u;
	u.i = res;
	*val = u.f;
	return (ofs + len);
}

int
ReadFnt64(char *ptr, int ofs, double *val, int n) {
	size_t len = 8;
	int64_t res = 0;
	for (size_t i = 0; i < len; i++) {
		res |= ptr[ofs + i] << (8 * i);
	}
	union if64 u;
	u.i = res;
	*val = u.f;
	return (ofs + len);
}

int
ReadString(char *ptr, int ofs, char *val, size_t *size, int n) {
#if defined(_DEBUG)
	assert((ofs + 4) < n);
#endif
	if ((ofs + 4) < n) {
		int len = 0;
		ofs = ReadInt32(ptr, ofs, &len, n);
		int m = min(len, *size);
		memcpy(val, ptr + ofs, m);
		*size = m;
		return (ofs + len);

	} else {
		return ofs;
	}
}