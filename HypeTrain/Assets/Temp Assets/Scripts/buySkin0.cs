using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buySkin0 : MonoBehaviour {
	
	public int price;
	Text text;
	ParticleSystem particles;
	
	// Use this for initialization
	void Start () {
		particles = transform.Find ("glow").GetComponent<ParticleSystem> ();
		text = transform.Find ("Canvas/Text").GetComponent <Text> ();
		text.text = "$" + price;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(true){
				particles.Play();
			}
		}
	}
	void OnTriggerExit2D(Collider2D colObj){
		if(colObj.tag == "Player"){
			if(true){
				particles.Stop();
			}
		}
	}
	
}
