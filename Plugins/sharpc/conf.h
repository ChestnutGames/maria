#ifndef CONF_H
#define CONF_H

#ifdef WIN32
#define SHARPC_API __declspec (dllexport)
#else
#define SHARPC_API extern
#endif

#endif // !CONF_H

