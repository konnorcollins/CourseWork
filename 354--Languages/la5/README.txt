Konnor Collins
CS 354
12/5/2017


I used the resources @ http://zetcode.com/lang/csharp/csharp/ as well as some documentation for the Convert class https://msdn.microsoft.com/en-us/library/system.convert(v=vs.110).aspx

Porting the LightSwitch application was fairly uneventful.  I will list most of the hurdles I encountered below:

1. Forgetting to specify a type for constructor arguments.  An easy fix, albiet embaressing.
2. As I was using Notepad++ for editing, there were several instances of non-existant symbols.  Mainly caused by typos.
3. Correctly compiling the executable was slightly difficult until I took the time to create a batch file for my local machine. 

Surprisingly, there are not that many noticable differences between Java and C# when creating a simple GUI like this.
The most noticible difference I would 'point' out is being able to pass a method directly to an EventHandler object in C#, whereas in Java you would normally have to encapsulate or override a previously defined method.

Also, this is just some bias on my part, but I am not the biggest fan of the conventions for naming methods in C#. Capitalizing everything does not sit right with me.