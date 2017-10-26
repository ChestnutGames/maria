// play_test.cpp : Defines the entry point for the console application.
//

#include "sharpc\sharpc.h"
#include "hexmap\hexmapaux.h"

#include <stdarg.h>
#include <stdio.h>

#include <iostream>
#include <string>

static void log(char *fmt, ...) {
	va_list ap;
	va_start(ap, fmt);
	char buffer[256] = { 0 };

	/*snprintf(buffer, 256, fmt, ##__);*/
	va_end(ap);
	printf(buffer);
}

static int sharpc_cb(int argc, struct CSObject *argv) {
	return 1;
}

static void hexmap_cb(struct Hex *h) {
	if (h->main.q == -20 && h->main.s == 20) {
		printf("%f%f%f", h->pos.x, h->pos.y, h->pos.z);
	}
}


int main(int argc, char* argv[]) {
	int a = 1 + 3;
	printf("%d", a);
	// test hexmap
	struct HexMap *map = hexmap_create(FLAT, 2, HEX, 65, 65);
	hexmap_foreach(map, hexmap_cb);
	int cnt = hexmap_hex_count(map);
	struct vector3 position = { 1.7f, 0.f, -6.3f };
	struct Hex *h = hexmap_find_hex_by_position(map, position);

	char *out;
	uint32_t size = 0;
	hexmap_save_to_plist(map, &out, &size, "map");
	package_t *content = package_alloc(out, size);
	free(out);
	size = package_size(content);

	FILE *f = fopen("map.plist", "wb");
	fwrite((const void *)content->src, 1, content->size, f);
	fflush(f);
	fclose(f);

	struct HexMap *nmap = hexmap_create_from_plist(content->src, content->size);
	struct Hex *nh = hexmap_find_hex_by_position(nmap, position);
	assert(nh->main.q == h->main.q &&
		nh->main.r == h->main.r &&
		nh->main.s == h->main.s);

	char inbuffer[128] = { 0 };
	int oofs = WriteString(inbuffer, 0, "hello", 5, 128);

	char outbuffer[128] = { 0 };
	memcpy(outbuffer, "hello", 5);
	size_t sz = 128;
	int nofs = ReadString(inbuffer, 0, outbuffer, &sz, 128);

	// del
	int oldc = hexmap_hex_count(nmap);
	position = { -162.7f, 0.0f, 57.9f };
	h = hexmap_find_hex_by_position(nmap, position);
	hexmap_remove_hex(nmap, h);
	int newc = hexmap_hex_count(nmap);

	// test package
	package_t *package = package_alloci(32);
	char *buffer = package_buffer(package);
	int ofs = 0;
	ofs = WriteInt32(buffer, ofs, h->main.q, 32);
	ofs = WriteInt32(buffer, ofs, h->main.r, 32);
	ofs = WriteInt32(buffer, ofs, h->main.s, 32);

	ofs = 0;
	int q, r, s;
	ofs = ReadInt32(buffer, ofs, &q, 32);
	ofs = ReadInt32(buffer, ofs, &r, 32);
	ofs = ReadInt32(buffer, ofs, &s, 32);


	struct CSObject cb;
	cb.type = C_INTPTR;
	cb.v32 = 1;
	struct sharpc *sc = sharpc_create(sharpc_cb);
	struct hexmapaux * aux = hexmapaux_create(sc, FLAT, 1, HEX, 20, 20, cb);

	return 0;
}

