using UnityEngine;
using System.Collections;

public class GetBonus : MonoBehaviour {
	public AudioClip kaChing;
	public int value = 100;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D hit){
		//Debug.Log ("\n");
		//Debug.Log (hit.gameObject.tag);
		//Debug.Log ("\n");
		if(hit.gameObject.tag == "Player"){
			Debug.Log ("getBonus: Collided with player.");
			AudioSource.PlayClipAtPoint(kaChing, Camera.main.transform.position);
			ScoreKeeper.Score += value;
			Game.addLoot (value);
			Destroy (transform.parent.gameObject);
		}
	}
}
