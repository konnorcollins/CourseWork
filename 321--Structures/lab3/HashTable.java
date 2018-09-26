import java.util.NoSuchElementException;

/**
 * A basic Hash Table implementation. <br />
 * Stores Value/Key pairs and places them in an array based on pre-determined
 * hash functions and a user-specified type of probing.
 * 
 * @author Konnor Collins
 * @since 10-16-2017
 * @param <T>
 */
public class HashTable<T> {

	/**
	 * The type of probing in use by the HashTable.
	 */
	private OpenAddressType type;

	/**
	 * The array storing the hashed objects.
	 */
	private HashObject<T>[] hashTable;

	/**
	 * Stores an integer count array to keep track of duplicate hashed objects
	 * in the above hashTable.
	 */
	private int[] frequency;

	/**
	 * The maximum capacity of the HashTable. Can be referred to as "m".
	 */
	private int capacity; // m

	/**
	 * The ratio between the HashTable's capacity and the size of the set of
	 * elements to be stored in the HashTable. <br />
	 * Can be referred to as "a" (alpha).
	 */
	private float loadFactor;

	/**
	 * The amount of elements stored in
	 */
	private int size; // n

	/**
	 * Keeps track of the amount of probes conducted in this HashTable.
	 */
	private int numProbes;

	/**
	 * The maximum number of elements
	 */
	private int maxSize;

	/**
	 * The default capacity of the Hash Table.
	 */
	private final int DEFAULT_CAPACITY = 13;

	/**
	 * The default load factor of the Hash Table.
	 */
	private final float DEFAULT_LOAD_FACTOR = 0.75f;

	/**
	 * The default probing type of the Hash Table.
	 */
	private final OpenAddressType DEFAULT_TYPE = OpenAddressType.linear;

	/**
	 * Creates a HashTable with the default capacity, load factor, and probing
	 * type. <br />
	 * Refer to DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR, and DEFAULT_TYPE.
	 */
	public HashTable() {
		capacity = DEFAULT_CAPACITY;
		loadFactor = DEFAULT_LOAD_FACTOR;
		type = DEFAULT_TYPE;
		init();
	}

	/**
	 * Creates a HashTable with the user-specified capacity (m), the default
	 * load factor (a), and the default probing type (linear). <br />
	 * Refer to DEFAULT_LOAD_FACTOR and DEFAULT_TYPE for the aforementioned
	 * default values.
	 * 
	 * @param capacity
	 *            (m of the HashTable)
	 */
	public HashTable(int capacity) {
		this.capacity = capacity;
		loadFactor = DEFAULT_LOAD_FACTOR;
		type = DEFAULT_TYPE;
		init();
	}

	/**
	 * Creates a HashTable with the user-specified capacity (m), the
	 * user-specified load factor (a), and the default probing type (linear).
	 * <br />
	 * Refer to DEFAULT_TYPE for the aforementioned default value.
	 * 
	 * @param capacity
	 *            (m of the HashTable)
	 * @param loadFactor
	 *            (a of the HashTable)
	 */
	public HashTable(int capacity, float loadFactor) {
		this.capacity = capacity;
		this.loadFactor = loadFactor;
		type = DEFAULT_TYPE;
		init();
	}

	/**
	 * Creates a HashTable with the user-specified capacity (m), load factor
	 * (a), and probing type (linear or double hashing).
	 * 
	 * @param capacity
	 *            (m of the HashTable)
	 * @param loadFactor
	 *            (a of the HashTable)
	 * @param type
	 *            (linear or double hashing)
	 */
	public HashTable(int capacity, float loadFactor, OpenAddressType type) {
		this.capacity = capacity;
		this.loadFactor = loadFactor;
		this.type = type;

		if (type == OpenAddressType.quadratic) {
			throw new IllegalArgumentException("Quadratic probing not implemented.  Have a secure day!");
		}

		init();
	}

