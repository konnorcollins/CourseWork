import java.util.Objects;

/**
 * Stores a generic object and key pair for Hash Table use.
 * @author Konnor Collins
 * @since 10-15-2017
 * @param <T> a generic object of type T
 */
public class HashObject<T> {
	
	/**
	 * The stored generic object.
	 */
	private T value;
	
	/**
	 * The stored generic object's key.
	 */
	private int key;
	
	/**
	 * Creates a HashObject with the given generic object and key pair.
	 * @param value
	 * @param key
	 */
	public HashObject(T value, int key) {
		this.value = value;
		this.key = key;
	}
	
	/**
	 * Returns the stored generic object.
	 * @return the generic object (T)
	 */
	public T getValue() {
		return value;
	}
	
	/**
	 * Sets the stored generic object to the specified value.
	 * @param value (generic object of type T)
	 */
	public void setValue(T value) {
		this.value = value;
	}
	
	/**
	 * Returns the stored generic object's key.
	 * @return the generic object's key (int)
	 */
	public int getKey() {
		return key;
	}
	
	/**
	 * Sets the stored generic object's key to the specified value.
	 * @param key to be set (int)
	 */
	public void setKey(int key) {
		this.key = key;
	}
	
	@Override
	public String toString() {
		String s = "" + value.toString() + " " + key;
		return s;
	}
	
	@Override
	public boolean equals(Object o) {
		// based on graciously provided example
		if (!(o instanceof HashObject)) {
			return false;
		}
		
		HashObject<T> comp = (HashObject<T>) o;
		
		return ((this.value == comp.getValue()) && (this.key == comp.getKey()));		
	}
	
	@Override
	public int hashCode() {
		return Math.abs(Objects.hash(key, value));
	}
	
}
