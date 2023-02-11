using UnityEngine;

public class MovementCrouch : MovementAddon
{
	[SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
	[SerializeField] float crouchHeightDrop = .25f;
	[SerializeField] Transform crouchTransform;

	bool isCrouched = false;
	bool crouchPressed = false;
	Vector3 startPos;

	protected override void Start()
	{
		base.Start();
		startPos = crouchTransform.localPosition;
	}

	void ToggleCrouch(bool isCrouched)
	{
		this.isCrouched = isCrouched;
		float hieghtOffset = (isCrouched) ? crouchHeightDrop : 0;
		crouchTransform.localPosition = startPos - hieghtOffset * Vector3.up;
		animator.SetBool("IsCrouched", isCrouched);
	}

	void Update()
	{
		crouchPressed = Input.GetKeyDown(crouchKey);
		if (isCrouched && !crouchPressed) ToggleCrouch(false);
		else if (!isCrouched && crouchPressed) ToggleCrouch(true);
	}

	public override void GetMovementForce(ref Vector3 movementForce)
	{
		if (isCrouched) movementForce *= speedMultiplier;
	}
}