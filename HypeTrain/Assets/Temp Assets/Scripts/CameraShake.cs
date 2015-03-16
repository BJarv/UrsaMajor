using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	//Currently makes use of having 2 objects above the camera. 1 to keep a stationary position to return to, and 1 to transform the
	//localposition of. The camera can then be moved freely without any worry of the camera bump affecting it.

	bool shake = false;
	public float shakeTime = 0.2f;

	public float shakeAmount = 0.5f;

	public bool test = false;

	void Start() {
		StartCoroutine(trainShake());
	}
	
	// Update is called once per frame
	void Update () {
		if(test) {
			test = false;
			bumpIt ();
		}
		if (shake) {
			transform.localPosition = Vector3.zero + Random.insideUnitSphere * shakeAmount;
		} else {
			transform.localPosition = Vector3.zero;
		}
	}

	public void bumpIt() {
		shake = true;
		StartCoroutine(shakeOff (shakeTime));
	}

	public void bumpIt(float time) {
		shake = true;
		StartCoroutine(shakeOff (time));
	}

	IEnumerator shakeOff(float time) {
		yield return new WaitForSeconds(time);
		shake = false;
	}

	IEnumerator trainShake(){
		InvokeRepeating("bumpIt", 0f, 1.2f);
		yield return new WaitForSeconds(0.2f);
		InvokeRepeating ("bumpIt", 0f, 1.2f);
	}
}
