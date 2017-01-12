// play_test.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <App.h>

#include <iostream>

struct data {
	int dummy;
};

int _tmain(int argc, _TCHAR* argv[])
{
	App *app = new App();
	app->start();
	bool exit = false;
	while (!exit) {
		std::cout << std::endl;
		std::cout << "please enter c:" << std::endl;
		char c;
		std::cin >> c;
		switch (c) {
		case 'c': {
			int id = app->join(1, 0);
		}
				  break;
		case 'n':
			break;
		case 'e':
			break;
		default:
			break;
		}
		app->updata(1.0f / 20.0f); // fps
	}

	return 0;
}

