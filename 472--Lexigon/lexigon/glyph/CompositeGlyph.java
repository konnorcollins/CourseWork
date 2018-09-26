package lexigon.glyph;

import java.util.ArrayList;
import java.util.List;

import lexigon.glyph.iterator.Iterator;
import lexigon.glyph.iterator.ListIterator;
import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Composite
 */
public class CompositeGlyph  {
	
	List<Glyph> _children = new ArrayList<Glyph>();

	public void draw(Window window) {
		for (Glyph g: _children) {
			g.draw(window);
		}
	}

	public void add(Glyph g, int index) {
		_children.add(index, g);
	}

	public void remove(Glyph g) {
		_children.remove(g);
	}

	@Deprecated
	public Glyph getChild(int index) {
		return _children.get(index);
	}
	
	public void click() {
	}
	
	public Iterator<Glyph> createIterator() {
		return new ListIterator<Glyph>(_children);
	}

}
