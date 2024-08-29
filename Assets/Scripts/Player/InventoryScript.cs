using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	private PlayerControls Controls;
	public InventoryItem[] Inventory = new InventoryItem[40];
	public SpriteRenderer[] SpriteRenderers = new SpriteRenderer[40];
	public GameObject SelectedItemGameObject;
	public int SelectedItemIndex = 0;
	private bool IsInventoryOpen = false;
	private Image InventoryImage;

	public void Awake()
	{
	}
	// Start is called before the first frame update
	void Start()
	{
		InventoryImage = GetComponent<Image>();
		InventoryImage.enabled = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
	}
	public void ToggleInventory()
	{
		IsInventoryOpen = !IsInventoryOpen;
		if (IsInventoryOpen)
		{
			InventoryImage.enabled = true;
			return;
		}
		InventoryImage.enabled = false;
	}
}
