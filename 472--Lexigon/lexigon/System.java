package lexigon;

/**
 * Contains environment variables used by Lexigon for debugging purposes.<br />
 * @author Konnor Collins
 *
 */
public class System {

	/**
	 * While true, all environment variables will be accessed from their respective variables in THIS System implementation.<br />
	 * While false, all environment variables will be checked through the JDK java.lang.System implementation.
	 */
	private static final boolean DEBUG_MODE = true;
	private static final String LEXI_WINDOW_TYPE = "Awt";
	private static final String LEXI_LOOK_AND_FEEL_TYPE = "Green";
	

	/**
	 * Returns the current state of the given variable.
	 * @param sysvar (variable name)
	 * @return state of given variable, "null" if no such variable exists
	 */
	public static String getenv(String sysvar) {
		
		if (!DEBUG_MODE) {
			return java.lang.System.getenv(sysvar);
		}
		if (sysvar.equalsIgnoreCase("LexiWindow")) {
			return LEXI_WINDOW_TYPE;
		}
		
		if (sysvar.equalsIgnoreCase("LexiLookAndFeel")) {
			return LEXI_LOOK_AND_FEEL_TYPE;
		}
		
		return "null";
	}
}
