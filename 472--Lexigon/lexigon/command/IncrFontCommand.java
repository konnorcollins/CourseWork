package lexigon.command;

import lexigon.window.Window;

/* PATTERNS
 * Command -> Concrete Command
 */
/**
 * Command for incrementing font size by a given amount.
 * @author Konnor Collins
 */
public class IncrFontCommand extends Command {

	private Window _target;
	private int _amount;
	
	public IncrFontCommand(Window target, int amt) {
		_target = target;
		_amount = amt;
	}

	@Override
	public void execute() {
		int size = _target.getFontSize();
		if (size + _amount >= 0)
			_target.setFontSize(size + _amount);
	}

	@Override
	public void unexecute() {
		int size = _target.getFontSize();
		if (size - _amount >= 0)
			_target.setFontSize(size - _amount);
	}

}
