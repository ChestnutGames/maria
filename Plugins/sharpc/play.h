#pragma once
#ifndef __PLAY_H_
#define __PLAY_H_

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include "conf.h"
#include "sharpc.h"

#include <stdbool.h>

struct play;
SHARPC_API struct play* play_alloc(struct CSObject ex, struct CSObject cb);

SHARPC_API void play_free(struct play *self);

SHARPC_API void play_start(struct play *self);

SHARPC_API void play_close(struct play *self);

SHARPC_API void play_kill(struct play *self);

SHARPC_API void play_update(struct play *self, struct CSObject delta);

SHARPC_API bool play_join(struct play *self, struct CSObject uid, struct CSObject sid, struct CSObject session);

SHARPC_API void play_leave(struct play *self, struct CSObject uid, struct CSObject sid, struct CSObject session);

SHARPC_API int play_fetch(struct play *self, struct CSObject ptr, struct CSObject len);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PLAY_H_
