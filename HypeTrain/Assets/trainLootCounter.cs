using UnityEngine;
using System.Collections;

public class trainLootCounter : MonoBehaviour {
	public GUIStyle style;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.color = Color.black;
		//GUI.Label (new Rect (transform.position.x, transform.position.y, 500, 50), "Total Loot: $" + Game.lifetimeLoot, style);
	}
}
