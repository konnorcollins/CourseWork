package lexigon.glyph;

import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 * AbstractFactory -> Concrete Product
 */
/**
 * A red-colored label.
 * @author Konnor Collins
 */
public class RedLabel extends Label implements Glyph {
	
	/**
	 * Constructs a red-colored label.
	 */
	public RedLabel() {
		super();
	}

	public void draw(Window window) {
		window.drawLabel(_bounds.x, _bounds.y, _bounds.width, _bounds.height, "red");
		for (Glyph g: _children) {
			g.draw(window);
		}
	}
}
