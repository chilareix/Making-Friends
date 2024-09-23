using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileProjectile : MonoBehaviour
{
	private Vector3 Direction;
	private GameObject PlayerObject;
	private Rigidbody2D PlayerRigidBody;
	public Rigidbody2D ProjectileRigidBody;
	private CapsuleCollider2D PlayerCollider;
	private PlayerInteractions _PlayerInteractions;
	public GameObject HostileProjectileObject;
	private float LifeTime;
	public float ProjectileSpeed;
	public int ProjectileDamage;
	public float MaxDuration;

    void Start()
    {
		PlayerObject = GameObject.FindGameObjectWithTag("Player");
		PlayerRigidBody = PlayerObject.GetComponent<Rigidbody2D>();
		PlayerCollider = PlayerObject.GetComponent<CapsuleCollider2D>();
		_PlayerInteractions = PlayerObject.GetComponent<PlayerInteractions>();
		ProjectileRigidBody.linearVelocity = new Vector2(
			(PlayerRigidBody.transform.position.x - transform.position.x) * ProjectileSpeed,
			(PlayerRigidBody.transform.position.y - transform.position.y) * ProjectileSpeed);
	}

    void FixedUpdate()
    {
		//Determines how long the projectile has existed
		LifeTime += Time.fixedDeltaTime;
		//Detects collision with player
		if (GetComponent<CircleCollider2D>().IsTouching(PlayerCollider))
		{
			//Player takes damage
			_PlayerInteractions.DamageTakenBuffer += ProjectileDamage;
			//Projectile GameObject destroyed
			Destroy(HostileProjectileObject);
		}
		//Projectile GameObject destroyed after exisiting for MaxDuration in seconds
		if (LifeTime >= MaxDuration) Destroy(HostileProjectileObject);
    }
}
