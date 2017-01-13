#include "stdafx.h"
#include "play.h"

#include "App.h"
#include "pack.h"

#include <new>
#include <assert.h>

struct play {
	App *app;
};

struct play* play_alloc() {
	struct play *inst = (struct play *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));
	inst->app = new App();
	return inst;
}

void play_free(struct play *self) {
	if (self) {
		if (self->app) {
			delete self->app;
		}
		free(self);
	}
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
	self->app->updata(delta);
}

bool play_join(struct play *self, int uid, int sid) {
	return self->app->join(uid, sid);
}

void play_leave(struct play *self, int uid, int sid) {
	self->app->leave(uid, sid);
}

void play_fetch(struct play *self, char *ptr) {
	WriteInt32(ptr, 0, 3);
}