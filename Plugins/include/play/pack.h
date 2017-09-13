#ifndef __PACK_H_
#define __PACK_H_

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include <stdint.h>

void WriteByte(char *ptr, int ofs, uint8_t val);
void WriteInt16(char *ptr, int ofs, int16_t val);
void WriteInt32(char *ptr, int ofs, int32_t val);
void WriteInt64(char *ptr, int ofs, int64_t val);

void WriteFnt32(char *ptr, int ofs, float val);
void WriteFnt64(char *ptr, int ofs, double val);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PACK_H_
