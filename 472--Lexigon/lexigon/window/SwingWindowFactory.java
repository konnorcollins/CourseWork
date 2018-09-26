package lexigon.window;

/*
 * PATTERNS
 * AbstractFactory -> Concrete Factory
 * Singleton -> Singleton
 */
/**
 * A factory that produces SwingWindow instances.
 * @author Konnor Collins
 */
public class SwingWindowFactory extends WindowFactory {
	
	/**
	 * Can only be instantiated by the WindowFactory class.
	 */
	protected SwingWindowFactory() {}

	@Override
	public WindowImp createWindow(String title, Window window) {
		SwingWindow sw = new SwingWindow(title, window);
		window.setWindowImp(sw);
		return sw;
	}

}
