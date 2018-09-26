package lexigon.glyph;

import lexigon.window.Window;

/*
 * PATTERNS
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 * AbstractFactory -> Concrete Product
 */
/**
 * A red-colored button.
 * @author Konnor Collins
 */
public class RedButton extends Button implements Glyph {
	
	/**
	 * Constructs a red-colored button.
	 */
	public RedButton() {
		super();
	}

	public void draw(Window window) {
		window.drawButton(_bounds.x, _bounds.y, _bounds.width, _bounds.height, "red");
		for (Glyph g: _children) {
			g.draw(window);
		}
	}

}
