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
		text.text = "[E] $" + price;
		arm = GameObject.Find("character/gun");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(!PlayerPrefsBool.GetBool ("skin3")){
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
			if(Input.GetButton ("Interact") && !PlayerPrefsBool.GetBool ("skin3") && (PlayerPrefs.GetInt ("lifetimeLoot") - price) >= 0) {
				text.enabled = false;
				PlayerPrefsBool.SetBool ("skin3", true);
				particles.Stop ();
				PlayerPrefs.SetInt ("lifetimeLoot", (PlayerPrefs.GetInt ("lifetimeLoot") - price));
				arm.GetComponentInParent<SpriteRenderer>().sprite = gunArm;
				gameObject.GetComponentInParent<Shop>().player.GetComponent<Animator>().runtimeAnimatorController = gameObject.GetComponentInParent<Shop>().skin;
			}
			if(Input.GetButton ("Interact") && !PlayerPrefsBool.GetBool ("skin3") == true) {
				arm.GetComponentInParent<SpriteRenderer>().sprite = gunArm;
				gameObject.GetComponentInParent<Shop>().player.GetComponent<Animator>().runtimeAnimatorController = gameObject.GetComponentInParent<Shop>().skin;
			}
		}
	}
	
}
