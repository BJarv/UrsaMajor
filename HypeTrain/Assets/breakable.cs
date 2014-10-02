using UnityEngine;
using System.Collections;

public class breakable : MonoBehaviour {

	public bool glass = false;
	public float durability = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(){
		durability--;
		if (durability <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if(colObj.tag == "Player" && glass && colObj.rigidbody2D.velocity.x > 15){
			Destroy (gameObject);
		}
	}
}
