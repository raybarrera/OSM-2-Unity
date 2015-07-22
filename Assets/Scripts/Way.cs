using System.Collections.Generic;

public struct Way {
	public int id;
	public List<int> wnodes;

	public Way(int ID) {
		id = ID;
		wnodes = new List<int>();
	}
}