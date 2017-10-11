#ifndef __LOG_H_
#define __LOG_H_

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

#include <sharpc/sharpc.h>

typedef enum log_level {
	info = 1,
	warning = 2,
	error = 3,
} log_level;

struct logger {
	int reference;
	struct sharpc *sc;
	struct CSObject cb;
};

PLAY_API struct logger *
	log_create(struct sharpc *sc, struct CSObject cb);

PLAY_API void
	log_retain(struct logger *self);

PLAY_API void
	log_release(struct logger *self);

void log_info(struct logger *self, char *fmt, ...);
void log_warning(struct logger *self, char *fmt, ...);
void log_error(struct logger *self, char *fmt, ...);


#ifdef __cplusplus
}
#endif // __cplusplus
#endif // __LOG_H_
