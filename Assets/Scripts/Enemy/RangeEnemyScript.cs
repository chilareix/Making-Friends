using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyScript : EnemyHealth
{
	public GameObject Projectile;
	public BoxCollider2D EnemyCollider;
	public CapsuleCollider2D PlayerCollider;
	public Rigidbody2D PlayerRigidBody;
	public Rigidbody2D RangeEnemyRigidBody;
	public GameObject RangeEnemyGameObject;
	private float ProjectileTimeElapse = 0;
	public float ProjectileLifeTime = 2;

	// Start is called before the first frame update
	void Start()
    {
		base.BStart();
		PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
		PlayerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
	{
		base.BUpdate();
		//Determines time elapsed since last projectile
		ProjectileTimeElapse += Time.deltaTime;
		//Distance between the ranged enemy and player as well as the time since the last projectile has been fired
		if (RangeEnemyRigidBody.Distance(PlayerCollider).distance < 10 && ProjectileTimeElapse > ProjectileLifeTime)
		{
			Instantiate(Projectile, ProjectilePosition(), transform.rotation, transform);
			ProjectileTimeElapse = 0;
		}
		//Deletes enemy game object if a condition is met
			//Falls off map
		if (RangeEnemyRigidBody.position.y < -100) GameObject.Destroy(RangeEnemyGameObject);
			//Runs out of health
		if (CurrentHealth <= 0) GameObject.Destroy(RangeEnemyGameObject);
	}
	Vector3 ProjectilePosition()
	{
		Vector3 TempPos = transform.position;
		TempPos.x = PlayerRigidBody.transform.position.x <= TempPos.x ? 
			TempPos.x - (Projectile.transform.localScale.x / 2 + transform.localScale.x / 2):
			TempPos.x + (Projectile.transform.localScale.x / 2 + transform.localScale.x / 2);

		return TempPos;
	}
}
