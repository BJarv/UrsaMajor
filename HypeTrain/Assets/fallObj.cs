using UnityEngine;
using System.Collections;

public class fallObj : MonoBehaviour {

	public float damageSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (rigidbody2D.velocity.y);	
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if(colObj.tag == "player" && rigidbody2D.velocity.y <= -damageSpeed) {
			colObj.gameObject.GetComponent<Enemy>().Hurt(10f);
		}
		if(colObj.tag == "enemy" && rigidbody2D.velocity.y <= -damageSpeed) {
			Debug.Log ("BONK!");
			colObj.gameObject.GetComponent<Enemy>().Hurt(50f);
		}
	}
}
