using UnityEngine;
using System.Collections;

public class GetSafeKey : LogController {

	public AudioClip kaChing;
	public Sprite gunKey;
	
	private GameObject trainSpawn;
	private SpriteRenderer gunSprite;
	private Itemizer money;
    private float timer = 0;

	// Use this for initialization
	void Start () {
		trainSpawn = GameObject.Find ("TrainSpawner");
		gunSprite = GameObject.Find ("Gun").GetComponent<SpriteRenderer>();
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
        Invoke("ActivateCollider", .2f); //Delay trigger activation so player doesn't immediately pickup when firing
	}
	
	// Update is called once per frame
	void Update () {
        
	}
	
	void OnTriggerEnter2D(Collider2D hit){
		//When the player collides with the key, load it in the gun
		if(hit.gameObject.tag == "Player"){
			AudioSource.PlayClipAtPoint(kaChing, Camera.main.transform.position);

			gunSprite.sprite = gunKey; //Change gun sprite to have key in it
			Gun.keyLoaded = true;		   //Next fire attempt will shoot key instead of bullet
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D hit){
		//If the key collides with the vault, open it and dispense mad moneys
		if (hit.gameObject.name == "vault") {
			try {
				//Get location to instantiate loot at
				Vector3 vaultLoc = trainSpawn.GetComponent<TrainSpawner> ().headVault ();
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

    void ActivateCollider() { GetComponent<BoxCollider2D>().enabled = true; }
}
