#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include "smash.h"
#include "history.h"

#define MAXLINE 4096
#define MAXHIST 256
#define EXIT_COMMAND "exit"
#define HISTORY_COMMAND "history"
#define DIR_COMMAND "cd"

int main(void) {
  
  printf("$ "); // initial prompt

  char commands[MAXLINE]; // buffer for inputted commands

  // retrieving commands from standard in until return is given
  while (fgets(commands, MAXLINE, stdin) != NULL) 
    {

      // MEM ALLOC & HISTORY
      add_history(commands);
      
      // INPUT HANDLING
      char* token = strtok(commands, " "); // get first token & determine commands
      smash_trimtok(token); // trim first token of unnecessary chars
      
      if (strcmp(token, EXIT_COMMAND) == 0) // exit case
	{
	  clear_history();
	  return 0;
	}

      else if (strcmp(token, HISTORY_COMMAND) == 0) // history case
	{
	  print_history();
	}
      
      else if (strcmp(token, DIR_COMMAND) == 0) // chdir case
	{
	  char* path = strtok(NULL, "\n");
	  smash_chdir(path);
	}
      
      else // echo case
	{
	  int counter = 0; // echo index 
	  while (token != NULL) 
	    {
	      smash_echo(token, counter);
	      counter += 1;
	      token = strtok(NULL, " ");
	    }
	}

      
      // prep next line of input
      printf("$ ");
    }
  
  return 0;
}

int smash_echo(char* token, int index)
{
  smash_trimtok(token);
  printf("[%d] %s\n", index, token);
  return 0;
}

int smash_trimtok(char* token)
{
  int trimlen = strcspn(token, " \n"); // check for useless chars in first token
  if (trimlen != strlen(token))
    {
      token[trimlen] = 0; // adjust null character if necessary
    }

  return 0;
}

int smash_chdir(char* path)
{

  if (path == NULL)
    {
      printf("error: no parameters given!\n");
      return -1;
    }
   
  int result = chdir(path);

  if (result != 0) // path doesn't exist
    {
      printf("error: %s does not exist!\n", path);
      return result;
    }

  char* buf = getcwd(NULL, MAXLINE);
  if (strcmp(buf, path) != 0) // chdir failure
    {
      printf("error: failed to nagivate to %s\n", path);
      free(buf);
      return -1;
    }

  printf("%s\n", path); // maybe change this?

  free(buf);
  return 0;
}
