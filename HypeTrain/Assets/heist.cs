using UnityEngine;
using System.Collections;

public class heist : MonoBehaviour {
	
	public Component[] laserBounds;
	// Use this for initialization
	void Start () {
		laserBounds = GetComponentsInChildren<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (BoxCollider2D bound in laserBounds) {
			//Might need
		}
	}
	
	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			gameObject.SetActive(false);
			Debug.Log ("BUSTED");
		}
	}
}
