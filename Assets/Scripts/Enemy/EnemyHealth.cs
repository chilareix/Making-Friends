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
	void Start()
	{
		HealthBar.maxValue = EnemyMaxHealth;
		HealthBar.value = CurrentHealth;

	}

    // Update is called once per frame
    void Update()
    {
		HealthBar.value = CurrentHealth;
	}
	void TakeDamage(float DamageToTake)
	{
		CurrentHealth = Mathf.Clamp(CurrentHealth - DamageToTake, 0, EnemyMaxHealth);
	}
}
