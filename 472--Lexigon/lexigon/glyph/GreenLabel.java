package lexigon.glyph;

import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 * AbstractFactory -> Concrete Product
 */
/**
 * A green-colored label.
 * @author Konnor Collins
 */
public class GreenLabel extends Label implements Glyph {
	
	/**
	 * Constructs a green-colored label.
	 */
	public GreenLabel() {
		super();
	}

	public void draw(Window window) {
		window.drawLabel(_bounds.x, _bounds.y, _bounds.width, _bounds.height, "green");
		for (Glyph g: _children) {
			g.draw(window);
		}
	}
}
