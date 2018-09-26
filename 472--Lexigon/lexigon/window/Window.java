package lexigon.window;


import lexigon.command.KeyMap;
import lexigon.glyph.*;

/*
 * PATTERNS
 * Bridge -> Abstraction
 */
public interface Window {

	void setWindowImp(WindowImp window);
	
    void drawCharacter(char c, int x, int y);
    void drawRectangle(int x, int y, int width, int height);

    int charWidth(char c);
    int charHeight(char c);

    void setContents(Glyph glyph);
    void setFontSize(int size);
    int getFontSize();
    void draw();

    void addBorder(int x1, int y1, int x2, int y2, int width);
    void addScrollBar(int x, int y, int width, int height);
    
    void drawButton(int x, int y, int width, int height, String color);
    void drawLabel(int x, int y, int width, int height, String color);

    
    void setKeymap(KeyMap k);
    void key(char c);
    void click(int x, int y);
}
