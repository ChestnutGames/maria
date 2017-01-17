#include "stdafx.h"
#include "play.h"

#include "App.h"
#include "pack.h"

#include <new>
#include <assert.h>

struct play {
	App *app;
	struct CSObject ex;
	struct CSObject fetch;
};

struct play* play_alloc(struct CSObject ex, struct CSObject cb) {
	struct play *inst = (struct play *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));
	inst->app = new App();
	inst->ex = ex;
	inst->fetch = cb;
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

void play_update(struct play *self, struct CSObject delta) {
	assert(delta.type == REAL);
	self->app->updata((float)delta.f);

	// 同步各个玩家数据

}

bool play_join(struct play *self, struct CSObject uid, struct CSObject sid) {
	return self->app->join(uid.v32, sid.v32);
}

void play_leave(struct play *self, struct CSObject uid, struct CSObject sid) {
	self->app->leave(uid.v32, sid.v32);
}
