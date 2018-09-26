/**
 * Handles and evaluates a factor comprised according to the following grammar formula:
 *    fact -> num
 * Whereas number is any double value.
 */
public class NodeFactNum extends NodeFact {

    private String num;

    public NodeFactNum(String num) {
	this.num=num;
    }

    public double eval(Environment env) throws EvalException {
	return Double.parseDouble(num);
    }

}
