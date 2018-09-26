package lexigon.glyph;

import java.awt.Point;
import java.awt.Rectangle;

import lexigon.glyph.iterator.Iterator;
import lexigon.glyph.iterator.NullIterator;
import lexigon.visitor.GlyphVisitor;
import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 */
public class Character implements Glyph {

	private char _character;
	private Rectangle _bounds;
	private Glyph _parent;
	private Window _window;

	public Character(char c, Window window) {
		_character = c;
		_bounds = new Rectangle();
		_window = window;
	}

	public void draw(Window window) {
		window.drawCharacter(_character, _bounds.x, _bounds.y);
	}

	public void add(Glyph g, int index) {
		throw new UnsupportedOperationException();
	}

	public void remove(Glyph g) {
		throw new UnsupportedOperationException();
	}

	public Glyph getChild(int index) {
		throw new UnsupportedOperationException();
	}

	public Glyph getParent() {
		return _parent;
	}

	@Override
	public Rectangle getBounds() {
		return _bounds;
	}

	@Override
	public boolean intersects(Point p) {
		return _bounds.contains(p);
	}

	@Override
	public void setBounds(Rectangle r) {
		_bounds.x = r.x;
		_bounds.y = r.y;
		_bounds.width = _window.charWidth(_character);
		_bounds.height = _window.charHeight(_character);
	}

	@Override
	public void setParent(Glyph parent) {
		_parent = parent;
	}

	@Override
	public void click() {
	}

	@Override
	public Iterator<Glyph> createIterator() {
		return new NullIterator<Glyph>();
	}

	@Override
	public void accept(GlyphVisitor visitor) {
		visitor.checkCharacter(this);
	}
	
	public String toString() {
		return "" + _character;
	}

}
