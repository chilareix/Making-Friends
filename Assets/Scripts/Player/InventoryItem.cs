using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
	public Transform _Transform;
	public int Quantity;
	public int MaxQuantity;
	public SpriteRenderer _SpriteRenderer;
	public bool IsWeapon = false;

	public void Start()
	{
		_Transform = GetComponent<Transform>();
		_SpriteRenderer = GetComponent<SpriteRenderer>();
		if (_SpriteRenderer == null) Debug.Log(_SpriteRenderer.name);
	}

}
