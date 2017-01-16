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

typedef struct reg {
	const char *name;
	c_callback func;
} reg;


typedef int(*open_callback)(reg *args);

struct sharpc {
};

struct sharpc*
sharpc_alloc();

void
sharpc_free(struct sharpc *self);

int
sharpc_call(struct sharpc *self, int argc, struct CSObject *argv);

int sharpc_regiseter_class(struct sharpc *self, open_callback);

int
sharpc_register_function(struct sharpc *self, const char *name, c_callback cb);

#endif // !SHARPC_H
