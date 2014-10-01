using UnityEngine;
using System.Collections;

public class coinSpawn : MonoBehaviour {
		
	public GameObject coin;
	GameObject nuCoin;
	public float xMin = -400;
	public float xMax = 400;
	public float yMin = 1300;
	public float yMax = 1600;

	public void At(Vector3 here)
	{
		nuCoin = (GameObject)Instantiate(coin, here, Quaternion.identity); //spawn coin
		nuCoin.rigidbody2D.AddForce (new Vector2(Random.Range (xMin, xMax), Random.Range (yMin, yMax)));//pop it up
	}
}
