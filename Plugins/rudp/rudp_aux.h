#ifndef __RUDP_AUX_H
#define __RUDP_AUX_H

#include <stdio.h>

#ifdef WIN32
#define RUDP_API __declspec (dllexport)
#else
#define RUDP_API extern
#endif

#endif