package fa.nfa;

import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Set;

import fa.FAInterface;
import fa.State;
import fa.dfa.DFA;

/**
 * A simulation of an Non-Deterministic Finite Automata
 * @author Andrew White, Konnor Collins
 */
public class NFA implements FAInterface, NFAInterface {

	/**
	 * The key character that represents the empty-string.
	 */
	private final char EMPTY_STRING = 'e';
	
	/**
	 * The name of the "Trap" state in the case that a transition does not exist in the final DFA.
	 */
	private final String TRAP_STATE = "[]";

	/**
	 * Stores all NFAState objects belonging to this NFA machine.
	 */
	private Set<NFAState> states;
	
	/**
	 * Refers to the starting NFAState object of this machine.
	 */
	private NFAState start;
	
	/**
	 * Stores all valid input characters, except for the character selected by EMPTY_STRING.
	 */
	private Set<Character> ordAbc;

	/**
	 * Creates a new, empty NFA machine.
	 */
	public NFA() {
		states = new LinkedHashSet<NFAState>();
		ordAbc = new LinkedHashSet<Character>();
	}

	@Override
	public DFA getDFA() {
		DFA dfa = new DFA();
		boolean trap_set = false; // flag that keeps track of if and when we need to insert a trap state into the machine

		// no dupes please
		Set<Set<NFAState>> processedStates = new LinkedHashSet<Set<NFAState>>(); // transitions have been processed
		Set<String> knownStates = new LinkedHashSet<String>();	// state has been created
		Queue<Set<NFAState>> toProcess = new LinkedList<Set<NFAState>>(); // still need to work on these states for one of the above

		// START STATE HANDLING
		Set<NFAState> startState = eClosure(start);
		processedStates.add(startState);
		if (setHasFinalState(startState)) {
			dfa.addFinalState(startState.toString());
		} else {
			dfa.addState(startState.toString());
		}
		dfa.addStartState(startState.toString());
		knownStates.add(startState.toString());

		// transition handling
		for (char c : ordAbc) {
			Set<NFAState> transition = new LinkedHashSet<NFAState>();
			for (NFAState ns : startState) {
				Set<NFAState> charTransition = ns.getTo(c);
				if (ns.getTo(c) != null) {
					Set<NFAState> eTransition = eClosure(charTransition);
					if (eTransition != null) {
						transition.addAll(eTransition);
					}
				}
			}

			if (!knownStates.contains(transition.toString()) && !transition.isEmpty()) {
				toProcess.add(transition);
				if (setHasFinalState(transition)) {
					dfa.addFinalState(transition.toString());
				} else {
					dfa.addState(transition.toString());
				}
				knownStates.add(transition.toString());
			}

			if (!transition.isEmpty())
				dfa.addTransition(startState.toString(), c, transition.toString());
			else {
				if (!trap_set) {
					dfa.addState(TRAP_STATE);
					trap_set = true;
				}
				dfa.addTransition(startState.toString(), c, TRAP_STATE);
			}
		}

		processedStates.add(startState);

		// OTHER STATE HANDLING
		while (!toProcess.isEmpty()) {
			Set<NFAState> unprocessedNFA = toProcess.poll();
			if (!knownStates.contains(unprocessedNFA.toString()) && !toProcess.contains(unprocessedNFA)) {
				if (setHasFinalState(unprocessedNFA)) {
					dfa.addFinalState(unprocessedNFA.toString());
				} else {
					dfa.addState(unprocessedNFA.toString());
				}
			}

			// FOR ALL VALID CHARACTERS
			for (char c : ordAbc) {
				Set<NFAState> transition = new LinkedHashSet<NFAState>();

				// GRAB TRANSITIONS
				for (NFAState ns : unprocessedNFA) {
					Set<NFAState> charTransition = ns.getTo(c);
					if (charTransition != null) {
						Set<NFAState> eTransition = eClosure(charTransition);
						if (eTransition != null)
							transition.addAll(eTransition);
					}
				}

				// NEW STATE NEEDS TO BE MADE?
				if (!knownStates.contains(transition.toString()) && !transition.isEmpty()) {
					if (!toProcess.contains(transition))
						toProcess.add(transition);
					if (setHasFinalState(transition)) {
						dfa.addFinalState(transition.toString());
					} else {
						dfa.addState(transition.toString());
					}
					knownStates.add(transition.toString());
				}

				// ADD TRANSITIONS TO FROMSTATE
				if (!transition.isEmpty())
					dfa.addTransition(unprocessedNFA.toString(), c, transition.toString());
				else {
					if (!trap_set) {
						dfa.addState(TRAP_STATE);
						trap_set = true;
					}
					dfa.addTransition(unprocessedNFA.toString(), c, TRAP_STATE);
				}
			}

			processedStates.add(unprocessedNFA);

		}

		// prepare trap state
		if (trap_set) {
			for (char c : ordAbc) {
				dfa.addTransition(TRAP_STATE, c, TRAP_STATE);
			}
		}

		// TODO: Look into removing the separate "start state" processing above the main loop
		return dfa;
	}

