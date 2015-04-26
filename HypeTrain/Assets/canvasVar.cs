using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvasVar : MonoBehaviour {

	private Text var;

	public bool cars;
	public bool kills;

	// Use this for initialization
	void Start () {
		var = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cars) var.text = ScoreKeeper.carsCompleted.ToString();
		else if(kills) var.text = ScoreKeeper.enemiesKilled.ToString();
	}
}