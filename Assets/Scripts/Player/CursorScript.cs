using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
	private PlayerControls Controls;
	public Image Cursor;
	private Vector2 ControlBuffer;
	public float CursorSensitivity = 2;

	void Awake()
	{
		Controls = new();

		Controls.ControllerInputs.Cursor.performed += context => ControlBuffer = context.ReadValue<Vector2>() * CursorSensitivity;
		Controls.ControllerInputs.Cursor.canceled += context => ControlBuffer = Vector2.zero;
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
		float cw = Cursor.transform.localScale.x;
		float ch = Cursor.transform.localScale.y;
		float cx = Cursor.transform.position.x;
		float cy = Cursor.transform.position.y;
		float cz = Cursor.transform.position.z;
		
		//Buffer values
		float bx = ControlBuffer.x;
		float by = ControlBuffer.y;
		
		//Resolution Values
		float rw = Screen.width;
		float rh = Screen.height;

		//Cursor new values
		float cx1 = Mathf.Clamp( cx + bx, 0 + cw * 2, rw - cw * 2 );
		float cy1 = Mathf.Clamp( cy + by, 0 + ch * 2, rh - ch * 2 );

		Cursor.transform.position = new Vector3(cx1, cy1, cz);

	}
	public Vector2 GetCursorLocationToWorld()
	{
		return Cursor.transform.localToWorldMatrix.GetPosition();
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
