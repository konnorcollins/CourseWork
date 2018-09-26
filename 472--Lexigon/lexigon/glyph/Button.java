package lexigon.glyph;

import java.awt.Point;
import java.awt.Rectangle;
import java.util.ArrayList;
import java.util.List;

import lexigon.command.Command;
import lexigon.command.CommandHistory;
import lexigon.glyph.iterator.Iterator;
import lexigon.glyph.iterator.ListIterator;
import lexigon.visitor.GlyphVisitor;

/*
 * PATTERNS
 * Command -> Invoker
 * Composite -> Leaf
 * Decorator -> ConcreteComponent
 * AbstractFactory -> Abstract Product
 * Visitor -> 
 */
/**
 * A button glyph that can contain children glyphs.<br />
 * Only a visible element at the moment.
 * @author Konnor Collins
 */
public abstract class Button implements Glyph {
	
	protected Rectangle _bounds;
	protected List<Glyph> _children;
	private Glyph _parent;
	private Command _command;
	
	/**
	 * Creates an empty button instance.
	 */
	public Button() {
		_children = new ArrayList<Glyph>();
		_bounds = new Rectangle();
	}

	@Override
	public void setBounds(Rectangle r) {
		_bounds = r;
	}

	@Override
	public Rectangle getBounds() {
		return _bounds;
	}

	@Override
	public boolean intersects(Point p) {
		return _bounds.contains(p);
	}

	@Override
	public void add(Glyph g, int index) {
		_children.add(index, g);
	}

	@Override
	public void remove(Glyph g) {
		_children.remove(g);
	}

	@Override
	public Glyph getChild(int index) {
		return _children.get(index);
	}

	@Override
	public void setParent(Glyph parent) {
		_parent = parent;
	}

	@Override
	public Glyph getParent() {
		return _parent;
	}
	
	/**
	 * Sets a command to be executed when the button is pressed.
	 * @param c (Command)
	 */
	public void setCommand(Command c) {
		_command = c;
	}
	
	/**
	 * Executes the command assigned to this button.
	 */
	public void click() {
		CommandHistory.getHistory().execute(_command);
	}
	
	public Iterator<Glyph> createIterator() {
		return new ListIterator<Glyph>(_children);
	}
	
	@Override
	public void accept(GlyphVisitor visitor) {
		visitor.checkButton(this);
	}

}
