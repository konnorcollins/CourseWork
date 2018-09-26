package lexigon.glyph.iterator;

import java.util.List;


/*
 * PATTERNS
 * Iterator -> Concrete Iterator
 */
/**
 * Basic iterator for CompositeGlyphs that store children in List format.
 * @author Konnor Collins
 *
 */
public class ListIterator<E> implements Iterator<E> {
	
	private List<E> _list;
	private int index;
	
	public ListIterator(List<E> target) {
		_list = target;
		index = 0;
	}

	@Override
	public void first() {
		index = 0;
	}

	@Override
	public void next() {
		index++;
	}

	@Override
	public boolean isDone() {
		return (index >= _list.size());
	}

	@Override
	public E currentItem() {
		return _list.get(index);
	}

}
