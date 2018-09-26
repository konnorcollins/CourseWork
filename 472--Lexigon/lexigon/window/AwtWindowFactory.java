package lexigon.window;

/*
 * PATTERNS
 * AbstractFactory -> Concrete Factory
 * Singleton -> Singleton
 */
/**
 * A factory that produces AwtWindow instances.
 * @author Konnor Collins
 */
public class AwtWindowFactory extends WindowFactory {

	/**
	 * Can only be instantiated by the WindowFactory class.
	 */
	protected AwtWindowFactory() {}
	
	@Override
	public WindowImp createWindow(String title, Window window) {
		AwtWindow aw = new AwtWindow(title, window);
		window.setWindowImp(aw);
		return aw;
	}

}
