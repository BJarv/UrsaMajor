using UnityEngine;
using System.Collections;

public class Itemizer : MonoBehaviour {
	
	public GameObject coin;      //int 0 for coin
	public GameObject safeKey;   //int 1 for safeKey
	
	GameObject nuObj;
	public float xMin = -400;
	public float xMax = 400;
	public float yMin = 1300;
	public float yMax = 1600;
	
	public void At(Vector3 here, int itemNum)
	{
		if (itemNum == 0) {
			nuObj = (GameObject)Instantiate (coin, here, Quaternion.identity); //spawn item
			nuObj.rigidbody2D.AddForce (new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax)));//pop it up
		}
		else if (itemNum == 1) {
			nuObj = (GameObject)Instantiate (safeKey, here, Quaternion.identity); //spawn item
			nuObj.rigidbody2D.AddForce (new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax)));//pop it up
		}
	}
}
