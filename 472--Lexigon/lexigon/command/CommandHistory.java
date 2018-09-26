package lexigon.command;

import java.util.Stack;

/* PATTERNS
 * Singleton -> Singleton
 * Command -> Manager
 */
/**
 * A handler for storing Commands for un-execution and re-execution.
 * 
 * @author Konnor Collins
 *
 */
public class CommandHistory {

	private static CommandHistory _history;

	private Stack<Command> _undo;
	private Stack<Command> _redo;

	private CommandHistory() {
		_undo = new Stack<Command>();
		_redo = new Stack<Command>();
	}

	/**
	 * Returns the single CommandHistory instance.
	 * 
	 * @return
	 */
	public static CommandHistory getHistory() {
		if (_history == null)
			_history = new CommandHistory();
		return _history;
	}

	/**
	 * Executes the given command and stores it for later 'undo'ing by the
	 * user.<br />
	 * Clears any Commands currently stored for 'redo'ing.
	 * 
	 * @param c
	 *            (Command)
	 */
	public void execute(Command c) {
		if (c == null) {
			return;
		}

		c.execute();
		_undo.push(c);
		_redo.clear(); // overwriting old undone commands
	}

	/**
	 * Unexecutes the last executed command in history.
	 */
	public void undo() {
		if (!_undo.empty()) {
			Command c = _undo.pop();
			c.unexecute();
			_redo.push(c);
		}
	}

	/**
	 * Executes the last unexecuted command in history.
	 */
	public void redo() {
		if (!_redo.empty()) {
			Command c = _redo.pop();
			c.execute();
			_undo.push(c);
		}
	}

}
