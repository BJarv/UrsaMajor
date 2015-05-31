using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeSearch : MonoBehaviour {
	public GameObject node;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("doSearch", 1f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	List<Vector2> search(Vector2 s, Vector2 d) { //takes in a source and a destination and returns a list of path points
		//path list
		List<Vector2> path = new List<Vector2>(); 
		//instantiate nodes at source and dest to make complete path
		GameObject sn = (GameObject)Instantiate (node, s, Quaternion.identity); 
		GameObject dn = (GameObject)Instantiate (node, d, Quaternion.identity);
		//dist and previous dictionaries
		Dictionary<path_node, float> dist = new Dictionary<path_node, float>();
		Dictionary<path_node, path_node> prev = new Dictionary<path_node, path_node>();
		//queue for BFS
		Queue<path_node> q = new Queue<path_node> ();
		Queue<path_node> visited = new Queue<path_node> ();
		//setup initial values for search
		dist [sn.GetComponent<path_node>()] = 0;
		prev [dn.GetComponent<path_node>()] = null;
		q.Enqueue (sn.GetComponent<path_node>());

		while (q.Count > 0) { //while there are still nodes to search
			Debug.Log ("in main while");
			path_node temp = q.Dequeue();
			List<Collider2D> neighbors = temp.transform.GetComponent<path_node>().neighbors();
			visited.Enqueue(temp);
			foreach (Collider2D neighbor in neighbors) {
				Debug.Log ("neighbor: " + neighbor.name);
				if(!visited.Contains (neighbor.transform.GetComponent<path_node>())){
					Debug.Log ("in contains if");
					q.Enqueue(neighbor.transform.GetComponent<path_node>());
					prev[neighbor.transform.GetComponent<path_node>()] = temp;
					if(neighbor.transform.GetComponent<path_node>() == dn.GetComponent<path_node>()) {
						Debug.Log ("path found");
						//return shortest path
						path_node tracer = neighbor.transform.GetComponent<path_node>();
						while(prev[tracer] != null) { //construct shortest path by going up prev list
							Debug.Log ("in traceback");
							path.Add(tracer.getPos());
							tracer = prev[tracer];
						}
						Destroy(sn); //destroy temp source and dest nodes
						Destroy(dn);
						foreach(Vector2 pos in path) {
							Debug.Log ("node position: " + pos);
						}
						return path;
					}
				}
			}

		}
		Debug.LogError ("NO PATH FOUND, OH LORD");

		Destroy(sn); //destroy temp source and dest nodes
		Destroy(dn);
		return path;
	}



	List<Vector2> thePath;
	public Transform a;
	public Transform b;

	void OnDrawGizmos() {
		for(int i = 0; i < thePath.Count - 1; i++) {
			Gizmos.DrawLine(thePath[i], thePath[i+1]);
		}
	}

	void doSearch() {
		thePath = search (a.position, b.position);
		if(thePath.Count == 0) {
			CancelInvoke();
		}
	}
}
