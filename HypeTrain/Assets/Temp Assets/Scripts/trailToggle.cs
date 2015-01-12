using UnityEngine;
using System.Collections;

public class trailToggle : MonoBehaviour {

	public float trailLength = 2;
	TrailRenderer trail;

	void Start () {
		//Grab the linerenderer from the object and default it to disabled
		trail = gameObject.GetComponent<TrailRenderer>();
		trail.enabled = false;
		trail.sortingLayerName = "Player";
		trail.sortingOrder = 1;
	}

	public void On() {
		trail.enabled = true;
		trail.time = trailLength;
	}
	public void Off() {
		trail.enabled = false;
		trail.time = 0;
	}
}
