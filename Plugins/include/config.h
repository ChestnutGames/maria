#pragma once
#ifndef CONFIG_H
#define CONFIG_H

#if defined(WIN32) || defined(_WIN32)
#define PLAY_API __declspec(dllexport)
#else
#define PLAY_API 
#endif
#define CRYPT_API PLAY_API
#define SHARPC_API PLAY_API
#define RUDP_API PLAY_API 


#endif // !CONFIG_H

