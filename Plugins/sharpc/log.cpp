#include "log.h"
#include "sharpc.h"

#include <string.h>
#include <stdlib.h>
#include <stdarg.h>
#include <stdio.h>

void log_info(char *fmt, ...) {
#if defined(_DEBUG)
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsnprintf(buffer, 256, fmt, ap);
	va_end(ap);

	struct sharpc* sc = sharpc_alloc(NULL);

	struct CSObject args[2];
	args[0].type = INT32;
	args[0].v32 = log_level::info;
	args[1].type = STRING;
	args[1].ptr = buffer;

	sharpc_log(sc, args);
#endif // 
}

void log_warning(char *fmt, ...) {
#if defined(_DEBUG)
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsnprintf(buffer, 256, fmt, ap);
	va_end(ap);

	struct sharpc* sc = sharpc_alloc(NULL);

	struct CSObject args[2];
	args[0].type = INT32;
	args[0].v32 = log_level::warning;
	args[1].type = STRING;
	args[1].ptr = buffer;

	sharpc_log(sc, args);
#endif // 
}

void log_error(char *fmt, ...) {
#if defined(_DEBUG)
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	vsnprintf(buffer, 256, fmt, ap);
	va_end(ap);

	struct sharpc* sc = sharpc_alloc(NULL);

	struct CSObject args[2];
	args[0].type = INT32;
	args[0].v32 = log_level::error;
	args[1].type = STRING;
	args[1].ptr = buffer;

	sharpc_log(sc, args);

#endif // 
}
