using UnityEngine;

public class MovementJump : MovementAddon
{
	[SerializeField] KeyCode jumpKey = KeyCode.Space;
	[SerializeField] float jumpPower = 5f;

	bool awaitingJump = false;
	bool jumpPressed = false;

	public delegate void OnEvent();
	public OnEvent OnGrounded;

	void Update()
	{
		jumpPressed = Input.GetKeyDown(jumpKey);

		if (jumpPressed && !awaitingJump && isGrounded)
		{
			awaitingJump = true;
			if (OnGrounded != null) OnGrounded();
			animator.SetTrigger("Jump");
		}
	}

	public override void GetMovementForce(ref Vector3 movementForce)
	{
		movementForce += (awaitingJump) ? jumpPower * Vector3.up : Vector3.zero;
		awaitingJump = false;
	}
}