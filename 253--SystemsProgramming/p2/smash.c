#include <stdio.h>
#include <string.h>
#include <unistd.h>

#define MAXLINE 4096
#define EXIT_COMMAND "exit"
#define DIR_COMMAND "cd"

int main(void) {

  
  printf("$ "); // initial prompt

  char commands[MAXLINE]; // buffer for inputted commands

  // retrieving commands from standard in until return is given
  while (fgets(commands, MAXLINE, stdin) != NULL) 
    {


      char* token = strtok(commands, " "); // get first token & determine commands
      int trimlen = strcspn(token, " \n"); // check for useless chars
      if (trimlen != strlen(token))
	{
	  token[trimlen] = 0; // adjust null character if necessary
	}

      if (strcmp(token, EXIT_COMMAND) == 0)
	{
	  // exit case
	  // printf("exiting...\n");
	  return 0;
	}
      
      else if (strcmp(token, DIR_COMMAND) == 0)
	{
	  // chdir case
	  char* path = strtok(NULL, "\n");
	  int chdirresult = chdir(path);

	  if (chdirresult != 0) // path does not exist
	    {
	      printf("error: %s does not exist.\n", path);
	    }
	  else // path does exist
	    {
	      char* buf = getcwd(NULL, MAXLINE);
	      if (strcmp(buf, path) != 0)
		{ // unsuccessful chdir
		  printf("error: failed to navigate to %s\n", path);
		}
	      else
		{ // successful chdir
		  printf("%s\n", buf);
		}
	    }
	}
      
      else
	{
	  // echo case
	  int counter = 0; // echo index 
	  while (token != NULL) 
	    {
	      printf("[%d] %s", counter,  token);
	      counter += 1;
	      token = strtok(NULL, " ");
	      if (token != NULL)
		{
		  printf("\n");
		}
	    }
	}

      
      // prep next line of input
      printf("$ ");
    }
  
  
  return 0;
}
