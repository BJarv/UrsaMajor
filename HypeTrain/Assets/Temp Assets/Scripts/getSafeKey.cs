using UnityEngine;
using System.Collections;

public class getSafeKey : MonoBehaviour {

	public AudioClip kaChing;
	private GameObject trainSpawn;
	private Itemizer money;
	// Use this for initialization
	void Start () {
		trainSpawn = GameObject.Find ("trainSpawner");
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D hit){

		if(hit.gameObject.tag == "Player"){
			AudioSource.PlayClipAtPoint(kaChing, transform.position);
			try {
				Vector3 vaultLoc = trainSpawn.GetComponent<trainSpawner>().headVault();
				int repeat = (int)Random.Range (30, 60); //spawn coins between 1 and 5
				while(repeat > 0){
					money.At (vaultLoc, 0); //0 for coin
					repeat -= 1;
				}
				//play open safe animation

			} catch {
				Debug.Log ("No vault in current car");
			}
			Destroy (gameObject);
		}
	}
}
