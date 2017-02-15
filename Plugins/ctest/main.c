#include "playc.h"

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <stdbool.h>
#include <stdint.h>

int main(int argc, char * argv[]) {
	struct playc *p = playc_alloc(1, 2);
	playc_start(p);
	bool exit = false;
	while (!exit) {
		char c = 0;
		printf("%s\n", "please enter a charter.\n");
		scanf("%c", &c);
		switch (c) {
			case 'c':
			playc_join(p, 1, 0, 1);
			break;
			case 'n':
			break;
			case 'e':
			exit = true;
			break;
		}
		playc_update(p, 1.0f/20.0f);
	};
	
	playc_free(p);
	return 0;
}