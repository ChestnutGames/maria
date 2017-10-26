#ifndef hexmapaux_h
#define haxmapaux_h

#ifdef __cplusplus
extern "C" {
#endif

#include "hexmap.h"
#include "pack.h"
#include <cstdafx.h>
#include <sharpc\sharpc.h>

typedef	struct hexmapaux {
	struct sharpc *sc;
	struct HexMap *map;
	struct CSObject append[4];
} hexmapaux_t;

PLAY_API hexmapaux_t *
hexmapaux_create_from_list(struct sharpc *sc, package_t *package, struct CSObject foreach_cb);

PLAY_API hexmapaux_t *
hexmapaux_create(struct sharpc *sc, 
	int o,
	float oradis,
	int shape,
	int width,
	int height, struct CSObject foreach_cb);

PLAY_API void
hexmapaux_release(hexmapaux_t *self);

PLAY_API package_t *
hexmapaux_save_to_plist(hexmapaux_t *self, char *name);

PLAY_API void
hexmapaux_init(hexmapaux_t *self, struct CSObject owner);

PLAY_API package_t *
hexmapaux_find_cube_by_position(hexmapaux_t *self, struct CSObject x, struct CSObject y, struct CSObject z);

PLAY_API struct CSObject
hexmapaux_change_state(hexmapaux_t *self, struct CSObject x, struct CSObject y, struct CSObject z, struct CSObject state);

PLAY_API void
hexmapaux_sample_height(hexmapaux_t *self, struct CSObject q, struct CSObject r, struct CSObject s, struct CSObject height);

PLAY_API void
hexmapaux_del_hex_by_position(hexmapaux_t *self, struct CSObject owner, struct CSObject x, struct CSObject y, struct CSObject z);

#ifdef __cplusplus
}
#endif

#endif // !hexmapaux_h