	private void init() {
		hashTable = new HashObject[capacity];
		frequency = new int[capacity];
		for (int i = 0; i < frequency.length; i++) {
			frequency[i] = 0;
		}

		size = 0;
		numProbes = 0;
		maxSize = (int) ((double) loadFactor * (double) capacity); // maxSize =
																	// a * m
	}

	/**
	 * Places a new HashObject<T> with the given value T and key into the
	 * HashTable. <br />
	 * If an unequal value already exists at the determined position, the table
	 * will probe for another position until an open position or duplicate value
	 * is found. <br />
	 * If a duplicate value is found, the object will not be inserted, and a
	 * secondary array that keeps track of duplicate values will have the same
	 * index increased.
	 * 
	 * @param value
	 *            (generic object T)
	 * @param key
	 *            (key to provided value (int))
	 */
	public void put(T value, int key) {
		if (size >= maxSize) {
			throw new IllegalStateException("Cannot exceed the specified load factor.");
		}

		HashObject<T> o = new HashObject<T>(value, key); // the inserted object

		boolean placed = false;
		int i = 0; // counter for what iteration of the hash calculation we are
					// on

		while (!placed) {
			int index = getHash(o.hashCode(), i);
			if (hashTable[index] == null) { // free index
				hashTable[index] = o;
				size++;
				return;
			} else { // index is occupied
				HashObject<T> comp = hashTable[index]; // the object at that
														// index
				if (comp.equals(o)) { // both hashed objects are equivalent
					frequency[index] = frequency[index] + 1;
					return;
				} else { // the hash objects are not equivalent
					i++;
					numProbes++;
					continue;
				}
			}
		}

	}

	/**
	 * Searches for a HashObject<T> with the given value T and key in the
	 * HashTable and removes it. <br />
	 * If no value is found, <br />
	 * If the Object is removed, it and all duplicates will be removed from that
	 * index.
	 * 
	 * @param value
	 *            (generic type T)
	 * @param key
	 *            of the value (int)
	 * @return the removed object T
	 */
	public T remove(T value, int key) {
		if (isEmpty()) {
			throw new NoSuchElementException("Cannot remove an element from an empty HashTable!");
		}

		HashObject<T> rm = new HashObject<T>(value, key); // a HashObject to
															// compare with
		T tValue = null;

		boolean found = false;
		int i = 0; // the iteration of the hash function we are on

		while (!found) {
			int index = getHash(rm.hashCode(), i);
			if (!(hashTable[index] == null)) { // Does a value exist at this
												// index?
				HashObject<T> comp = hashTable[index];
				if (comp.equals(rm)) { // Is it equivalent to our given value?
					tValue = comp.getValue();
					hashTable[index] = null;
					frequency[index] = 0;
					return tValue;
				}
			}

			i++;

			if (i == capacity) { // done enough probing to conclude that the
									// value will NOT be found
				break;
			}
		}

		if (!found) { // Was the element found?
			throw new NoSuchElementException("Element could not be found!");
		}

		return tValue;
	}

	/**
	 * Determines if the HashTable contains a given Key/Value pair.
	 * 
	 * @param value
	 *            (generic type T)
	 * @param key
	 *            (int)
	 * @return (true) if the pair exists in the table, (false) otherwise
	 */
	public boolean contains(T value, int key) {
		if (isEmpty()) { // Why bother checking an empty HashTable?
			return false;
		}

		HashObject<T> comp = new HashObject<T>(value, key); // use to compare

		boolean found = false;
		int i = 0; // the iteration of the hash function we are on

		while (!found) {
			int index = getHash(comp.hashCode(), i);
			if (!(hashTable[index] == null)) { // Does a value exist at this
												// index?
				HashObject<T> target = hashTable[index];
				if (comp.equals(target)) { // Is it equivalent to our given
											// value?
					found = true;
				}
			}

			i++;

			if (i == capacity) { // done enough probing to conclude that the
									// value will NOT be found
				break;
			}
		}

		return found;
	}

