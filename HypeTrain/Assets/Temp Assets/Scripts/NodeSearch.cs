using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeSearch : MonoBehaviour {
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
		Dictionary<path_node, float> dist = new Dictionary<path_node, float>();
		Dictionary<path_node, path_node> prev = new Dictionary<path_node, path_node>();
		//queue for BFS
		Queue<path_node> q = new Queue<path_node> ();
		Queue<path_node> visited = new Queue<path_node> ();
		//setup initial values for search
		dist [sn.GetComponent<path_node>()] = 0;
		prev [sn.GetComponent<path_node>()] = null;
		q.Enqueue (sn.GetComponent<path_node>());

		while (q.Count > 0) { //while there are still nodes to search
			//in the case of coroutine, yield if taking too long
			path_node temp = q.Dequeue();
			List<Collider2D> neighbors = temp.transform.GetComponent<path_node>().neighbors();
			visited.Enqueue(temp);
			foreach (Collider2D neighbor in neighbors) {
				//Debug.Log ("neighbor: " + neighbor.name);
				if(!visited.Contains (neighbor.transform.GetComponent<path_node>())){
					//Debug.Log ("in contains if");
					q.Enqueue(neighbor.transform.GetComponent<path_node>());
					prev[neighbor.transform.GetComponent<path_node>()] = temp;
					if(neighbor.transform.GetComponent<path_node>() == dn.GetComponent<path_node>()) {
						Debug.Log ("path found");
						//return shortest path
						path_node tracer = neighbor.transform.GetComponent<path_node>();
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
		Debug.Log ("NO PATH FOUND, OH LORD");

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
