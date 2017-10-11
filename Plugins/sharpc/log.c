#include <sharpc/log.h>
#include <sharpc/sharpc.h>

#include <stdbool.h>
#include <stdint.h>
#include <math.h>
#include <assert.h>
#include <stdlib.h>
#include <stdio.h>
#include <memory.h>
#include <stdarg.h>






struct logger *
log_create(struct sharpc *sc, struct CSObject cb) {
	assert(sc != NULL);
	struct logger *inst = malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));
	inst->reference = 1;
	sharpc_retain(sc);
	inst->sc = sc;
	inst->cb = cb;

	return inst;
}

void
log_retain(struct logger *self) {
	assert(self != NULL);
	self->reference++;
}

void
log_release(struct logger *self) {
	assert(self != NULL);
	self->reference--;
	if (self->reference <= 0) {
		sharpc_release(self->sc);
		free(self);
		self = NULL;
	}
}

void log_info(struct logger *self, char *fmt, ...) {
#pragma(push)
#pragma warning ( disable : 4789 )
	// unused code that generates compiler warning C4789
#if defined(_DEBUG)
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsnprintf(buffer, 256, fmt, ap);
	va_end(ap);

	struct CSObject args[3];
	args[0] = self->cb;
	args[1].type = C_INT32;
	args[1].v32 = info;
	args[2].type = C_STRING;
	args[2].ptr = (void *)buffer;
	args[2].v32 = 256;

	int res = sharpc_callsharp(self->sc, 3, args);
	if (res > 0) {

	}
#endif // 
#pragma (pop)
}

void log_warning(struct logger *self, char *fmt, ...) {
#pragma(push)
#pragma warning ( disable : 4789 )
	// unused code that generates compiler warning C4789
#if defined(_DEBUG)
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsnprintf(buffer, 256, fmt, ap);
	va_end(ap);

	struct CSObject args[3];
	args[0] = self->cb;
	args[1].type = C_INT32;
	args[1].v32 = warning;
	args[2].type = C_STRING;
	args[2].ptr = buffer;
	args[2].v32 = 256;

	int res = sharpc_callsharp(self->sc, 3, args);
	if (res > 0) {
	}
#endif // 
#pragma(pop)
}

void log_error(struct logger *self, char *fmt, ...) {
#pragma(push)
#pragma warning ( disable : 4789 )
	// unused code that generates compiler warning C4789
#if defined(_DEBUG)
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsnprintf(buffer, 256, fmt, ap);
	va_end(ap);

	struct CSObject args[2];
	args[0] = self->cb;
	args[1].type = C_INT32;
	args[1].v32 = error;
	args[2].type = C_STRING;
	args[2].ptr = buffer;
	args[2].v32 = 256;

	int res = sharpc_callsharp(self->sc, 3, args);
	if (res > 0) {

	}

#endif // 
#pragma(pop)
}