	@Override
	public Set<NFAState> getToState(NFAState from, char onSymb) {
		return from.getTo(onSymb);
	}

	@Override
	public Set<NFAState> eClosure(NFAState s) {
		Set<NFAState> eValid = new LinkedHashSet<NFAState>();
		Queue<NFAState> toProcess = new LinkedList<NFAState>();
		toProcess.add(s);

		while (!toProcess.isEmpty()) {
			NFAState top = toProcess.poll();

			// please don't process dupes
			// endless loops are not fun
			if (!eValid.contains(top)) {
				eValid.add(top);
				Set<NFAState> gotTo = top.getTo(EMPTY_STRING);
				if (gotTo != null)
					toProcess.addAll(top.getTo(EMPTY_STRING));
			}
		}

		return eValid;
	}

	/**
	 * Returns the empty-string transitions for all states in the given set.
	 * @param s (Set(NFAState))
	 * @return (Set(NFAState))
	 */
	private Set<NFAState> eClosure(Set<NFAState> s) {
		Set<NFAState> all = new LinkedHashSet<NFAState>();
		for (NFAState ns : s) {
			Set<NFAState> eTrans = eClosure(ns);
			if (eTrans != null)
				all.addAll(eClosure(ns));
		}
		return all;
	}

	@Override
	public void addStartState(String name) {
		NFAState startState = get_state(name);
		if (startState == null) {
			startState = new NFAState(name);
		}

		start = startState;
		states.add(start);
	}

	@Override
	public void addState(String name) {
		NFAState newState = new NFAState(name);
		states.add(newState);
	}

	@Override
	public void addFinalState(String name) {
		NFAState newState = new NFAState(name, true);
		states.add(newState);

	}

	@Override
	public void addTransition(String fromState, char onSymb, String toState) {
		get_state(fromState).addTransition(onSymb, get_state(toState));

		if (!ordAbc.contains(onSymb) && onSymb != EMPTY_STRING) {
			ordAbc.add(onSymb);
		}
	}

	@Override
	public Set<? extends State> getStates() {
		return states;
	}

	@Override
	public Set<? extends State> getFinalStates() {

		Set<NFAState> fStates = new LinkedHashSet<NFAState>();

		for (NFAState ns : states) {
			if (ns.isFinal())
				fStates.add(ns);
		}

		return fStates;
	}

	@Override
	public State getStartState() {
		return start;
	}

	@Override
	public Set<Character> getABC() {
		return ordAbc;
	}

	/**
	 * Returns the given NFAState if there is a state with that name present in the
	 * Set of states. Otherwise returns null.
	 * 
	 * @param s
	 *            String name of NFAState to be searched
	 * @return NFAState from the list
	 */
	private NFAState get_state(String s) {

		NFAState returnState = null;

		for (NFAState ns : states) {
			if (ns.getName().equals(s)) {
				returnState = ns;
			}
		}
		return returnState;
	}

	/**
	 * Checks the given set of NFAStates to see if there is a final state among
	 * them.
	 * 
	 * @param s
	 *            (Set(NFAState))
	 * @return (true) if there is a final state in the given set.
	 */
	private boolean setHasFinalState(Set<NFAState> s) {
		boolean hasFinal = false;
		for (NFAState ns : s) {
			if (ns.isFinal())
				hasFinal = true;
		}
		return hasFinal;
	}

}