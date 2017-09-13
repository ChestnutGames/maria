#ifndef HEXMAP_H
#define HEXMAP_H

#include "math3d.h"
#include <cmath>


enum MapState {
	NONE,
};

enum MapOrientation {
	FLAT,
	POINTY,
};

struct CubeCoord {
	int q;
	int r;
	int s;
	CubeCoord() : q(0), r(0), s(0) {}
	CubeCoord(int q_, int r_, int s_) : q(q_), r(r_), s(s_) {}
};

struct FractionalCubeCoord {
	double q; // x
	double r; // z
	double s; // y
	FractionalCubeCoord(double q_, double r_, double s_) : q(q_), r(r_), s(s_) {}
};

struct OffsetCoord {
	int col;
	int row;
	OffsetCoord() : col(0), row(0) {}
	OffsetCoord(int col_, int row_) : col(col_), row(row_) {}
};

struct AxialCoord {
	int q;   // col
	int r;	 // row
	AxialCoord() : q(0), r(0) {}
	AxialCoord(int q_, int r_) : q(q_), r(r_) {}
};

/*
** cube coordinates
*/
struct Hex {
	struct CubeCoord   main;
	struct OffsetCoord offset;
	struct AxialCoord  axial;
	float              height;
	MapState           state;
	
	void              *ud;

	// 双向链表构建格子，避免内存浪费，对于有很多空格的时候
	struct Hex       *neighbor[6];

	Hex(struct CubeCoord main_);
	Hex(struct OffsetCoord offset_);
	Hex(struct AxialCoord axial_);
};

//void HexIn

struct Orientation {
	const double f0;
	const double f1;
	const double f2;
	const double f3;
	const double b0;
	const double b1;
	const double b2;
	const double b3;
	const double start_angle;
	Orientation(double f0_, double f1_, double f2_, double f3_, double b0_, double b1_, double b2_, double b3_, double start_angle_) : f0(f0_), f1(f1_), f2(f2_), f3(f3_), b0(b0_), b1(b1_), b2(b2_), b3(b3_), start_angle(start_angle_) {}
};

struct Layout {
	struct Orientation orientation;
	struct vector3 origin;
	float          innerRadis;
	float          outerRadis;
	Layout(struct Orientation orientation_, struct vector3 origin_, float innerRadis_, float outerRadis_) : orientation(orientation_), origin(origin_), innerRadis(innerRadis_), outerRadis(outerRadis_) {}
};

struct HexMap {
	struct Layout layout;
	struct Hex ***nequad; // 一
	struct Hex ***sequad; // 二
	struct Hex ***swquad; // 三
	struct Hex ***nwquad; // 四
	
	HexMap(struct Layout l_) : layout(l_) { InternalInit(); }

	void InternalInit();
};

// Forward declarations


inline CubeCoord cubecoord_add(CubeCoord a, CubeCoord b) {
	return CubeCoord(a.q + b.q, a.r + b.r, a.s + b.s);
}


inline CubeCoord cubecoord_subtract(CubeCoord a, CubeCoord b) {
	return CubeCoord(a.q - b.q, a.r - b.r, a.s - b.s);
}


inline CubeCoord cubecoord_scale(CubeCoord a, int k) {
	return CubeCoord(a.q * k, a.r * k, a.s * k);
}

CubeCoord cubecoord_direction(int direction);
CubeCoord cubecoord_neighbor(CubeCoord hex, int direction);
CubeCoord cubecoord_diagonal_neighbor(CubeCoord hex, int direction);
int cubecoord_length(CubeCoord hex); 
int cubecoord_distance(CubeCoord a, CubeCoord b);

static inline CubeCoord hex_round(FractionalCubeCoord h) {
	int q = int(round(h.q));
	int r = int(round(h.r));
	int s = int(round(h.s));
	double q_diff = abs(q - h.q);
	double r_diff = abs(r - h.r);
	double s_diff = abs(s - h.s);
	if (q_diff > r_diff && q_diff > s_diff) {
		q = -r - s;
	} else
		if (r_diff > s_diff) {
			r = -q - s;
		} else {
			s = -q - r;
		}
	return CubeCoord(q, r, s);
}

static inline FractionalCubeCoord hex_lerp(FractionalCubeCoord a, FractionalCubeCoord b, double t) {
	return FractionalCubeCoord(a.q * (1 - t) + b.q * t, a.r * (1 - t) + b.r * t, a.s * (1 - t) + b.s * t);
}


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

const int EVEN = 1;
const int ODD = -1;
static inline OffsetCoord qoffset_from_cube(int offset, CubeCoord h) {
	int col = h.q;
	int row = h.r + int((h.q + offset * (h.q & 1)) / 2);
	return OffsetCoord(col, row);
}

static inline CubeCoord qoffset_to_cube(int offset, OffsetCoord h) {
	int q = h.col;
	int r = h.row - int((h.col + offset * (h.col & 1)) / 2);
	int s = -q - r;
	return CubeCoord(q, r, s);
}

static inline OffsetCoord roffset_from_cube(int offset, CubeCoord h) {
	int col = h.q + int((h.r + offset * (h.r & 1)) / 2);
	int row = h.r;
	return OffsetCoord(col, row);
}


static inline CubeCoord roffset_to_cube(int offset, OffsetCoord h) {
	int q = h.col - int((h.row + offset * (h.row & 1)) / 2);
	int r = h.row;
	int s = -q - r;
	return CubeCoord(q, r, s);
}

static inline CubeCoord axial_to_cube(AxialCoord h) {
	return CubeCoord(h.q, h.r, -h.q - h.r);
}

static inline AxialCoord cube_to_axial(CubeCoord h) {
	return AxialCoord(h.q, h.s);
}

struct HexMap * hexmap_create(MapOrientation o, float oradis);
void            hexmap_release(struct HexMap *self);

//struct Hex *    hexmap_create_hex(struct HexMap *self);
//struct Hex *    hexmap_release_hex(struct HexMap *self, struct Hex *h);

struct Hex ***  hexmap_find_quad(struct HexMap *self, struct AxialCoord coord);

struct Hex *    hexmap_find_hex(struct HexMap *self, struct CubeCoord coord);

struct vector3  hexmap_to_position(struct HexMap *self, struct AxialCoord coord);

FractionalCubeCoord hexmap_to_cubcoord(struct HexMap *self, struct vector3 p);

#endif