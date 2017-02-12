#ifndef CONF_H
#define CONF_H

#ifdef WIN32
#define SHARPC_API __declspec (dllexport)
#else
#define SHARPC_API
#endif

//#ifndef PLAY_API
//#define PLAY_API SHARPC_API
//#endif // !PLAY_API

#endif // !CONF_H

