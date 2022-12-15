using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Aimer : MonoBehaviour
{
	//Aiming
	public Transform playerCamera;
	public Transform targetNormalPosition;
	public Transform targetAimingPosition;
	public Transform targetRotation;

	public Gun gun;
	public float positionSpeed = 10;
	public float rotationSpeed = 20;

	public Transform output;

	bool isAiming;
	public UnityEvent<bool> OnAim;

	// Update is called once per frame
	void LateUpdate()
	{
		if (!output || !targetNormalPosition || !targetAimingPosition || !targetRotation) return;

		Vector3 positionTarget = Vector3.zero;
		Quaternion rotationTarget = targetRotation.rotation;

		//Aiming
		bool aimPressed = Input.GetKey(KeyCode.Mouse1);
		if (aimPressed && gun.CanBeFiredIgnoreIsRecoiling())
		{
			//Position
			Vector3 offset = gun.GetAimOffset();
			positionTarget = targetAimingPosition.position + offset;

			if (!isAiming)
			{
				OnAim.Invoke(true);
				isAiming = true;
			}
		}

		//Normal
		else
		{
			//Position
			positionTarget = targetNormalPosition.position;

			if (isAiming)
			{
				OnAim.Invoke(false);
				isAiming = false;
			}
		}

		//Lerp
		output.position = Vector3.Lerp(output.position, positionTarget, Time.deltaTime * positionSpeed);
		output.rotation = Quaternion.Lerp(output.rotation, rotationTarget, Time.deltaTime * rotationSpeed);
	}

	void OnDisable()
	{
		if (isAiming)
		{
			OnAim.Invoke(false);
			isAiming = false;
		}
	}


}