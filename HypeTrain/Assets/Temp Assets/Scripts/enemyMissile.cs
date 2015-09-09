using UnityEngine;
using System.Collections;

public class enemyMissile : MonoBehaviour {

	//Player Reference
	[HideInInspector] public Transform player;

	//Collision/lifetime variables
	public int missileDeath = 3;
	[HideInInspector] public int layerOfTrigs = 8; //8 is the triggers layer
	[HideInInspector] public int layerOfLoot = 14; //14 is the Loot layer
	[HideInInspector] public int layerOfProj = 13; //13 is the Projectiles layer

	public float smoothrate = 0.5f;
	public Vector2 velocity = new Vector2 (0.5f, 0.5f);
	private Vector2 newPos2D = Vector2.zero;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("character").transform;
		Destroy (gameObject, missileDeath);
	}
	
	// Update is called once per frame
	void Update () {
		newPos2D.x = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, smoothrate);
		newPos2D.y = Mathf.SmoothDamp (transform.position.y, player.position.y, ref velocity.y, smoothrate);

		transform.position = new Vector3 (newPos2D.x, newPos2D.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		
		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == layerOfTrigs || colObj.gameObject.layer == layerOfLoot || colObj.gameObject.layer == layerOfProj) {
			return;
		}
		if(colObj.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().HurtPlus(10, gameObject);
			Destroy (gameObject);
		}
		//If it hits a breakable object
		if (colObj.GetComponent<Collider2D>().tag == "breakable") {
			colObj.gameObject.GetComponent<breakable>().Damage();
			Destroy (gameObject);
		} else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
	}
}
