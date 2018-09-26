package lexigon.embellish;

import java.awt.Point;
import java.awt.Rectangle;

import lexigon.glyph.Composition;
import lexigon.glyph.Glyph;
import lexigon.glyph.iterator.Iterator;
import lexigon.glyph.iterator.ListIterator;
import lexigon.visitor.GlyphVisitor;
import lexigon.window.Window;

/*
 * PATTERNS
 * Decorator -> ConcreteDecorator
 */
/**
 * Abstract wrapper for Glyphs.<br />
 * Forwards all Glyph calls to the wrapped Glyph object.<br />
 * 
 * @author Konnor Collins
 *
 */
public abstract class Embellishment implements Composition {

	protected Glyph _glyph;
	protected Rectangle _bounds;

	/**
	 * Draws the wrapped Glyph, as well as any additional elements as specified by
	 * the subclasses of Embellishment.
	 */
	public abstract void draw(Window window);

	/**
	 * Wraps this Embellishment around the given Glyph.
	 * 
	 * @param g
	 *            (Glyph)
	 */
	public void setGlyph(Glyph g) {
		_glyph = g;
	}

	@Override
	public boolean intersects(Point p) {
		return _glyph.getBounds().contains(p);
	}

	@Override
	public void setBounds(Rectangle r) {
		_bounds = r;
	}

	public Rectangle getBounds() {
		return _glyph.getBounds();
	}

	@Override
	public void add(Glyph g, int index) {
		_glyph.add(g, index);
	}

	@Override
	public void remove(Glyph g) {
		_glyph.remove(g);
	}

	@Override
	@Deprecated
	public Glyph getChild(int index) {
		return _glyph.getChild(index);
	}

	@Override
	public void setParent(Glyph parent) {
		_glyph.setParent(parent);
	}

	@Override
	public Glyph getParent() {
		return _glyph.getParent();
	}

	@Override
	public void compose() {
		// TODO: Can't do this as-is without casting. Probably requires some refactoring
	}
	
	public void click() {};
	
	public Iterator<Glyph> createIterator() {
		return _glyph.createIterator();
	}
	
	@Override
	public void accept(GlyphVisitor visitor) {
		_glyph.accept(visitor);
	}
}
