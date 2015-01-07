using UnityEngine;
using System.Collections;

public class trailToggle : MonoBehaviour {

	public float trailLength = 2;

	public void On() {
		gameObject.GetComponent<TrailRenderer> ().enabled = true;
		gameObject.GetComponent<TrailRenderer> ().time = trailLength;
	}
	public void Off() {
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		gameObject.GetComponent<TrailRenderer> ().time = 0;
	}
}
