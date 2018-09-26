/**
* A representation of a Process on a CPU.
* @author Konnor Collins
* @since 2/20/2017
*/
public class Process {

	/**
	* The amount of priority this Process has relative to other Process' on the CPU.
	*/
	private int m_priorityLevel;
	
	/**
	* The amount of time it will take for this Process to finish.
	*/
	private int m_timeToFinish;
	
	/**
	* The amount of time this Process has been idle on the CPU.
	*/
	private int m_timeNotProcessed;
	
	/**
	* The time the process was created.
	*/
	private int m_arrivalTime;
	
	/**
	* The maximum amount of Priority this process can have.
	*/
	private int m_maxPriorityLevel;

	/**
	 * Creates a process with the given currentTime, maximum process time, and
	 * max priority level.
	 * 
	 * @param currentTime
	 *            (the current time this process was created)
	 * @param maxProcessTime
	 * @param maxLevel
	 */
	public Process(int arrivalTime, int timeToFinish, int priorityLevel, int maxPriorityLevel) {
		m_arrivalTime = arrivalTime;
		m_timeToFinish = timeToFinish;
		m_priorityLevel = priorityLevel;
		m_maxPriorityLevel = maxPriorityLevel;

		m_timeNotProcessed = 0;

	}

	/**
	 * Decrements the timeToFinish counter
	 */
	public void reduceTimeRemaining() {
		m_timeToFinish--;
	}

	/**
	 * Increments the timeNotProcessed counter (idle timer).
	 */
	public void incrementTimeNotProcessed() {
		m_timeNotProcessed++;
	}

	/**
	 * Returns the current amount of time the process has been idle.
	 * 
	 * @return m_timeNotProcessed (int)
	 */
	public int getTimeNotProcessed() {
		return m_timeNotProcessed;
	}

	/**
	 * Returns the time remaining for the process to finish.
	 * 
	 * @return m_timeToFinish (int)
	 */
	public int getTimeRemaining() {
		return m_timeToFinish;
	}

	/**
	 * Checks to see if the program has finished running.
	 * 
	 * @return (true) if the program has finished, (false) otherwise.
	 */
	public boolean done() {
		return (m_timeToFinish <= 0);
	}

	/**
	 * Returns the time the process was created.
	 * 
	 * @return m_arrivalTime (int)
	 */
	public int getArrivalTime() {
		return m_arrivalTime;
	}

	/**
	 * Returns the priority level of this process.
	 * 
	 * @return m_priorityLevel (int)
	 */
	public int getPriority() {
		return m_priorityLevel;
	}

	/**
	 * Increments the process' priority level.
	 */
	public void incrementPriority() {
		if (m_priorityLevel < m_maxPriorityLevel)
			m_priorityLevel++;
	}

	/**
	 * Sets the Time Not Processed counter back to 0.
	 */
	public void resetTimeNotProcessed() {
		m_timeNotProcessed = 0;
	}

	/**
	 * Sets the priority level of this process to the given level.
	 * 
	 * @param priority level (int)
	 */
	private void setPriorityLevel(int level) {
		if (level > m_maxPriorityLevel) {
			m_priorityLevel = m_maxPriorityLevel;
		} else
			m_priorityLevel = level;
	}

	/**
	 * Returns the current amount of time for this process to finish.
	 * 
	 * @return remaining time (int)
	 */
	public int getTimeToFinish() {
		return m_timeToFinish;
	}

	/**
	 * Sets the Time to Finish counter to the given time.
	 * 
	 * @param remaining time (int)
	 */
	private void setTimeToFinish(int time) {
		m_timeToFinish = time;
	}

	/**
	 * Sets the Time Not Processed counter to the given amount.
	 * 
	 * @param idle time (int)
	 */
	private void setTimeNotProcessed(int time) {
		m_timeNotProcessed = time;
	}

	/**
	 * Sets the Arrival Time counter to the given time.
	 * 
	 * @param arrival time (int)
	 */
	private void setArrivalTime(int time) {
		m_arrivalTime = time;
	}

	/**
	 * Returns the maximum priority level for the process.
	 * 
	 * @return m_maxPriorityLevel (int)
	 */
	public int getMaxPriorityLevel() {
		return m_maxPriorityLevel;
	}

	/**
	 * Sets the maximum priority level for this process to the given amount.
	 * @param maxPriorityLevel (int)
	 */
	public void setMaxPriorityLeveL(int maxPriorityLevel) {
		m_maxPriorityLevel = maxPriorityLevel;
	}
}
