import java.util.HashMap;

// This class provides a stubbed-out environment.
// You are expected to implement the methods.
// Accessing an undefined variable should throw an exception.

// Hint!
// Use the Java API to implement your Environment.
// Browse:
//   https://docs.oracle.com/javase/tutorial/tutorialLearningPaths.html
// Read about Collections.
// Focus on the Map interface and HashMap implementation.
// Also:
//   https://www.tutorialspoint.com/java/java_map_interface.htm
//   http://www.javatpoint.com/java-map
// and elsewhere.

/**
 * Simulates an Environment wherein an assigned variable remains accessible for future statements in the same program.
 * @author CS 354
 */
public class Environment {

	/**
	 * Stores the defined variables in the given environment.
	 */
	private HashMap<String, Double> environmentVariables = new HashMap<String, Double>();

	/**
	 * Stores the assigned variable and its value.  Future statements may access these stored variables.
	 * @param var (the name of the variable)
	 * @param val (the value of the variable)
	 * @return
	 */
	public double put(String var, double val) {
		environmentVariables.put(var, val);
		return val;
	}

	
	/**
	 * Retrieves a variable from the environment with the given name.
	 * @param pos
	 * @param var (the name of the defined variable)
	 * @return val (the value of the defined variable)
	 * @throws EvalException (if the variable does not exist)
	 */
	public double get(int pos, String var) throws EvalException {
		Double val = environmentVariables.get(var);
		
		if (val == null) {
			throw new EvalException(pos, "Tried to access undefined variable '" + var + "'!  Aborting...");
		}
		
		return val;
	}

}
