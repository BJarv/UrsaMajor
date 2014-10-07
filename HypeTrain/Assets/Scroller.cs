using UnityEngine;
using System.Collections;

// <summary>
// Changes transform.position for an object
// </summary>

public class Scroller : MonoBehaviour 
{

	public float scrollSpeed;
	
	void Update() 
	{
	}
	
	void FixedUpdate()
	{
		Debug.Log (transform.position.x);
		transform.position = new Vector2(transform.position.x - scrollSpeed,transform.position.y);
	}
}
