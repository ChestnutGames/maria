#ifndef SHARPC_H
#define SHARPC_H

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus
#include "conf.h"
#include <stdint.h>

typedef enum CSType {
	NIL = 0,
	INT32 = 1,
	INT64 = 2,
	REAL = 3,
	BOOLEAN = 4,
	STRING = 5,
	INTPTR = 6,
	SHARPOBJECT = 7,
	SHARPFUNCTION = 8,
	SHARPSTRING = 9,
} CSType;

typedef struct CSObject {
	//public WeakReference obj { get; set; }

	CSType  type;
	void *  ptr;
	int32_t v32; // len or key or d
	int64_t v64;
	double  f;
} CSObject;


typedef int(*sharp_callback)(int argc, struct CSObject *argv, int args, int res);

struct sharpc {
	sharp_callback sharpcall;
	struct CSObject log;
};

SHARPC_API struct sharpc*
	sharpc_alloc(sharp_callback cb);

SHARPC_API void
sharpc_free(struct sharpc *self);

/*
	* @return 0 正确， 1 以后错误，代码
*/
SHARPC_API int
sharpc_call(struct sharpc *self, int argc, struct CSObject *argv, int args, int res);

SHARPC_API void
sharpc_log(struct sharpc *self, struct CSObject xx[2]);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !SHARPC_H
