using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LootUI : MonoBehaviour {
	private Text loot;
	// Use this for initialization
	void Start () {
		loot = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		loot.text = "$" + ScoreKeeper.Score;
	}
}
