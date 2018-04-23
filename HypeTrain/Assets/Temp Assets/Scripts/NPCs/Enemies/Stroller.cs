using UnityEngine;
using System.Collections;

public class Stroller : LogController {

	public float StrollSpeed = .5f;
	public float StrollDist = 15f;  //distance enemy walks back and forth during idle
	[HideInInspector] public int direction = -1; //direction enemy is facing, 1 for right, -1 for left

	private Vector2 StrollStart = new Vector2(0, 0);
	private bool strolling = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!strolling)
		{
			StrollStart = transform.position;
			strolling = true;
		}
		
		if (Vector2.Distance (transform.position, StrollStart) > StrollDist) 
		{
			direction *= -1;
			strolling = false;
		}

		if (direction == -1)
			gameObject.transform.localScale = new Vector3 (-1, 1, 1);
		else
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
		
		transform.position = new Vector3 (transform.position.x + (StrollSpeed * direction), transform.position.y, transform.position.z);	
	}
}
