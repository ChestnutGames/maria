#ifndef __RUDP_AUX_H
#define __RUDP_AUX_H

#include <stdio.h>
#include <tchar.h>

#ifdef WIN32
#define RUDP_API __declspec (dllexport)
#else
#define RUDP_API extern
#endif

#endif