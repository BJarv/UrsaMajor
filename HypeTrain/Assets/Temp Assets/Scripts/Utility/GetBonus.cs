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
		if(hit.gameObject.tag == "Player"){
			AudioSource.PlayClipAtPoint(kaChing, Camera.main.transform.position);
			ScoreKeeper.Score += value;
			Game.addLoot (value);
			Destroy (transform.parent.gameObject);
		}
	}
}
