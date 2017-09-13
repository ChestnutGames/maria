#ifndef PLAYC_H
#define PLAYC_H

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include "config.h"

#include <stdbool.h>
#include <stdint.h>

struct playc;
PLAY_API struct playc* playc_alloc(int ex, int cb);

PLAY_API void playc_free(struct playc *self);

PLAY_API void playc_start(struct playc *self);

PLAY_API void playc_close(struct playc *self);

PLAY_API void playc_kill(struct playc *self);

PLAY_API void playc_update(struct playc *self, float delta);

PLAY_API bool playc_join(struct playc *self, int uid, int sid, int session);

PLAY_API void playc_leave(struct playc *self, int uid, int sid, int session);

PLAY_API void playc_opcode(struct playc *self);

PLAY_API int playc_fetch(struct playc *self, char *ptr, int len);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PLAY_H_
