using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	private PlayerControls Controls;
	public InventoryItem[] Inventory = new InventoryItem[34];
	public SpriteRenderer[] SpriteRenderers = new SpriteRenderer[34];
	public GameObject SelectedItemGameObject;
	public int SelectedItemIndex = 0;
	private bool IsInventoryOpen = false;

	public void Awake()
	{
		Controls = new();
		Controls.ControllerInputs.InventorySelect.performed += context => SelectedItemIndex = Mathf.Clamp(SelectedItemIndex + context.ReadValue<int>(), 0, 4);
		Controls.ControllerInputs.InventorySelect.canceled += context => SelectedItemIndex += 0;
	}
	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(IsInventoryOpen) DisplayInventory();
		DisplayHotbar();
	}
	void DisplayInventory()
	{
		if (IsInventoryOpen)
		{

		}
	}
	void DisplayHotbar()
	{

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
