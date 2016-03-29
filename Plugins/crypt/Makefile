CC ?= gcc

SRC := lsha1.c lua-crypt.c

CFLAGS := -g -o2 -Wall

../crypt.so: $(SRC)
	$(CC) $(CFLAGS) --shared -o $@ $^
	
clean:
	rm -rf ../crypt.so
	

	
	