using System.Xml;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParserTest : MonoBehaviour {
	
	private XmlDocument doc = new XmlDocument();
	private List<Transform> wayObjects = new List<Transform>();

	[SerializeField]
	private float x;
	[SerializeField]
	private float y;
	[SerializeField]
	private float boundsX = 34;
	[SerializeField]
	private float boundsY = -118;
	[SerializeField]
	private string mapName = "map2.osm";
	
	private List<Node> nodes = new List<Node>();
	private List<Way> ways = new List<Way>();
	
	private void Start () {
		doc.Load(new XmlTextReader("Assets/" + mapName));
		XmlNodeList elemList = doc.GetElementsByTagName("node");

		for (int i = 0; i < elemList.Count; i++) {
			nodes.Add(new Node(int.Parse(elemList[i].Attributes["id"].InnerText),
				float.Parse(elemList[i].Attributes["lat"].InnerText),
				float.Parse(elemList[i].Attributes["lon"].InnerText)
			));
		}

		XmlNodeList wayList = doc.GetElementsByTagName("way");
		int ct = 0;
		foreach (XmlNode node in wayList) {			
			XmlNodeList wayNodes = node.ChildNodes;
			ways.Add(new Way(int.Parse(node.Attributes["id"].InnerText)));
			foreach(XmlNode nd in wayNodes) {
				if(nd.Attributes[0].Name == "ref") {
					ways[ct].wnodes.Add(int.Parse(nd.Attributes["ref"].InnerText));
					Debug.Log(ways[ct].wnodes.Count);
				}
			}
			ct++;
		}

		for (int i = 0; i < ways.Count; i++) {
			wayObjects.Add(new GameObject("wayObject"+ ways[i].id).transform);
			wayObjects[i].gameObject.AddComponent("LineRenderer");
			wayObjects[i].GetComponent<LineRenderer>().SetWidth(0.05f,0.05f);
			wayObjects[i].GetComponent<LineRenderer>().SetVertexCount(ways[i].wnodes.Count);
			for (int j = 0; j < ways[i].wnodes.Count; j++) {				
				foreach (Node nod in nodes) {
					if (nod.id == ways[i].wnodes[j]) {
						x = nod.lat;
						y = nod.lon;
					}
				}
				wayObjects[i].GetComponent<LineRenderer>().SetPosition(j, new Vector3((x-boundsX)*100,0,(y-boundsY)*100));
			}
		}		
	}
}
