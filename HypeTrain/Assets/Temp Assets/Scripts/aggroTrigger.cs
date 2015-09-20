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

	}
}
