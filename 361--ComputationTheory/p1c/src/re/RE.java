package re;

import fa.nfa.NFA;

/**
 * Can store a user-defined regular expression.<br />
 * Can construct a Non-deterministic Finite Automata from a user-defined regular expression.
 * @author Andrew White, Konnor Collins
 */
public class RE implements REInterface {

	/**
	 * The current expression stored internally.
	 */
	private String _expression;
	
	/**
	 * The NFA being constructed equivalent to the expression.
	 */
	private NFA _nfa;
	
	/**
	 * Internal counter for generating unique state names.
	 */
	private int _counter;

	/**
	 * Creates a new Regular Expression with the given expression.
	 * @param regEx
	 */
	public RE(String regEx) {
		_expression = regEx;
		_counter = 0;
	}

	@Override
	public NFA getNFA() {
		_nfa = new NFA();
		_counter = 0;
		_nfa.addStartState("S");
		_nfa.addFinalState("F");
		parseReg("S", "F");

		return _nfa;
	}

	/**
	 * Determines if the conversion from Regular Expression to Non-deterministic Finite Automata is complete.
	 * @return (true) if finished, (false) otherwise
	 */
	public boolean done() {
		return !(_expression.length() > 0);
	}

	/**
	 * Returns the next character in the expression, without removing it.
	 * @return (char)
	 */
	private char peek() {
		return _expression.charAt(0);
	}

	/**
	 * Removes the next character in the expression if it matches with the given character.
	 * @param character to eat (char)
	 */
	private void eat(char c) {
		if (peek() == c) {
			_expression = _expression.substring(1);
		}

		else
			System.err.println("ERR: Attempted to eat " + peek() + " given " + c);
	}

	/**
	 * Internal method for generating unique state names.
	 * @return (String)
	 */
	private String genName() {
		return "" + _counter++;
	}

	/*
	 * GRAMMAR 
	 * reg	: term '|' reg 
	 * 		| term
	 * 
	 * term : {fact}
	 * 
	 * fact : base '*' 
	 * 		| base
	 * 
	 * base : char 
	 * 		| '(' reg ')'
	 * 
	 */

	/**
	 * Creates a new section of the NFA that is equivalent to the given regular expression.
	 * @param start (String) name of starting state
	 * @param finish (String) name of final state
	 */
	private void parseReg(String start, String finish) {
		String regStart = genName();
		String regFinish = genName();
		_nfa.addState(regStart);
		_nfa.addState(regFinish);
		_nfa.addTransition(start, 'e', regStart);
		_nfa.addTransition(regFinish, 'e', finish);

		parseTerm(regStart, regFinish);
		if (!done()) {
			if (peek() == '|') {
				eat('|');
				parseReg(start, finish);
			}
		}
	}


	/**
	 * Creates a new section of the NFA that is equivalent to the given term.
	 * @param start (String) name of starting state
	 * @param finish (String) name of final state
	 */
	private void parseTerm(String start, String finish) {

		while (!done() && peek() != ')' && peek() != '|') {
			String termStart = genName();
			String termFinish = genName();
			_nfa.addState(termStart);
			_nfa.addState(termFinish);
			_nfa.addTransition(start, 'e', termStart);
			parseFact(termStart, termFinish);
			start = termFinish;
		}

		_nfa.addTransition(start, 'e', finish);
	}


	/**
	 * Creates a new section of the NFA that is equivalent to the given factor.
	 * @param start (String) name of starting state
	 * @param finish (String) name of final state
	 */
	private void parseFact(String start, String finish) {
		String factStart = genName();
		String factFinish = genName();
		parseBase(factStart, factFinish);
		if (!done()) {
			if (peek() == '*') {
				eat('*');
				String loopStart = genName();
				String loopStop = genName();
				_nfa.addState(loopStart);
				_nfa.addState(loopStop);
				_nfa.addTransition(loopStart, 'e', loopStop);
				_nfa.addTransition(loopStop, 'e', loopStart);
				_nfa.addTransition(loopStart, 'e', factStart);
				_nfa.addTransition(factFinish, 'e', loopStop);
				_nfa.addTransition(start, 'e', loopStart);
				_nfa.addTransition(loopStop, 'e', finish);
			} else {
				_nfa.addTransition(start, 'e', factStart);
				_nfa.addTransition(factFinish, 'e', finish);
			}
		} else {
			_nfa.addTransition(start, 'e', factStart);
			_nfa.addTransition(factFinish, 'e', finish);
		}
	}


	/**
	 * Creates a new section of the NFA that is equivalent to the given base character.
	 * @param start (String) name of starting state
	 * @param finish (String) name of final state
	 */
	private void parseBase(String start, String finish) {
		char c = peek();
		if (c == '(') {
			eat('(');
			parseReg(start, finish);
			eat(')');
		} else {
			eat(c);
			_nfa.addTransition(start, c, finish);
		}
	}

}
