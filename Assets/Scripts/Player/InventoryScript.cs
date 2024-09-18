using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	public InventoryItem[] Inventory = new InventoryItem[40];
	private GameObject[] InventoryBackgrounds = new GameObject[40];
	private GameObject[] InventoryForegrounds = new GameObject[40];
	public int SelectedItemIndex = 35;
	private bool InventoryIsOpen = false;
	private Image PanelImage;
	private Color SelectedGray = new Color(0.5f, 0.5f, 0.5f, 0.75f);
	private Sprite DefaultSprite;
	private TMP_Text[] Counters = new TMP_Text[40];
	private ChilareixUtilities Util = new ChilareixUtilities();

	// Start is called before the first frame update... And in many practices, such as my own, is used to instantiate
	void Start()
	{
		DefaultSprite = Resources.Load<Sprite>("Sprites/Default");
		InventoryBackgrounds = Util.SortGameObjectsByName(GameObject.FindGameObjectsWithTag("Inventory Element").ToArray());
		InventoryForegrounds = Util.SortGameObjectsByName(GameObject.FindGameObjectsWithTag("Inventory Foreground").ToArray());
		for (int i = 0; i < InventoryBackgrounds.Length; i++)
		{
			InventoryForegrounds[i].GetComponent<Image>().color = Color.clear;
			if (Inventory[i] is not null)
			{
				InventoryForegrounds[i].GetComponent<Image>().sprite = Inventory[i].GetComponent<SpriteRenderer>().sprite;
				continue;
			}

			InventoryForegrounds[i].GetComponent<Image>().sprite = DefaultSprite;

		}
		InventoryBackgrounds[35].GetComponent<Image>().color = SelectedGray;
		PanelImage = GetComponent<Image>();
		PanelImage.color = Color.clear;
		UpdateItems();
	}
	//Toggles inventory background's visibility and changes current selected slot to hotbar if closing
	public void ToggleInventoryPanel()
	{
		InventoryIsOpen = !InventoryIsOpen;
		ToggleInventorySlots();
		if (InventoryIsOpen)
		{
			PanelImage.color = Color.white;
			return;
		}
		InventoryBackgrounds[SelectedItemIndex].GetComponent<Image>().color = Color.clear;
		MoveItemSelector(0);
		InventoryBackgrounds[SelectedItemIndex].GetComponent<Image>().color = SelectedGray;
		PanelImage.color = Color.clear;
	}
	//Toggles inventory slots visiblity
	void ToggleInventorySlots()
	{
		for (int i = 0; i < 35; i++)
		{
			if (InventoryIsOpen)
			{
				InventoryForegrounds[i].GetComponent<Image>().color = Color.white;
				continue;
			}
			InventoryForegrounds[i].GetComponent<Image>().color = Color.clear;
		}
	}
	//Moves the current item selected by increment clamping it within the inventory
	public void MoveItemSelector(int increment)
	{
		InventoryBackgrounds[SelectedItemIndex].GetComponent<Image>().color = Color.clear;
		if  (InventoryIsOpen) SelectedItemIndex = Mathf.Clamp(SelectedItemIndex + increment, 0 , 39);
		if (!InventoryIsOpen) SelectedItemIndex = Mathf.Clamp(SelectedItemIndex + increment, 35, 39);
		InventoryBackgrounds[SelectedItemIndex].GetComponent<Image>().color = SelectedGray;

	}
	//Updates inventory sprites by what is in the "Inventory" array
	public void UpdateItems()
	{
		for (int i = 0; i < InventoryBackgrounds.Length;i++)
		{
			if (Inventory[i] is not null)
			{
				InventoryForegrounds[i].GetComponent<Image>().sprite = Inventory[i].GetComponent<SpriteRenderer>().sprite;
				continue;
			}
			InventoryForegrounds[i].GetComponent<Image>().sprite = DefaultSprite;
		}
	}
	//[WIP] Updates a specific inventory slot or appends an item to the inventory
	public void UpdateItems(InventoryItem item, int index = -1)
	{
		InventoryForegrounds[index].GetComponent<Image>().sprite = Inventory[index].GetComponent<SpriteRenderer>().sprite;
	}
}
