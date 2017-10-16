#include "hexmap.h"

#ifdef __cplusplus
extern "C" {
#endif

#include <plist/plist.h>
#include <stdio.h>
#include <stdlib.h>
#include <assert.h>
#include <string.h>
#include <math.h>

#ifdef __cplusplus
}
#endif


static int
bh_wp_compare(bh_wp_iterator_t l, bh_wp_iterator_t r) {
	return ((*l)->f > (*r)->f);
}

static int
bh_wp_equal(bh_wp_iterator_t l, bh_wp_iterator_t r) {
	return ((*l)->hex == (*r)->hex);
}

static void
bh_wp_free(bh_wp_iterator_t i) {
	hexmap_release_hexastar((*i)->hex->map, *i);
}


#ifdef FIXEDPT
static fix16_t fix16_half = fix16_div(fix16_one, fix16_from_int(2));
#else
#include <math.h>
static float M_PI = 3.14159265359f;
static float M_SQR3 = 1.7320508076;
#endif // !FIXEDPT

#ifndef max
#define max(a, b) ((a) > (b) ? (a) : (b))
#endif // !max
#ifndef min
#define min(a, b) ((a) > (b) ? (b) : (a))
#endif // !min



static struct CubeCoord hex_directions[6] = { {1, 0, -1}, {1, -1, 0}, {0, -1, 1}, {-1, 0, 1}, {-1, 1, 0}, {0, 1, -1} };
static struct CubeCoord hex_diagonals[6] = { {2, -1, -1}, {1, -2, 1}, {-1, -1, 2}, {-2, 1, 1}, {-1, 2, -1}, {1, 1, -2} };

static inline struct Orientation layout_pointy() {
#ifdef FIXEDPT
	fix16_t fp3 = fix16_from_int(3);
	fix16_t fp2 = fix16_from_int(2);

	struct Orientation o;
	o.f0 = fix16_sqrt(fp3);
	o.f1 = fix16_div(fix16_sqrt(fp3), fp2);
	o.f2 = 0;
	o.f3 = fix16_div(fp3, fp2);
	o.b0 = fix16_div(fix16_sqrt(fp3), fp3);
	o.b1 = fix16_div(fix16_sub(0, fix16_one), fp3);
	o.b2 = 0;
	o.b3 = fix16_div(fp2, fp3);
	o.start_angle = fix16_half;
	return o;
#else
	struct Orientation o;
	o.f0 = sqrt(3.0f);
	o.f1 = sqrt(3.0f);
	o.f2 = 0.0f;
	o.f3 = 3.0f / 2.0f;
	o.b0 = sqrt(3.0f) / 3.0f;
	o.b1 = -1.0f / 3.0f;
	o.b2 = 0.0f;
	o.b3 = 2.0f / 3.0f;
	o.start_angle = 0.5f;
	return o;
#endif // FIXEDPT
}

static inline struct Orientation layout_flat() {
#ifdef FIXEDPT
	fix16_t FIXEDPT3 = fix16_from_int(3);
	fix16_t FIXEDPT2 = fix16_from_int(2);

	struct Orientation o;
	o.f0 = fix16_div(FIXEDPT3, FIXEDPT2);
	o.f1 = 0;
	o.f2 = fix16_div(fix16_sqrt(FIXEDPT3), FIXEDPT2);
	o.f3 = fix16_sqrt(FIXEDPT3);

	o.b0 = fix16_div(FIXEDPT2, FIXEDPT3);
	o.b1 = 0;
	o.b2 = fix16_div(fix16_neg(fix16_one), FIXEDPT3);
	o.b3 = fix16_div(fix16_sqrt(FIXEDPT3), FIXEDPT2);

	o.start_angle = 0;

	return o;
#else
	return  { 3.0 / 2.0, 0.0, sqrt(3.0) / 2.0, sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, sqrt(3.0) / 3.0, 0.0 };
#endif // FIXEDPT
}

