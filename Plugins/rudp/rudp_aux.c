#include "rudp_aux.h"
#include "rudp.h"
#include <stdlib.h>
#include <stdio.h>
#include <memory.h>
#include <assert.h>

//struct package {
//	char *buffer;
//	int sz;
//	int cap;
//};

struct rudp_aux {
	struct rudp *u;
	char buffer[MAX_PACKAGE];
};

RUDP_API struct rudp_aux *
aux_new(int send_delay, int expired_time) {
	struct rudp_aux *aux = malloc(sizeof(*aux));
	memset(aux->buffer, 0, MAX_PACKAGE);
	aux->u = NULL;
	struct rudp *u = rudp_new(send_delay, expired_time);
	aux->u = u;
	return aux;
}

RUDP_API void
aux_delete(struct rudp_aux *aux) {
	rudp_delete(aux->u);
	free(aux);
}

RUDP_API int
aux_recv(struct rudp_aux *aux, char *buffer, int sz) {
	int res = rudp_recv(aux->u, aux->buffer);
	if (res == -1) {
		return -1; // false
	} else if (res == 0) {
		return res;
	} else {
		assert(res < sz);
		memcpy(buffer, aux->buffer, res);
		return res;
	}
}

RUDP_API void
aux_send(struct rudp_aux *aux, char *buffer, int sz) {
	rudp_send(aux->u, buffer, sz);
}

RUDP_API struct rudp_package *
aux_update(struct rudp_aux *aux, char *buffer, int sz, int tick) {
	struct rudp_package *res = rudp_update(aux->u, buffer, sz, tick);
	return res;
}

//RUDP_API void
//aux_free_package(struct package pack) {
//	free(pack.buffer);
//}