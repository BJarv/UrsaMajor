using UnityEngine;
using System.Collections;

public class charcontroller : MonoBehaviour {

	public float maxSpeed = 10f;
	//bool facingRight = true;
	// Use this for initialization
	//Animator anim;


	void Start () {
	//	anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, Rigidbody2D.velocity.y); 

		//if (move > 0 && !facingRight) {
		//		Flip ();
		//else if (move < 0 && facingRight)
		//		Flip();
	}

	/*void Flip()
	{
			facingRight = !facingRight;
			Vector3 theScale = Transform.localScale;
			theScale.x *= -1;
			Transform.localScale = theScale;
	}*/
}