//static struct Orientation layout_pointy = { sqrt(3.0), sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5 };
//static struct Orientation layout_flat = { 3.0 / 2.0, 0.0, sqrt(3.0) / 2.0, sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, sqrt(3.0) / 3.0, 0.0 };

static inline struct CubeCoord cubecoord_add(struct CubeCoord a, struct CubeCoord b) {
	return { a.q + b.q, a.r + b.r, a.s + b.s };
}

static inline struct CubeCoord cubecoord_subtract(struct CubeCoord a, struct CubeCoord b) {
	return { a.q - b.q, a.r - b.r, a.s - b.s };
}

static inline struct CubeCoord cubecoord_scale(struct CubeCoord a, int k) {
	return { a.q * k, a.r * k, a.s * k };
}

static inline struct CubeCoord cubecoord_direction(int direction) {
	return hex_directions[direction];
}

static inline struct CubeCoord cubecoord_neighbor(struct CubeCoord hex, int direction) {
	return cubecoord_add(hex, cubecoord_direction(direction));
}

static inline struct CubeCoord cubecoord_diagonal_neighbor(struct CubeCoord hex, int direction) {
	return cubecoord_add(hex, hex_diagonals[direction]);
}

static inline int cubecoord_length(struct CubeCoord hex) {
	return (int)((abs(hex.q) + abs(hex.r) + abs(hex.s)) / 2);
}

static inline int cubecoord_distance(struct CubeCoord a, struct CubeCoord b) {
	return cubecoord_length(cubecoord_subtract(a, b));
}


static const int EVEN = 1;
static const int ODD = -1;
static inline struct OffsetCoord qoffset_from_cube(int offset, struct CubeCoord h) {
	struct OffsetCoord coord;
	coord.c = h.q;
	coord.r = h.r + (int)((h.q + offset * (h.q & 1)) / 2);
	return coord;
}

static inline struct CubeCoord qoffset_to_cube(int offset, struct OffsetCoord h) {
	struct CubeCoord coord;
	coord.q = h.c;
	coord.r = h.r - (int)((h.c + offset * (h.c & 1)) / 2);
	coord.s = -coord.q - coord.r;
	return coord;
}

static inline struct OffsetCoord roffset_from_cube(int offset, struct CubeCoord h) {
	struct OffsetCoord coord;
	coord.c = h.q + (int)((h.r + offset * (h.r & 1)) / 2);
	coord.r = h.r;
	return coord;
}

static inline struct CubeCoord roffset_to_cube(int offset, struct OffsetCoord h) {
	struct CubeCoord coord;
	coord.q = h.c - (int)((h.r + offset * (h.r & 1)) / 2);
	coord.r = h.r;
	coord.s = -coord.q - coord.r;
	return coord;
}

static inline struct CubeCoord axial_to_cube(struct AxialCoord h) {
	struct CubeCoord coord;
	coord.q = h.q;
	coord.r = h.r;
	coord.s = -h.q - h.r;
	return coord;
}

static inline struct AxialCoord cube_to_axial(struct CubeCoord h) {
	struct AxialCoord coord;
	coord.q = h.q;
	coord.r = h.r;
	return coord;
}

static inline int64_t axial_to_index(struct AxialCoord h) {
	int64_t index = 0;
	index |= h.q << 32;
	index |= h.r;
	return index;
}

static inline struct CubeCoord hex_round(struct FractionalCubeCoord h) {
#ifdef FIXEDPT

	int q = (int)(fix16_to_int(h.q));
	int r = (int)(fix16_to_int(h.r));
	int s = (int)(fix16_to_int(h.s));

	fix16_t q_diff = fix16_abs(fix16_sub(fix16_to_int(q), h.q));
	fix16_t r_diff = fix16_abs(fix16_sub(fix16_to_int(r), h.r));
	fix16_t s_diff = fix16_abs(fix16_sub(fix16_to_int(s), h.s));
	if (q_diff > r_diff && q_diff > s_diff) {
		q = -r - s;
	} else
		if (r_diff > s_diff) {
			r = -q - s;
		} else {
			s = -q - r;
		}
	return { q, r, s };
#else
	int q = (int)(round(h.q));
	int r = (int)(round(h.r));
	int s = (int)(round(h.s));
	double q_diff = fabs(q - h.q);
	double r_diff = fabs(r - h.r);
	double s_diff = fabs(s - h.s);
	if (q_diff > r_diff && q_diff > s_diff) {
		q = -r - s;
	} else {
		if (r_diff > s_diff) {
			r = -q - s;
		} else {
			s = -q - r;
		}
	}
	struct CubeCoord coord = { q, r, s };
	return coord;
#endif // FIXEDPT
}

