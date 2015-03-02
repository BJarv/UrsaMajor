using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeLootDisplay : MonoBehaviour {

	Text total;


	// Use this for initialization
	void Start () {
		total = transform.Find ("amount").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		total.text = "$" + PlayerPrefs.GetInt ("lifetimeLoot");
	}
}
