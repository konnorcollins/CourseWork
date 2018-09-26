/**
 * Handles and evaluates any valid expression that follows the following grammar formula(s):
 * 		expr -> term addop expr
 * 		expr -> term
 * <br /> Whereas 'term' refers to any valid term defined by the context free grammar included in the submitted files.
 * <br /> Whereas 'addop' refers to any valid addition/subtration operation, defined with the '+' and '-' characters.
 * <br /> Wheras 'expr' refers to any valid expression defined by the included context free grammar.
 *
 */
public class NodeExpr extends Node {

    private NodeTerm term;
    private NodeAddop addop;
    private NodeExpr expr;

    public NodeExpr(NodeTerm term, NodeAddop addop, NodeExpr expr) {
	this.term=term;
	this.addop=addop;
	this.expr=expr;
    }

    public void append(NodeExpr expr) {
	if (this.expr==null) {
	    this.addop=expr.addop;
	    this.expr=expr;
	    expr.addop=null;
	} else
	    this.expr.append(expr);
    }

    public double eval(Environment env) throws EvalException {
	return expr==null
	    ? term.eval(env)
	    : addop.op(expr.eval(env),term.eval(env));
    }

}
