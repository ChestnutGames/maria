#include "hexmapaux.h"
#include "pack.h"
#include "sharpc\log.h"
#include <setjmp.h>
#include <exception>

static void hexmapaux_foreach_callsharp(struct Hex *h);

hexmapaux_t *
hexmapaux_create_from_list(struct sharpc *sc, package_t *package, struct CSObject foreach_cb) {
	hexmapaux_t *inst = (hexmapaux_t *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(sizeof(*inst)));
	inst->sc = sc;
	inst->append[0] = foreach_cb;
	inst->map = hexmap_create_from_plist(package->src, package->size);
	inst->map->ud = inst;
	return inst;
}

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
	inst->append[0] = foreach_cb;

	map->ud = inst;

	return inst;
}

void
hexmapaux_release(hexmapaux_t *self) {
	hexmap_release(self->map);
	free(self);
}

PLAY_API package_t *
hexmapaux_save_to_plist(hexmapaux_t *self, char *name) {
	char *buffer = NULL;
	uint32_t size = 0;
	hexmap_save_to_plist(self->map, &buffer, &size, name);
	package_t *package = package_alloc(buffer, size);
	free(buffer);
	return package;
}

static void hexmapaux_foreach_callsharp(struct Hex *h) {
	struct HexMap *map = h->map;
	hexmapaux_t *self = (hexmapaux_t *)map->ud;
	assert(map != NULL && self != NULL);
	char buffer[64] = { 0 };
	int offset = 0;
	offset = WriteInt32(buffer, offset, h->main.q, 64);
	offset = WriteInt32(buffer, offset, h->main.r, 64);
	offset = WriteInt32(buffer, offset, h->main.s, 64);

	offset = WriteFnt32(buffer, offset, h->pos.x, 64);
	offset = WriteFnt32(buffer, offset, h->pos.y, 64);
	offset = WriteFnt32(buffer, offset, h->pos.z, 64);

	offset = WriteFnt32(buffer, offset, h->height, 64);
	offset = WriteInt32(buffer, offset, h->state, 64);

	struct CSObject args[4];
	memset(args, 0, sizeof(args));
	args[0] = self->append[0];
	args[1] = self->append[1];
	args[2].type = C_INT32;
	args[2].v32 = 1;
	args[3].type = C_INTPTR;
	args[3].ptr = buffer;
	self->sc->sharpcall(4, args);
}

void
hexmapaux_init(hexmapaux_t *self, struct CSObject owner) {
	self->append[1] = owner;
	hexmap_foreach(self->map, hexmapaux_foreach_callsharp);
}

package_t *
hexmapaux_find_cube_by_position(hexmapaux_t *self, struct CSObject x, struct CSObject y, struct CSObject z) {
	struct vector3 position = { x.f, y.f, z.f };
	struct Hex *h = hexmap_find_hex_by_position(self->map, position);

	package_t *package = package_alloci(32);
	char *buffer = package_buffer(package);
	int ofs = 0;
	ofs = WriteInt32(buffer, ofs, h->main.q, 32);
	ofs = WriteInt32(buffer, ofs, h->main.r, 32);
	ofs = WriteInt32(buffer, ofs, h->main.s, 32);
	return package;
}

struct CSObject
hexmapaux_change_state(hexmapaux_t *self, struct CSObject x, struct CSObject y, struct CSObject z, struct CSObject state) {
	struct vector3 position = { x.f, y.f, z.f };
	struct Hex *hex = hexmap_find_hex_by_position(self->map, position);
	if (hex->state != state.v32) {
		int old = hex->state;
		hex->state = (HexState)state.v32;
		state.v32 = old;
		return state;
	} else {
		return state;
	}
}

void
hexmapaux_sample_height(hexmapaux_t *self, struct CSObject q, struct CSObject r, struct CSObject s, struct CSObject height) {
	struct CubeCoord cube = { q.v32, r.v32, s.v32 };
	struct Hex *hex = hexmap_find_hex_by_cube(self->map, cube);
	hex->height = height.f;
}

void
hexmapaux_del_hex_by_position(hexmapaux_t *self, struct CSObject owner, struct CSObject x, struct CSObject y, struct CSObject z) {
	try {
		self->append[1] = owner;
		struct vector3 position = { x.f, y.f, z.f };
		struct Hex *h = hexmap_find_hex_by_position(self->map, position);
		if (h != NULL) {
			char buffer[64] = { 0 };
			int ofs = 0;
			int q = h->main.q;
			int r = h->main.r;
			int s = h->main.s;
			ofs = WriteInt32(buffer, ofs, 100, 64);
			ofs = WriteInt32(buffer, ofs, q, 64);
			ofs = WriteInt32(buffer, ofs, r, 64);
			ofs = WriteInt32(buffer, ofs, s, 64);

			struct CSObject args[7];
			memset(args, 0, sizeof(args));
			args[0] = self->append[0];
			args[1] = self->append[1];
			args[2].type = C_INT32;
			args[2].v32 = 2;
			args[3].type = C_INTPTR;
			args[3].ptr = buffer;
			args[4].v32 = q;
			args[5].v32 = r;
			args[6].v32 = s;

			self->sc->sharpcall(7, args);

			hexmap_remove_hex(self->map, h);
		} else {
			char buffer[32] = { 0 };
			int ofs = 0;
			ofs = WriteInt32(buffer, ofs, 101, 256);

			struct CSObject args[4];
			memset(args, 0, sizeof(args));
			args[0] = self->append[0];
			args[1] = self->append[1];
			args[2].type = C_INT32;
			args[2].v32 = 2;
			args[3].type = C_INTPTR;
			args[3].ptr = buffer;

			self->sc->sharpcall(4, args);
		}
	}
	catch (const std::exception& ex) {
		char buffer[256] = { 0 };
		int ofs = 0;
		ofs = WriteInt32(buffer, ofs, 101, 256);
		const char *msg = ex.what();
		int len = strlen(msg);
		if (len < 256 - ofs) {
			WriteString(buffer, ofs, msg, len, 256);
		} else {
			WriteString(buffer, ofs, "Error", 5, 256);
		}
		
		struct CSObject args[4];
		memset(args, 0, sizeof(args));
		args[0] = self->append[0];
		args[1] = self->append[1];
		args[2].type = C_INT32;
		args[2].v32 = 2;
		args[3].type = C_INTPTR;
		args[3].ptr = buffer;

		self->sc->sharpcall(4, args);
	}
	
}