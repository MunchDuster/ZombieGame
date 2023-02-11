using UnityEngine;

public class MovementHeadBob : MovementAddon
{
	[SerializeField] float bobSpeed = 10f;
	[SerializeField] Vector3 bobAmount = new Vector3(.15f, .05f, 0f);
	[SerializeField] Transform joint;

	float timer = 0;
	Vector3 jointOriginalPos;

	protected override void Start()
	{
		base.Start();
		jointOriginalPos = joint.localPosition;
	}

	void Update()
	{
		// Resets when stops moving
		if (!isWalking)
		{
			timer = 0;
			joint.localPosition = new Vector3(
				Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed),
				Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed),
				Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed)
			);
			return;
		}

		//Update timer
		float bobSpeedMultiplier = playerMovement.rb.velocity.magnitude;
		timer += Time.deltaTime * bobSpeedMultiplier;

		// Applies HeadBob movement
		joint.localPosition = new Vector3(
			jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x,
			jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y,
			jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z
		);
	}
}