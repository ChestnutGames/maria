#include "stdafx.h"
#include "rudp.h"
#include <stdlib.h>
#include <stdio.h>
#include <memory.h>

struct package {
	char *buffer;
	int sz;
	int cap;
};

struct rudp_aux {
	struct rudp *u;
	char buffer[MAX_PACKAGE];
};

RUDP_API struct rudp_aux * 
aux_new(int send_delay, int expired_time) {
	struct rudp_aux *aux = malloc(sizeof(*aux));
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
aux_recv(struct rudp_aux *aux, struct package pack) {
	int sz = rudp_recv(aux->u, aux->buffer);
	if (sz == -1) {
		return 0; // false
	} else {
		//char * buffer = malloc(sizeof(char) * sz);
		if (pack.cap > sz) {
			memcpy(pack.buffer, aux->buffer, sz);
			pack.sz = sz;
			return 1;
		} else {
			return 2;
		}
	}
}

RUDP_API void
aux_send(struct rudp_aux *aux, struct package pack) {
	rudp_send(aux->u, pack.buffer, pack.sz);
}

RUDP_API struct package
aux_update(struct rudp_aux *aux, struct package pack, int tick) {
	struct rudp_package *res = rudp_update(aux->u, pack.buffer, pack.sz, tick);
	char *buffer = malloc(res->sz);
	memcpy(res->buffer, buffer, res->sz);
	struct package r;
	r.buffer = buffer;
	r.sz = res->sz;
	return r;
}

RUDP_API void
aux_free_package(struct package pack) {
	free(pack.buffer);
}