/*
** @breif 此函数有问题
*/
//static inline struct FractionalCubeCoord hex_lerp(struct FractionalCubeCoord a, struct FractionalCubeCoord b, double t) {
//	return { a.q * (1 - t) + b.q * t, a.r * (1 - t) + b.r * t, a.s * (1 - t) + b.s * t };
//}


//vector<Hex> hex_linedraw(Hex a, Hex b) {
//	int N = hex_distance(a, b);
//	FractionalHex a_nudge = FractionalHex(a.q + 0.000001, a.r + 0.000001, a.s - 0.000002);
//	FractionalHex b_nudge = FractionalHex(b.q + 0.000001, b.r + 0.000001, b.s - 0.000002);
//	vector<Hex> results = {};
//	double step = 1.0 / max(N, 1);
//	for (int i = 0; i <= N; i++) {
//		results.push_back(hex_round(hex_lerp(a_nudge, b_nudge, step * i)));
//	}
//	return results;
//}

struct HexMap *
	hexmap_create_from_plist(const char *src, int len) {

	plist_t root = NULL;
	plist_from_xml(src, len, &root);
	if (!root) {
		printf("PList XML parsing failed\n");
		return NULL;
	}

	struct HexMap * inst = (struct HexMap *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));

	if (PLIST_IS_DICT(root)) {
		plist_t name_node = plist_dict_get_item(root, "name");
		char *name = NULL;
		plist_get_string_val(name_node, (char **)&name);
		plist_t width_node = plist_dict_get_item(root, "width");
		uint64_t width;
		plist_get_uint_val(width_node, &width);
		uint64_t height;
		plist_t height_node = plist_dict_get_item(root, "height");
		assert(PLIST_IS_UINT(height_node));
		plist_get_uint_val(height_node, &height);

		uint64_t orientation;
		plist_t orientation_node = plist_dict_get_item(root, "orientation");
		plist_get_uint_val(orientation_node, &orientation);
		if (orientation == 0) {
			inst->layout.orientation = layout_pointy();
		} else {
			inst->layout.orientation = layout_flat();
		}

		plist_t grids_node = plist_dict_get_item(root, "grids");
		uint32_t size = plist_array_get_size(grids_node);
		for (size_t i = 0; i < size; i++) {
			plist_t grid_node = plist_array_get_item(grids_node, i);
			uint64_t g;
			plist_t g_node = plist_dict_get_item(grid_node, "g");
			plist_get_uint_val(g_node, &g);

			uint64_t r;
			plist_t r_node = plist_dict_get_item(grid_node, "r");
			plist_get_uint_val(r_node, &r);

			struct Hex *h = hexmap_create_hex(inst);
			h->axial = { (int)g, (int)r };
			h->main = axial_to_cube(h->axial);

			h->pos = hexmap_to_position(inst, h->axial);
			h->key = axial_to_index(h->axial);

			hexmap_add_hex(inst, h);
		}
	}

	return NULL;
}

static struct HexMap *
hexmap_create_hex(struct HexMap *self,
	int width,
	int height) {

	int mapSize = max(width, height);

	for (int q = -mapSize; q <= mapSize; q++) {
		int r1 = max(-mapSize, -q - mapSize);
		int r2 = min(mapSize, -q + mapSize);
		for (int r = r1; r <= r2; r++) {
			struct Hex *hex = hexmap_create_hex(self);
			struct AxialCoord axial = { q, r };
			struct CubeCoord cube = axial_to_cube(axial);
			struct vector3 position = hexmap_to_position(self, axial);
			hex->axial = axial;
			hex->main = cube;
			hex->pos = position;
			hex->key = axial_to_index(axial);
			hexmap_add_hex(self, hex);
		}
	}

	return self;
}

