using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TuckerState {
	FOLLOW,
	ATTACK,
	FLY
}

public class TuckerController: MonoBehaviour {
	GameObject player;
	public TuckerState state = TuckerState.FOLLOW;
	NodeSearch nodeSearch;
	public GameObject target;
	public float searchFreq = 1f;
	public float withinVertDist = .5f;
	Rigidbody2D rigbod;

	int towardTarget = 0;

	public float addSpeedX = 35f;
	public float maxSpeedX = 18f;
	public float addSpeedY = 200f;
	public float maxSpeedY = 30f;

	public LayerMask wallMask;
	public float wallCheckLength;

	public AudioClip bark1;
	public AudioClip bark2;
	public AudioClip bark3;
	public AudioClip bark4;
	AudioSource audio;

	bool attackOnCD = false;
	float attackCD = .5f;

	List<Vector2> path;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("character");
		audio = GetComponent<AudioSource> ();
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
		float distToPlayer = Vector2.Distance (player.transform.position, transform.position);
		if(distToPlayer > 25f && distToPlayer < 100f) {
			state = TuckerState.FLY;
		} else if(distToPlayer > 100f) {
			transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 25f);
		}
		if (target) {
			switch (state) {
				case TuckerState.FOLLOW:
					if(target.tag == "Player") {
						if(Vector2.Distance(transform.position, target.transform.position) > 2f && notWithin ()) { //if not right next to player, follow
							follow ();
						} else {
							Debug.Log ("in else of follow player");
						}
					} else if (target.tag == "enemy") {
						if (Vector2.Distance (transform.position, target.transform.position) > 2f || notWithin ()) { //if not right next to player, follow
							follow ();
						} else {
							if (!attackOnCD) {
								state = TuckerState.ATTACK;
								int random = Random.Range (0,101);
								if (random < 33) audio.PlayOneShot (bark1);
								else if (random >= 33 && random < 66) audio.PlayOneShot(bark2);
								else if (random >= 66 && random < 99) audio.PlayOneShot (bark3);
								else audio.PlayOneShot (bark4);
								attackOnCD = true;
								StartCoroutine (attackOffCD ());
							}
						}
					}
					break;

				case TuckerState.ATTACK:
					//If the target still exists...
					//Move toward the target. Collision constitutes attacking.
					transform.position = Vector3.MoveTowards (transform.position, target.transform.position, .3f);
					//If you somehow get too far away, follow again.
					if (Vector2.Distance (transform.position, target.transform.position) > 2f || notWithin ()) {
						state = TuckerState.FOLLOW;
					}
				break;

				case TuckerState.FLY:
					Physics2D.IgnoreLayerCollision (18, 12, true); //ignore tucker collision with trains
					Physics2D.IgnoreLayerCollision (18, 9, true); //ignore tucker collision with trains
					Physics2D.IgnoreLayerCollision (18, 0, true);
					rigbod.gravityScale = 0;	
					transform.position = Vector2.MoveTowards (transform.position, player.transform.position, 1.3f);
					target = player;
					if (Vector2.Distance (transform.position, target.transform.position) < 2f) {
						Physics2D.IgnoreLayerCollision (18, 12, false); //unignore tucker collision with trains
						Physics2D.IgnoreLayerCollision (18, 9, false);
						Physics2D.IgnoreLayerCollision (18, 0, false);
						rigbod.gravityScale = 3;
						state = TuckerState.FOLLOW;
					}
					break;

			
			}
		} else {
			target = player;
		}
		if (target.transform.position.x < transform.position.x && towardTarget != -1) { //if target switches sides, update path
			updatePathOnce ();
			towardTarget = -1;
		} else if (target.transform.position.x > transform.position.x && towardTarget != 1) {
			updatePathOnce ();
			towardTarget = 1;
		}
	}

	void FixedUpdate () {
		Flip (rigbod.velocity.x);
	}

	bool notWithin() { //returns true if player is too far vertically away from player
		if(transform.position.y <= target.transform.position.y - withinVertDist || transform.position.y >= target.transform.position.y + withinVertDist){
			return true;
		}
		return false;
	}

	void follow() {
		Vector2 to = Vector2.zero; //target node
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
		} else if(path.Count == 1) {
			to = path[0];
		} else {
			//no path found
		}
		if((to != Vector2.zero) && (to.x < transform.position.x)) { 		//move left
			Debug.Log ("moveleft");
			if(rigbod.velocity.x > -maxSpeedX) {
				rigbod.AddForce(new Vector2(-addSpeedX, 0));
			}
			if(target.tag == "Player" && isGrounded () && (Random.Range (0, 10) > 7)) {//random hops
				rigbod.AddForce (new Vector2(0, 200f));
			}
		} else if((to != Vector2.zero) && (to.x > transform.position.x)) { //move right
			Debug.Log ("moveright");
			if(rigbod.velocity.x <= maxSpeedX) {
				rigbod.AddForce(new Vector2(addSpeedX, 0));
			}
			if(target.tag == "Player" && isGrounded () && (Random.Range (0, 10) > 7)) {//random hops
				rigbod.AddForce (new Vector2(0, 200f));
			}
		} else { //no path found, just head toward target however you can
			if(target.transform.position.x < transform.position.x) { //left
				if(rigbod.velocity.x > -maxSpeedX) {
					rigbod.AddForce(new Vector2(-addSpeedX, 0));
				}
			} else { 												//right
				if(rigbod.velocity.x <= maxSpeedX) {
					rigbod.AddForce(new Vector2(addSpeedX, 0));
				}
			}
		}
		if((to != Vector2.zero) && (target.transform.position.y > transform.position.y) && nearWall()) { //jump if wall
			if(rigbod.velocity.y <= maxSpeedY) {
				rigbod.AddForce(new Vector2(0, addSpeedY));
			}
		} else if ((to != Vector2.zero) && to.y > transform.position.y + 2f) { //if target node is above current position, jump
			if(rigbod.velocity.y <= maxSpeedY) {
				rigbod.AddForce(new Vector2(0, addSpeedY));
			}
		}
	}

	public bool isGrounded()
	{
		return Physics2D.Raycast (transform.position, -Vector2.up, 1f, wallMask);
	}

	bool nearWall() {
		return Physics2D.Raycast (transform.position, transform.right, wallCheckLength, wallMask);
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
		//if (path.Count > 0) {
		//	Gizmos.DrawWireSphere (path [1], 1f);
		//}
		for(int i = 0; i < path.Count - 1 && path.Count != 0; i++) {
			Gizmos.DrawLine(path[i], path[i+1]);
		}
	}
}
