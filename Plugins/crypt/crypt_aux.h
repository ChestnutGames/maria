#ifndef __CRYPT_AUX_H_
#define __CRYPT_AUX_H_

#ifdef WIN32
#define CRYPT_API __declspec (dllexport)
#else
#define CRYPT_API extern
#endif

#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>

typedef struct PACKAGE {
	char   *src;
	int32_t len;
} PACKAGE;


#endif // !__CRYPT_AUX_H

