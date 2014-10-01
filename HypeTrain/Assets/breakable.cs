using UnityEngine;
using System.Collections;

public class breakable : MonoBehaviour {

	public float durability = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage(){
		durability--;
		if (durability <= 0) {
			Destroy (gameObject);
		}
	}
}
