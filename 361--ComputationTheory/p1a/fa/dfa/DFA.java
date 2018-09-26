package fa.dfa;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.HashSet;
import fa.*;

/**
 * A representation of a Deterministic Finite Automata. <br />
 * Contains methods for constructing the DFA, as well as testing string input to see if they are accepted by said DFA.
 * @author Konnor Collins, Andrew White
 */
public class DFA implements DFAInterface, FAInterface {

	private Map<String, DFAState> states = new HashMap<String, DFAState>(); // All states
	private String alphabet = ""; // alphabet
	private DFAState startState = null; // SINGLE start state
	private List<DFAState> finalStates = new ArrayList<DFAState>(); // all final states

	// Store ALL states in the states map, we use this to refer to them by name
	// Store a state in BOTH finalStates AND states if it is a final state, we use
	    // finalStates to check if a given state IS final
	// Store a start state in both startState AND states if it is a start state. We
      // use the startState reference when checking valid input
	// Alphabet is stored as a string, use String.contains to check if a char symbol
	    // is valid
	// transitions are stored as a map in each DFA state, see DFAState for more
	    // details

	@Override
	public void addStartState(String name) { // adds a start state, failsafed to not set another startstate after one
												// has already been set
		if (startState != null)
			return;
		startState = new DFAState(name);
		states.put(name, startState);
	}

	@Override
	public void addState(String name) { // creates new state with name, maps that state to that name
		states.put(name, new DFAState(name));
	}

	@Override
	public void addFinalState(String name) {
		DFAState state = new DFAState(name);
		states.put(name, state);
		finalStates.add(state);
	}

	@Override
	public void addTransition(String fromState, char onSymb, String toState) {

		if (!(alphabet.contains("" + onSymb))) { // place symbol into alphabet if not already there
			alphabet = alphabet + onSymb;
		}

		states.get(fromState).addTransition(onSymb, toState);
	}

	@Override
	public Set<? extends State> getStates() {

		Set<DFAState> s = new HashSet<DFAState>();
		states.forEach((K, v) -> {
			s.add(v);
		});

		return s;
	}

	@Override
	public Set<? extends State> getFinalStates() {
		Set<DFAState> s = new HashSet<DFAState>();

		for (DFAState state : finalStates) {
			s.add(state);
		}

		return s;
	}

	@Override
	public State getStartState() {
		return startState;
	}

	@Override
	public Set<Character> getABC() {

		Set<Character> s = new HashSet<Character>();

		for (int i = 0; i < alphabet.length(); i++) {
			s.add(alphabet.charAt(i));
		}

		return s;

	}

	/**
	* Determines if the given input string is accepted by this DFA.<br />
	* @param (String) input --> The string to be checked by the automata.<br />
	* @return (boolean) --> true if the String is accepted by the DFA, false otherwise.
	*/
	public boolean accepts(String input) {
		DFAState currentState = startState;

		while (input.length() > 0) {
			String nextState = currentState.getTransition(input.charAt(0)); // check transition for this state
			if (nextState == null)
				return false; // no transition for this character, does not accept this input
			currentState = states.get(nextState); // set current state equal to next state
			input = input.substring(1, input.length()); // remove input symbol we read
		}

		return finalStates.contains(currentState); // if input is exhausted and current state is a final state, input is
													// accepted
	}

	/**
	* Returns the next State given a current state and input Symbol.<br />
	* Not really used in this implementation, but good for error checking.
	* @param (DFAState) from --> the state to check the transition for.
	* @param (char) onSymb --> The input symbol for the transition.
	* @return (DFAState) The destination state for the transition, null if no such state exists.
	*/
	public DFAState getToState(DFAState from, char onSymb) {
		return states.get(from.getTransition(onSymb);
	}

	/**
	* Returns a description containing the 5-tuple defining this automata.
	*/
	public String toString() {
		StringBuilder sb = new StringBuilder();
		sb.append("Q = { ");
		for (DFAState d : states.values()) {
			sb.append(d.getName() + " ");
		}
		sb.append("}\n");

		sb.append("Sigma = { ");
		for (int i = 0; i < alphabet.length(); i++) {
			sb.append(alphabet.charAt(i) + " ");
		}
		sb.append("}\n");

		sb.append("delta = \n\t");
		for (int i = 0; i < alphabet.length(); i++) {
			sb.append("\t" + alphabet.charAt(i));
		}
		sb.append("\n");
		for (DFAState d : states.values()) {
			sb.append("\t" + d.getName());
			for (int i = 0; i < alphabet.length(); i++) {
				sb.append("\t" + d.getTransition(alphabet.charAt(i)));
			}
			sb.append("\n");
		}

		sb.append("q0 = " + startState.getName() + "\n");

		sb.append("F = { ");
		for (DFAState d : finalStates) {
			sb.append(d.getName() + " ");
		}
		sb.append("}\n");

		return sb.toString();
	}
}