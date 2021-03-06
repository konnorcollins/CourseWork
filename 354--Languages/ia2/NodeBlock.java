/**
 * Handles and evaluates statements that fit the following grammar formulas:
 * block -> stmt ';' block | stmt
 */
public class NodeBlock extends Node {

	private NodeStmt stmt;
	private NodeBlock block;

	public NodeBlock(NodeStmt st) {
		stmt = st;
		block = null;
	}

	public NodeBlock(NodeStmt st, NodeBlock bl) {
		stmt = st;
		block = bl;
	}

	/**
	 * Evaluates the statement inside this block. A semicolon at the end of the
	 * statement denotes another statement to be evaluated.
	 */
	public double eval(Environment env) throws EvalException {
		if (block != null) {
			stmt.eval(env);
			return block.eval(env);
		}

		return stmt.eval(env);

	}
}
