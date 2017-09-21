#ifndef SHARPC_H
#define SHARPC_H

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus
#include "config.h"
#include <stdint.h>

typedef enum CSType {
	C_NIL = 0,
	C_INT32 = 1,
	C_INT64 = 2,
	C_REAL = 3,
	C_BOOLEAN = 4,
	C_STRING = 5,
	C_INTPTR = 6,
	CS_OBJECT = 7,
	CS_FUNCTION = 8,
	CS_STRING = 9,
} CSType;

typedef struct CSObject {
	//public WeakReference obj { get; set; }

	CSType  type;
	void *  ptr;
	int32_t v32; // len or key or d
	int64_t v64;
	double  f;
} CSObject;




typedef int(*sharp_callback)(int argc, struct CSObject *argv, int res);

struct sharpc {
	int reference;
	sharp_callback sharpcall;
};

/*
** @brief C#调用次函数生成一个对象
**
*/
SHARPC_API struct sharpc*
sharpc_create(sharp_callback cb);

SHARPC_API void
sharpc_retain(struct sharpc *self);

SHARPC_API void
sharpc_release(struct sharpc *self);

/*
** @breif 回调sharp代码
** @param argc CSObject 几个
** @param 
** @return 0 正确， 1 以后错误，代码
 */
int
sharpc_callsharp(struct sharpc *self, int argc, struct CSObject *argv, int res);

/*
** @breif 调用c代码
** @param argc CSObject 几个
** @param
** @return 0 正确， 1 以后错误，代码
*/
SHARPC_API int
sharpc_callc(struct sharpc *self, int argc, struct CSObject *argv, int res);

void
sharpc_log(struct sharpc *self, struct CSObject xx[2]);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !SHARPC_H
