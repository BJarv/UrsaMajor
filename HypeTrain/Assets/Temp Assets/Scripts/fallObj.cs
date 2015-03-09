using UnityEngine;
using System.Collections;

public class fallObj : MonoBehaviour {

	public float damageSpeed;
	public float playerDamage;
	public float enemyDamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (rigidbody2D.velocity.y);	
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if(colObj.tag == "player" && GetComponent<Rigidbody2D>().velocity.y <= -damageSpeed) {
			colObj.gameObject.GetComponent<Enemy>().Hurt(playerDamage);
		}
		if(colObj.tag == "enemy" && GetComponent<Rigidbody2D>().velocity.y <= -damageSpeed) {
			Debug.Log ("BONK!");
			colObj.gameObject.GetComponent<Enemy>().Hurt(enemyDamage);
		}
	}
}
