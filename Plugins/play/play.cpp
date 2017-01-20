#include "stdafx.h"
#include "play.h"

#include "App.h"
#include "pack.h"

#if defined(SHARPC)
#include "../sharpc/sharpc.h"
#include "../sharpc/log.h"
#endif

#include <new>
#include <assert.h>
#include <string.h>
#include <stdarg.h>

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

#if defined(SHARPC)
	log_info("hello world");
#endif
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
	log_info("delta:%f", delta.f);

	self->app->updata((float)delta.f);

	// 同步各个玩家数据
	/*char buffer[1024] = { 0 };
	int res = self->app->fetch(buffer, 1024);*/

	//CSObject args[4];
	//args[0] = self->fetch;
	//args[1] = self->ex;
	//args[2].type = INTPTR;
	//args[2].ptr = buffer;
	//args[3].type = INT32;
	//args[3].v32 = res;

}

bool play_join(struct play *self, struct CSObject uid, struct CSObject sid, struct CSObject session) {
	log_info("play join uid: %d, sid:%d, session: %d", 1, 0, 1);
	return self->app->join(uid.v32, sid.v32, session.v32);
}

void play_leave(struct play *self, struct CSObject uid, struct CSObject sid, struct CSObject session) {
	self->app->leave(uid.v32, sid.v32, session.v32);
}

int play_fetch(struct play *self, struct CSObject ptr, struct CSObject len) {
	return self->app->fetch((char *)ptr.ptr, len.v32);
}
