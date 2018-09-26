/**
 * A cache implemented with a DLL for storing elements of a generic type.
 * 
 * @author Konnor Collins
 * @since 9/1/2017
 */
public class Cache<T> implements ICache<T> {

	/**
	 * The first element stored in the Cache. <br />
	 * You can visualize this as the leftmost or topmost element. <br />
	 * This will store the most recently accessed data element in the cache.
	 */
	private DLLNode<T> head;

	/**
	 * The last element stored in the Cache. <br />
	 * You can visualize this as the rightmost or lowest element <br />
	 * This will store the least recently accessed data element in the cache,
	 * Cache capacity permitting.
	 */
	private DLLNode<T> rear;

	/**
	 * The maximum capacity of the Cache. <br />
	 * Note that the cache can hold any number of elements between 0 and
	 * CAPACITY inclusively.
	 */
	final int CAPACITY;

	/**
	 * The current number of data elements stored in the Cache.
	 */
	private int elementsStored;

	/**
	 * Records the number of successful times data is accessed.
	 */
	private double hits;

	/**
	 * Records the total number of data access is attempted.
	 */
	private double access;

	/**
	 * Creates an empty Cache with a capacity of 100.
	 */
	public Cache() {
		CAPACITY = 100;
		elementsStored = 0;
		hits = 0.0;
		access = 0.0;
	}

	/**
	 * Creates an empty Cache with a given capacity.
	 * 
	 * @param capacity
	 */
	public Cache(int capacity) {
		CAPACITY = capacity;
		elementsStored = 0;
		hits = 0.0;
		access = 0.0;
	}

	@Override
	public T get(T target) {
		access++;
		// Is the data in the cache?
		/*
		 * loop through cache until data is found or reaching end of list
		 */
		if (elementsStored <= 0) {
			// System.out.println("DEBUG: Attempted to get target from empty
			// list via get(T). Aborting...");
			return null;
		}

		// Cycle through until we find the target element
		DLLNode<T> checkNode = head;
		while (!checkNode.getElement().equals(target) && checkNode.getNext() != null) {
			checkNode = checkNode.getNext();
		}

		if (checkNode.getElement().equals(target)) // FOUND
		{
			hits++;

			if (elementsStored == 0) { // only node in the list
				head = checkNode;
				rear = checkNode;
				checkNode.setPrevious(null);
				checkNode.setNext(null);
			} else if (checkNode == head) { // node is the head node
				// do nothing
				// goal is already accomplished
			} else if (checkNode == rear) { // node is the rear node
				// remove node from end of list
				rear = checkNode.getPrevious();
				rear.setNext(null);
				checkNode.setPrevious(null);
				
				// put node at front of list
				head.setPrevious(checkNode);
				checkNode.setNext(head);
				head = checkNode;
			} else { // node is in the middle of the list
				DLLNode<T> prevNode = checkNode.getPrevious();
				DLLNode<T> nextNode = checkNode.getNext();
				
				// join ends of node together
				prevNode.setNext(nextNode);
				nextNode.setPrevious(prevNode);
				
				// put node at front of list
				checkNode.setNext(head);
				head.setPrevious(checkNode);
				head = checkNode;
				checkNode.setPrevious(null);
			}
			
			return checkNode.getElement();
		}

		// NOT FOUND
		return null;
	}

	@Override
	public void clear() {
		head = null;
		rear = null;
		elementsStored = 0;
	}

	@Override
	public void add(T data) {
		// Make new DLL node with given data
		DLLNode<T> dataNode = new DLLNode<T>(data);

		// Set cache head to new data node
		DLLNode<T> oldHead = head;
		head = dataNode;

		// Set new data node's 'next' to old head node
		dataNode.setNext(oldHead);

		// Set old head node's 'prev' to new head node
		if (oldHead != null) {
			oldHead.setPrevious(dataNode);
		}

		// increment elementCount
		elementsStored++;

		if (elementsStored == 1) {
			rear = dataNode;
		}
		// if elementCount exceeds capacity, expunge cache 'rear' node.
		if (elementsStored > CAPACITY) {
			DLLNode<T> nextRear = rear.getPrevious();
			rear = nextRear;
			nextRear.setNext(null);
			elementsStored--;
		}
	}

