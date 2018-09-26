/**
 * Handles and evaluates statements that fit the following grammar formula:
 *   Prog -> Block
 *
 */
public class NodeProg {
	private NodeBlock block;
	
	public NodeProg(NodeBlock b) {
		block = b;
	}
	
	public double eval(Environment env) throws EvalException {
		return block.eval(env);
	}
}
