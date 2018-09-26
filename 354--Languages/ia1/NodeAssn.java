/**
 * Handles and evaluates a basic assignment statement
 * A valid 'assignment' or 'assn' will adhere to the following grammar formula:
 * 		assn -> id '=' expr
 * Wheras 'id' refers to a user-defined variable of a given name, e.g. 'x'
 * Wheras 'expr' is any mathematically valid expression that adheres to the context free grammar's 'expr' formula(s).
 */
public class NodeAssn extends Node {

    private String id;
    private NodeExpr expr;

    public NodeAssn(String id, NodeExpr expr) {
	this.id=id;
	this.expr=expr;
    }

    public double eval(Environment env) throws EvalException {
	return env.put(id,expr.eval(env));
    }

}
