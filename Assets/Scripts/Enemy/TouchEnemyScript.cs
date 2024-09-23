using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEnemyScript : Enemy
{
	private bool ContactBroken = true;
	private double TimeSinceDmgTick = 0;

    // Start is called before the first frame update
    void Start()
    {
		base.BStart();
    }

    // Update is called once per frame
    void Update()
    {
		base.BUpdate();

    }
	public void FixedUpdate()
	{
		//Deletes enemy game object if a condition is met
		if (EnemyRigidBody.position.y < -100) GameObject.Destroy(EnemyGameObject);
		if (CurrentHealth <= 0) GameObject.Destroy(EnemyGameObject);

		//Detects if player touches and adds damage upon touch
		if (EnemyCollider.IsTouching(PlayerCollider) && !ContactBroken)
		{
			_PlayerInteractions.DamageTakenBuffer += 1;
		}
		if (Vector2.Distance(PlayerCollider.transform.position, EnemyCollider.transform.position) < 10)
		{
			MoveTowardsPlayer();
		}

	}

	private void MoveTowardsPlayer()
	{
		if(PlayerCollider.transform.position.x - EnemyCollider.transform.position.x < 0)
		{
			EnemyRigidBody.AddForce(Vector2.left);
		}
		if (PlayerCollider.transform.position.x - EnemyCollider.transform.position.x > 0)
		{
			EnemyRigidBody.AddForce(Vector2.right);
		}
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && Time.timeAsDouble + 1 > TimeSinceDmgTick)
		{
			_PlayerInteractions.DamageTakenBuffer += 1;
		}
	}
}
