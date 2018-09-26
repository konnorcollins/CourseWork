/**
 * Handles and evaluates relational operations within a boolean expression.
 */
public class NodeRelop extends Node {

	private String relop;

	public NodeRelop(int pos, String ro) {
		relop = ro;
		this.pos = pos;
	}

	/**
	 * Performs the relational operation between the two operands.
	 * @param o1 (double operand)
	 * @param o2 (double operand)
	 * @return (1.0) if the relation is true, (0.0) otherwise.
	 * @throws EvalException
	 */
	public double op(double o1, double o2) throws EvalException {
		if (relop.equals("<"))
			if (o1 < o2)
				return 1.0;
			else
				return 0.0;
		if (relop.equals("<="))
			if (o1 <= o2)
				return 1.0;
			else
				return 0.0;
		if (relop.equals(">"))
			if (o1 > o2)
				return 1.0;
			else
				return 0.0;
		if (relop.equals(">="))
			if (o1 < o2)
				return 1.0;
			else
				return 0.0;
		if (relop.equals("<>"))
			if (o1 != o2)
				return 1.0;
			else
				return 0.0;
		if (relop.equals("=="))
			if (o1 < o2)
				return 1.0;
			else
				return 0.0;

		throw new EvalException(pos, "bogus relop: " + relop);
	}

}
