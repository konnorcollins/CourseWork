package lexigon;

import java.awt.Rectangle;

import lexigon.command.IncrFontCommand;
import lexigon.command.KeyMap;
import lexigon.command.PrintWordsCommand;
import lexigon.command.SetFontSizeCommand;
import lexigon.embellish.Border;
import lexigon.embellish.Embellishment;
import lexigon.embellish.Scrollbar;
import lexigon.glyph.Button;
import lexigon.glyph.Column;
import lexigon.glyph.Composition;
import lexigon.glyph.Glyph;
import lexigon.glyph.Label;
import lexigon.glyph.Row;
import lexigon.widget.WidgetFactory;
import lexigon.window.ApplicationWindow;
import lexigon.window.Window;
import lexigon.window.WindowFactory;
import lexigon.window.WindowImp;

/**
 * Main driver for launching my Java implementation of Lexi. <br />
 * Tasks:<br />
 * Document Structure (Composite)[x]<br />
 * Formatting (Strategy)[x]<br />
 * Embellishment (Decorator)[x]<br />
 * Look and Feel (Abstract Factory)[x]<br />
 * Multiple Windows (Bridge)[x]<br />
 * User Operations (Command)[x]<br />
 * Spell Check/Hyphen (Iterator)[x]<br />
 * 
 * @author Konnor Collins
 */
public class Main {

	public static void main(String[] args) {

		// new fancy bridge pattern
		// see test.System for easy changes to environment variables you want to change.
		Window window = new ApplicationWindow();
		WindowImp sw = WindowFactory.getFactory().createWindow("Lexigon v6", window);

		// bounds setup
		Rectangle borderBounds = new Rectangle(0, 0, 150, 150);
		Rectangle scrollbarBounds = new Rectangle(125, 4, 20, 141);
		Composition doc = new Row(" Lexigon VI, WIP", window);

		// widget setup
		WidgetFactory factory = WidgetFactory.getFactory();
		Label label = factory.createLabel();
		label.setBounds(new Rectangle(3, 3, 120, 30));
		label.add(doc, 0);

		Button size14 = factory.createButton();
		size14.setCommand(new SetFontSizeCommand(window, 14));
		size14.setBounds(new Rectangle(3, 40, 20, 15));
		size14.add(new Row("=14", window), 0);

		Button size20 = factory.createButton();
		size20.setCommand(new SetFontSizeCommand(window, 20));
		size20.setBounds(new Rectangle(3, 70, 20, 15));
		size20.add(new Row("=20", window), 0);

		Button sizeP1 = factory.createButton();
		sizeP1.setCommand(new IncrFontCommand(window, 1));
		sizeP1.setBounds(new Rectangle(3, 100, 20, 15));
		sizeP1.add(new Row("+1", window), 0);

		Button sizeM1 = factory.createButton();
		sizeM1.setCommand(new IncrFontCommand(window, -1));
		sizeM1.setBounds(new Rectangle(3, 130, 20, 15));
		sizeM1.add(new Row("-1", window), 0);

		Glyph[] contents = {label, size14, size20, sizeP1, sizeM1};
		Column col = new Column(contents, window);
		
		// embellishments
		Embellishment scrollbar = new Scrollbar(scrollbarBounds, col);
		Embellishment border = new Border(borderBounds, 3, scrollbar);

		
		// keymap
		KeyMap keys = new KeyMap();
		keys.put('i', new IncrFontCommand(window, 1));
		keys.put('d', new IncrFontCommand(window, -1));
		keys.put('w', new PrintWordsCommand(border));
		window.setKeymap(keys);
		
		
		//
		doc.compose();
		window.setContents(border);
	}
}
