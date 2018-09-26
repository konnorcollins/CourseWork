/**
* Handles and evaluates statements that fit the following grammar formula:
 *   stmt -> assn
 */
public class NodeStmtAssn extends NodeStmt {

	private NodeAssn assn;
	
	public NodeStmtAssn(NodeAssn as) {
		assn = as;
	}
	
	public double eval(Environment env) throws EvalException {
		return assn.eval(env);
	}
}
