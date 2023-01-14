using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swayer : MonoBehaviour
{
	public Transform swayPoint;
	public PlayerMovement playerMovement;
	public float swaySize;
	public float swaySpeed;
	public float swayClamp;
	public float runSway;

	float verticalSway;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (playerMovement.IsGrounded())
		{

		}

		float swayHorizontal = playerMovement.lookInput.x;
		float swayVertical = GetVerticalSway();
		Vector3 targetPos = new Vector3(swayHorizontal, swayVertical, 0);
		swayPoint.localPosition = Vector3.Lerp(swayPoint.localPosition, targetPos, Time.deltaTime * swaySpeed);
	}

	float GetVerticalSway()
	{
		if (playerMovement.was)
	}

}
