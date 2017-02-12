#ifndef PLAYC_H
#define PLAYC_H

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include <stdbool.h>
#include <stdint.h>

#define PLAY_API

struct play;
PLAY_API struct play* play_alloc(int ex, int cb);

PLAY_API void play_free(struct play *self);

PLAY_API void play_start(struct play *self);

PLAY_API void play_close(struct play *self);

PLAY_API void play_kill(struct play *self);

PLAY_API void play_update(struct play *self, float delta);

PLAY_API bool play_join(struct play *self, int uid, int sid, int session);

PLAY_API void play_leave(struct play *self, int uid, int sid, int session);

PLAY_API void play_opcode(struct play *self);

PLAY_API int play_fetch(struct play *self, char *ptr, int len);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // !__PLAY_H_
