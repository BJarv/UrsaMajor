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

	List<Vector2> path;
	// Use this for initialization
	void Start () {
		nodeSearch = GetComponent<NodeSearch>();
		target = GameObject.Find ("character");
		StartCoroutine (updatePath());
	}
	
	// Update is called once per frame
	void Update () {

		switch (state) {
			case TuckerState.FOLLOW:
				if(target.tag == "Player") {
					if(Vector2.Distance(transform.position, target.transform.position) > 2f) {
						
					}
				} else if (target.tag == "enemy") {

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
		Debug.Log ("Target is" + target.tag);
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
