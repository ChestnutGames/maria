#pragma once
#ifndef __PLAY_H_
#define __PLAY_H_

#include "stdafx.h"

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include <stdbool.h>

struct play;
PLAY_API struct play* play_alloc();

PLAY_API void play_free(struct play *self);

PLAY_API void play_start(struct play *self);

PLAY_API void play_close(struct play *self);

PLAY_API void play_kill(struct play *self);

PLAY_API void play_update(struct play *self, float delta);

PLAY_API bool play_join(struct play *self, int uid, int sid);

PLAY_API void play_leave(struct play *self, int uid, int sid);

PLAY_API void play_fetch(struct play *self, char *ptr);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PLAY_H_