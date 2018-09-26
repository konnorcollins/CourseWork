package lexigon.command;

import java.awt.Point;

import lexigon.glyph.Glyph;

/**
 * Responsible for determining if a visible element was clicked by the user's mouse.<br />
 * If such an element was clicked, it's click() method will be called.
 * @author Konnor Collins
 *
 */
public class GlyphFinder {

	public static void check(Glyph g, Point p) {
		if (g.intersects(p)) {
			execute(g);
			try {
				check(g.getChild(0), p);
			} catch (UnsupportedOperationException e) {

			}
		}
	}

	public static void execute(Glyph g) {
		g.click();
	}
}
