// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HYPEAnimator : LogController {

	private Animator animator;

	void Awake() {
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(ScoreKeeper.HYPE == 0) animator.Play ("HYPE_0");
		if(ScoreKeeper.HYPE == 2) animator.Play ("HYPE_1");
		if(ScoreKeeper.HYPE == 4) animator.Play ("HYPE_2");
		if(ScoreKeeper.HYPE == 6 && !ScoreKeeper.HYPED) animator.Play ("HYPE_Full");
		if(ScoreKeeper.HYPE == 6 && ScoreKeeper.HYPED) animator.Play ("HYPE_Active");
	}
}
