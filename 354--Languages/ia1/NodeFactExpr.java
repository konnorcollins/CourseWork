/**
 * Handles and evaluates a Factor comprised of the following grammar formula:
 *   fact -> '(' expr ')'
 * Whereas 'expr' is any valid expression according to the included context free grammar.
 *
 */
public class NodeFactExpr extends NodeFact {

    private NodeExpr expr;

    public NodeFactExpr(NodeExpr expr) {
	this.expr=expr;
    }

    public double eval(Environment env) throws EvalException {
	return expr.eval(env);
    }

}
