using UnityEngine;
using System.Collections;

public class trailToggle : MonoBehaviour {

	public float trailLength = 2;
	LineRenderer trail;

	void Start () {
		//Grab the linerenderer from the object and default it to disabled
		trail = gameObject.GetComponent<LineRenderer>();
		trail.enabled = false;
		trail.sortingLayerName = "Player";
		trail.sortingOrder = 1;
	}

	public void On() {
		gameObject.GetComponent<TrailRenderer> ().enabled = true;
		gameObject.GetComponent<TrailRenderer> ().time = trailLength;
	}
	public void Off() {
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		gameObject.GetComponent<TrailRenderer> ().time = 0;
	}
}
