using UnityEngine;
using System.Collections;

public class magnetic : MonoBehaviour {

	public GameObject player;
	public bool magnetized = false;
	public float speed;
	// Use this for initialization
	void Start () {
		speed = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		if(magnetized){
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
		}

	}
}