struct HexMap *
	hexmap_create(MapOrientation o, float outerRadis,
		MapShape shape,
		int width,
		int height) {

#ifdef FIXEDPT
	fix16_t FIXEDPT3 = fix16_from_int(3);
	fix16_t FIXEDPT2 = fix16_from_int(2);
	fix16_t innerRadis = fix16_div(fix16_mul(fix16_from_float(oradis), fix16_sqrt(FIXEDPT3)), FIXEDPT2);
#else
#if defined(_DEBUG)
	float innerRadis = outerRadis * sqrt(3.0f) / 2.0f;
#else
	float innerRadis = outerRadis * 0.8660254038f;
#endif // 
#endif // FIXEDPT

	struct vector3 origin = { 0, 0, 0 };
	struct Layout l;
	if (o == FLAT) {
		l.orientation = layout_flat();
		l.origin = origin;
		l.innerRadis = innerRadis;
		l.outerRadis = outerRadis;
	} else {
		l.orientation = layout_pointy();
		l.origin = origin;
		l.innerRadis = innerRadis;
		l.outerRadis = outerRadis;
	}
	struct HexMap *inst = (struct HexMap *)malloc(sizeof(*inst));
	memset(inst, 0, sizeof(*inst));
	inst->layout = l;
	inst->hexhash = NULL;
	inst->hexpool = NULL;
	inst->hexwppool = NULL;
	switch (shape) {
	case HEX:
		return hexmap_create_hex(inst, width, height);
		break;
	default:
		break;
	}
	return inst;
}

void
hexmap_release(struct HexMap *self) {
	struct Hex *h, *tmp;
	HASH_ITER(hh, self->hexhash, h, tmp) {
		free(h);
	}
	LL_FOREACH_SAFE(self->hexpool, h, tmp) {
		free(h);
	}
	free(self);
}

struct Hex *
	hexmap_create_hex(struct HexMap *self) {
	struct Hex *elt, *res;
	int count;
	LL_COUNT(self->hexpool, elt, count);
	if (count > 0) {
		res = self->hexpool->next;
		LL_DELETE(self->hexpool, self->hexpool->next);
		return res;
	}
	res = (struct Hex *)malloc(sizeof(*res));
	return res;
}

void
hexmap_release_hex(struct HexMap *self, struct Hex *h) {
	assert(self != NULL && h != NULL);
	LL_APPEND(self->hexpool, h);
}

struct HexWaypoint *
	hexmap_create_hexastar(struct HexMap *self) {
	struct HexWaypoint *elt, *res;
	int count;
	LL_COUNT(self->hexwppool, elt, count);
	if (count > 0) {
		res = self->hexwppool->next;
		LL_DELETE(self->hexwppool, self->hexwppool->next);
		return res;
	}
	res = (struct HexWaypoint *)malloc(sizeof(*res));
	return res;
}

void
hexmap_release_hexastar(struct HexMap *self, struct HexWaypoint *h) {
	assert(self != NULL && h != NULL);
	LL_APPEND(self->hexwppool, h);
}

struct Hex *    hexmap_find_hex_by_coord(struct HexMap *self, struct CubeCoord coord) {
	struct AxialCoord axialcoord = cube_to_axial(coord);
	int64_t key = axial_to_index(axialcoord);
	return hexmap_find_hex(self, key);
}

