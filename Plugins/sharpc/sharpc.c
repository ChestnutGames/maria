#include "sharpc.h"

#include <stdlib.h>
#include <stdarg.h>
#include <string.h>

static struct sharpc *inst = NULL;

struct sharpc*
sharpc_alloc(c_callback cb) {
	if (inst == NULL) {
		inst = (struct sharpc *)malloc(sizeof(*inst));
		memset(inst, 0, sizeof(inst));
		inst->cb = cb;
		return inst;
	}
	return inst;
}

void
sharpc_free(struct sharpc *self) {
	if (inst == NULL) {
		free(inst);
	}
}

int
sharpc_call(struct sharpc *self, int argc, struct CSObject *argv) {
	self->cb(argc, argv);
	return 0;
}
