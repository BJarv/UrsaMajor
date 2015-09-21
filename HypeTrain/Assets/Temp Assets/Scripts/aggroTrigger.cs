using UnityEngine;
using System.Collections;

public class aggroTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D colObj) {
		if (colObj.tag == "Player") {
			Transform parent = transform.parent;
			if(parent != null) 
			{
				parent.BroadcastMessage ("aggro");
			}
		}
<<<<<<< HEAD
	
=======

>>>>>>> ac6ed25318199103c6845ae22a2108584be446b2
	}
}
