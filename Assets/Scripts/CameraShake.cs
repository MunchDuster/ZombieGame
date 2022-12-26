using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public static CameraShake instance;
	public float shakeMultiplier;
	public float maxDist;

	public AnimationCurve shakeOverTime;
	public AnimationCurve shakeOverDistance;
	public float maxRotate = 20;

	float curShake;
	float shakeTime;

	void Awake()
	{
		instance = this;
	}

	public void AddShake(Vector3 point, float amount)
	{
		curShake += amount * shakeOverDistance.Evaluate(Vector3.Distance(transform.position, point) / maxDist) * shakeMultiplier;
		shakeTime = 0;
	}

	// Update is called once per frame
	void Update()
	{
		shakeTime += Time.deltaTime;
		curShake *= shakeOverTime.Evaluate(shakeTime);
		transform.localPosition = Random.onUnitSphere * curShake;
		Vector3 rotate = curShake * new Vector3(Random.Range(-maxRotate, maxRotate), Random.Range(-maxRotate, maxRotate), Random.Range(-maxRotate, maxRotate));
		transform.localRotation = Quaternion.Euler(rotate);
	}
}
