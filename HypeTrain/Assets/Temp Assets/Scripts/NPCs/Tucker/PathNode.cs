using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode : LogController {
	//MAKE SURE TO TURN OFF PLAYER COLLISION WITH THE NODES

	Collider2D myNodeCollider;
	public float checkRadius; 
	public LayerMask nodeLayer;

	public Vector2 getPos() {
		return transform.position;
	}

	public List<Collider2D> neighbors() {
		List<Collider2D> hits = new List<Collider2D>(Physics2D.OverlapCircleAll (transform.position, checkRadius, nodeLayer));
		//One of the things returned is myself. We have the trimHits function to remedy this if we need to.
		hits = trimHits (hits);
		return hits;
	}

	List<Collider2D> trimHits(List<Collider2D> hits) {
		List<Collider2D> trimmedHits = new List<Collider2D> ();
		foreach (Collider2D hit in hits) {
			if (hit != myNodeCollider) {
				trimmedHits.Add(hit);
			}
		}
		return trimmedHits;
	}

	void OnDrawGizmosz() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, checkRadius);
	}
	// Use this for initialization
	void Start () {
		myNodeCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
