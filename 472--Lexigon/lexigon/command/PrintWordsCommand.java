package lexigon.command;

import lexigon.glyph.Glyph;
import lexigon.glyph.iterator.PreorderIterator;
import lexigon.visitor.WordsVisitor;

/* PATTERNS
 * Command -> Concrete Command
 */
/**
 * Command for printing out all words in Lexi to System out.  Can be scoped in on a target "root" glyph.
 * @author Konnor Collins
 */
public class PrintWordsCommand extends Command {
	
	private Glyph _target;
	
	public PrintWordsCommand(Glyph target) {
		_target = target;
	}

	@Override
	public void execute() {
		PreorderIterator i = new PreorderIterator (_target);
		i.first();
		WordsVisitor wv = new WordsVisitor();
		while (!i.isDone()) {	
			Glyph g = i.currentItem();
			if (g != null) g.accept(wv);		
			i.next();		
		}
		System.out.println("Test words");
	}

	@Override
	public void unexecute() {
	}

}
