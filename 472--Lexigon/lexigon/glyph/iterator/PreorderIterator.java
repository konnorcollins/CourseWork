package lexigon.glyph.iterator;

import java.util.Stack;

import lexigon.glyph.Glyph;

/*
 * PATTERNS
 * Iterator -> Concrete Iterator
 */
/**
 * Iterator for pre-order traversal through Glyphs.
 * 
 * @author Konnor Collins
 *
 */
public class PreorderIterator implements Iterator<Glyph> {

	private Glyph _root;
	private Stack<Iterator<Glyph>> _iterators;

	public PreorderIterator(Glyph root) {
		_root = root;
		_iterators = new Stack<Iterator<Glyph>>();
	}

	@Override
	public void first() {
		Iterator<Glyph> i = _root.createIterator();

		if (i != null) {
			i.first();
			_iterators.removeAllElements();
			_iterators.push(i);
		}

	}

	@Override
	public void next() {
		Iterator<Glyph> i = _iterators.peek().currentItem().createIterator();

		i.first();
		_iterators.push(i);

		while (_iterators.size() > 0 && _iterators.peek().isDone()) {
			_iterators.pop();
			if (!_iterators.isEmpty())
				_iterators.peek().next();
		}
	}

	@Override
	public boolean isDone() {
		return !(_iterators.size() > 0);
	}

	@Override
	public Glyph currentItem() {
		return _iterators.size() > 0 ? _iterators.peek().currentItem() : null;
	}

}
