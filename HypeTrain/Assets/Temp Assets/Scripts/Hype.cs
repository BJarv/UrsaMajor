using UnityEngine;
using System.Collections;

abstract public class Hype : MonoBehaviour {

	//	These scripts will be attached to gun and define how gun should shoot

	protected Color color;
	protected GameObject gunGlow;

	protected float intershotDelay = 0f;
	protected float kickForce = 0f;

	protected float hypeDuration = 7f;

	// Use this for initialization
	void Start () {
		gunGlow = transform.Find ("Glow").gameObject;
		gunGlow.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//	EMPTY, override in children
	public abstract float Shoot(GameObject shootFrom);
		
	//	Now you're in HYPE mode
	//	White overrides this function because it has no glow. Other colors do not override this.
	public float Activate() {
		gunGlow.SetActive (true);
		//	Initiate crazy visual effects that we haven't implemented yet (flashing, bright colors, etc.)
		return hypeDuration;
	}

	public void Deactivate() {
		gunGlow.SetActive (false);
	}
}
