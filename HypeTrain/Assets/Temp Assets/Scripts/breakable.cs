using UnityEngine;
using System.Collections;

public class breakable : MonoBehaviour {

	public float durability = 1f;
	public bool glass = false;
	public bool dropCash = false;
	public GameObject requiredProjectile = null;

	public Animator breakAnimator;
	public AudioClip breakSound;


	[HideInInspector] public Itemizer money; 
	// Use this for initialization
	void Start () {
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Deal damage if there is no specific projectile or projectile matches required one
	public void Damage(GameObject projectile){
		if (requiredProjectile == null) {
			durability--; //subtract one point from durability on hit
		} else if (projectile.name == requiredProjectile.name + "(Clone)") {
			durability--;
		}

		//If durability is zero, play animation if it exists, drop cash, and destroy object
		if (durability <= 0) {
			if (breakAnimator != null){ //Do this if animator exists
				if (breakSound != null){ 
					AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
				}
				if (dropCash){ //Drop cash if true
					money.At(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), Random.Range ((int)(1 * Multiplier.moneyDrop),(int)(5 * Multiplier.moneyDrop)));
				}
				breakAnimator.Play ("Bin_Opening");
				gameObject.GetComponent<Collider2D>().enabled = false;
				dropCash = false;
			} else {  //Do this if there is no animator to play
				if (dropCash){ //Drop cash if true
					money.At(transform.position, Random.Range ((int)(1 * Multiplier.moneyDrop),(int)(5 * Multiplier.moneyDrop)));
				}
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if(colObj.tag == "Player" && glass && colObj.GetComponent<Rigidbody2D>().velocity.x > 15){
			Destroy (gameObject);
		}
	}
}
