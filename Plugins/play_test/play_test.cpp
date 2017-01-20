// play_test.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "play.h"

#include <iostream>
#include <stdarg.h>

struct data {
	int dummy;
};

static void log(char *fmt, ...) {
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };
	
	/*snprintf(buffer, 256, fmt, ##__);*/
	va_end(ap);

	printf(buffer);
}

int _tmain(int argc, _TCHAR* argv[])
{
	log("hello %d", 5);

	struct CSObject nil;
	nil.type = NIL;
	struct play *play = play_alloc(nil, nil);
	play_start(play);
	bool exit = false;

	while (!exit) {
		std::cout << std::endl;
		std::cout << "please enter c:" << std::endl;
		char c;
		std::cin >> c;
		switch (c) {
		case 'c': {
			struct CSObject args[3];
			args[0].type = INT32;
			args[0].v32 = 1;
			args[1].type = INT32;
			args[1].v32 = 0;
			args[2].type = INT32;
			args[2].v32 = 1;

			int id = play_join(play, args[0], args[1], args[2]);
		}
				  break;
		case 'n':
			break;
		case 'e':
			break;
		default:
			break;
		}
		struct CSObject delta;
		delta.type = REAL;
		delta.f = 1.0f / 20.f;
		play_update(play, delta);
	}

	return 0;
}

