package lexigon.glyph;

import java.awt.Point;
import java.awt.Rectangle;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import lexigon.composition.Compositor;
import lexigon.composition.SimpleCompositor;
import lexigon.glyph.iterator.Iterator;
import lexigon.glyph.iterator.ListIterator;
import lexigon.visitor.GlyphVisitor;
import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Composite
 * Decorator -> ConcreteComponent
 */
public class Row implements Glyph, Composition {
	
	private List<Glyph> _children;
	private Rectangle _bounds;
	private Glyph _parent;
	private Compositor _compositor;
	public Window _window;
	
	/**
	 * Creates a Row composite with Character glyphs extracted from the given String.
	 * @param string (String)
	 * @param window (Window)
	 */
	public Row(String string, Window window) {
		_window = window;
		_children = new ArrayList<Glyph>();
		for (int i = 0; i < string.length(); i++) {
			add(new Character(string.charAt(i), window), i);
		}
		_bounds = new Rectangle();		
		_compositor = new SimpleCompositor(_window);
		_compositor.setComposition(this);

	}
	
	/**
	 * Creates a Row composite with the given children Glyphs.
	 * @param glyphs (Glyph[])
	 * @param window (Window)
	 */
	public Row(Glyph[] glyphs, Window window) {
		_window = window;
		for (int i = 0; i < glyphs.length; i++) {
			add(glyphs[i], i);
		}
		_bounds = new Rectangle();		
		_compositor = new SimpleCompositor(_window);
		_compositor.setComposition(this);
	}

	@Override
	public void draw(Window window) {
		for (Glyph g: _children) {
			g.draw(window);
		}
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
	public Glyph getParent() {
		return _parent;
	}

	@Override
	public Collection<Glyph> getChildren() {
		return _children;
	}
	
	@Override
	public void setBounds(Rectangle r) {
		_bounds = r;
	}
	
	@Override
	public void compose() {
		_compositor.compose();
	}

	@Override
	public void setParent(Glyph parent) {
		_parent = parent;
	}
	
	public void click() {};
	
	public Iterator<Glyph> createIterator() {
		return new ListIterator<Glyph>(_children);
	}

	@Override
	public void accept(GlyphVisitor visitor) {
		visitor.checkRow(this);
	}
}
