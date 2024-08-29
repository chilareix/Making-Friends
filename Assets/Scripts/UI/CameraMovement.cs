using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	private Rigidbody2D Player;
	public float maxDistance = 10;
	public float minDistance = 10;
	private bool CameraReachedTarget = true;
	private Vector3 PlayerVelocity;
	// Start is called before the first frame update
	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		PlayerVelocity = new Vector3(Player.linearVelocity.x, Player.linearVelocity.y, 0);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		CameraReachedTarget = CameraReachTest();
		UpdateVelocity();
		if(!CameraReachedTarget)transform.position += PlayerVelocity;


	}

	//Tests for the position of the camera relative to the player to see if it is close enough to stop or far enough to go
	public bool CameraReachTest()
	{
		float distance = Vector2.Distance(transform.position, Player.position);

		if ( distance >= maxDistance) return false;
		if ( distance <= minDistance) return true;
		return false;
	}

	//Changes the velocity of the camera depending on the max speed of the player
	public void UpdateVelocity()
	{
		PlayerVelocity = new Vector3(XVelocity() * Time.deltaTime, YVelocity() * Time.deltaTime, 0);
	}

	//Changes the x velocity
	public float XVelocity()
	{
		if(Player.linearVelocity.x < 0 && Player.linearVelocity.x < PlayerVelocity.x) return Player.linearVelocity.x;
		return Player.linearVelocity.x > PlayerVelocity.x ? Player.linearVelocity.x : PlayerVelocity.x;
	}

	//Changes the y velocity
	public float YVelocity()
	{
		if (Player.linearVelocity.y < 0 && Player.linearVelocity.y < PlayerVelocity.y) return Player.linearVelocity.y;
		return Player.linearVelocity.y > PlayerVelocity.y ? Player.linearVelocity.y : PlayerVelocity.y;
	}
}
