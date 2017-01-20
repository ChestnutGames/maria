#ifndef __LOG_H_
#define __LOG_H_

typedef enum log_level {
	info = 1,
	warning = 2,
	error = 3,
} log_level;

void log_info(char *fmt, ...);
void log_warning(char *fmt, ...);
void log_error(char *fmt, ...);

#endif // __LOG_H_
