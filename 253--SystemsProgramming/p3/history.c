#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include "history.h"

#define MAXHIST 4096

int cmdindex = 0;
struct cmd* history[MAXHIST];

int init_history(struct cmd* hist)
{
  hist->pid = NULL;
  hist->cmd = NULL;
  return 0;
}


int add_history(char commandString[])
{
  if (cmdindex < MAXHIST)
    {
      // create new empty struct
      struct cmd* commandmem = malloc(sizeof(struct cmd));
      init_history(commandmem);

      // prepare string for insertion
      int cmdlen = strlen(commandString) + 1;
      char* commandStrMem = malloc(cmdlen * sizeof(char));
      strncpy(commandStrMem, commandString, cmdlen);

      // insert history entry
      commandmem->cmd = commandStrMem;
      history[cmdindex] = commandmem;
      cmdindex++;
      
      return 0;
    }
  else
    {
      printf("Error: Maximum history size allocated. Please find a better use of your time.");
      return -1;
    }
}

int clear_history()
{
  int i = 0;
  for (i = 0; i < cmdindex; i++)
    {
      free(history[i]->cmd);
      free(history[i]);
    }
  cmdindex = 0;
  return 0;
}

int print_history()
{
  int i = 0;
  for (i = 0; i < cmdindex; i++)
    {
      printf("%d %s", i, history[i]->cmd);
    }
  return 0;
}
