using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TuckerState {
	FOLLOW,
	ATTACK,
	FLY,
	JUMP
}

public class TuckerController: MonoBehaviour {
	private TuckerState state = TuckerState.FOLLOW;
	NodeSearch nodeSearch;
	public GameObject target;
	public float searchFreq = 1f;
	public float withinVertDist = .5f;
	Rigidbody2D rigbod;

	public float addSpeed = 25f;
	public float maxSpeed = 4f;

	List<Vector2> path;
	// Use this for initialization
	void Start () {
		rigbod = GetComponent<Rigidbody2D> ();
		nodeSearch = GetComponent<NodeSearch>();
		target = GameObject.Find ("character");
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), target.GetComponent<Collider2D> ()); //stops collision with player
		//other ignore collisions set in charcontrol
		StartCoroutine (updatePath());
	}
	
	// Update is called once per frame
	void Update () {

		switch (state) {
			case TuckerState.FOLLOW:
				if(target.tag == "Player") {
					if(Vector2.Distance(transform.position, target.transform.position) > 2f || notWithin ()) { //if not right next to player, follow
						follow ();
					}
				} else if (target.tag == "enemy") {
					if(Vector2.Distance(transform.position, target.transform.position) > 2f || notWithin ()) { //if not right next to player, follow
						follow ();
					}
				}
				break;

			case TuckerState.ATTACK:
				
				break;

			case TuckerState.FLY:
				break;

			case TuckerState.JUMP:
				break;
		}

	}

	bool notWithin() { //returns true if player is too far vertically away from player
		if(transform.position.y <= target.transform.position.y - withinVertDist || transform.position.y >= target.transform.position.y + withinVertDist){
			return true;
		}
		return false;
	}

	void follow() {
		Vector2 to;
		if(Vector2.Distance(path[0], transform.position) > .3f) { //if too close to the first node, dont try to path to it
			to = path[0];
		} else {
			to = path[1];
		}
		if(to.x < transform.position.x) { //move left
			if(rigbod.velocity.x > -maxSpeed) {
				rigbod.AddForce(new Vector2(-addSpeed, 0));
			}
		} else if(to.x > transform.position.x) { //move right
			if(rigbod.velocity.x <= maxSpeed) {
				rigbod.AddForce(new Vector2(addSpeed, 0));
			}
		}
	}

	IEnumerator updatePath() { //repeatedly updates path
		path = nodeSearch.search (transform.position, target.transform.position);
		yield return new WaitForSeconds (searchFreq);
		StartCoroutine (updatePath());
	}

	//This will change the target so long as the target isn't an enemy
	public void changeTarget(GameObject t) {
		if (target.tag.Equals ("enemy"))
			return;
		else
			target = t;
		Debug.Log ("Tucker target is " + target.tag);
	}

	void updatePathOnce() { //used to update path when initially changing targets
		path = nodeSearch.search (transform.position, target.transform.position);
	}

	void Flip(float moveH)
	{
		if (moveH > 0)
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		else if (moveH < 0)
			transform.localEulerAngles = new Vector3 (0, 180, 0);
	}
}
