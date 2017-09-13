#include "hexmap.h"
#include <vector>
#include <math.h>
#include <assert.h>
#include <stdlib.h>
#include <stdio.h>

using namespace std;

static float M_PI = 3.14159265359f;

const vector<CubeCoord> hex_directions = { CubeCoord(1, 0, -1), CubeCoord(1, -1, 0), CubeCoord(0, -1, 1), CubeCoord(-1, 0, 1), CubeCoord(-1, 1, 0), CubeCoord(0, 1, -1) };
const vector<CubeCoord> hex_diagonals = { CubeCoord(2, -1, -1), CubeCoord(1, -2, 1), CubeCoord(-1, -1, 2), CubeCoord(-2, 1, 1), CubeCoord(-1, 2, -1), CubeCoord(1, 1, -2) };

const Orientation layout_pointy = Orientation(sqrt(3.0), sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
const Orientation layout_flat = Orientation(sqrt(3.0), 0.0, -1.0f, 2.0f, 2.0 / sqrt(3.0), 0.0, -2.0f, 0.0f, 0.0);

Hex::Hex(struct CubeCoord main_) : main(main_) {
	//this->offset
	this->axial = cube_to_axial(main);
	this->height = 0;
	this->state = MapState::NONE;
	this->udi = 0;
	this->ud = 0;
}

Hex::Hex(struct OffsetCoord offset_) : offset(offset_) {
	assert(0);
}

Hex::Hex(struct AxialCoord axial_) : axial(axial_) {
	this->main = axial_to_cube(axial_);
	this->height = 0.0f;
	this->state = MapState::NONE;
	this->udi = 0;
	this->ud = 0;
}

void HexMap::InternalInit() {
	// 分配第一象限
	struct Hex *** q = (struct Hex ***)malloc(sizeof(*q) * 1000);
	this->nequad = q;
	for (size_t qi = 0; qi < 1000; qi++) {
		// 列
		struct Hex ** r = (struct Hex **)malloc(sizeof(*r) * 1000);
		this->nequad[qi] = r;
		for (size_t ri = 0; ri < 1000; ri++) {
			struct AxialCoord coord(qi, ri);
			struct Hex *h = new Hex(coord);
			nequad[qi][ri] = h;
		}
	}


	// 第二象限
}

CubeCoord cubecoord_direction(int direction) {
	return hex_directions[direction];
}

CubeCoord cubecoord_neighbor(CubeCoord hex, int direction) {
	return cubecoord_add(hex, cubecoord_direction(direction));
}

CubeCoord cubecoord_diagonal_neighbor(CubeCoord hex, int direction) {
	return cubecoord_add(hex, hex_diagonals[direction]);
}

int cubecoord_length(CubeCoord hex) {
	return int((abs(hex.q) + abs(hex.r) + abs(hex.s)) / 2);
}

int cubecoord_distance(CubeCoord a, CubeCoord b) {
	return cubecoord_length(cubecoord_subtract(a, b));
}

struct HexMap * hexmap_create(MapOrientation o, float oradis) {
#if defined(_DEBUG)
	float innerRadis = oradis * sqrt(3.0f) / 2.0f;
#else
	float innerRadis = oradis * 0.8660254038f;
#endif // 

	struct vector3 origin = { 0.0, 0.0, 0.0 };
	if (o == MapOrientation::FLAT) {
		Layout l(layout_flat, origin, innerRadis, oradis);
		struct HexMap *inst = new HexMap(l);

		return inst;
	} else {
		Layout l(layout_flat, origin, innerRadis, oradis);
		struct HexMap *inst = new HexMap(l);

		return inst;
	}
}

void            hexmap_release(struct HexMap *self) {
	for (size_t qi = 0; qi < 1000; qi++) {
		for (size_t ri = 0; ri < 1000; ri++) {
			delete self->nequad[qi][ri];
		}
		delete self->nequad[qi];
	}
	delete self;
}

//struct Hex *    hexmap_create_hex(struct HexMap *self) {
//
//}
//
//struct Hex *    hexmap_release_hex(struct HexMap *self, struct Hex *h) {
//
//}

struct Hex ***  hexmap_find_quad(struct HexMap *self, struct AxialCoord coord) {
	if (coord.q >= 0 && coord.r >= 0) {
		return self->nequad;
	} else if (coord.q >= 0 && coord.r < 0) {
		return self->nwquad;
	} else if (coord.q < 0 && coord.r >= 0) {
		return self->sequad;
	} else if (coord.q < 0 && coord.r < 0) {
		return self->swquad;
	} else {
		assert(0);
	}
}

struct Hex *    hexmap_find_hex(struct HexMap *self, struct CubeCoord coord) {
	AxialCoord axialcoord = cube_to_axial(coord);
	struct Hex *** quad = hexmap_find_quad(self, axialcoord);
	struct Hex *h = quad[axialcoord.q][axialcoord.r];
	return h;
}

struct vector3  hexmap_to_position(struct HexMap *self, struct AxialCoord coord) {
	Orientation M = self->layout.orientation;
	struct vector3 origin = self->layout.origin;
	struct AxialCoord h = coord;

	double x = (M.f0 * h.q + M.f1 * h.r) * self->layout.innerRadis;
	double z = (M.f2 * h.q + M.f3 * h.r) * self->layout.innerRadis;
	struct vector3 res;
	res.x = x + origin.x;
	res.y = 0.0f;
	res.z = z + origin.z;
	return res;
}

FractionalCubeCoord hexmap_to_cubcoord(struct HexMap *self, struct vector3 p) {
	Orientation M = self->layout.orientation;
	struct vector3 origin = self->layout.origin;

	vector3 pt;
	pt.x = (p.x - origin.x) / self->layout.innerRadis;
	pt.z = (p.y - origin.y) / self->layout.innerRadis;
	double q = M.b0 * pt.x + M.b1 * pt.z - 1;
	double r = M.b2 * pt.x + M.b3 * pt.z - 1;
	return FractionalCubeCoord(q, r, -q - r);
}