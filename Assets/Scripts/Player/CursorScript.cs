using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
	private PlayerControls Controls;
	public Image Cursor;
	private Vector2 ControlBuffer;
	public float CursorSensitivity = 0.25f;
	private Vector3 CursorPos = Vector3.zero;
	private RectTransform CursorRectTransform;

	void Awake()
	{
		Controls = new();

		Controls.ControllerInputs.Cursor.performed += context => ControlBuffer = context.ReadValue<Vector2>() * CursorSensitivity;
		Controls.ControllerInputs.Cursor.canceled += context => ControlBuffer = Vector2.zero;
	}
	void Start()
	{
		CursorRectTransform = GetComponent<RectTransform>();
	}

    // Update is called once per frame
    void Update()
    {
		MoveCursor();
    }

	//This might be the worst code for memory that I've made, but it's clean, so it's staying... for now
	void MoveCursor()
	{
		//Cursor old values
		float cx = CursorRectTransform.anchoredPosition.x;
		float cy = CursorRectTransform.anchoredPosition.y;
		float cz = 0;
		
		//Buffer values
		float bx = ControlBuffer.x;
		float by = ControlBuffer.y;
		
		//Resolution Values
		float rw = Screen.width;
		float rh = Screen.height;

		//Cursor new values
		float cx1 = Mathf.Clamp( cx + bx, -rw/2, rw/2 );
		float cy1 = Mathf.Clamp( cy + by, -rh/2, rh/2 );

		CursorPos = new Vector3(cx1, cy1, cz);

		CursorRectTransform.anchoredPosition = CursorPos;
		
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
