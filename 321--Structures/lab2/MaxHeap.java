
/**
 * A basic Max Heap implementation.
 * 
 * @author Konnor Collins
 * @since 9/11/2017
 * @param <T>
 *            (any generic object type)
 */
public class MaxHeap<T> {

	/**
	 * The array of heap nodes.
	 */
	private HeapNode<T>[] m_heap;
	
	/**
	 * The current size of the heap.
	 */
	private int m_heapSize;
	
	/**
	 * The maximum number of nodes the heap can store.
	 */
	private int m_capacity;
	
	/**
	 * The default capacity of the heap if none is specified.
	 */
	private final int DEFAULT_CAPACITY = 50;

	/**
	 * Creates a max-heap with the default capacity and no nodes.
	 */
	public MaxHeap() {
		m_heap = new HeapNode[DEFAULT_CAPACITY];
		m_capacity = DEFAULT_CAPACITY;
		m_heapSize = 0;
	}

	/**
	 * Creates a max-heap with the given object/key pairs, and a capacity double the amount of object/key pairs.
	 * @param objects (T[])
	 * @param keys (int[])
	 */
	public MaxHeap(T[] objects, int[] keys) {
		int size = objects.length > DEFAULT_CAPACITY ? objects.length * 2 : DEFAULT_CAPACITY;
		m_heap = new HeapNode[size];

		// note that if the provided objects & keys are different in length,
		// excess keys/objects will be dropped
		for (int i = 0; i < objects.length && i < keys.length; i++) {
			m_heap[i] = new HeapNode<T>(objects[i], keys[i]);
			m_heapSize = i;
		}

		m_capacity = size;
		
	}

	/**
	* The current object with the highest key in the heap.
	*/
	public T heapMax;

	/**
	* Returns the object with the highest key in the heap, which is then removed from the heap.
	* <br />
	* This will max-heapify the heap.
	*/
	public T extractHeapMax() {
		if (m_heapSize <= 0) {
			return null;
		}
		
		maxHeapify(0);
		heapMax = m_heap[0].getObject();
		
		T value = heapMax;
		
		// swap root and last leaf
		exchange(0, m_heapSize - 1);
		
		// delete last leaf
		m_heap[m_heapSize - 1] = null;
		m_heapSize -= 1;
		
		if (m_heapSize > 0) {
			heapMax = m_heap[0].getObject();
		} else {
			heapMax = null;
		}
		
		// max-heapify tree from the root
		maxHeapify(0);
		
		return value;
	}

	/**
	 * Increases the key of the node of the given index by the given amount.
	 * @param index (int)
	 * @param amount (int)
	 */
	public void increaseHeapKey(int index, int amount) {
		// increment the node's key
		if (index < m_heapSize && index >= 0) {
			int currentKey = m_heap[index].getKey();
			m_heap[index].setKey(currentKey + amount);
		}
		
		// re-heapify the node's parents until we max-heapify the root.
		while (index > 0)
		{
			index /= 2;
			maxHeapify(index);
		}
	}

	/**
	 * Inserts the given object with the given key at the end of the max-heap.
	 * <br />
	 * The max-heap will max-heapified.
	 * @param object (any object of generic type T)
	 * @param key (a key or priority value for the object)
	 */
	public void insert(T object, int key) {
		if (heapMax == null) {
			heapMax = object;
		}
		// determine target index, increase heap capacity if necessary
		int target = m_heapSize;
		if (target >= m_capacity - 1) {
			expandCapacity();
		}
		
		// insert the node into the heap
		m_heap[target] = new HeapNode<T>(object, key);
		
		// update size
		m_heapSize++;
		
		// max-heapify
		while (target > 0) {
			target /= 2;
			maxHeapify(target);
		}

		
	}
	
	/**
	 * Doubles the capacity of the heap in its current state.
	 */
	private void expandCapacity() {
		HeapNode<T>[] newheap = new HeapNode[m_capacity * 2];
		for (int i = 0; i < m_capacity; i++) {
			newheap[i] = m_heap[i];
		}

		m_heap = new HeapNode[m_capacity * 2];
		for (int i = 0; i < m_capacity; i++) {
			m_heap[i] = newheap[i];
		}
		
		m_capacity = m_capacity * 2;
		
	}

