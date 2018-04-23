using UnityEngine;
using System.Collections;

public class AggroTrigger : LogController {

	void OnTriggerEnter2D(Collider2D colObj) {
		if (colObj.tag == "Player") {
			Transform parent = transform.parent;
			if(parent != null) 
			{
				parent.BroadcastMessage ("aggro");
			}
		}
	}
}