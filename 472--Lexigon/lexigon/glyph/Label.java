package lexigon.glyph;

import java.awt.Point;
import java.awt.Rectangle;
import java.util.ArrayList;
import java.util.List;

import lexigon.glyph.iterator.Iterator;
import lexigon.glyph.iterator.ListIterator;
import lexigon.visitor.GlyphVisitor;
import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 * AbstractFactory -> Abstract Product
 */
/**
 * A label glyph that can contain glyphs.
 * @author Konnor Collins
 */
public abstract class Label implements Glyph {

	protected Rectangle _bounds;
	protected List<Glyph> _children;
	private Glyph _parent;
	
	public abstract void draw(Window window);
	
	/**
	 * Constructs an empty label.
	 */
	public Label() {
		_children = new ArrayList<Glyph>();
		_bounds = new Rectangle();
	}

	@Override
	public void setBounds(Rectangle r) {
		_bounds = r;
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
	public void add(Glyph g, int index) {
		_children.add(index, g);
	}

	@Override
	public void remove(Glyph g) {
		_children.remove(g);
	}

	@Override
	public Glyph getChild(int index) {
		return _children.get(index);
	}

	@Override
	public void setParent(Glyph parent) {
		_parent = parent;
	}

	@Override
	public Glyph getParent() {
		return _parent;
	}
	
	public void click() {};
	
	public Iterator<Glyph> createIterator() {
		return new ListIterator<Glyph>(_children);
	}
	
	@Override
	public void accept(GlyphVisitor visitor) {
		visitor.checkLabel(this);
	}

}
