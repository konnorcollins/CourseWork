/**
 * Handles and evaluates an addition operation within an expression.
 * These will usually follow the following grammar formula:
 * 		addop -> '+'
 * 		addop -> '-'
 * Whereas '+' denotes addition, and '-' denotes subtraction.
 *
 */
public class NodeAddop extends Node {

    private String addop;

    public NodeAddop(int pos, String addop) {
	this.pos=pos;
	this.addop=addop;
    }

    /**
     * Adds or subtracts this operator's adjacent operands.
     * @param o1 (double value)
     * @param o2 (double value)
     * @return (either o1 + o2 or o1 - o2, depending on the operation specified) (double value)
     * @throws EvalException (if you  use an undefined addition operation)
     */
    public double op(double o1, double o2) throws EvalException {
	if (addop.equals("+"))
	    return o1+o2;
	if (addop.equals("-"))
	    return o1-o2;
	throw new EvalException(pos,"bogus addop: "+addop);
    }

}
