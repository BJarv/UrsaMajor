using UnityEngine;
using System.Collections;

// <summary>
// Simply moves the current game object
// </summary>

public class Scroller : MonoBehaviour 
{
	
	void Update() 
	{
	}
	
	void FixedUpdate()
	{
		transform.position = new Vector3 (transform.position.x - .05f, transform.position.y, transform.position.z);
	}
}