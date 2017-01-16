#ifndef SHARPC_H
#define SHARPC_H

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
	//public CSObject() {
	//}

	//public CSType Type { get; set; }
	//public WeakReference obj { get; set; }
	//public object obj { get; set; }
	CSType  type;
	void *  ptr;
	int32_t v32; // len or key or d
	int64_t v64;
	double  f;
} CSObject;

typedef int(*c_callback)(int argc, struct CSObject *argv);


struct sharpc {
	c_callback cb;
};

struct sharpc*
sharpc_alloc(c_callback cb);

void
sharpc_free(struct sharpc *self);

int
sharpc_call(struct sharpc *self, int argc, struct CSObject *argv);

#endif // !SHARPC_H
