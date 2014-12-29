using UnityEngine;
using System.Collections;

public class buySkin1 : MonoBehaviour {

	public int price;
	bool guiOn = false;
	public GUIStyle style;

	public bool buyable = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Game.skin1 == false){
				guiOn = true;
				gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Game.skin1 == false){
				guiOn = false;
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}
	void OnTriggerStay2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Input.GetKey (KeyCode.E) && Game.skin1 == false) {
				guiOn = false;
				Game.skin1 = true;
				buyable = true;
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				Game.lifetimeLoot -= price;
				gameObject.GetComponentInParent<Shop>().player.GetComponent<Animator>().runtimeAnimatorController = gameObject.GetComponentInParent<Shop>().skin;
			}
		}
	}
	void OnGUI() {
		if(guiOn){
			GUI.color = Color.black;
			//GUI.Label (new Rect (transform.position.x, transform.position.y - 25, 200, 25), "Price: $" + price, style);
		}
	}
}
