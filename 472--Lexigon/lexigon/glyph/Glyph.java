package lexigon.glyph;

import java.awt.Point;
import java.awt.Rectangle;

import lexigon.glyph.iterator.Iterator;
import lexigon.visitor.GlyphVisitor;
import lexigon.window.Window;


/*
 * PATTERNS
 * Composite -> Component
 * Decorator -> Component
 * Iterator -> Iteratee
 * Factory Method -> Abstract Factory Method
 */
public interface Glyph {
	

	/**
	 * Draws this Glyph onto the given window.
	 * @param window (the display to be drawn onto)
	 */
	public  void draw(Window window);
	
	/**
	 * Sets the boundaries of the Glyph with the information in the given Rectangle, where applicable.
	 * @param r (rectangle to be filled)
	 */
	public void setBounds(Rectangle r);
	
	/**
	 * Returns the boundaries of this glyph.
	 * @return (Rectangle) object containing boundary information.
	 */
	public Rectangle getBounds();
	
	/**
	 * Determines if the given point in 2D space is occupied by this Glyph.
	 * @param p (java.awt.Point object specifying a point in 2D space)
	 * @return (true) if the Point intersects with the Glyph, (false) otherwise
	 */
	public boolean intersects(Point p);
	
	/**
	 * Adds a child Glyph to this Glyph, if applicable.
	 * @param g (the child Glyph node to be added to this Glyph)
	 * @param index (the index of the child node)
	 */
	abstract void add(Glyph g, int index);
	
	/**
	 * Removes a child Glyph node from this Glyph, if applicable.
	 * @param g (the child Glyph to remove)
	 */
	abstract void remove(Glyph g);
	
	/**
	 * Returns a reference to the child Glyph node at the given index, if any.
	 * @param index (the index of the child Glyph node)
	 * @return (Glyph) reference
	 */
	@Deprecated
	public abstract Glyph getChild(int index);
	
	/**
	 * Sets this Glyph's parent to the given Glyph.
	 * @param parent (Glyph)
	 */
	public void setParent(Glyph parent);
	
	/**
	 * Returns a reference to this Glyph's parent, if any.
	 * @return (Glyph) reference
	 */
	public Glyph getParent();
	
	/**
	 * Executes a command attached to this Glyph, if any.
	 */
	public void click();
	
	/**
	 * Creates a new Iterator on the given glyph.
	 */
	public Iterator<Glyph> createIterator();
	
	/**
	 * Performs given visitor's operation on this object.
	 * @param visitor
	 */
	public void accept(GlyphVisitor visitor);

}
