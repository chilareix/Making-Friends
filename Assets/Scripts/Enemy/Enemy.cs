using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	public float CurrentHealth = 1;
	public float EnemyMaxHealth = 1;
	public Slider HealthBar;
	protected GameObject PlayerObject;
	protected PlayerInteractions _PlayerInteractions;
	protected CapsuleCollider2D PlayerCollider;
	public Collider2D EnemyCollider;
	public GameObject EnemyGameObject;
	public Rigidbody2D EnemyRigidBody;

	// Start is called before the first frame update
	public void BStart()
	{
		PlayerObject = GameObject.FindGameObjectWithTag("Player");
		_PlayerInteractions = PlayerObject.GetComponent<PlayerInteractions>();
		PlayerCollider = PlayerObject.GetComponent<CapsuleCollider2D>();
		HealthBar.maxValue = EnemyMaxHealth;
		HealthBar.value = CurrentHealth;

	}

	// Update is called once per frame
	public void BUpdate()
	{
		HealthBar.value = CurrentHealth;
	}
	public void TakeDamage(float DamageToTake)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth - DamageToTake, 0, EnemyMaxHealth);
	}

}
