using UnityEngine;
using System.Collections;

public class getSafeKey : MonoBehaviour {

	public AudioClip kaChing;
	public Sprite gunKey;
	
	private GameObject trainSpawn;
	private SpriteRenderer gunSprite;
	private Itemizer money;

	// Use this for initialization
	void Start () {
		trainSpawn = GameObject.Find ("trainSpawner");
		gunSprite = GameObject.Find ("actual gun").GetComponent<SpriteRenderer>();
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter2D(Collider2D hit){
		//When the player collides with the key, load it in the gun
		if(hit.gameObject.tag == "Player"){
			AudioSource.PlayClipAtPoint(kaChing, transform.position);

			gunSprite.sprite = gunKey; //Change gun sprite to have key in it
			gun.keyLoaded = true;		   //Next fire attempt will shoot key instead of bullet
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D hit){
		//If the key collides with the vault, open it and dispense mad moneys
		if (hit.gameObject.name == "vault") {
			try {
				//Get location to instantiate loot at
				Vector3 vaultLoc = trainSpawn.GetComponent<trainSpawner> ().headVault ();
				int repeat = (int)Random.Range (30 * Multiplier.safeDrop, 60 * Multiplier.safeDrop); //spawn coins between 30 and 60
				while (repeat > 0) {
					money.At (vaultLoc, 1);
					repeat--;
				}
				money.At (vaultLoc, 25);
				//play open safe animation
			
			} catch {
				Debug.Log ("No vault in current car");
			}
			Destroy (gameObject);
		}
	}
}
