CC = gcc

SRC := lsha1.c lua-crypt.c

CFLAGS := -Shared

../crypt.so: $(SRC)
	$(CC) $(CFLAGS) -o $@
	
clean:
	rm -rf ../crypt.so
	

	
	