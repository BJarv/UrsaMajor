using UnityEngine;
using System.Collections;

public class getWidthCar : MonoBehaviour {

	public GameObject left;
	public GameObject right;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float carWidth(){

		return right.transform.position.x - left.transform.position.x;
	}
}
