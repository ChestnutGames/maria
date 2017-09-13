#include "play/playc.h"
#include "play/App.h"

#include <stdlib.h>

struct playc {
	App	*app;
};

struct playc* playc_alloc(int ex, int cb) {
	struct playc *inst = (struct playc *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));
	inst->app = new App();
	return inst;
}

void playc_free(struct playc *self) {
	if (self->app) {
		delete self->app;
	}
	free(self);
}

void playc_start(struct playc *self) {
	self->app->start();
}

void playc_close(struct playc *self) {
	self->app->close();
}

void playc_kill(struct playc *self) {
	self->app->kill();
}

void playc_update(struct playc *self, float delta) {
	self->app->update(delta);
}

bool playc_join(struct playc *self, int uid, int sid, int session) {
	self->app->join(uid, sid, session);
	return true;
}

void playc_leave(struct playc *self, int uid, int sid, int session) {
	self->app->leave(uid, sid, session);
}

void playc_opcode(struct playc *self)  {
	self->app->opcode();
}

int playc_fetch(struct playc *self, char *ptr, int len) {
	self->app->fetch(ptr, len);
	return 0;
}
