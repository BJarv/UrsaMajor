using UnityEngine;
using System.Collections;

public class ColorRandomizer : LogController {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().color = new Color(Random.value+.25f,Random.value+.25f,Random.value+.25f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
