#ifndef ARGPARSE_H
#define ARGPARSE_H

int parse_args(char* line, char** argv, int argc);
char* trim_args(char* token);
int free_args(int arc, char** argv);
int print_args(int argc, char** argv);

#endif
