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
public class RectangleGlyph implements Glyph {

	private Rectangle _bounds;
	private Glyph _parent;

	public RectangleGlyph(Rectangle rectangle) {
		_bounds = rectangle;
	}
	
	@Override
	public void draw(Window window) {
		window.drawRectangle(_bounds.x, _bounds.y, _bounds.width, _bounds.height);
	}

	@Override
	public void add(Glyph g, int index) {
		throw new UnsupportedOperationException();
	}

	@Override
	public void remove(Glyph g) {
		throw new UnsupportedOperationException();
	}

	@Override
	public Glyph getChild(int index) {
		throw new UnsupportedOperationException();
	}
	
	@Override
	public void setParent(Glyph parent) {
		_parent = parent;		
	}

	@Override
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
		_bounds = r;
	}
	
	public void click() {}

	@Override
	public Iterator<Glyph> createIterator() {
		return new NullIterator<Glyph>();
	}

	@Override
	public void accept(GlyphVisitor visitor) {
		visitor.checkRectangle(this);
	};
}
