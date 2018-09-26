package lexigon.glyph;

import java.util.Collection;

import lexigon.composition.Compositor;
import lexigon.window.Window;

/*
 * PATTERNS
 * Strategy -> Context
 */
/**
 * Represents a Composite of Glyphs that can be formatted.
 * @author Konnor Collins
 *
 */
public interface Composition extends Glyph {
	public Collection<Glyph> getChildren();
	
	/**
	 * Requests the Composition's Compositor to compose the Glyph.
	 */
	public void compose();
	

}