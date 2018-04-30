using UnityEngine;
using System.Collections;

abstract public class Hype : LogController {

	//	These scripts will be attached to gun and define how gun should shoot

	protected Color color;

	protected float intershotDelay = 0f;
	protected float kickForce = 0f;

	protected float hypeDuration = 7f;

    // Use this for initialization
    protected abstract void Start();
	
	// Update is called once per frame
	void Update () {
	
	}

	//	EMPTY, override in children
	public abstract float Shoot(GameObject shootFrom);
		
	public float GetKickForce() {
		return kickForce;
	}

	public Color GetColor() {
		return color;
	}
}
