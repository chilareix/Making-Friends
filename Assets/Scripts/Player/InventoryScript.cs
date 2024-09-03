using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	private PlayerControls Controls;
	public InventoryItem[] Inventory = new InventoryItem[40];
	private Image[] InventoryImages = new Image[40];
	private Image[] SlotBackgrounds = new Image[40];
	private GameObject[] InventorySlots = new GameObject[40];
	public int SelectedItemIndex = 35;
	private bool InventoryIsOpen = false;
	private Image PanelImage;
	private Color SelectedGray = new Color(0.5f, 0.5f, 0.5f, 0.75f);

	public void Awake()
	{
	}
	// Start is called before the first frame update
	void Start()
	{
		InventorySlots = GameObject.FindGameObjectsWithTag("Inventory Element");
		for(int i = 0; i < InventoryImages.Length; i++)
		{
			InventoryImages.Append(InventorySlots[i].GetComponentInChildren<Image>());
			Debug.Log("Element" + i + " is " + InventoryImages[i]);
			//InventoryImages[i].sprite = Inventory[i].GetComponent<SpriteRenderer>().sprite;
		}
		InventorySlots[SelectedItemIndex].GetComponent<Image>().color = SelectedGray;
		PanelImage = GetComponent<Image>();
		PanelImage.color = Color.clear;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
	}
	public void ToggleInventoryPanel()
	{
		InventoryIsOpen = !InventoryIsOpen;
		if (InventoryIsOpen)
		{
			PanelImage.color = Color.white;
			return;
		}
		InventorySlots[SelectedItemIndex].GetComponent<Image>().color = Color.clear;
		MoveSelectedItem(35);
		InventorySlots[SelectedItemIndex].GetComponent<Image>().color = SelectedGray;
		PanelImage.color = Color.clear;
	}
	void ToggleInventorySlots()
	{
		foreach (Image img in InventoryImages)
		{
			if (InventoryIsOpen)
			{
				img.color = Color.white;
				continue;
			}
			img.color = Color.clear;
		}
	}
	public void MoveSelectedItem(int val)
	{
		InventorySlots[SelectedItemIndex].GetComponent<Image>().color = Color.clear;
		SelectedItemIndex = Mathf.Clamp(SelectedItemIndex + val, 0, InventoryIsOpen? 39 : 4);
		InventorySlots[SelectedItemIndex].GetComponent<Image>().color = SelectedGray;

	}
}
