/**
* Handles and evaluates statements that fit the following grammar formula:
 *   boolexpr: expr relop expr
 */
public class NodeBoolexpr extends Node {

	private NodeExpr o1, o2;
	private NodeRelop relop;

	public NodeBoolexpr(NodeExpr o1, NodeRelop rl, NodeExpr o2) {
		this.o1 = o1;
		this.o2 = o2;
		relop = rl;
	}

	/**
	 * Evaluates the relation between the two expressions.
	 */
	public double eval(Environment env) throws EvalException {
		return relop.op(o1.eval(env), o2.eval(env));
	}
}
