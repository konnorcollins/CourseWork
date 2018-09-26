CC = gcc
CFLAGS =-Wall -g

all: smash.o
	$(CC) $(CFLAGS) $< -o smash

%.o: %.c
	$(CC) $(CFLAGS) -c $<

smash: smash.o
	$(CC) $(CFLAGS) $< -o $@
	./smash

clean:
	rm -f ./smash.o
	rm -f ./smash
