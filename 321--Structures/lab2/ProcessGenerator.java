import java.util.Random;

/**
 * A utility class that generates Process objects with semi-random values.
 * @author Konnor Collins
 * @since 9/11/2017
 */
public class ProcessGenerator {

	/**
	* The probability that Queue() will return (true)
	*/
	private double m_probability;
	
	/**
	* Reference to the java random generator.
	*/
	private Random random;
	
	/**
	 * Creates a new instance of ProcessGenerator with the given probability.
	 * @param probability
	 */
	public ProcessGenerator(double probability)
	{
		m_probability = probability;
		random = new Random();
	}
	
	/**
	 * Creates a new instance of ProcessGenerator with the given probability and seed.
	 * @param probability
	 * @param seed
	 */
	public ProcessGenerator(double probability, long seed)
	{
		m_probability = probability;
		random = new Random(seed);
	}
	
	/**
	 * Returns a random boolean value.
	 * @return (true) or (false) randomly
	 */
	public boolean query()
	{
		return random.nextDouble() < m_probability;
	}
	
	/**
	 * Returns a new Process with the given parameters.
	 * <br />
	 * arrivalTime: The time at which the process was created.
	 * <br />
	 * maxProcessTime: The maximum time the process should take to complete.
	 * <br />
	 * maxLevel: The maximum priority level the process should have.
	 * <br />
	 * The time it takes to finish the process as well as the process priority level are determined randomly.  These values will not exceed the given maximums.
	 * @param arrivalTime (int)
	 * @param maxProcessTime (int)
	 * @param maxLevel (int)
	 * @return process object (Process)
	 */
	public Process getNewProcess(int arrivalTime, int maxProcessTime, int maxLevel)
	{
		int processTime = 1 + random.nextInt(maxProcessTime);
		int priorityLevel = 1 + random.nextInt(maxLevel);
		return new Process(arrivalTime, processTime, priorityLevel, maxLevel);
	}
	
	/**
	 * Sets the probability that Query() will return (true).
	 * <br />
	 * This value should be set between 0.0d (inclusive) and 1.0d (exclusive).  The higher the value, the more likely Query() will return (true).
	 * @param probability (double)
	 */
	private void setProbability(double probability)
	{
		m_probability = probability;
	}
	
	
}
