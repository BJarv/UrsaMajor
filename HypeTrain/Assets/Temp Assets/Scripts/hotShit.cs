using UnityEngine;
using System.Collections;

public class hotShit : MonoBehaviour {

	private GameObject player = null;
	public float knockUpForce;

	void Start () {
		player = GameObject.Find("character");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//When character lands on this surface, deal damage, apply knockup and hitAnim
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "character") {
			col.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			col.gameObject.rigidbody2D.AddForce(new Vector2(0, knockUpForce));	
			player.GetComponent<CharControl>().hitAnim();
		}
	}
	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.name == "character") {
			col.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			col.gameObject.rigidbody2D.AddForce(new Vector2(0, knockUpForce));	
			player.GetComponent<CharControl>().hitAnim();	
		}
	}
}
