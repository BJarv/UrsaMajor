using UnityEngine;
using System.Collections;

public class breakable : MonoBehaviour {

	public bool glass = false;
	public bool dropCash = false;

	public float durability = 1f;
	public Itemizer money; 
	// Use this for initialization
	void Start () {
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(){
		durability--;
		if (durability <= 0) {
			if (dropCash){
				money.At(transform.position, 0);
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if(colObj.tag == "Player" && glass && colObj.rigidbody2D.velocity.x > 15){
			Destroy (gameObject);
		}
	}
}
