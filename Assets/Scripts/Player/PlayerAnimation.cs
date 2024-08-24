using System.Text;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	public float HorizontalVelocity;
	public float VerticalVelocity;
	private Animator PlayerAnimator;
	public bool Grounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		PlayerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		PlayerAnimator.SetBool("Grounded", Grounded);
		PlayerAnimator.SetFloat("Vertical Velocity", VerticalVelocity);
		PlayerAnimator.SetFloat("Horizontal Velocity", Mathf.Abs(HorizontalVelocity));
    }
}
