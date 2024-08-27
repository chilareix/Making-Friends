using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.ComponentModel;

public class PlayerInteractions : MonoBehaviour
{
	private Weapon _Weapon;

	private PlayerControls Controls;
	public Rigidbody2D PlayerRigidBody;

	public GameObject _CursorObject;
	public CursorScript _Cursor;

	public GameObject Projectile;
	private bool Attacking = false;

	public static int StaminaSpent = 0;
	public static int StaminaRegenned = 0;
	public static int StaminaRegennedNat = 0;

	public static int DamageTaken = 0;
	public static int HealthRegenned = 0;
	public static int HealthRegennedNat = 0;

	public static int ManaSpent = 0;
	public static int ManaRegenned = 0;
	public static int ManaRegennedNat = 0;

	public static int PlayerMaxHealth = 10;
	public static int CurrentPlayerHealth = 10;
	public static int DamageTakenBuffer = 0;
	public static int Armor;
	public static int PlayerMaxStamina = 10;
	public static int CurrentPlayerStamina = 10;
	public static int PlayerMaxMana = 10;
	public static int CurrentPlayerMana = 10;
	private TMP_Text[] TMPArray;
	private Slider[] SliderArray;
	private TMP_Text HealthText;
	private Slider HealthSlider;
	private TMP_Text StaminaText;
	private Slider StaminaSlider;
	private TMP_Text ManaText;
	private Slider ManaSlider;
	private PlayerAnimation AnimationScript;

	void Awake()
	{
		_Cursor = _CursorObject.GetComponent<CursorScript>();

		Controls = new();

		Controls.ControllerInputs.Attack.performed += context => Attacking = true;
		Controls.ControllerInputs.Attack.canceled += context => Attacking = false;
	}
	void Start()
    {
		AnimationScript = GetComponent<PlayerAnimation>();
		SliderArray = Slider.FindObjectsByType<Slider>(FindObjectsSortMode.None);
		TMPArray = TMP_Text.FindObjectsByType<TMP_Text>(FindObjectsSortMode.None);
		for (int i = 0; i < TMPArray.Length; i++)
		{
			if (TMPArray[i].name == "HP Text") HealthText = TMPArray[i];
			if (TMPArray[i].name == "Stamina Text") StaminaText = TMPArray[i];
			if (TMPArray[i].name == "Mana Text") ManaText = TMPArray[i];
		}
		for (int i = 0; i < SliderArray.Length; i++)
		{
			if (SliderArray[i].name == "HP") HealthSlider = SliderArray[i];
			if (SliderArray[i].name == "Stamina") StaminaSlider= SliderArray[i];
			if (SliderArray[i].name == "Mana") ManaSlider= SliderArray[i];
		}
	}

    // Update is called once per frame
    void Update()
	{
		SetStatsInUI();
	}
	private void FixedUpdate()
	{
		TakeDamage();

		//Resets player on death
		if (CurrentPlayerHealth <= 0) PlayerMovement.Reset = true;

		if (Attacking) StartCoroutine(AttackDelayCoroutine());
	}
	public void TakeDamage()
	{
		CurrentPlayerHealth = Mathf.Clamp(CurrentPlayerHealth - (DamageTakenBuffer - Armor), 0, PlayerMaxHealth);
		DamageTakenBuffer = 0;
	}
	//Sets player stats in UI
	public void SetStatsInUI()
	{
		//Sets health
		HealthText.text = CurrentPlayerHealth + " / " + PlayerMaxHealth;
		HealthSlider.maxValue = PlayerMaxHealth;
		HealthSlider.value = CurrentPlayerHealth;
		//Sets stamina
		StaminaText.text = CurrentPlayerStamina + " / " + PlayerMaxStamina;
		StaminaSlider.maxValue = PlayerMaxStamina;
		StaminaSlider.value = CurrentPlayerStamina;
		//Sets mana
		ManaText.text = CurrentPlayerMana+ " / " + PlayerMaxMana;
		ManaSlider.maxValue = PlayerMaxMana;
		ManaSlider.value = CurrentPlayerMana;
	}

	IEnumerator AttackDelayCoroutine()
	{
		while (Attacking)
		{
			yield return new WaitForSeconds(0.13f);
			//Attack(_Weapon);
			AnimationScript.Throwing = true;
		}
		AnimationScript.Throwing = false;
	}

	void Attack(Weapon weapon)
	{
		Vector2 playerPos = PlayerRigidBody.position;
		Vector2 cursorPos = _Cursor.GetCursorLocationToWorld();
		_Weapon.Fire(playerPos, Mathf.Atan( (cursorPos.y - playerPos.y) / (cursorPos.x - playerPos.x) ));
	}

	public void OnEnable()
	{
		Controls.ControllerInputs.Enable();
	}
	public void OnDisable()
	{
		Controls.ControllerInputs.Disable();
	}
}
