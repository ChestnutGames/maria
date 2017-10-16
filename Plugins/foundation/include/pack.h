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

package_t *
package_alloc(char *src, int size);

int
package_size(package_t *self);

void
package_memcpy(package_t *self, char *dst, int len);

void
package_free(package_t *self);

int
WriteUInt8(char *ptr, int ofs, uint8_t val);

int
WriteInt16(char *ptr, int ofs, int16_t val);

int
WriteInt32(char *ptr, int ofs, int32_t val);

int
WriteInt64(char *ptr, int ofs, int64_t val);

int
WriteFnt32(char *ptr, int ofs, float val);

int
WriteFnt64(char *ptr, int ofs, double val);


#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PACK_H_
