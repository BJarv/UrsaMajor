using UnityEngine;
using System.Collections;

public class particleLayerSwap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingLayerName = "UI";
		particleSystem.renderer.sortingOrder = 2;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
