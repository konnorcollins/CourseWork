package lexigon.widget;

import lexigon.System;
import lexigon.glyph.Button;
import lexigon.glyph.Label;

/*
 * PATTERNS
 * AbstractFactory -> Abstract Factory
 * Singleton -> Singleton
 */
/**
 * Responsible for the creation of all Widgets (Buttons, Labels) dictated by
 * look and feel.
 * 
 * @author Konnor Collins
 */
public abstract class WidgetFactory {

	/**
	 * The currently active (WidgetFactory). Can be set by calling
	 * getFactory(String) at least once.
	 */
	private static WidgetFactory _factory;

	/**
	 * Returns the currently set WidgetFactory reference. If no reference is set,
	 * sets a new WidgetFactory as active.
	 * 
	 * @param (String)
	 *            color, the name of the color factory
	 * @return (WidgetFactory) reference.
	 */
	public static WidgetFactory getFactory() {
		if (_factory == null) { // no factory selected yet
			String ftype = System.getenv("LexiLookAndFeel");
			if (ftype.equalsIgnoreCase("Green"))
				_factory = new GreenFactory();
			if (ftype.equalsIgnoreCase("Red"))
				_factory = new RedFactory();
		}

		return _factory;
	}

	/**
	 * Creates a new instance of button according to the set look and feel.
	 * 
	 * @return (Button) reference.
	 */
	public abstract Button createButton();

	/**
	 * Creates a new instance of Label according to the set look and feel.
	 * 
	 * @return (Label) reference.
	 */
	public abstract Label createLabel();

}
