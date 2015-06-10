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

	int towardTarget = 0;

	public float addSpeed = 25f;
	public float maxSpeed = 4f;

	bool attackOnCD = false;
	float attackCD = .5f;

	List<Vector2> path;
	// Use this for initialization
	void Start () {
		rigbod = GetComponent<Rigidbody2D> ();
		nodeSearch = GetComponent<NodeSearch>();
		target = GameObject.Find ("character");
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), target.GetComponent<Collider2D> ()); //stops collision with player
		//other ignore collisions set in charcontrol for things such as nodes and projectiles
		if (target.transform.position.x < transform.position.x) {
			towardTarget = -1;
		} else {
			towardTarget = 1;
		}
		StartCoroutine (updatePath());
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			switch (state) {
			case TuckerState.FOLLOW:
				if (target.tag == "Player") {
					if (Vector2.Distance (transform.position, target.transform.position) > 2f || notWithin ()) { //if not right next to player, follow
						if (path.Count > 1) {
							follow ();
						}
					} else {
						Debug.Log ("in else of follow player");
					}
				} else if (target.tag == "enemy") {
					if (Vector2.Distance (transform.position, target.transform.position) > 2f || notWithin ()) { //if not right next to player, follow
						follow ();
					} else {
						if (!attackOnCD) {
							attack ();
							attackOnCD = true;
							StartCoroutine (attackOffCD ());
						}
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
			if (target.transform.position.x < transform.position.x && towardTarget != -1) { //if target switches sides, update path
				updatePathOnce ();
				towardTarget = -1;
			} else if (target.transform.position.x > transform.position.x && towardTarget != 1) {
				updatePathOnce ();
				towardTarget = 1;
			}
		} else
			target = GameObject.Find ("character");
	}

	bool notWithin() { //returns true if player is too far vertically away from player
		if(transform.position.y <= target.transform.position.y - withinVertDist || transform.position.y >= target.transform.position.y + withinVertDist){
			return true;
		}
		return false;
	}

	void follow() {
		Vector2 to;
		if(path.Count > 2){ //since no priority queue in C#, must check 3 nodes deep to ensure that search doesnt get stuck by searching one node back first.
			if(nodeBetweenTarget(path[0])) {
				to = path[0];
				Debug.Log ("path0");
			} else if (nodeBetweenTarget(path[1])) {
				to = path[1];
				updatePathOnce();
				Debug.Log ("path1");
			} else {
				to = path[2];
				updatePathOnce();
				Debug.Log ("path2");
			}
		} else if(path.Count > 1){
			if(nodeBetweenTarget(path[0])) {
				to = path[0];
				Debug.Log ("path0");
			} else {
				to = path[1];
				updatePathOnce();
				Debug.Log ("path1");
			} 
		} else {
			to = path[0];
		}
		if(to.x < transform.position.x) { //move left
			Debug.Log ("moveleft");
			if(rigbod.velocity.x > -maxSpeed) {
				rigbod.AddForce(new Vector2(-addSpeed, 0));
			}
		} else if(to.x > transform.position.x) { //move right
			Debug.Log ("moveright");
			if(rigbod.velocity.x <= maxSpeed) {
				rigbod.AddForce(new Vector2(addSpeed, 0));
			}
		}
		//if(target.
	}

	void attack() {
		//If the target still exists...
		if (target) {
			//Move toward the target. Collision constitutes attacking.
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, .3f);
			//If you somehow get too far away, follow again.
			if (Vector2.Distance (transform.position, target.transform.position) > 2f || notWithin ()) {
				state = TuckerState.FOLLOW;
			}
		} else {
			changeTarget(GameObject.Find ("character"));
		}
	}

	bool nodeBetweenTarget(Vector2 node) { //returns true if node given is between dog and target
		if((target.transform.position.x < node.x && node.x < transform.position.x) || (target.transform.position.x > node.x && node.x > transform.position.x)){
			return true;
		} else {
			return false;
		}

	}

	IEnumerator updatePath() { //repeatedly updates path
		Debug.Log ("searching");
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

	void OnCollisionEnter2D(Collision2D col) {
		Collider2D colObj = col.collider;
		Debug.Log ("NEVER GOES HERE");
		if(colObj.tag == "enemy") {
			colObj.gameObject.GetComponent<Enemy>().Hurt(10f);
			if(transform.position.x - colObj.transform.position.x > 0)
			{
				colObj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 375));
				rigbod.AddForce (new Vector2(200, 375));
			}
			else if(transform.position.x - colObj.transform.position.x < 0)
			{
				colObj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 375));
				rigbod.AddForce (new Vector2(-200, 375));
			}
			colObj.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		}
		state = TuckerState.FOLLOW;
	}

	IEnumerator attackOffCD() {
		yield return new WaitForSeconds (attackCD);
		attackOnCD = false;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		//highlight thePath[1] in red
		if (path.Count > 0) {
			Gizmos.DrawWireSphere (path [1], 1f);
		}
		for(int i = 0; i < path.Count - 1 && path.Count != 0; i++) {
			Gizmos.DrawLine(path[i], path[i+1]);
		}
	}
}
