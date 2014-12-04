// AUTHOR
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class lazerFire : MonoBehaviour {
		
	//What the lazer collides with
	public LayerMask lazerStoppers;
	LineRenderer lazer;
	public float lazerLength;
		
	void Start () {
		//Grab the linerenderer from the object and default it to disabled
		lazer = gameObject.GetComponent<LineRenderer>();
		lazer.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Fire1")){
			StopCoroutine("Firelazer");
			StartCoroutine("Firelazer");
		}
	}

	//Coroutine to raycast then display lazer
	IEnumerator Firelazer(){
		lazer.enabled = true;
			
		while(Input.GetButton("Fire1")){
			Ray2D ray = new Ray2D(transform.position, transform.right);
			lazer.SetPosition(0, ray.origin);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, lazerLength, lazerStoppers);

			//If the ray collides with something in the lazerStoppers, set the lazer's endpoint to the point of the collision
			if(hit){
				lazer.SetPosition(1, hit.point);
				//If the ray hits an enemy, deal damage
				if(hit.collider.gameObject.tag == "enemy"){
					hit.collider.gameObject.GetComponent<Enemy>().Hurt(50f);
				}
			}
			else //If no collision, set the lazer's endpoint to the lazerlength
				lazer.SetPosition(1, ray.GetPoint(lazerLength));
			yield return null;
		}
		lazer.enabled = false;
	}
}
