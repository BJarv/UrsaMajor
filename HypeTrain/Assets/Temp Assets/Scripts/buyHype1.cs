using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buyHype1 : MonoBehaviour {
	
	public int price;
	Text text;
	ParticleSystem particles;
	
	// Use this for initialization
	void Start () {
		particles = transform.Find ("glow").GetComponent<ParticleSystem> ();
		text = transform.Find ("Canvas/Text").GetComponent <Text> ();
		text.text = "[E] $" + price;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Game.hype1 == false){
			if(!PlayerPrefsBool.GetBool("hype1")){
				text.enabled = true;
				particles.Play();
			} else {
				text.enabled = true;
				text.text = "OWNED";
			}
		}
	}
	void OnTriggerExit2D(Collider2D colObj){
		if(colObj.tag == "Player"){
				text.enabled = false;
				particles.Stop();
		}
	}
	void OnTriggerStay2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Input.GetKey (KeyCode.E) && Game.hype1 == false && (Game.lifetimeLoot - price) >= 0) {
			if(Input.GetKey (KeyCode.E) && !PlayerPrefsBool.GetBool ("hype1") && (PlayerPrefs.GetInt ("lifetimeLoot") - price) >= 0) {
				text.enabled = false;
				Game.hype1 = true;
				PlayerPrefsBool.SetBool ("hype1", true);
				particles.Stop ();
				Game.lifetimeLoot -= price;
				PlayerPrefs.SetInt ("lifetimeLoot", (PlayerPrefs.GetInt ("lifetimeLoot") - price));
				HYPEController.HYPEMode = gameObject.GetComponentInParent<Shop>().HYPEColor;
			}
			if(Input.GetKey (KeyCode.E) && Game.hype1 == false) {
			if(Input.GetKey (KeyCode.E) && !PlayerPrefsBool.GetBool ("hype1")) {
				HYPEController.HYPEMode = gameObject.GetComponentInParent<Shop>().HYPEColor;
			}
		}
	}
	
}
