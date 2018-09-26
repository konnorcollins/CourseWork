package lexigon.window;

import lexigon.System;

/*
 * PATTERNS
 * AbstractFactory -> Abstract Factory
 * Singleton -> Singleton
 */
/**
 * Responsible for the creation of Window Implementations dependent on the user's platform.
 * @author Konnor Collins
 */
public abstract class WindowFactory {

	/**
	 * The active (WindowFactory). Can be set and retrieved by calling getFactory().
	 */
	private static WindowFactory _factory;

	/**
	 * Returns the activate factory reference.<br />
	 * If no factory is active, a new factory is created depending on the
	 * "LexiWindow" environment variable.
	 */
	public static WindowFactory getFactory() {
		if (_factory == null) {
			String wtype = System.getenv("LexiWindow");
			if (wtype.equalsIgnoreCase("Awt")) {
				_factory = new AwtWindowFactory();
			}

			if (wtype.equalsIgnoreCase("Swing")) {
				_factory = new SwingWindowFactory();
			}
		}

		return _factory;
	}

	/**
	 * Creates a new instance of a Window Implementation.
	 * @return (WindowImp) instance.
	 */
	public abstract WindowImp createWindow(String title, Window window);

}
