# Interpreter Project(s)

## Overview
These Interpreters are designed to interpret codeblocks given by the user.  The grammar for each Interpreter is defined in the "Grammar" file in the root of each project.


## Structure

There are several main components to these Interpreters.

First, the Parser.  The Parser parses the source given by the user.  It then constructs a tree representing the structure of that source in accordance with the Grammar, located in the Grammar file.  If given source violates this grammer, the Interpreter exits prematurely.

Second, the individual nodes.  Each node has an evaluation function which evaluates the expression assigned to that particular node.  Each node subclass evaluates differently, depending on the grammar.  Once the tree of nodes is constructed, it is evaluated.
Note: You can usually infer which Node is responsible for evaluating what part of the Grammer by it's file/class name.  If there are multliple rules for a given block, they will usually be separated into individual subclasses.

Finally, the Scanner, which is responsible for keeping track of which character combinations represent operators and keywords.


