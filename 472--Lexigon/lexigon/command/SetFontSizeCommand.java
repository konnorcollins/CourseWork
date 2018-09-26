package lexigon.command;

import lexigon.window.Window;

/*
 * PATTERNS
 * Command -> Concrete Command
 */
public class SetFontSizeCommand extends Command {
	
	private Window _target;
	private int _size;
	private int _prevSize;
	
	public SetFontSizeCommand(Window target, int size) {
		_target = target;
		_size = size;
	}

	@Override
	public void execute() {
		_prevSize = _target.getFontSize();
		_target.setFontSize(_size);		
	}

	@Override
	public void unexecute() {
		_target.setFontSize(_prevSize);
	}

}
