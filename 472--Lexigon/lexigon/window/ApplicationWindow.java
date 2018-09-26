package lexigon.window;

import java.awt.Point;

import lexigon.command.CommandHistory;
import lexigon.command.GlyphFinder;
import lexigon.command.KeyMap;
import lexigon.glyph.Glyph;

/*
 * PATTERNS
 * Bridge -> Refined Abstraction
 */
/**
 * The main application window for Lexigon.
 * 
 * @author Konnor Collins
 *
 */
public class ApplicationWindow implements Window {

	/**
	 * The reference for the Window Implementation.
	 */
	private WindowImp _windowImp;

	private KeyMap _keyMap;

	// private ButtonMap _buttonMap;

	/**
	 * The parent glyph.
	 */
	private Glyph _glyph;

	@Override
	public void drawCharacter(char c, int x, int y) {
		_windowImp.drawCharacter(c, x, y);
	}

	@Override
	public void drawRectangle(int x, int y, int width, int height) {
		_windowImp.drawRectangle(x, y, width, height);
	}

	@Override
	public int charWidth(char c) {
		return _windowImp.charWidth(c);
	}

	@Override
	public int charHeight(char c) {
		return _windowImp.charHeight(c);
	}

	@Override
	public void setContents(Glyph glyph) {
		_glyph = glyph;
		_windowImp.setContents();
	}
	
	public int getFontSize() {
		return _windowImp.getFontSize();
	}
	
	public void setFontSize(int size) {
		_windowImp.setFontSize(size);
	}
	

	@Override
	public void draw() {
		if (_glyph != null) {
			_glyph.draw(this);
		}
	}

	@Override
	public void addBorder(int x1, int y1, int x2, int y2, int width) {
		_windowImp.addBorder(x1, y1, x2, y2, width);
	}

	@Override
	public void addScrollBar(int x, int y, int width, int height) {
		_windowImp.addScrollBar(x, y, width, height);
	}

	@Override
	public void drawButton(int x, int y, int width, int height, String color) {
		_windowImp.drawButton(x, y, width, height, color);
	}

	@Override
	public void drawLabel(int x, int y, int width, int height, String color) {
		_windowImp.drawLabel(x, y, width, height, color);
	}

	@Override
	public void setWindowImp(WindowImp window) {
		_windowImp = window;
	}

	public void setKeymap(KeyMap k) {
		_keyMap = k;
	}

	@Override
	public void key(char c) {
		switch (c) {
		case ('u'):
			CommandHistory.getHistory().undo();
		case ('r'):
			CommandHistory.getHistory().redo();
		default:
			if (_keyMap.get(c) != null)
				CommandHistory.getHistory().execute(_keyMap.get(c));
		}
		
		_windowImp.repaint();

	}

	@Override
	public void click(int x, int y) {
		GlyphFinder.check(_glyph, new Point(x, y)); // Broken still
	}
}
