// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once


// TODO: reference additional headers your program requires here
#ifdef WIN32
#define PLAY_API __declspec (dllexport)
#else
#define PLAY_API extern
#endif