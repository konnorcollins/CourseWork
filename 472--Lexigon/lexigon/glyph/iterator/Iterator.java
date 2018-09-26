package lexigon.glyph.iterator;

/*
 * PATTERNS
 * Iterator -> Abstract Iterator
 */
/**
 * Basic interface for Iterators.
 * @author Konnor Collins
 * @param <E>
 */
public interface Iterator<E> {
	
	/**
	 * Resets the Iterator on it's first element, if any.
	 */
	public void first();
	
	/**
	 * Advances the Iterator.
	 */
	public void next();
	
	/**
	 * Determines if the Iterator has any more elements to process.
	 * @return (true) if no elements remain, (false) otherwise
	 */
	public boolean isDone();
	
	/**
	 * Returns the current element in the Iterator.  Does not advance.
	 * @return (E)
	 */
	public E currentItem();

}
