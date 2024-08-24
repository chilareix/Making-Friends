using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Weapon : InventoryItem
{

	protected WeaponType _WeaponType;
	public int Damage = 1;
	public float Range = 0.5f;
	public float Size = 1.0f;
	public Vector2 StartingPositionFromPlayer = Vector2.zero;
	public Rigidbody2D WeaponRigidBody;

	public void Fire(Vector2 startPosition, float fireDirection)
	{
		if (_WeaponType == WeaponType.Projectile)
			FireProjectile(startPosition, new Vector2(Range*Mathf.Cos(fireDirection), Range*Mathf.Sin(fireDirection)));
	}

	protected void FireProjectile(Vector2 startPosition, Vector2 endPosition)
	{

	}
	protected void FireHitscan(Vector2 startPosition, Vector2 endPosition)
	{

	}
	protected void FireReturningProjectile(Vector2 startPosition, Vector2 endPosition)
	{

	}

	public WeaponType GetWeaponType() { return _WeaponType; }

	public enum WeaponType
	{
		ReturningProjectile,
		Hitscan,
		Projectile
	}
	
}
