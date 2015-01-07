using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buySkin3 : MonoBehaviour {
	
	public int price;
	public Sprite gunArm;
	[HideInInspector] public GameObject arm;

	Text text;
	ParticleSystem particles;
	
	
	// Use this for initialization
	void Start () {
		particles = transform.Find ("glow").GetComponent<ParticleSystem> ();
		text = transform.Find ("Canvas/Text").GetComponent <Text> ();
		text.text = "$" + price;
		arm = GameObject.Find("character/gun");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Game.skin3 == false){
				text.enabled = true;
				particles.Play();
			}
		}
	}
	void OnTriggerExit2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Game.skin3 == false){
				text.enabled = false;
				particles.Stop();
			}
		}
	}
	void OnTriggerStay2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(Input.GetKey (KeyCode.E) && Game.skin3 == false && (Game.lifetimeLoot - price) >= 0) {
				text.enabled = false;
				Game.skin3 = true;
				particles.Stop ();
				Game.lifetimeLoot -= price;
				arm.GetComponentInParent<SpriteRenderer>().sprite = gunArm;
				gameObject.GetComponentInParent<Shop>().player.GetComponent<Animator>().runtimeAnimatorController = gameObject.GetComponentInParent<Shop>().skin;
			}
			if(Input.GetKey (KeyCode.E) && Game.skin3 == true) {
				arm.GetComponentInParent<SpriteRenderer>().sprite = gunArm;
				gameObject.GetComponentInParent<Shop>().player.GetComponent<Animator>().runtimeAnimatorController = gameObject.GetComponentInParent<Shop>().skin;
			}
		}
	}
	
}
