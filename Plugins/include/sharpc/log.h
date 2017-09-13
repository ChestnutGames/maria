#ifndef __LOG_H_
#define __LOG_H_

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

typedef enum log_level {
	info = 1,
	warning = 2,
	error = 3,
} log_level;

void log_info(char *fmt, ...);
void log_warning(char *fmt, ...);
void log_error(char *fmt, ...);

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // __LOG_H_
