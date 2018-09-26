package lexigon.composition;

import java.awt.Point;
import java.awt.Rectangle;

import lexigon.glyph.Column;
import lexigon.glyph.Composition;
import lexigon.glyph.Glyph;
import lexigon.glyph.Row;
import lexigon.window.Window;

/*
 * PATTERNS
 * Strategy -> Concrete Strategy
 */
/**
 * A simple composition strategy with no particular justification.
 * @author Konnor Collins
 */
public class SimpleCompositor implements Compositor {
	
	private Composition _composition;
	private Window _window;
	
	public SimpleCompositor(Window window) {
		_window = window;
	}

	@Override
	public void setComposition(Composition composition) {
		_composition = composition;
	}

	@Override
	public void compose() {
		Rectangle bounds = _composition.getBounds();
		Point p = new Point(bounds.x, bounds.y);
		for (int i = 0; i < _composition.getChildren().size(); i++) {
			
			Glyph g = _composition.getChild(i);
			g.setParent(_composition);
			Rectangle r = new Rectangle();
			
			// set child pos on cursor
			r.setLocation(p.x, p.y);
			g.setBounds(r);
			
			// recursively compose
			if (g instanceof Composition) { 
				// ((Composition) g).compose();
				//TODO: Refactor to fix recursive composition WITHOUT casting
			}
			
			// adjust cursor
			if (_composition instanceof Row)
				p.translate(g.getBounds().width, 0);
			
			if (_composition instanceof Column)
				p.translate(0, g.getBounds().height);
			
		}
		
		_composition.getBounds().width += p.x;
		_composition.getBounds().height += p.y;
		
		
		// Thanks for the pseudocode.  Helped quite a bit.
		// create cursor based on parent
		//for (... child= ...) {
		    // ask (leaf) child to set size, based on window
		    // ask child to set position, based on cursor
		    // ask child to compose itself, recursively
		    // ask parent to adjust itself and cursor, based on child
		//}
		// ask parent to adjust itself, based on cursor
	}

}
