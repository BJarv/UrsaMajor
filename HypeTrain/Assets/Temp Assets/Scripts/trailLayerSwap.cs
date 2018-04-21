using UnityEngine;
using System.Collections;

public class TrailLayerSwap : MonoBehaviour {

	public int sortLayer = 2; //1 is rendered in front of all sprites on particles layer, 2 is rendered on obstacles layer, 3 is rendered behind train
	public bool renderForward = false; //false for setting effect behind set layer, true for effect in front of set layer
	// Use this for initialization
	void Start () {
		if (renderForward) {
			gameObject.GetComponent<TrailRenderer>().GetComponent<Renderer>().sortingOrder = 1;
		} else {
			gameObject.GetComponent<TrailRenderer>().GetComponent<Renderer>().sortingOrder = -1;
		}
		if (sortLayer == 1){
			gameObject.GetComponent<TrailRenderer>().GetComponent<Renderer>().sortingLayerName = "Particles";
		} else if (sortLayer == 2) {
			gameObject.GetComponent<TrailRenderer>().GetComponent<Renderer>().sortingLayerName = "Obstacles";
		} else if (sortLayer == 3){
			gameObject.GetComponent<TrailRenderer>().GetComponent<Renderer>().sortingLayerName = "Default";
		} else {
			Debug.Log ("bad input for sortLayer");
		}
		
	}
	
	
	// Update is called once per frame
	void Update () {

	}
}
