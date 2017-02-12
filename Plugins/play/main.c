#include "playc.h"

#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <stdbool.h>
#include <stdint.h>

int main(int argc, char * argv[]) {
	struct play *p = play_alloc(1, 2);
	bool exit = false;
	while (!exit) {
		char c = 0;
		printf("%s\n", "please enter a charter.");
		scanf("%c", &c);
		switch (c) {
			case 'c':
			play_join(p, 1, 0, 1);
			break;
			case 'n':
			break;
			case 'e':
			exit = true;
			break;
		}
		play_update(p, 1.0f/20.0f);
	};
	
	play_free(p);
	return 0;
}