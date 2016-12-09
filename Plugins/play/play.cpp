#include "stdafx.h"
#include "play.h"
#include "App.h"

#include <cstdlib>

#ifdef __cplusplus
extern "C" {  // only need to export C interface if
#endif

struct play {
	App *app;
};

PLAY_API struct play * play_alloc() {
	struct play *inst = (struct play *)malloc(sizeof(*inst));
	inst->app = new App();
	return inst;
}

PLAY_API void play_free(struct play *self) {
	delete self->app;
	free(self);
}

PLAY_API void play_update(struct play *self) {
	self->app->updata();
}

PLAY_API int  play_join(struct play *self, void *ud) {
	return self->app->join(ud);
}

PLAY_API void play_leave(struct play *self, int id) {
	self->app->leave(id);
}

#ifdef __cplusplus
}
#endif