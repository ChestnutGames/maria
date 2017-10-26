#ifndef __PACK_H_
#define __PACK_H_

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include <config.h>
#include <stdbool.h>
#include <stdint.h>
#include <math.h>
#include <assert.h>
#include <stdlib.h>
#include <stdio.h>
#include <memory.h>

typedef struct PACKAGE {
	int     size;
	char    src[0];
} package_t;

PLAY_API package_t *
package_alloc(char *src, int size);

PLAY_API package_t *
package_alloci(int size);

PLAY_API int
package_size(package_t *self);

PLAY_API char *
package_buffer(package_t *self);

PLAY_API void
package_memcpy(package_t *self, char *dst, int len);

PLAY_API void
package_free(package_t *self);

PLAY_API int
WriteUInt8(char *ptr, int ofs, uint8_t val, int n);

PLAY_API int
WriteInt16(char *ptr, int ofs, int16_t val, int n);

PLAY_API int
WriteInt32(char *ptr, int ofs, int32_t val, int n);

PLAY_API int
WriteInt64(char *ptr, int ofs, int64_t val, int n);

PLAY_API int
WriteFnt32(char *ptr, int ofs, float val, int n);

PLAY_API int
WriteFnt64(char *ptr, int ofs, double val, int n);

PLAY_API int
WriteString(char *ptr, int ofs, const char *src, int len, int n);

PLAY_API int
ReadUInt8(char *ptr, int ofs, uint8_t *val, int n);

PLAY_API int
ReadInt16(char *ptr, int ofs, int16_t *val, int n);

PLAY_API int
ReadInt32(char *ptr, int ofs, int32_t *val, int n);

PLAY_API int
ReadInt64(char *ptr, int ofs, int64_t *val, int n);

PLAY_API int
ReadFnt32(char *ptr, int ofs, float *val, int n);

PLAY_API int
ReadFnt64(char *ptr, int ofs, double *val, int n);

PLAY_API int
ReadString(char *ptr, int ofs, char *val, size_t *size, int n);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PACK_H_
