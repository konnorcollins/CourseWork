/**
 * Handles and evaluates multiplication operations in the following grammar formula:
 *    mulop -> '*'
 *    mulop -> '/'
 *
 */
public class NodeMulop extends Node {

	/**
	 * The operation token
	 */
    private String mulop;

    public NodeMulop(int pos, String mulop) {
	this.pos=pos;
	this.mulop=mulop;
    }

    /**
     * Evaluates the multiplicative operation.
     * This does not support a remainder function denoted by '%'.
     * @param o1 (double value)
     * @param o2 (double value)
     * @return o1 * o2 (double value)
     * @throws EvalException (if you use an undefined multiplicative operation)
     */
    public double op(double o1, double o2) throws EvalException {
	if (mulop.equals("*"))
	    return o1*o2;
	if (mulop.equals("/"))
	    return o1/o2;
	throw new EvalException(pos,"bogus mulop: "+mulop);
    }

}
