using System.Xml;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Latitude = Horizontal
//Longitude = Vertical

public class parserTest : MonoBehaviour {
	
	XmlDocument doc = new XmlDocument();
	List<Transform> wayObjects = new List<Transform>();
	//public Node n;
	public float x;
	public float y;
	public float boundsX= 34;
	public float boundsY=-118;
	public string mapName = "map2.osm";
	
	public struct Node
	{
	
		public int id;
		public float lat, lon;
		
		public Node(int ID, float LAT, float LON)
		{
			id = ID;
			lat = LAT;
			lon = LON;
			Debug.Log("ID: " + id + ", LAT: " + lat + ", LON: " + lon);
		}
	}
	
	public struct Way
	{
		public int id;
		public List<int> wnodes;
		
		public Way(int ID)
		{
			id = ID;
			wnodes = new List<int>();
		}
	}
	
	public List<Node> nodes = new List<Node>();
	public List<Way> ways = new List<Way>();
	
	void Start () 
	{
		doc.Load(new XmlTextReader("Assets/" + mapName));
		XmlNodeList elemList = doc.GetElementsByTagName("node");
		foreach (XmlNode attr in elemList)
		{
			nodes.Add(new Node(int.Parse(attr.Attributes["id"].InnerText), float.Parse(attr.Attributes["lat"].InnerText), float.Parse(attr.Attributes["lon"].InnerText)));
		}
		
		XmlNodeList wayList = doc.GetElementsByTagName("way");
		int ct = 0;
		foreach (XmlNode node in wayList)
		{
			
			XmlNodeList wayNodes = node.ChildNodes;
			ways.Add(new Way(int.Parse(node.Attributes["id"].InnerText)));
			foreach(XmlNode nd in wayNodes)
			{
				if(nd.Attributes[0].Name == "ref")
				{
					ways[ct].wnodes.Add(int.Parse(nd.Attributes["ref"].InnerText));
					Debug.Log(ways[ct].wnodes.Count);
				}
			}
			ct++;
		}
		for (int i = 0; i < ways.Count; i++)
		{
			wayObjects.Add(new GameObject("wayObject"+ ways[i].id).transform);
			wayObjects[i].gameObject.AddComponent("LineRenderer");
			wayObjects[i].GetComponent<LineRenderer>().SetWidth(0.05f,0.05f);
			wayObjects[i].GetComponent<LineRenderer>().SetVertexCount(ways[i].wnodes.Count);
			for (int j = 0; j < ways[i].wnodes.Count; j++)
			{
				
				foreach (Node nod in nodes)
				{
					if (nod.id == ways[i].wnodes[j])
					{
						//Debug.Log("MATCH!");
						x = nod.lat;
						y = nod.lon;
					}
				}
				wayObjects[i].GetComponent<LineRenderer>().SetPosition(j, new Vector3((x-boundsX)*100,0,(y-boundsY)*100));
			}
		}
		
	}
}
