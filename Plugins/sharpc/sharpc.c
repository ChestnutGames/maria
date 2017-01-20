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

void
sharpc_log(struct sharpc *self, struct CSObject xx[2]) {
	if (xx[0].type == SHARPFUNCTION) {
		self->log = xx[0];
	} else if (xx[0].type == INT32) {
		CSObject args[3];
		args[0] = self->log;

		for (size_t i = 0; i < 2; i++) {
			args[i + 1] = xx[i];
		}

		int res = sharpc_call(self, 3, args, 2, 0);
		if (res == 0) { // Ö´ÐÐÕýÈ·
		}
	}
}