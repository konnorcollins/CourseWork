package lexigon.embellish;

import java.awt.Point;
import java.awt.Rectangle;
import java.util.Collection;

import lexigon.glyph.Glyph;
import lexigon.window.Window;

/*
 * PATTERNS
 * Decorator -> Decorator
 */
/**
 * Represents a border around a given boundary. <br />
 * You can specify the position, width & height, as well as the thickness of the border.
 * @author Konnor Collins
 */
public class Border extends Embellishment {
	
	private Rectangle _bounds;
	private int _thickness;
	
	/**
	 * Creates a Border with the given bounds and thickness.
	 * @param bounds (Rectangle)
	 * @param thickness (int)
	 */
	public Border(Rectangle bounds, int thickness) {
		_bounds = bounds;
		_thickness = thickness;
	}
	
	/**
	 * Creates a Border with the given bounds, thickness, and wrapped Glyph.
	 * @param bounds (Rectangle)
	 * @param thickness (int)
	 * @param glyph (Glyph)
	 */
	public Border(Rectangle bounds, int thickness, Glyph glyph) {
		_bounds = bounds;
		_thickness = thickness;
		_glyph = glyph;
	}
	
	@Override
	public void draw(Window window) {
		_glyph.draw(window);
		window.addBorder(_bounds.x, _bounds.y, _bounds.x + _bounds.width, _bounds.y + _bounds.height, _thickness);
	}
	

	@Override
	public Collection<Glyph> getChildren() {
		return null;
	}

}
