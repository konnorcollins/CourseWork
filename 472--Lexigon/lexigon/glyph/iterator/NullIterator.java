package lexigon.glyph.iterator;

/*
 * PATTERNS
 * Iterator -> Concrete Iterator
 */
/**
 * Iterator that does not actually Iterate.  Can be used for non-iterable glyphs.
 * @author Konnor Collins
 *
 */
public class NullIterator<E> implements Iterator<E> {

	@Override
	public void first() {
	}

	@Override
	public void next() {
	}

	@Override
	public boolean isDone() {
		return true;
	}

	@Override
	public E currentItem() {
		return null;
	}


}
