// play_test.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "play.h"

#include <iostream>

struct data {
	int dummy;
};

int _tmain(int argc, _TCHAR* argv[])
{
	struct play *play = play_alloc();
	play_start(play);
	bool exit = false;

	while (!exit) {
		std::cout << std::endl;
		std::cout << "please enter c:" << std::endl;
		char c;
		std::cin >> c;
		switch (c) {
		case 'c': {
			int id = play_join(play, 1, 0);
		}
				  break;
		case 'n':
			break;
		case 'e':
			break;
		default:
			break;
		}
		play_update(play, 1.0f / 20.0f);
	}

	return 0;
}

