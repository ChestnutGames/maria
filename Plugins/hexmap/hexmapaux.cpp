#include "hexmapaux.h"
#include "pack.h"

hexmapaux_t *
hexmapaux_create(struct sharpc *sc,
	int o,
	float oradis,
	int shape,
	int width,
	int height,
	struct CSObject foreach_cb) {
	hexmapaux_t *inst = (hexmapaux_t *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(sizeof(*inst)));
	struct HexMap *map = hexmap_create((MapOrientation)o, oradis, (MapShape)shape, width, height);
	inst->map = map;
	inst->sc = sc;
	inst->foreach_cb = foreach_cb;

	map->ud = inst;

	return inst;
}

void
hexmapaux_release(hexmapaux_t *self) {
	hexmap_release(self->map);
	free(self);
}

static void hexmapaux_foreach_initposition(struct Hex *h) {
	struct HexMap *map = h->map;
	hexmapaux_t *self = (hexmapaux_t *)map->ud;
	char buffer[32] = { 0 };
	int offset = 0;
	offset = WriteInt32(buffer, offset, h->main.q);
	offset = WriteFnt32(buffer, offset, h->main.r);
	offset = WriteFnt32(buffer, offset, h->main.s);

	offset = WriteFnt32(buffer, offset, h->pos.x);
	offset = WriteFnt32(buffer, offset, h->pos.y);
	offset = WriteFnt32(buffer, offset, h->pos.z);

	struct CSObject args[2];
	args[0] = self->foreach_cb;
	args[1].type = C_INTPTR;
	args[1].ptr = buffer;
	self->sc->sharpcall(2, args);
}

void
hexmapaux_initposition(hexmapaux_t *self, struct CSObject cb) {
	hexmap_foreach(self->map, hexmapaux_foreach_initposition);
}