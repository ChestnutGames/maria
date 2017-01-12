#include "rudp_aux.h"
#include "rudp.h"
#include <stdlib.h>
#include <stdio.h>
#include <memory.h>
#include <assert.h>

typedef void (*cb)(void *buffer, int len);

struct rudp_aux {
	struct rudp *u;
	cb send;
	cb recv;
	char buffer[MAX_PACKAGE];
};

RUDP_API struct rudp_aux *
aux_alloc(int send_delay, int expired_time, cb send, cb recv) {
	struct rudp_aux *aux = malloc(sizeof(*aux));
	memset(aux, 0, sizeof(aux));
	aux->u = NULL;
	aux->send = send;
	aux->recv = recv;
	struct rudp *u = rudp_new(send_delay, expired_time);
	aux->u = u;
	return aux;
}

RUDP_API void
aux_free(struct rudp_aux *aux) {
	rudp_delete(aux->u);
	free(aux);
}

RUDP_API void
aux_send(struct rudp_aux *aux, char *buffer, int sz) {
	rudp_send(aux->u, buffer, sz);
}

RUDP_API void
aux_update(struct rudp_aux *aux, char *buffer, int sz, int tick) {
	struct rudp_package *res = rudp_update(aux->u, buffer, sz, tick);
	while (res) {
		aux->send(res->buffer, res->sz);
		res = res->next;
	}
	int size = rudp_recv(aux->u, aux->buffer);
	while (size > 0) {
		aux->recv(aux->buffer, size);
		size = rudp_recv(aux->u, aux->buffer);
	}
}
