using UnityEngine;
using System.Collections;

public class particleLayerSwap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "Default";
		particleSystem.renderer.sortingOrder = 1;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
