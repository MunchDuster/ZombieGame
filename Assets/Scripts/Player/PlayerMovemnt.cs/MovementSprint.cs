using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementSprint : MovementAddon
{
	[SerializeField] float sprintDuration = 5f;
	[SerializeField] float sprintFOV = 80f;
	[SerializeField] float sprintFOVStepTime = 10f;
	[SerializeField] Camera playerCamera;
	[SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

	float sprintRemaining;
	float sprintCooldownReset;
	bool isSprinting;

	void Update()
	{
		bool sprintPressed = Input.GetKeyDown(sprintKey);
		bool canSprint = sprintRemaining > 0f;
		isSprinting = isWalking && sprintPressed && canSprint;

		playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);
		sprintRemaining = isSprinting ?
		Mathf.Clamp(sprintRemaining - Time.deltaTime, -0.1f, sprintDuration) :
		Mathf.Clamp(sprintRemaining + Time.deltaTime, -0.1f, sprintDuration);
	}

	public override void GetMovementForce(ref Vector3 movementForce)
	{
		if (isSprinting) movementForce *= speedMultiplier;
	}
}