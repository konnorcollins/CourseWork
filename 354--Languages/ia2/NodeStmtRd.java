/**
 * Handles and evaluates statements that fit the following grammar formula:
 *   stmt -> 'rd' id
 */
public class NodeStmtRd extends NodeStmt{
	
	String id;

	public NodeStmtRd(String id) {
		this.id = id;
	}
	
	public double eval(Environment env) throws EvalException {
		java.util.Scanner in = new java.util.Scanner(System.in);
		System.out.print("Please enter a value for " + id + ": ");
		id=in.next();
		in.close();
		double val = Double.parseDouble(id);
		env.put(id, val);
		return val;
	}

}
