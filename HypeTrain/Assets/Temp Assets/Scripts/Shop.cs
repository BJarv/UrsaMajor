using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {
	public GameObject player;
	public RuntimeAnimatorController skin;
	public string HYPEcolor;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("character");
		//skin = transform.Find("skin").GetComponent<SpriteRenderer>().sprite;
		//hypeMode = transform.Find("hype").........
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			Debug.Log ("player entered shop box");
			//HYPEController.HYPEMode = HYPEcolor;
			//player.GetComponent<CharControl>().skinChange (skin);
		}
		else
			Debug.Log ("NOT-player entered shop box");
	}
}
