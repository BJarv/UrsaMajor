using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

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
