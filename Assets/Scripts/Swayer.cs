using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swayer : MonoBehaviour
{
	public Transform swayPoint;
	public float swaySize;
	public PlayerMovement playerMovement;
	public MovementJump movementJump;
	public float swaySpeed;
	public float swayClamp;
	public float runSway;
	public float verticalForce;

	float verticalSway;

	// Start is called before the first frame update
	void Start()
	{
		movementJump.OnGrounded += () => { verticalSway = -verticalForce; };
	}

	// Update is called once per frame
	void Update()
	{
		verticalSway = Mathf.Lerp(verticalSway, 0, Time.deltaTime * 5);

		float swayHorizontal = playerMovement.lookInput.x;

		Vector3 targetPos = new Vector3(swayHorizontal, verticalSway, 0);
		swayPoint.localPosition = Vector3.Lerp(swayPoint.localPosition, targetPos, Time.deltaTime * swaySpeed);
	}
}
