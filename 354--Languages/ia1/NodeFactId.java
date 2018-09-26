/**
 * Handles the following grammar:
 * fact  -> id
 * Whereas 'id' can refer to any previously defined variable.
 */
public class NodeFactId extends NodeFact {

	/**
	 * The ID of the variable.
	 */
    private String id;

    public NodeFactId(int pos, String id) {
	this.pos=pos;
	this.id=id;
    }

    public double eval(Environment env) throws EvalException {
	return env.get(pos,id);
    }

}
