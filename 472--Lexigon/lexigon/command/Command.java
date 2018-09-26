package lexigon.command;

/*
 * PATTERNS
 * Command -> Abstract Command
 */
/**
 * Class for encapsulating certain actions for readily available use.
 * @author Konnor Collins
 *
 */
public abstract class Command {
	
	/**
	 * Executes this Command's action(s).
	 */
	public abstract void execute();
	
	/**
	 * Reverts this Command's action(s).
	 */
	public abstract void unexecute();
	
	//public abstract Command clone();

}
