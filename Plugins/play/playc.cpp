#include "stdafx.h"
#include "playc.h"
#include "App.h"

#include <stdlib.h>

struct play {
	App	*app;
};

struct play* play_alloc(int ex, int cb) {
	struct play *inst = (struct play *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));
	inst->app = new App();
	return inst;
}

void play_free(struct play *self) {
	if (self->app) {
		delete self->app;
	}
	free(self);
}

void play_start(struct play *self) {
	self->app->start();
}

void play_close(struct play *self) {
	self->app->close();
}

void play_kill(struct play *self) {
	self->app->kill();
}

void play_update(struct play *self, float delta) {
	self->app->update(delta);
}

bool play_join(struct play *self, int uid, int sid, int session) {
	self->app->join(uid, sid, session);
	return true;
}

void play_leave(struct play *self, int uid, int sid, int session) {
	self->app->leave(uid, sid, session);
}

void play_opcode(struct play *self)  {
	self->app->opcode();
}

int play_fetch(struct play *self, char *ptr, int len) {
	self->app->fetch(ptr, len);
	return 0;
}
