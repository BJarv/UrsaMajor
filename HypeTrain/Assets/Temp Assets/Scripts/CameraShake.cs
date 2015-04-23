using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	//Currently makes use of having 2 objects above the camera. 1 to keep a stationary position to return to, and 1 to transform the
	//localposition of. The camera can then be moved freely without any worry of the camera bump affecting it.
	
	bool shake = false;
	bool altShake = false;
	public float shakeTime = 0.2f;
	
	public float shakeAmount = 0.13f;
	public float altShakeAmount = 2f;
	
	public bool test = false;
	
	void Start() {
		StartCoroutine(trainShake());
	}
	
	// Update is called once per frame
	void Update () {
		if(test) {
			test = false;
			bumpIt(altShakeAmount);
		}
		if (altShake){
			transform.localPosition = Vector3.zero + Random.insideUnitSphere * altShakeAmount;
		} else if (shake) {
			transform.localPosition = Vector3.zero + Random.insideUnitSphere * shakeAmount; 
		} else {
			transform.localPosition = Vector3.zero;
		}
		
	}
	
	
	public void bumpIt(float intensity = 2f, float time = .1f) {
		altShakeAmount = intensity;
		altShake = true;
		StartCoroutine(altShakeOff (time));
	}
	
	void trainBumpIt() {
		shake = true;
		StartCoroutine(shakeOff (shakeTime));
	}
	
	IEnumerator shakeOff(float time) {
		yield return new WaitForSeconds(time);
		shake = false;
	}
	
	IEnumerator altShakeOff(float time) {
		yield return new WaitForSeconds(time);
		altShake = false;
	}
	
	IEnumerator trainShake(){
		InvokeRepeating("trainBumpIt", 0f, 1.4f);
		yield return new WaitForSeconds(0.2f);
		InvokeRepeating ("trainBumpIt", 0f, 1.4f);
	}

	public void stopAllShake() { //stop any shake that is currently happening
		shake = false;
		altShake = false;
		transform.localPosition = Vector3.zero;
	}
	
}
