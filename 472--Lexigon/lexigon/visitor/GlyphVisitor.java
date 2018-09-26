package lexigon.visitor;

import lexigon.glyph.*;
import lexigon.glyph.Character;

/*
 * PATTERNS
 * Visitor -> Abstract Visitor
 */
/**
 * Basic interface for Glyph visitors.
 * @author Konnor Collins
 */
public interface GlyphVisitor {

	/**
	 * Performs the defined operation on the given character.
	 */
	void checkCharacter(Character c);
	

	/**
	 * Performs the defined operation on the given rectangle.
	 */
	void checkRectangle(RectangleGlyph r);
	

	/**
	 * Performs the defined operation on the given Row.
	 */
	void checkRow(Row r);
	

	/**
	 * Performs the defined operation on the given Column.
	 */
	void checkColumn(Column c);
	

	/**
	 * Performs the defined operation on the given button.
	 */
	void checkButton(Button b);
	

	/**
	 * Performs the defined operation on the given label.
	 */
	void checkLabel(Label l);
	
}
