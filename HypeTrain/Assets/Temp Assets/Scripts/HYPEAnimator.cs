using UnityEngine;
using System.Collections;

public class HYPEAnimator : MonoBehaviour {

	public Sprite HYPE_0;
	public Sprite HYPE_1;
	public Sprite HYPE_2;

	private SpriteRenderer HYPE_sprite;

	public Animator animator;

	void Awake() {
		animator = GetComponent<Animator>();
		HYPE_sprite = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(ScoreKeeper.HYPE == 0) animator.Play ("HYPE_0");
		if(ScoreKeeper.HYPE == 1) animator.Play ("HYPE_1");
		if(ScoreKeeper.HYPE == 2) animator.Play ("HYPE_2");
		if(ScoreKeeper.HYPE == 3 && !ScoreKeeper.HYPED) animator.Play ("HYPE_Full");
		if(ScoreKeeper.HYPE == 3 && ScoreKeeper.HYPED) animator.Play ("HYPE_Active");
	}
}
