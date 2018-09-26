package lexigon.composition;

import lexigon.glyph.Composition;

/*
 * PATTERNS
 * Strategy -> Abstract Strategy
 */
/**
 * An interface for composing strategies.
 * @author Konnor Collins
 */
public interface Compositor {
	/**
	 * Sets the current composition to the given composition.
	 * @param composition (Composition)
	 */
	void setComposition(Composition composition);
	
	/**
	 * Composes the currently selected composition.
	 */
	void compose();
}
