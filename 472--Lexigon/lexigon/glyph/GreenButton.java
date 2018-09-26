package lexigon.glyph;

import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 * AbstractFactory -> Concrete Product
 */
/**
 * A green-colored button.
 * @author Konnor Collins
 */
public class GreenButton extends Button implements Glyph {
	
	/**
	 * Constructs a green-colored button.
	 */
	public GreenButton() {
		super();
	}

	public void draw(Window window) {
		window.drawButton(_bounds.x, _bounds.y, _bounds.width, _bounds.height, "green");
		for (Glyph g: _children) {
			g.draw(window);
		}
	}

}
