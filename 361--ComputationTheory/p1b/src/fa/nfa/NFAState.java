package fa.nfa;

import java.util.HashMap;
import java.util.LinkedHashSet;
import java.util.Set;

import fa.State;

/**
 * Representation of a State in an NFA machine.
 * @author Andrew White, Konnor Collins
 *
 */
public class NFAState extends State {
	
	/**
	 * Indicates if this state is a valid final state.
	 */
	private boolean _isFinal;
	
	/**
	 * Contains the transition function respective to this state.
	 */
	private HashMap<Character, Set<NFAState>> _transitionFunction;
	
	/**
	 * Creates a new non-final NFAState with the given name.
	 * @param name (String)
	 */
	public NFAState(String name) {
		this.name = name;
		_isFinal = false;
		_transitionFunction = new HashMap<Character, Set<NFAState>>();
	}
	
	/**
	 * Creates a new NFAState with the given name & validity for being final.
	 * @param name (String)
	 * @param isFinal (boolean)
	 */
	public NFAState(String name, boolean isFinal) {
		this.name = name;
		_isFinal = isFinal;
		_transitionFunction = new HashMap<Character, Set<NFAState>>();
	}
	
	/**
	 * Returns true if the state is a "final" state.  False otherwise.
	 * @return (boolean)
	 */
	public boolean isFinal() {
		return _isFinal;
	}
	
	/**
	 * Adds the given transition to this state.
	 * @param onSymb (char)
	 * @param toState (NFAState)
	 */
	public void addTransition(char onSymb, NFAState toState) {
		Set<NFAState> transition = _transitionFunction.get(onSymb);
		if (transition == null) {
			Set<NFAState> newTransition = new LinkedHashSet<NFAState>();
			newTransition.add(toState);
			_transitionFunction.put(onSymb, newTransition);
		} else {
			transition.add(toState);
		}
	}
	
	/**
	 * Returns a set of all valid state transitions for a given symbol.
	 * @param symb (char)
	 * @return (Set(NFAState))
	 */
	public Set<NFAState> getTo(char symb) {
		return _transitionFunction.get(symb);
	}
	
	public String toString() {
		return name;
	}
}