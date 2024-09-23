using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : InventoryItem
{
	public bool Consumable;
	public WeaponType _WeaponType;
	public int Damage = 1;
	public float Range = 0.5f;
	public float Size = 1.0f;
	public Vector2 StartingPositionFromPlayer = Vector2.zero;
	public Rigidbody2D WeaponRigidBody;
	public GameObject _Player;
	public Collider2D _WeaponCollider;
	private Vector2 EndPosition;
	public LayerMask _LayerMask;
	//end position(Range * cos(dir) = x, Range * sin(dir) = y)
	public void Awake()
	{
		IsWeapon = true;
	}
	public void Fire(Vector2 startPosition,	Vector2 fireDirectionVector)
	{
		float fireDirectionFloat = Mathf.Atan(fireDirectionVector.y / fireDirectionVector.x);
		EndPosition = new Vector2(Range * Mathf.Cos(fireDirectionFloat), Range * Mathf.Sin(fireDirectionFloat));
		if (Consumable) Quantity--;
		if (_WeaponType == WeaponType.Projectile) FireProjectile(startPosition, EndPosition);
		if (_WeaponType == WeaponType.ReturningProjectile) FireReturningProjectile(startPosition, EndPosition); 
		if (_WeaponType == WeaponType.Hitscan)  FireHitscan(startPosition, fireDirectionVector); 
	}

	protected void FireProjectile(Vector2 startPosition, Vector2 endPosition)
	{

	}
	protected void FireHitscan(Vector2 startPosition, Vector2 direction, float distance = 10)
	{
		RaycastHit2D raycastHit = Physics2D.Raycast(startPosition, direction, distance, _LayerMask);
		if (raycastHit.collider is null) return;
		if (raycastHit.collider.gameObject.tag == "Enemy")
		{
			raycastHit.collider.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
		}
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
