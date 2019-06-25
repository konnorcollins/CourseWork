# How to run

You can either use the CSV-Converter.exe, or you can run the Java jar file by entering:

'java -jar CSVConverter-all-1.0-SNAPSHOT.jar [file]'

If either the exe is run, or the jar is executed without a command line argument, the GUI
version will start. If you run the jar with a command line argument, it will automatically
convert that .csv file into .json files. 

# How to use

Simply pick the desired .csv file in the pop up and the program will create seperate .json files
in the same folder that the .csv file is located. 

# IMPORTANT 

The .csv file has a simple, yet important format that needs to be kept for the program to work. 
There is an example (example.csv), but basically the left most column is the key column, DO NOT
CHANGE! This is only important for the json files! Every column after that is a separate language
that the game can support. Each of these columns has a title, for example 'en' for English. The title
doesn't really matter, but having one is important. 

To add another language, simply append another column to the right of the rightmost column, and translate
the phrase to the left into the desired language.