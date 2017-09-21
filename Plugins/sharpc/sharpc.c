#include "cstdafx.h"

static struct sharpc *inst = NULL;

struct sharpc*
sharpc_create(sharp_callback cb) {
	if (inst == NULL) {
		assert(cb != NULL);
		inst = (struct sharpc *)malloc(sizeof(*inst));
		memset(inst, 0, sizeof(inst));
		inst->sharpcall = cb;
		inst->reference = 1;
		return inst;
	} else {
		sharpc_retain(inst);
		return inst;
	}
}

void
sharpc_retain(struct sharpc *self) {
	assert(self == inst && self != NULL);
	self->reference++;
}

void
sharpc_release(struct sharpc *self) {
	assert(self == inst && self != NULL);
	self->reference--;
	if (self->reference == 1) {
		free(inst);
		inst = NULL;
	}
}

int
sharpc_callsharp(struct sharpc *self, int argc, struct CSObject *argv, int res) {
	return self->sharpcall(argc, argv, res);
}

int
sharpc_callc(struct sharpc *self, int argc, struct CSObject *argv, int res) {
	assert(argc >= 1);
	assert(argv[0].type == C_STRING);
}

void
sharpc_log(struct sharpc *self, struct CSObject xx[2]) {
	CSObject args[3];
	args[0].type = C_STRING;
	args[0].ptr = "log";
	args[0].v32 = 3;

	for (size_t i = 0; i < 2; i++) {
		args[i + 1] = xx[i];
	}

	int res = sharpc_callsharp(self, 3, args, 0);
	if (res == 0) { // Ö´ÐÐÕýÈ·
	}
}