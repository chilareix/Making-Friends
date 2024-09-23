using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private PlayerControls Controls;

	private bool PlayerHasJumped = false;
	public Camera _Camera;

	public float MoveSpeedAmplifier;
	private float MoveSpeedTimesFifty;

	public float PlayerWalkSpeed;
	public float PlayerRunSpeed;
	private float PlayerMaxSpeed;
	private bool StartedRunning;
	private bool InputRunning;
	private bool Crouching;

	public float PlayerJumpAmplifier;
	private float PlayerJumpAmpTimesFifty;
	private bool PlayerInputtingJump;
	private float QFall;
	private bool Grounded;

	private bool IsStaminaRegenning;

	private PlayerInteractions _PlayerInteractions;
	private Rigidbody2D PlayerRigidBody;
	private CapsuleCollider2D PlayerCollider;

	public Vector2 StartPoint = new(0, 0.25f);
	public Vector2 Checkpoint = new(0, 0.25f);
	public static bool Reset = false;

	private float HorizontalFloat;

	private PlayerAnimation AnimationScript;
	private Transform PlayerTransform;


	void Awake()
	{
		_PlayerInteractions = GetComponent<PlayerInteractions>();
		QFall = 0;
		IsStaminaRegenning = false;

		InputRunning = false;
		PlayerInputtingJump = false;

		StartedRunning = false;
		PlayerMaxSpeed = PlayerWalkSpeed;
		MoveSpeedTimesFifty = PlayerWalkSpeed * 50;

		PlayerHasJumped = false;
		PlayerJumpAmpTimesFifty = PlayerJumpAmplifier * 50;

		PlayerCollider = GetComponent<CapsuleCollider2D>();
		PlayerRigidBody = GetComponent<Rigidbody2D>();

		Controls = new();

		Controls.ControllerInputs.Crouch.performed += context => Crouching = true;
		Controls.ControllerInputs.Crouch.canceled += context => Crouching = false;

		Controls.ControllerInputs.Horizontal.performed += context => HorizontalFloat = context.ReadValue<float>();
		Controls.ControllerInputs.Horizontal.canceled += context => HorizontalFloat = 0f;

		Controls.ControllerInputs.Jump.performed += context => PlayerInputtingJump = true;
		Controls.ControllerInputs.Jump.canceled += context => PlayerInputtingJump = false;

		Controls.ControllerInputs.Sprint.performed += context => InputRunning = true;
		Controls.ControllerInputs.Sprint.canceled += context => InputRunning = false;

		Controls.ControllerInputs.QuickFall.performed += context => QFall = 1000f;
		Controls.ControllerInputs.QuickFall.canceled += context => QFall = 0f;
	}

	// Start is called before the first frame update
	void Start()
	{
		PlayerTransform = GetComponent<Transform>();
		AnimationScript = GetComponent<PlayerAnimation>();
		_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		MoveSpeedTimesFifty = MoveSpeedAmplifier * 50;
		PlayerJumpAmpTimesFifty = PlayerJumpAmplifier * 50;
		PlayerMaxSpeed = PlayerWalkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		ApplyCrouch();

		if (Reset)
		{
			Reset = false;
			PlayerReset();
		}

		MoveHorizontally(HorizontalFloat);
		if (PlayerInputtingJump) PlayerJump();
		if (InputRunning) PlayerSprint();
		if (!InputRunning)
		{
			StartedRunning = false;
			if(!IsStaminaRegenning) StartCoroutine(RegenStaminaCoroutine());
		}

		//Maximum velocity implementation
		PlayerRigidBody.linearVelocity = new Vector2(PlayerRigidBody.linearVelocity.x, Mathf.Clamp(PlayerRigidBody.linearVelocity.y, -PlayerMaxSpeed - QFall, float.MaxValue));


		//Some code that helps ensure the player jumps appropriately
		if (PlayerRigidBody.linearVelocity.y >= 0) PlayerHasJumped = false;

		//Allows the player to fall quicker
		if (!PlayerCollider.IsTouchingLayers() && QFall > 0f) PlayerRigidBody.AddForce(Vector2.down * 100);

		//Resets the player's position if he or she falls too far off the map
		if (PlayerRigidBody.position.y < -100)
		{
			PlayerRigidBody.position = Checkpoint;
			PlayerRigidBody.linearVelocity = Vector2.zero;
			_Camera.transform.position = new Vector3(PlayerRigidBody.position.x, PlayerRigidBody.position.y + 5, -10);
			_PlayerInteractions.DamageTakenBuffer += (int) Mathf.Ceil(_PlayerInteractions.PlayerMaxHealth/2);
		}
	}

	private void ApplyCrouch()
	{
		float yScale = PlayerTransform.localScale.y;
		float xScale = PlayerTransform.localScale.x;
		PlayerTransform.localScale = new Vector3(xScale, 
												 Mathf.MoveTowards(yScale, Crouching? 0.5f: 1, 0.266f),
												 1);

	}

	public void PlayerReset()
	{
		PlayerRigidBody.position = StartPoint;
		PlayerRigidBody.linearVelocity = Vector2.zero;
		_PlayerInteractions.CurrentPlayerHealth = _PlayerInteractions.PlayerMaxHealth;
		_Camera.transform.position = new Vector3(PlayerRigidBody.position.x, PlayerRigidBody.position.y + 5, -10);
	}

	//Horizontal movement code
		//Moves the player horizontally based on input
	public void MoveHorizontally(float horizontalValue)
	{
		float xScale = PlayerTransform.localScale.x;
		float yScale = PlayerTransform.localScale.y;
		PlayerTransform.localScale = new Vector3(PlayerRigidBody.linearVelocityX < -0.1f?
			Mathf.MoveTowards(xScale, -1, 0.133f) : 
			Mathf.MoveTowards(xScale,  1, 0.133f),
			yScale, 1);
		AnimationScript.HorizontalVelocity = HorizontalAnimationVelocity();
		//Allows for horizontal movement
		if (Math.Abs(PlayerRigidBody.linearVelocity.x) <= PlayerMaxSpeed)
		{
			PlayerRigidBody.AddForce(horizontalValue * MoveSpeedTimesFifty * Vector2.left);
		}
	}

	private float HorizontalAnimationVelocity()
	{
		float playerVelocity = Mathf.Abs(PlayerRigidBody.linearVelocityX);
		if (playerVelocity > PlayerWalkSpeed * 0.75f) return (1 / (PlayerRunSpeed - PlayerWalkSpeed)) * Mathf.Abs(playerVelocity - PlayerWalkSpeed) + 0.01f;
		return 0;
	}

	//Sprinting Code
	//Coroutine for draining stamina
	IEnumerator DrainStaminaCoroutine()
	{
		while(StartedRunning && _PlayerInteractions.CurrentPlayerStamina > 0)
		{
			yield return new WaitForSeconds(0.5f);
			_PlayerInteractions.CurrentPlayerStamina--;
			_PlayerInteractions.StaminaSpent++;
		}
		//Sets speed back to walk speed
		PlayerMaxSpeed = PlayerWalkSpeed;
	}
			//Starts stamina drain coroutine and sets speed to run speed
	public void PlayerSprint()
	{
		if (_PlayerInteractions.CurrentPlayerStamina > 0)
		{
			PlayerMaxSpeed = PlayerRunSpeed;
			if (!StartedRunning)
			{
				PlayerMaxSpeed = PlayerRunSpeed;
				StartedRunning = true;
				StartCoroutine(DrainStaminaCoroutine());
			}
		}
	}
			//Stamina replenishment coroutine
	IEnumerator RegenStaminaCoroutine()
	{
		IsStaminaRegenning = true;
		while(!StartedRunning && _PlayerInteractions.CurrentPlayerStamina < _PlayerInteractions.PlayerMaxStamina)
		{
			yield return new WaitForSeconds(0.5f);
			_PlayerInteractions.CurrentPlayerStamina++;
			_PlayerInteractions.StaminaRegenned++;
			_PlayerInteractions.StaminaRegennedNat++;
		}
		IsStaminaRegenning = false;
	}

	//Code for Jumping
		//Checks if the player hitbox is colliding and if they haven't jumped and allows the player to jump
	public void PlayerJump()
	{
		if (!Grounded) return;
		if (PlayerHasJumped) return;
		if (PlayerRigidBody.linearVelocity.y > 0) return;

		PlayerHasJumped = true;
		PlayerRigidBody.AddForce(PlayerJumpAmpTimesFifty * Vector2.up);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Floor"))
		{
			Grounded = true;
			AnimationScript.Grounded = true;
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Floor"))
		{
			Grounded = false;
			AnimationScript.Grounded = false;
		}
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
