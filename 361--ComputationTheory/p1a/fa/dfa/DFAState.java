package fa.dfa;

import fa.State;

import java.util.HashMap;
import java.util.Map;

/**
* A functional representation of a State in a deterministic finite automata.
* @author Konnor Collins, Andrew White
*/
public class DFAState extends State {
	
  /**
   * A collection of all transitions originating from this State.
   */
	private Map<Character, String> transitions = new HashMap<Character, String>(); // maps all possible input symbols to the name of the states they transist to
	
	/**
	 * Creates a DFA State with the given name.<br />
	 * Note that you will need to define transitions for this state for it to function properly.
	 * @param name
	 */
	public DFAState(String name) {
		this.name = name;
	}
	
	
	/**
	 * Defines a transition for this state.
	 * @param symb (the input read in)
	 * @param nextState (the name of the state to transist to)
	 */
	void addTransition(char symb, String nextState) {
		transitions.put(symb, nextState);
	}
	
	/**
	 * Checks to see if a transition exists for the given symbol.
	 * @param symb (the input read in)
	 * @return String (the name of the state to transist to.  null if no transition exists)
	 */
	String getTransition(char symb) {
		return transitions.get(symb);
	}
	
}
