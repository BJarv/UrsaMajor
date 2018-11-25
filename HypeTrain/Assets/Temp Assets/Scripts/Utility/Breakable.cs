using UnityEngine;
using System.Collections;

public class Breakable : LogController
{
    [Tooltip("How many hits the breakable can take before breaking.")]
    public float durability = 1f;

    [Tooltip("If true, the player colliding with the object at a fast enough speed will cause it to break.")]
    public bool glass = false;

    [Tooltip("Should the breakable drop cash when destroyed?")]
    public bool dropCash = false;

    [Tooltip("If specified, the breakable will only take damage from the specified projectile.")]
    public GameObject requiredProjectile = null;

    //Break effects
    public AudioClip hitSFX;
    public AudioClip breakSFX;
    public GameObject breakParticles;
    public Animator breakAnimator;

    //Used for spawning cash
    [HideInInspector] public Itemizer money;

	// Use this for initialization
	void Start ()
	{
		money = Camera.main.transform.GetComponent<Itemizer> ();
	}

    public void Damage(GameObject projectile)
    {
        //Handle durability changes
        if (requiredProjectile == null)
        {
            durability--;
        }
        else if (projectile.name == requiredProjectile.name + "(Clone)")
        {
            durability--;
        }

        //Call Break() if object is out of durability
        if (durability > 0)
        {
            //Play break sound
            if (hitSFX != null) AudioSource.PlayClipAtPoint(hitSFX, transform.position);
        }
        else if (durability <= 0)
        {
            Break();
        }
	}

	void OnTriggerEnter2D (Collider2D colObj)
	{
        //If player smashes into the "glass" object, call Break()
		if (glass && colObj.tag == "Player" && colObj.GetComponent<Rigidbody2D> ().velocity.x > 15) Break ();
	}

    //Handles Break FX, loot drop, and destruction
	void Break ()
	{
        //Play break sound
        if (breakSFX != null) AudioSource.PlayClipAtPoint(breakSFX, transform.position);

        //Play break particles
        if (breakParticles != null) Instantiate(breakParticles, transform.position, Quaternion.identity);

        //Drop cash
        if (dropCash) money.At(transform.position, Random.Range((int)(1 * Multiplier.moneyDrop), (int)(5 * Multiplier.moneyDrop)));

        //If there's an animator, use its "OnBreak" animation for destruction
        if (breakAnimator != null) {
            breakAnimator.Play("OnBreak");
            gameObject.GetComponent<Collider2D>().enabled = false;
            dropCash = false;
        }
        else {
            Destroy(gameObject);
        }
	}
}
