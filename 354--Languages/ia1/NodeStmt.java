/**
 * Handles and evaluates statements that fit the following grammar formula:
 *   stmt -> assn ';'
 *
 */
public class NodeStmt extends Node {

    private NodeAssn assn;

    public NodeStmt(NodeAssn assn) {
	this.assn=assn;
    }

    public double eval(Environment env) throws EvalException {
	return assn.eval(env);
    }

}
