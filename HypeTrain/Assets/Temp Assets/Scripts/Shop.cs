using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	[HideInInspector] public GameObject player;
	public RuntimeAnimatorController skin;
	public string HYPEColor;

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
		try {
			if(!gameObject.transform.Find("restricted").GetComponent<SpriteRenderer>().enabled) {
				if(colObj.tag == "Player"){
					Debug.Log ("player entered shop box");
					if(skin){
						player.GetComponent<Animator>().runtimeAnimatorController = skin;
					} else if (HYPEColor != "") {
						HYPEController.HYPEMode = HYPEColor;
					} else {
						Debug.Log ("shop item with no attachment!");
					}
					//gameObject.GetComponent<ParticleSystem>().Play();
					//player.GetComponent<CharControl>().skinChange (skin);
				} else {
					Debug.Log ("NOT-player entered shop box");
				}
			}
		} catch {

		}
	}
}
