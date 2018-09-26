#ifndef HISTORY_S
#define HISTORY_S

struct cmd {
  void* pid;
  char* cmd;
};

#endif

#ifndef HISTORY_F
#define HISTORY_F

int init_history(struct cmd* hist);
int add_history(char commandString[]);
int clear_history();
int print_history();

#endif

