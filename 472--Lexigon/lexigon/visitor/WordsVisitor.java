package lexigon.visitor;

import lexigon.glyph.Button;
import lexigon.glyph.Character;
import lexigon.glyph.Column;
import lexigon.glyph.Label;
import lexigon.glyph.RectangleGlyph;
import lexigon.glyph.Row;

/*
 * PATTERNS
 * Visitor -> Concrete Visitor
 */
/**
 * Basic visitor that prints out all words in a given Lexi window.
 * @author Konnor Collins
 *
 */
public class WordsVisitor implements GlyphVisitor {

	@Override
	public void checkCharacter(Character c) {
		System.out.print(c.toString());
	}
	
	
	@Override
	public void checkRectangle(RectangleGlyph r) {
	}
	
	
	@Override
	public void checkRow(Row r) {
		System.out.println();
	}

	@Override
	public void checkColumn(Column c) {
	}
	
	@Override
	public void checkButton(Button b) {
	}

	@Override
	public void checkLabel(Label l) {
	}



}
