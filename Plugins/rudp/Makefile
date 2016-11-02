CC ?= gcc

SRC := stdafx.c rudp.c

CFLAGS := -g -o2 -Wall -std=c99

../crypt.so: $(SRC)
	$(CC) $(CFLAGS) -I./ --shared -o $@ $^
	
clean:
	rm -rf ../crypt.so
	

	
	