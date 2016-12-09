#ifndef __PLAY_H_
#define __PLAY_H_

#include "stdafx.h"

#ifdef __cplusplus
extern "C" {  // only need to export C interface if
#endif
	struct play;

	PLAY_API struct play * play_alloc();
	PLAY_API void play_free(struct play *self);

	PLAY_API void play_update(struct play *self);
	PLAY_API int  play_join(struct play *self, void *ud);
	PLAY_API void play_leave(struct play *self, int id);

#ifdef __cplusplus
}
#endif

#endif