CC = gcc
CFLAGS =-Wall -g
VFLAGS =--leak-check=yes
OBJS = smash.o history.o argparse.o
EXEN = smash

all: $(OBJS)
	$(CC) $(CFLAGS) $(OBJS) -o $(EXEN)

%.o: %.c
	$(CC) $(CFLAGS) -c $<

exe: $(OBJS)
	$(CC) $(CFLAGS) $(OBJS) -o $(EXEN)

smash: exe
	./$(EXEN)

valgrind: exe
	valgrind $(VFLAGS) ./$(EXEN)

clean:
	rm -f $(OBJS)
	rm -f ./$(EXEN)
