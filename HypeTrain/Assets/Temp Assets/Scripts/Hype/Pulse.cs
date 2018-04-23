using UnityEngine;
using System.Collections;

public class Pulse : LogController {

	Vector3 minSize = new Vector3 (.25f, .25f, .25f);
	Vector3 maxSize = new Vector3 (1f, 1f, 1f);
	Vector3 targetSize;

	// Use this for initialization
	void OnEnable() {
		transform.localScale = minSize;
	}

	void Update() {
		if (transform.localScale.x >= maxSize.x * .9)
			targetSize = minSize;
		else if (transform.localScale.x <= minSize.x * 1.1)
			targetSize = maxSize;
		transform.localScale = Vector3.Lerp (transform.localScale, targetSize, .05f);
	}

}
