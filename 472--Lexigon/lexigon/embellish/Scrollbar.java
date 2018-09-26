package lexigon.embellish;

import java.awt.Point;
import java.awt.Rectangle;
import java.util.Collection;

import lexigon.glyph.Glyph;
import lexigon.window.Window;

/*
 * PATTERNS
 * Decorator -> ConcreteDecorator
 */
/**
 * Represents a Scrollbar Embellishment wrapped around a Glyph.<br />
 * You can specify the boundaries the Scrollbar will cover on the screen.
 * @author Konnor Collins
 */
public class Scrollbar extends Embellishment {
	
	private Rectangle _bounds;
	
	/**
	 * Creates a Scrollbar with the given bounds.
	 * @param bounds (Rectangle)
	 */
	public Scrollbar(Rectangle bounds) {
		_bounds = bounds;
	}
	
	/**
	 * Creates a Scrollbar with the given bounds, containing the given Glyph.
	 * @param bounds (Rectangle)
	 * @param glyph (Glyph)
	 */
	public Scrollbar(Rectangle bounds, Glyph glyph) {
		_bounds = bounds;
		_glyph = glyph;
	}
	
	@Override
	public void draw(Window window) {
		_glyph.draw(window);
		window.addScrollBar(_bounds.x, _bounds.y, _bounds.width, _bounds.height);
	}
	
	@Override
	public Collection<Glyph> getChildren() {
		// TODO Auto-generated method stub
		return null;
	}

}
