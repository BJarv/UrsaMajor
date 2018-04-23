using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeSearch : LogController {
	public GameObject node;
	private GameObject sn;
	private GameObject dn;


	// Use this for initialization
	void Start () {
		//InvokeRepeating ("doSearch", 1f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<Vector2> search(Vector2 s, Vector2 d) { //takes in a source and a destination and returns a list of path points
		//path list
		List<Vector2> path = new List<Vector2>(); 
		if (Vector2.Distance (transform.position, GetComponent<TuckerController> ().target.transform.position) < .3f) {
			return path;
		}
		//instantiate nodes at source and dest to make complete path
		if(!sn) {
			sn = (GameObject)Instantiate (node, s, Quaternion.identity); 
			dn = (GameObject)Instantiate (node, d, Quaternion.identity);
		} else {
			sn.transform.position = s;
			dn.transform.position = d;
		}
		//dist and previous dictionaries
		Dictionary<PathNode, float> dist = new Dictionary<PathNode, float>();
		Dictionary<PathNode, PathNode> prev = new Dictionary<PathNode, PathNode>();
		//queue for BFS
		Queue<PathNode> q = new Queue<PathNode> ();
		Queue<PathNode> visited = new Queue<PathNode> ();
		//setup initial values for search
		dist [sn.GetComponent<PathNode>()] = 0;
		prev [sn.GetComponent<PathNode>()] = null;
		q.Enqueue (sn.GetComponent<PathNode>());

		while (q.Count > 0) { //while there are still nodes to search
			//in the case of coroutine, yield if taking too long
			PathNode temp = q.Dequeue();
			List<Collider2D> neighbors = temp.transform.GetComponent<PathNode>().neighbors();
			visited.Enqueue(temp);
			foreach (Collider2D neighbor in neighbors) {
				//Debug.Log ("neighbor: " + neighbor.name);
				if(!visited.Contains (neighbor.transform.GetComponent<PathNode>())){
					//Debug.Log ("in contains if");
					q.Enqueue(neighbor.transform.GetComponent<PathNode>());
					prev[neighbor.transform.GetComponent<PathNode>()] = temp;
					if(neighbor.transform.GetComponent<PathNode>() == dn.GetComponent<PathNode>()) {
						Debug.Log ("path found");
						//return shortest path
						PathNode tracer = neighbor.transform.GetComponent<PathNode>();
						while(prev[tracer] != null) { //construct shortest path by going up prev list
							//Debug.Log ("adding to path: " + tracer.getPos ());
							path.Add(tracer.getPos());
							tracer = prev[tracer];
						}
						//Destroy(sn); //destroy temp source and dest nodes
						//Destroy(dn);
						//sn.transform.position = new Vector2(0, 1000f); //move rather than destroy nodes
						//dn.transform.position = new Vector2(0, 1000f);
						path.Reverse ();
						return path;
					}
				}
			}

		}
		//Debug.Log ("NO PATH FOUND, OH LORD");

		//Destroy(sn); //destroy temp source and dest nodes
		//Destroy(dn);
		//sn.transform.position = new Vector2(0, 1000f); //move rather than destroy nodes
		//dn.transform.position = new Vector2(0, 1000f);
		return path;
	}

	void OnDrawGizmos() {
		//Gizmos.color = Color.cyan;
		// highlight thePath[1] in red
		//if (thePath.Count > 0) {
		//	Gizmos.DrawWireSphere (thePath [0], 1f);
		//}
		//for(int i = 0; i < thePath.Count - 1 && thePath.Count != 0; i++) {
		//	Gizmos.DrawLine(thePath[i], thePath[i+1]);
		//}
	}

	/*List<Vector2> thePath;
	public Transform a;
	public Transform b;



	void doSearch() {
		thePath = search (a.position, b.position);
		//Debug.Log ("First node: " + thePath[1]);
		if(thePath.Count == 0) {
			CancelInvoke();
		}
	}*/
}
