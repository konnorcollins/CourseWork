package lexigon.widget;

import lexigon.glyph.Button;
import lexigon.glyph.GreenButton;
import lexigon.glyph.GreenLabel;
import lexigon.glyph.Label;

/*
 * PATTERNS
 * AbstractFactory -> Concrete Factory
 * Singleton -> Singleton
 */
/**
 * A look and feel factory that produces Green colored objects.
 * @author Konnor Collins
 */
public class GreenFactory extends WidgetFactory {

	/**
	 * Can only be constructed via getFactory(String) in WidgetFactory.
	 */
	GreenFactory(){} // cannot be instantiated outside of widget package
	
	@Override
	public Button createButton() {
		return new GreenButton();
	}

	@Override
	public Label createLabel() {
		return new GreenLabel();
	}

}