/*              ***
**            *     *
			***     *
		  *     ***
		  *     *
			***
*/
struct vector3
	hexmap_to_position(struct HexMap *self, struct AxialCoord coord) {
	struct Orientation M = self->layout.orientation;
	struct vector3 origin = self->layout.origin;
	struct AxialCoord h = coord;

#ifdef FIXEDPT
	fix16_t x = fix16_mul(fix16_add(fix16_mul(M.f0, fix16_from_int(h.q)), fix16_mul(M.f1, fix16_from_int(h.r))), self->layout.innerRadis);
	fix16_t z = fix16_mul(fix16_add(fix16_mul(M.f2, fix16_from_int(h.q)), fix16_mul(M.f3, fix16_from_int(h.r))), self->layout.innerRadis);

	struct vector3 res;
	res.x = x;
	res.y = 0;
	res.z = z;
#else
	float x = (M.f0 * h.q + M.f1 * h.r) * self->layout.innerRadis;
	float z = (M.f2 * h.q + M.f3 * h.r) * self->layout.innerRadis;
	struct vector3 res;
	res.x = x;
	res.y = 0.0f;
	res.z = z;
#endif // FIXEDPT
	return res;
}

int
hexmap_get_pathid(struct HexMap *self) {
	int i = 0;
	for (; i < MAX_PATH_NUM; i++) {
		if (self->pathState[i].free == 0) {
			return i;
		}
	}
	return -1;
}

/*
** -        -
** | f0, f1 |    * | q |  = | x |
** | f2, f3 |      | r |    | y |
** -        -
** 求逆矩阵
** | q | = | f0, f1 |-1 * | x |
** | r |   | f2, f3 |     | y |
*/
struct FractionalCubeCoord
	hexmap_to_cubcoord(struct HexMap *self, struct vector3 p) {
	struct Orientation M = self->layout.orientation;
	struct vector3 origin = self->layout.origin;

#ifdef FIXEDPT
	struct vector3 pt;
	pt.x = fix16_div(fix16_sub(p.x, origin.x), self->layout.innerRadis);
	pt.z = fix16_div(fix16_sub(p.z, origin.z), self->layout.innerRadis);

	fix16_t q = fix16_add(fix16_mul(M.b0, pt.x), fix16_mul(M.b1, pt.z));
	fix16_t r = fix16_add(fix16_mul(M.b2, pt.x), fix16_mul(M.b3, pt.z));

	struct FractionalCubeCoord coord;
	coord.q = q;
	coord.r = r;
	coord.s = fix16_sub(fix16_neg(q), r);
	return coord;

#else
	struct vector3 pt;
	pt.x = (p.x - origin.x) / self->layout.innerRadis;
	pt.z = (p.z - origin.z) / self->layout.innerRadis;
	double q = M.b0 * pt.x + M.b1 * pt.z;
	double r = M.b2 * pt.x + M.b3 * pt.z;
	struct FractionalCubeCoord coord = { q, r, -q - r };
	return coord;
#endif // FIXEDPT

}

static int
hexmap_h(struct HexMap *self, struct vector3 startPos, struct vector3 exitPos) {
#ifdef FIXEDPT
	return fix16_add(fix16_abs(fix16_sub(exitPos.x, startPos.x)), fix16_abs(fix16_sub(exitPos.z, startPos.z)));
#else
	return fabs(exitPos.x - startPos.x) + fabs(exitPos.z - startPos.z);
#endif // FIXEDPT
}

static int
hexastar_compare(void *lhs, void *rhs) {
	return (((struct HexWaypoint *)(lhs))->f < ((struct HexWaypoint *)(rhs))->f);
}

static int
hexastar_equal(struct HexWaypoint * lhs, struct HexWaypoint * rhs) {

	if (((struct HexWaypoint *)lhs)->hex == ((struct HexWaypoint *)rhs)->hex && ((struct HexWaypoint *)lhs)->hex != NULL) {
		return 0;
	}
	return 1;
}

