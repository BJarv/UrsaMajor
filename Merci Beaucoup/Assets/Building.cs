using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public GameObject window;
	public GameObject window1;
	public GameObject window2;
	public GameObject window3;
	public GameObject window4;
	public GameObject window5;
	public GameObject window6;
	public GameObject window7;
	public GameObject window8;

	public GameObject collectible;

	public GameObject player;

	GameObject[] windows;

	// Use this for initialization
	void Start () {

		windows = new GameObject[]{window, window1, window2, window3, window4, window5, window6, window7, window8};
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i <= windows.Length; i++) {
			float r = Random.value;
			if (r < .005f) {
				Vector3 spawnPos = new Vector3(windows[i].transform.position.x, windows[i].transform.position.y, player.transform.position.z);
				Instantiate (collectible, spawnPos, Quaternion.identity);
			}
		}
	
	}
}
