#include "cstdafx.h"
#include "rudp.h"

struct rudp_aux {
	struct rudp *u;
	struct CSObject sc;
	struct CSObject ex;
	struct CSObject send;
	struct CSObject recv;
	char buffer[MAX_PACKAGE];
};

RUDP_API struct rudp_aux *
rudpaux_alloc(int send_delay, int expired_time, struct CSObject sc, struct CSObject ex, struct CSObject send, struct CSObject recv) {
	struct rudp_aux *aux = (struct rudp_aux *)malloc(sizeof(*aux));
	memset(aux, 0, sizeof(aux));
	aux->u = NULL;
	aux->sc = sc;
	aux->ex = ex;
	aux->send = send;
	aux->recv = recv;
	struct rudp *u = rudp_new(send_delay, expired_time);
	aux->u = u;

	sharpc_retain(aux->sc.ptr);
	return aux;
}

RUDP_API void
rudpaux_free(struct rudp_aux *aux) {
	rudp_delete(aux->u);
	sharpc_release(aux->sc.ptr);
	free(aux);
}

RUDP_API void
rudpaux_send(struct rudp_aux *aux, char *buffer, int sz) {
	rudp_send(aux->u, buffer, sz);
}

RUDP_API void
rudpaux_update(struct rudp_aux *aux, char *buffer, int sz, int tick) {
	struct rudp_package *res = rudp_update(aux->u, buffer, sz, tick);
	while (res) {
		
		struct CSObject args[4];
		args[0] = aux->send;
		args[1] = aux->ex;
		args[2].type = C_INTPTR;
		args[2].ptr = res->buffer;
		args[3].type = C_INT32;
		args[3].v32 = res->sz;
		sharpc_callsharp(aux->sc.ptr, 4, args, 0);

		res = res->next;
	}
	int size = rudp_recv(aux->u, aux->buffer);
	while (size > 0) {

		struct CSObject args[4];
		args[0] = aux->recv;
		args[1] = aux->ex;
		args[2].type = C_INTPTR;
		args[2].ptr = aux->buffer;
		args[3].type = C_INT32;
		args[3].v32 = size;

		sharpc_callsharp(aux->sc.ptr, aux->u, 4, args, 0);

		size = rudp_recv(aux->u, aux->buffer);
	}
}