int
hexmap_findpath(struct HexMap *self, struct vector3 startPos, struct vector3 exitPos) {

	int pathid = hexmap_get_pathid(self);
	self->pathState[pathid].free = 1;
	self->pathState[pathid].pathid = pathid;
	self->pathState[pathid].startPos = startPos;
	self->pathState[pathid].exitPos = exitPos;
	struct CubeCoord coord = hex_round(hexmap_to_cubcoord(self, exitPos));
	self->pathState[pathid].exitHex = hexmap_find_hex_by_coord(self, coord);
	self->pathState[pathid].open = bh_wp_new(bh_wp_compare, bh_wp_free);
	self->pathState[pathid].closed = NULL;

	struct HexWaypoint *tmp = hexmap_create_hexastar(self);
	tmp->g = 0;
	tmp->h = hexmap_h(self, startPos, exitPos);
	tmp->f = tmp->g + tmp->h;

	coord = hex_round(hexmap_to_cubcoord(self, startPos));
	tmp->hex = hexmap_find_hex_by_coord(self, coord);

	LL_APPEND(self->pathState[pathid].closed, tmp);

	return pathid;
}

static void hexastar_visit_free(void *h) {
	struct HexWaypoint *ptr = (struct HexWaypoint *)(h);
	if (ptr) {
		hexmap_release_hexastar(ptr->hex->map, ptr);
	}
}

int
hexmap_findpath_update(struct HexMap *self, int pathid, struct Hex **h) {
	int i = 0;
	for (; i < MAX_PATH_NUM; ++i) {
		struct HexWaypointHead *path = &self->pathState[i];
		if (path->free == 1) // 占用
		{
			if (bh_wp_size(&path->open) > 0) {
				bh_wp_iterator_t top = NULL;
				bh_wp_peek(&path->open, &top);
				LL_APPEND(path->closed, *top);

				assert((*top) != NULL);
				struct HexWaypoint *elt, etmp;
				int j = 0;
				for (; j < 6; j++) {
					if ((*top)->hex->neighbor[i] == NULL) continue;

					etmp.hex = (*top)->hex->neighbor[i];
					
					LL_SEARCH(path->closed, elt, &etmp, hexastar_equal);
					if (elt) // found
					{
						continue;
					}

					bh_wp_iterator_t ret = NULL;
					bh_wp_value_t bhetmp = &etmp;
					bh_wp_search(&path->open, &ret, &bhetmp, bh_wp_equal);
					if (ret != NULL) {
						continue;
					}

					struct HexWaypoint *tmp = hexmap_create_hexastar(self);
					int cost = hexmap_h(self, (*top)->hex->pos, (*top)->hex->neighbor[i]->pos);
					tmp->g = (*top)->g + cost;
					tmp->h = hexmap_h(self, (*top)->hex->neighbor[i]->pos, self->pathState[pathid].exitPos);
					tmp->f = tmp->g + tmp->h;

					bh_wp_push(&path->open, &tmp);
				}
				path->nextHex = (*top)->hex;
			}
		}
	}
	return 0;
}

int
hexmap_findpath_clean(struct HexMap *self, int pathid) {
	struct HexWaypointHead *path = &self->pathState[pathid];

	bh_wp_destroy_free(&path->open, bh_wp_free);

	struct HexWaypoint *elt, *tmp, etmp;
	LL_FOREACH_SAFE(path->closed, elt, tmp) {
		hexmap_release_hexastar(self, elt);
	}

	return 0;
}

struct Hex *
	hexmap_find_hex(struct HexMap *self, int64_t key) {
	struct Hex *h;
	HASH_FIND_INT(self->hexhash, &key, h);
	return h;
}

void
hexmap_add_hex(struct HexMap *self, struct Hex *h) {
	assert(h != NULL);
	struct Hex *res = NULL;
	HASH_FIND_INT(self->hexhash, &h->key, res);
	if (res == NULL) {
		HASH_ADD_INT(self->hexhash, key, h);
	}
}

void
hexmap_remove_hex(struct HexMap *self, struct Hex *h) {
	HASH_DEL(self->hexhash, h);
	hexmap_release_hex(self, h);
}

void
hexmap_foreach(struct HexMap *self, hexmap_foreach_cb cb) {
	struct Hex *h, *tmp;
	HASH_ITER(hh, self->hexhash, h, tmp) {
		cb(h);
	}
}