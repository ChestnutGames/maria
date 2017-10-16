#ifndef hexmapaux_h
#define haxmapaux_h

#ifdef __cplusplus
extern "C" {
#endif

#include "hexmap.h"
#include <cstdafx.h>
#include <sharpc\sharpc.h>

typedef	struct hexmapaux {
	struct sharpc *sc;
	struct HexMap *map;
	struct CSObject foreach_cb;

} hexmapaux_t;

PLAY_API hexmapaux_t *
hexmapaux_create(struct sharpc *sc, 
	int o,
	float oradis,
	int shape,
	int width,
	int height, struct CSObject foreach_cb);

PLAY_API void
hexmapaux_release(hexmapaux_t *self);

PLAY_API void
hexmapaux_initposition(hexmapaux_t *self, struct CSObject cb);

#ifdef __cplusplus
}
#endif

#endif // !hexmapaux_h