	/**
	 * Clears the Hash Table of all stored Objects, duplicate counts, and resets
	 * the size counter to 0.
	 */
	public void clear() {
		for (int i = 0; i < hashTable.length; i++) {
			hashTable[i] = null;
			frequency[i] = 0;
		}

		size = 0;
	}

	/**
	 * Gets the hashed value of the given key based on the type of probing the
	 * Hash Table is set to. <br />
	 * Note that the type of probing will only matter if the first attempt of
	 * getting a valid hash results in two unequal objects occupying the same
	 * space. In this case, the "i" parameter should be incremented, and the
	 * hash calculated again.
	 * 
	 * @param k
	 *            (the value being hashed)
	 * @param i
	 *            (the iteration being done)
	 * @return the hash value of the given key (int)
	 */
	public int getHash(int k, int i) {
		if (type == OpenAddressType.doubleHashing) { // check for double hashing
			return (hash1(k, capacity) + i * hash2(k, capacity)) % capacity;
		} else { // otherwise assume linear, no quadratic
			return (hash1(k, capacity) + i) % capacity;
		}
	}

	/**
	 * The h_1 function. <br />
	 * Adheres to the following formula: h_1(k) = k mod m
	 * 
	 * @param k
	 *            (the key value)
	 * @param m
	 *            (the capacity of the hash table)
	 * @return h_1(k) (int)
	 */
	public int hash1(int k, int m) {
		return k % m;
	}

	/**
	 * The h_2 function. <br />
	 * Adheres to the following formula: h_2(k) = 1 + (k mod (m - 2))
	 * 
	 * @param k
	 *            (the key value)
	 * @param m
	 *            (the capacity of the hash table)
	 * @return h_2(k) (int)
	 */
	public int hash2(int k, int m) {
		return 1 + (k % (m - 2));
	}

	/**
	 * Returns the current type of probing this Hash Table is using.
	 * 
	 * @return (OpenAddressType) linear, quadratic (defunct), or doubleHashing
	 */
	public OpenAddressType getType() {
		return type;
	}

	/**
	 * Returns the current capacity of the Hash Table.
	 * 
	 * @return
	 */
	public int getCapacity() {
		return capacity;
	}

	/**
	 * Returns the current load factor of the Hash Table.
	 * 
	 * @return
	 */
	public float getLoadFactor() {
		return loadFactor;
	}
	
	/**
	 * Returns the amount of duplicates found in the Hash Table.
	 * @return count of duplicate elements (int)
	 */
	public int getDupeCount() {
		int sum = 0;
		
		for (int i = 0; i < frequency.length; i++) {
			sum += frequency[i];
		}
		
		return sum;
	}

	/**
	 * Returns the number of elements currently stored in the hash table (not
	 * including duplicates).
	 * 
	 * @return
	 */
	public int size() {
		return size;
	}

	/**
	 * Determines if the Hash Table is currently empty.
	 * 
	 * @return (true) if the table is empty, (false) otherwise.
	 */
	public boolean isEmpty() {
		return (size == 0);
	}
	
	/**
	 * Determines if the Hash Table is at or above full capacity.
	 * @return (true) if the table is full, (false) otherwise.
	 */
	public boolean isFull() {
		return (size >= maxSize);
	}

	@Override
	public String toString() {
		String s = "";
		for (int i = 0; i < hashTable.length; i++) {
			s = s + "Table[" + i + "] ";
			if (hashTable[i] != null) {
				s = s + hashTable[i].toString() + frequency[i];
			}
			s = s + "\n";
		}

		return s;
	}

	/**
	 * Returns the number of probes conducted on the hash table.
	 * 
	 * @return
	 */
	public int getNumProbes() {
		return numProbes;
	}

	/**
	 * Returns the hypothetical maximum number of elements in the Hash Table.
	 * 
	 * @return
	 */
	public int getMaxSize() {
		return maxSize;
	}

}
