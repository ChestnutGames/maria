#include "sharpc.h"

#include <stdlib.h>
#include <stdarg.h>
#include <string.h>
#include <assert.h>

struct test {
	int dummy;
};

static struct test *
test_alloc() {
	struct test *inst = (struct test *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(inst));
	return inst;
}

static void 
test_free(struct test *self) {

}

static int 
test_print(int argc, struct CSObject *argv) {
	assert(argc > 0);
	struct test *self = (struct test *)argv[0].ptr;

}

int sharpc_regiseter_class() {

}

int
sharpc_register_function(struct sharpc *self, const char *name, c_callback cb) {

}

int open_test() {

}