/**
* A Priority Queue that uses a Max-Heap to function.
* @author Konnor Collins
* @since 9/20/2017
* @param T (any generic object type)
*/
public class PQueue<T> {

	private MaxHeap<T> m_maxHeap;
	
	/**
	 * Creates an empty Priority Queue with a default capacity of 50 object/key pairs.
	 * <br />
	 * The capacity of the Queue will be set to twice the amount of pairs given.
	 * <br />
	 * Note that the capacity of the Queue will automatically expand when it is exceeded.
	 */
	public PQueue()
	{
		m_maxHeap = new MaxHeap<T>();
	}
	
	/**
	 * Creates a Priority Queue with the given object/key pairs.
	 * <br />
	 * The capacity of the Queue will be set to twice the amount of pairs given.
	 * <br />
	 * Note that the capacity of the Queue will automatically expand when it is exceeded.
	 * @param objects (an array of objects of generic type T)
	 * @param keys (an array of keys of type (int))
	 */
	public PQueue(T[] objects, int[] keys)
	{
		m_maxHeap = new MaxHeap<T>(objects, keys);
	}
	
	/**
	 * Returns the object with the highest priority in the Queue.
	 * @return the generic object (T) with the highest priority
	 */
	public T maximum()
	{
		return m_maxHeap.heapMax;
	}
	
	/**
	 * Returns the object with the highest priority in the Queue.  The object will also be removed from the Queue.
	 * @return
	 */
	public T extractMax()
	{
		return m_maxHeap.extractHeapMax();
	}
	
	public void increaseKey(int index, int value)
	{
		m_maxHeap.increaseHeapKey(index, value);
	}
	
	/**
	 * Inserts an object with a given priority into the Priority Queue.
	 * @param object (the object to be inserted into the Queue)
	 * @param key (the object's priority in the Queue)
	 */
	public void insert(T object, int key)
	{
		m_maxHeap.insert(object, key);
	}
	
	/**
	 * Determines whether or not the Priority Queue is empty.
	 * @return if the priority queue is empty (true), otherwise (false)
	 */
	public boolean isEmpty()
	{
		return m_maxHeap.isEmpty();
	}
	
	/**
	 * Returns the current size of the Priority Queue.
	 * @return the size of the priority queue (int)
	 */
	public int size()
	{
		return m_maxHeap.getHeapSize();
	}
}
