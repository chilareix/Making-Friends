using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public float CurrentHealth = 1;
	public float EnemyMaxHealth = 1;
	public Slider HealthBar;
	// Start is called before the first frame update
	protected void BStart()
	{
		HealthBar.maxValue = EnemyMaxHealth;
		HealthBar.value = CurrentHealth;

	}

    // Update is called once per frame
    protected void BUpdate()
    {
		HealthBar.value = CurrentHealth;
	}
	protected void TakeDamage(float DamageToTake)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth - DamageToTake, 0, EnemyMaxHealth);
	}
}
