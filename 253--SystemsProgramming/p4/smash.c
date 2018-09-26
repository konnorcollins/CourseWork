#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

#include "smash.h"
#include "history.h"
#include "argparse.h"

#define MAXLINE 4096
#define MAXHIST 256

int main(int argc, char* argv[]) {
  
  printf("$ "); // initial prompt

  int pid;

  char cmd_buf[MAXLINE] = {}; // buffer for inputted commands

  int argcount = 0;
  char* args[2048] = {};
  
  // retrieving commands from standard in until return is given
  while (fgets(cmd_buf, MAXLINE, stdin) != NULL) 
    {

      // MEM ALLOC & HISTORY
      // remove newln chars after logging command but BEFORE parsing
      add_history(cmd_buf);
      argcount = parse_args(cmd_buf, args, argcount);
      
      // INPUT HANDLING
      if (strcmp(args[0], "exit") == 0) // exit case
	{
	  free_args(argcount, args);
	  smash_clean();
	  return 0;
	}

      else if (strcmp(args[0], "history") == 0) // history case
	{
	  print_history();
	}
      
      else if (strcmp(args[0], "cd") == 0) // chdir case
	{
	  smash_chdir(args[1]);
	}
      
      else // execute vp OR echo case
	{
     
	  /*
	   * Attempt to run arg[0] as file program with argument array (execvp)
	   * if that fails (if execvp returns -1 [will not return non -1)
	   * echo out arguments as before (print_args)
	   */

	  pid = fork();
	  if (pid < 0)
	    {
	      printf("Forking failed.  Applying knife to main process...");
	      exit(-1);
	    }
	  else if (pid == 0) // child
	    {
	      if (execvp(args[0], args) == -1)
		{
		  print_args(argcount, args);
		}
	      exit(1); // successful completion of child process
	    }
	  else // parent process
	    {
	      // 1/4th chance of killing

	      if (rand() < RAND_MAX / 4)
		{
		  printf("PROCESS TERMINATED LATE\n");
		  kill(pid, SIGKILL);
		}
	      else
		{
		  int status = 0;
		  wait(&status);
		}
	    }

	}

      argcount = free_args(argcount, args);
      // prep next line of input
      printf("$ ");
    }
  
  return 0;
}


/**
 * A basic change directory function in smash.
 * If you provide a path to the function, it will attempt to nagivate to that path.
 * If that path does not exist, an error message will be displayed to the console.  A similar error will be thrown if the system call to chdir fails.
 * If a path argument is not given to the chdir command, an error will be printed to the console, and the function will terminate prematurely.
 */
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

/**
 * Responsible for freeing all remaining malloc'd memory before 'exit'ing the smash console.
 */
int smash_clean(void)
{
  clear_history();
  // free_args();

  return 0;
}
