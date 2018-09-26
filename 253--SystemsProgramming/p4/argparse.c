#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "argparse.h"

#define MAX_ARG_LENGTH 2048

int parse_args(char* line, char** argv, int argc)
{

  int arg_index = 0;
  char* token = strtok(line, " ");
  while (token != NULL)
    {
      trim_args(token);
      int tokenlen = strlen(token) + 1;
      char* tokenmem = malloc(sizeof(char) * tokenlen);
      strncpy(tokenmem, token, tokenlen);
      argv[arg_index] = tokenmem;
      arg_index += 1;
      token = strtok(NULL, " ");
      if (arg_index >= MAX_ARG_LENGTH)
	{
	  token = NULL;
	}
    }
  
  return arg_index;
}

/**
 * Carbon copy of smash_trimtok
 *
 */
char* trim_args(char* token)
{
  int trimlen = strcspn(token, " \n");
  if (trimlen != strlen(token))
    {
      token[trimlen] = 0;
    }

  return token;
}


/**
 * Frees all memory malloc'd in argparse.
 *
 */
int free_args(int argc, char** argv)
{
  int i = 0;
  for (i = 0; i < argc; i++)
    {
      free(argv[i]);
      argv[i] = NULL;
    }

  return 0;
}

/**
 * Echoes out all arguments in a similar fashion to smash_echo.
 *
 */
int print_args(int argc, char** argv)
{

  int i = 0;
  for (i = 0; i < argc; i++)
    {
      printf("ECHO:[%d] %s\n", i, argv[i]);
    }
  return 0;
}
