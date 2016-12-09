// play_test.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <App.h>
#include <play.h>

#include <iostream>

struct data {
	int dummy;
};

int _tmain(int argc, _TCHAR* argv[])
{
	struct play *app = play_alloc();
	bool exit = false;
	while (!exit) {
		std::cout << "please enter c:" << std::endl;
		char c;
		std::cin >> c;
		switch (c) {
		case 'c': {
			struct data *ud = (struct data *)malloc(sizeof(*ud));
			int id = play_join(app, ud);
		}
				  break;
		case 'n':
			break;
		case 'e':
			break;
		default:
			break;
		}
		play_update(app);
	}

	return 0;
}

