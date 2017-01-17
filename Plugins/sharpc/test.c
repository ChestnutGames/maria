#include "sharpc.h"
#include "conf.h"

#include <stdlib.h>
#include <stdarg.h>
#include <string.h>
#include <assert.h>

SHARPC_API int test1(CSObject cso) {
	CSObject args[4] = { 0 };
	args[0] = cso;
	args[1].type = INT32;
	args[1].v32 = 2;
	args[2].type = INT32;
	args[2].v32 = 3;
	struct sharpc *sharpc = sharpc_alloc(NULL);
	int res = sharpc_call(sharpc, 4, args, 2, 1);
	if (res == 1) {
		if (args[3].type == INT32) {
			return args[3].v32;
		}
	}
	return 0;
}

