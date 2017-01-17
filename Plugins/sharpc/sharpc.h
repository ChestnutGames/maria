#ifndef SHARPC_H
#define SHARPC_H

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
};

SHARPC_API struct sharpc*
sharpc_alloc(sharp_callback cb);

SHARPC_API void
sharpc_free(struct sharpc *self);

SHARPC_API int
sharpc_call(struct sharpc *self, int argc, struct CSObject *argv, int args, int res);

#endif // !SHARPC_H
