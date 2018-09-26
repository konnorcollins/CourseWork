package lexigon.widget;

import lexigon.glyph.Button;
import lexigon.glyph.Label;
import lexigon.glyph.RedButton;
import lexigon.glyph.RedLabel;

/*
 * PATTERNS
 * AbstractFactory -> Concrete Factory
 * Singleton -> Singleton
 */
/**
 * A look and feel factory that produces Red colored objects.
 * @author Konnor Collins
 */
public class RedFactory extends WidgetFactory {
	
	/**
	 * Can only be constructed via getFactory(String) in WidgetFactory.
	 */
	RedFactory() {} // cannot be instantiated outside of widget package

	@Override
	public Button createButton() {
		return new RedButton();
	}

	@Override
	public Label createLabel() {
		return new RedLabel();
	}

}
