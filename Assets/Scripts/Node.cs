public struct Node {
	public int id;
	public float lat, lon;

	public Node(int ID, float LAT, float LON) {
		id = ID;
		lat = LAT;
		lon = LON;
//		Debug.Log("ID: " + id + ", LAT: " + lat + ", LON: " + lon);
	}
}
