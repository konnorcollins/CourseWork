CC = gcc
CFLAGS =-Wall -g
OBJS = smash.o history.o
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
	valgrind --leak-check=yes ./$(EXEN)

clean:
	rm -f $(OBJS)
	rm -f ./$(EXEN)