	/**
	 * Sorts the specified node and its children to select the maximum key
	 * value, and place it at the top of the max-heap (the top of the max-heap
	 * is denoted by the provided index).
	 * <br />
	 * If applicable, the children of the specified node will be Max-Heapified too.
	 * 
	 * @param key
	 */
	public void maxHeapify(int index) {
		int largest = index;
		int l = left(index + 1) - 1;
		int r = right(index + 1) - 1;

		// check the left child's key
		if (l < m_heapSize && m_heap[l] != null) {
			if (m_heap[l].getKey() > m_heap[largest].getKey()) {
				largest = l;
			}
		}

		// check the right child's key
		if (r < m_heapSize && m_heap[r] != null) {
			if (m_heap[r].getKey() > m_heap[largest].getKey()) {
				largest = r;
			}
		}

		// if a child was bigger, swap the two and recur on the child node's old index
		if (largest != index) {
			exchange(index, largest);
			maxHeapify(largest);
		}
	}

	/**
	 * Moves the node at the given index into it's parent node.
	 * 
	 * @param index of the node to be moved up (int)
	 */
	private void moveUp(int index) {
		// abort if top index
		if (index == 0 || index >= m_heapSize)
			return;

		// move the node up
		int p = parent(index);
		m_heap[p] = m_heap[index];
		
		// heapify the heap with the root equal to the provided index
		int l = left(index + 1) - 1;
		int r = right(index + 1) - 1;
		int largest = index;
		
		// check left child
		if (l < m_heapSize) {
			if (m_heap[1].getKey() > m_heap[largest].getKey()) {
				largest = l;
			}
		}
		
		// check right child
		if (r < m_heapSize) {
			if (m_heap[r].getKey() > m_heap[largest].getKey()) {
				largest = r;
			}
		}
		
		if (largest != index) { // move the largest child up
			moveUp(largest);
		} else { // if no child, set current index to null
			m_heap[index] = null;
			m_heapSize--;
		}
		
		
	}

	/**
	 * Returns the current size of the heap.
	 * @return the heap's size (int)
	 */
	public int getHeapSize() {
		return m_heapSize;
	}

	/** 
	 * Determines whether the heap is empty or not.
	 * @return (true) if the heap is empty, (false) otherwise.
	 */
	public boolean isEmpty() {
		return (m_heapSize == 0);
	}

	/**
	 * Exchanges the position of the given index's nodes.
	 * 
	 * @param indexA
	 *            (index of the first node)
	 * @param indexB
	 *            (index of the second node)
	 */
	private void exchange(int indexA, int indexB) {
		// swap the two nodes
		HeapNode<T> temp = m_heap[indexA];
		m_heap[indexA] = m_heap[indexB];
		m_heap[indexB] = temp;
	}

	/**
	 * Returns the theoretical index of the given index's left child.
	 * 
	 * @param the index of the node
	 * @return theoretical
	 *            index of the node's left child (int).
	 */
	private int left(int index) {
		return index * 2;
	}

	/**
	 * Returns the theoretical index of the given index's right child.
	 * 
	 * @param the index of the node
	 * @return theoretical
	 *            index of the node's right child (int).
	 */
	private int right(int index) {
		return index * 2 + 1;
	}

	/**
	 * Returns the theoretical index of the given index's parent.
	 * 
	 * @param the index of the node
	 * @return theoretical
	 *            index of the node's parent (int).
	 */
	private int parent(int index) {
		return index / 2;
	}

	/**
	 * Sets the size of the heap to the given amount.
	 * 
	 * @param size of the heap (int) 
	 */
	private void setHeapSize(int size) {
		m_heapSize = size;
	}

	/**
	 * Sets the capacity of the heap to the given amount.
	 * @param capacity of the heap (int)
	 */
	private void setCapacity(int capacity) {
		m_capacity = capacity;
	}

}