	@Override
	public void removeLast() {
		// Only do this if there is something to remove
		if (elementsStored > 0) {
			DLLNode<T> nextRear = rear.getPrevious();
			nextRear.setNext(null);
			rear = nextRear;
			elementsStored--;
		} else {
			System.out.println("DEBUG: Attempted to remove element from empty Cache via removeLast().  Aborting...");
		}
	}

	@Override
	public void remove(T target) {
		// No element to remove
		if (elementsStored <= 0) {
			System.out.println("DEBUG: Attempted to remove element from empty Cache via remove(T).  Aborting...");
			return;
		}

		// Cycle through until we find the target element
		DLLNode<T> checkNode = head;
		while (!checkNode.getElement().equals(target) && checkNode.getNext() != null) {
			checkNode = checkNode.getNext();
		}

		if (checkNode.getElement().equals(target)) {
			// FOUND
			elementsStored--;
			// Only element in the list
			if (elementsStored == 1) {
				head = null;
				rear = null;
			} else {

				// Target node is the current head node
				if (checkNode == head) {
					head = checkNode.getNext();
					head.setPrevious(null);
					checkNode.setNext(null);
				} else

				// Target node is the current rear node
				if (checkNode == rear) {
					rear = checkNode.getPrevious();
					rear.setNext(null);
					checkNode.setPrevious(null);
				} else {
					// Target node is in the middle of the list
					DLLNode<T> prevNode = checkNode.getPrevious();
					DLLNode<T> nextNode = checkNode.getNext();
					prevNode.setNext(nextNode);
					nextNode.setPrevious(prevNode);
					checkNode.setNext(null);
					checkNode.setPrevious(null);
				}
			}

		} else {
			// NOT FOUND
			System.out.println("DEBUG: Could not find specified element in remove(T).  Aborting...");
		}
	}

	@Override
	public void write(T data) {
		// No element to write
		if (elementsStored <= 0) {
			System.out.println("DEBUG: Attempted to write element from empty Cache via write(T).  Aborting...");
			return;
		}

		// Cycle through until we find the target element
		DLLNode<T> checkNode = head;
		while (!checkNode.getElement().equals(data) && checkNode.getNext() != null) {
			checkNode = checkNode.getNext();
		}

		if (checkNode.getElement().equals(data)) {
			// FOUND
			if (elementsStored == 1) { // only element
				head = checkNode;
				rear = checkNode;
				checkNode.setPrevious(null);
				checkNode.setNext(null);
			} else if (checkNode == head) {
				// Node is already at the start of the list
				// easiest case to work with HEH
			} else if (checkNode == rear) {
				// Node is at the end of the list
				
				// remove node from end of list
				rear = checkNode.getPrevious();
				rear.setNext(null);
				checkNode.setPrevious(null);
				
				// place node at beginning of list
				checkNode.setNext(head);
				head.setPrevious(checkNode);
				head = checkNode;
			} else { // node is in the middle of the list
				DLLNode<T> prevNode = checkNode.getPrevious();
				DLLNode<T> nextNode = checkNode.getNext();
				
				// join both ends of found node
				prevNode.setNext(nextNode);
				nextNode.setPrevious(prevNode);
				
				// place found node at start of the list
				checkNode.setPrevious(null);
				checkNode.setNext(head);
				head.setPrevious(checkNode);
				head = checkNode;
			}
			
		} else {
			System.out.println("DEBUG: Could not find data to write via write(T). Aborting...");
		}
	}

	/**
	 * Returns the number of hits when 'get(T)'ing with this cache.
	 * @return the number of successful hits with this Cache (double)
	 */
	public double getHitCount() {
		return hits;
	}
	
	/**
	 * Returns the number of accesses when 'get(T)'ing with this cache.
	 * @return the number of times this Cache has been accessed (double)
	 */
	public double getAccessCount() {
		return access;
	}
	
	@Override
	public double getHitRate() {
		if (access > 0.0) {
			return hits / access;
		}
		return 0.0;
	}

	@Override
	public double getMissRate() {
		if (access > 0.0) {
			return 1.0 - (hits / access);
		}
		return 0.0;
	}

	@Override
	public boolean isEmpty() {
		return (elementsStored == 0);
	}

}
