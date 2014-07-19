using UnityEngine;
using System.Collections;

public enum EnemyState
{
	IDLE,
	ATTACK
}

public class BasicEnemy : MonoBehaviour 
{

	private EnemyState State = EnemyState.IDLE;
	public float EnemySpeed = 2f;
	public GameObject Player = null;
	private float AttackDist = 10f;
	// Use this for initialization
	void Start() 
	{
		Player = GameObject.Find("Character");
	}
	
	// Update is called once per frame
	void Update() 
	{
		var distToPlayer = Vector2.Distance (transform.position, Player.transform.position);
		Debug.Log (distToPlayer);
		if (distToPlayer < AttackDist) 
		{
			State = EnemyState.ATTACK;
		}
		else
		{
			State = EnemyState.IDLE;
		}
		Act();
	}
	void Act()
	{
		switch(State)
		{
		case EnemyState.ATTACK: Attack(); break;
		case EnemyState.IDLE: Idle(); break;
		default: Idle();break;

		}
	}
	void Attack()
	{
		if (transform.position.x < Player.transform.position.x) 
		{
			transform.position += transform.right * EnemySpeed * Time.deltaTime;
		} 
		else 
		{
			transform.position += transform.right*-1 * EnemySpeed * Time.deltaTime;
		}

		//transform.LookAt(Player.transform);

	}
	void Idle()
	{
	}
}
