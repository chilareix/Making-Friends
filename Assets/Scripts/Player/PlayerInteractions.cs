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
	private bool AttackEnable = true;
	private bool Attacking = false;

	public int StaminaSpent = 0;
	public int StaminaRegenned = 0;
	public int StaminaRegennedNat = 0;

	private int DamageTaken = 0;
	private int HealthRegenned = 0;
	private int HealthRegennedNat = 0;

	private int ManaSpent = 0;
	private int ManaRegenned = 0;
	private int ManaRegennedNat = 0;

	public int PlayerMaxHealth = 10;
	public int CurrentPlayerHealth = 10;
	public int DamageTakenBuffer = 0;
	public int Armor;
	public int PlayerMaxStamina = 10;
	public int CurrentPlayerStamina = 10;
	public int PlayerMaxMana = 10;
	public int CurrentPlayerMana = 10;
	private TMP_Text[] TMPArray;
	private Slider[] SliderArray;
	private TMP_Text HealthText;
	private Slider HealthSlider;
	private TMP_Text StaminaText;
	private Slider StaminaSlider;
	private TMP_Text ManaText;
	private Slider ManaSlider;
	private PlayerAnimation AnimationScript;
	private InventoryScript Inventory;
	private PauseMenu _PauseMenu;

	void Awake()
	{
		Controls = new();

		Controls.ControllerInputs.Escape.performed += context => _PauseMenu.TogglePause();

		Controls.ControllerInputs.InventorySelect.performed += context => Inventory.MoveItemSelector(Mathf.CeilToInt(context.ReadValue<float>()));

		Controls.ControllerInputs.InventoryToggle.performed += context => Inventory.ToggleInventoryPanel();

		Controls.ControllerInputs.Attack.performed += context => Attacking = true;
		Controls.ControllerInputs.Attack.canceled += context => Attacking = false;
	}
	void Start()
	{
		_PauseMenu = GameObject.FindGameObjectWithTag("HUD").GetComponent<PauseMenu>();
		_Cursor = _CursorObject.GetComponent<CursorScript>();
		Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryScript>();
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

		if (Attacking && AttackEnable) StartCoroutine(AttackDelayCoroutine());
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
		AttackEnable = false;
		while (Attacking)
		{
			yield return new WaitForSeconds(0.13f);
			if (Inventory.GetSelectedItem().IsWeapon) {
				Attack((Weapon) Inventory.GetSelectedItem());
				AnimationScript.Throwing = true;
			}
		}
		AnimationScript.Throwing = false;
		AttackEnable = true;
	}

	void Attack(Weapon weapon)
	{
		RectTransform canvas = GameObject.FindGameObjectWithTag("HUD").GetComponent<RectTransform>();
		Vector2 playerPos = PlayerRigidBody.position;
		Vector3 canvasCursorPos = _Cursor.GetComponent<RectTransform>().localPosition;
		Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, canvas.TransformPoint(canvasCursorPos));
		Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10));
		
		weapon.Fire(playerPos, ((Vector2)cursorWorldPos - playerPos));
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
