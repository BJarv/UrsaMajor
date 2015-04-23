using UnityEngine;
using System.Collections.Generic;

public class TrainEnter : MonoBehaviour {
	
	[HideInInspector] public GameObject cameraObj;
	[HideInInspector] public GameObject sidePanel;
	private GameObject trainSpawn;
	private Animator hatchAnimator;

	private bool soundPlayed;
	public AudioClip enterSound;	

	//Find the hatch's Animator
	void Awake() {
		hatchAnimator = transform.parent.gameObject.GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera");
		trainSpawn = GameObject.Find ("trainSpawner");
		sidePanel = GameObject.Find ("sidepanel");
	}

	// Update is called once per frame
	void Update () {

	}
	//Check if E is pressed in trigger zone
	void OnTriggerEnter2D(Collider2D hit) {
		if(Input.GetButton ("Interact")){
			EnteredTrain(hit);
		}
	}
	void OnTriggerStay2D(Collider2D hit) {
		if(Input.GetButton ("Interact")){
			EnteredTrain (hit);
		}
	}
	//What to do once trigger zone is left
	void OnTriggerExit2D(Collider2D hit) {
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.GetComponent<Collider2D>(), false);
	}

	//What to do if E is pressed in trigger
	void EnteredTrain(Collider2D hit){
		if(!soundPlayed){
			AudioSource.PlayClipAtPoint(enterSound, transform.position);
			soundPlayed = true;
		}
		//Special case for entering the Shop Car
		if (transform.parent.transform.parent.transform.parent.name == "ShopCar(Clone)") {
			//Lock camera on the shop car
			cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter(trainSpawn.GetComponent<trainSpawner>().theShopCar));
			sidePanel = trainSpawn.GetComponent<trainSpawner>().headPanel(trainSpawn.GetComponent<trainSpawner>().theShopCar);
		} 
		//Default case
		else {
			//Remove previous train
			trainSpawn.GetComponent<trainSpawner>().KillTrain();
			//Lock camera on the current car
			cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter());
			sidePanel = trainSpawn.GetComponent<trainSpawner>().headPanel();
		}
		hatchAnimator.Play ("Entry"); //Play entry animation once
		//Pass through
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.GetComponent<Collider2D>(), true);
		cameraObj.GetComponent<Camera2D>().setLock(true);
		//Remove side panel

		sidePanel.SetActive(false);
	}
}
