PROJECT 2: Basic Shell - Part 1
Author: Konnor Collins
Class: CS 253
-----------------------------------

Overview:
	This is the smash shell. It is capable of echoing commands you would typically use in a normal linux terminal, but true console functionality has been added, including a change directory command.

Compiling and Using:
	 To compile and execute the program, execute the following command from the console in the top-most level directory containing the source files:
	    'make smash'
	 If no errors occur, you will automatically launch the smash shell. To exit the shell at any time, simply use the following command:
	    'exit'
	 If you wish to enter the shell again after having already compiled, simply just use the following command in the same directory as the "smash" executable:
	    './smash'
	 To clean the source directory after use of the smash shell, use the 'make clean' command.  Note that you will need to compile the program again to use the smash shell.


Testing:
	 Blind testing was simple but annoying.  Before the exit command was correctly implemented, I had to recompile the smash shell.  The reason for this was a previously badly implemented Makefile and the need to prematurely terminate the smash shell from console.  This has since been rectified.
	 There were some issues with the grader script and user input being fundamentally different.  You can test with the grader script if you wish to see this for yourself.  The script should be included with the source files under the file 'backpack.sh'.  These differences are outlined in 'rubric.txt', which is also included with the source files.  Feel free to conduct your own tests on the smash shell at your discretion.

Sources Used:
	N/A