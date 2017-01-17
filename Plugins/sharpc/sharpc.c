#include "sharpc.h"

#include <stdlib.h>
#include <stdarg.h>
#include <string.h>
#include <assert.h>

static struct sharpc *inst = NULL;

struct sharpc*
sharpc_alloc(sharp_callback cb) {
	if (inst == NULL) {
		assert(cb != NULL);
		inst = (struct sharpc *)malloc(sizeof(*inst));
		memset(inst, 0, sizeof(inst));
		inst->sharpcall = cb;

		return inst;
	}
	return inst;
}

void
sharpc_free(struct sharpc *self) {
	if (inst == NULL) {
		free(inst);
		inst = NULL;
	}
}

int
sharpc_call(struct sharpc *self, int argc, struct CSObject *argv, int args, int res) {
	return self->sharpcall(argc, argv, args, res);
}
