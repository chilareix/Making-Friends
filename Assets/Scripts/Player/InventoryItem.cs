using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
	public RectTransform _Transform;
	public Image _Image;
	public int Quantity;
	public int MaxQuantity;
	public SpriteRenderer _SpriteRenderer;


	public void Start()
	{
		_Image = GetComponent<Image>();
		_SpriteRenderer = GetComponent<SpriteRenderer>();
		if (_Image == null || _SpriteRenderer == null) Debug.Log(_Image + " " + _SpriteRenderer);
		_SpriteRenderer.sprite = _Image.sprite;
	}

}
