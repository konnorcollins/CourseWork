package lexigon.command;

import java.util.*;

public class KeyMap {

	private Hashtable<String, Command> map;

	public KeyMap() {
		map = new Hashtable<String, Command>();
	}

	public Command get(char c) {
		return map.get(c + "");
	}

	public void put(char c, Command command) {
		map.put(c + "", command);
	}

}