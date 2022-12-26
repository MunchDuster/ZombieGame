using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Aimer : MonoBehaviour
{
	//Aiming
	public Transform targetNormalPosition;
	public Transform targetAimingPosition;

	public Gun gun;
	public float positionSpeed = 10;
	public float rotationSpeed = 20;

	public Transform output;

	bool wasAiming;
	public UnityEvent<bool> OnAim;

	// Update is called once per frame
	void LateUpdate()
	{
		//Find if aiming
		bool aimPressed = Input.GetKey(KeyCode.Mouse1);
		bool isAiming = aimPressed && gun.CanBeFiredIgnoreIsRecoiling();

		//Events
		if (isAiming && !wasAiming || !isAiming && wasAiming)
		{
			OnAim.Invoke(isAiming);
			wasAiming = isAiming;
			Crosshair.instance.HideCrossHair(isAiming);
		}

		Vector3 offset = isAiming ? gun.GetAimOffset() : Vector3.zero;
		Transform target = isAiming ? targetAimingPosition : targetNormalPosition;
		Vector3 positionTarget = target.position + offset;

		output.position = Vector3.Lerp(output.position, positionTarget, Time.deltaTime * positionSpeed);
	}

	void OnDisable()
	{
		if (wasAiming)
		{
			OnAim.Invoke(false);
			wasAiming = false;
			Crosshair.instance.HideCrossHair(false);
		}
	}
}