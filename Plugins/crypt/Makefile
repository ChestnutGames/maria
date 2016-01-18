
CC = gcc

objects = lsha1.o lua-crypt.o

SHARD_SO := crypt.so

CFlag := -Shared

SHARD_SO : $(objects)
	$(CC) -Shared -o $@
	
lsha1.o : lsha1.c
	$(CC) -c -o $@

lua-crypt.o : lua-crypt.c
	$(CC)) -c -o $@