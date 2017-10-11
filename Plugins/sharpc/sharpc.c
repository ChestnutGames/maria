#include "cstdafx.h"
#include <assert.h>


struct sharpc*
	sharpc_create(sharp_callback cb) {
	assert(cb != NULL);
	struct sharpc *inst = (struct sharpc *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(inst));
	inst->sharpcall = cb;
	inst->reference = 1;
	return inst;
}

void
sharpc_retain(struct sharpc *self) {
	assert(self != NULL);
	self->reference++;
}

void
sharpc_release(struct sharpc *self) {
	assert(self != NULL && self->reference > 0);
	self->reference--;
	if (self->reference <= 0) {
		free(self);
	}
}

int
sharpc_callsharp(struct sharpc *self, int argc, struct CSObject *argv) {
	return self->sharpcall(argc, argv);
}


