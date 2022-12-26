using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class MapPostProcessing : MonoBehaviour
{
	public static MapPostProcessing instance;

	ColorAdjustments colorAdjustments;
	MotionBlur motionBlur;
	DepthOfField depthOfField;
	// Start is called before the first frame update
	void Start()
	{
		instance = this;
		Volume volume = GetComponent<Volume>();
		volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
		volume.profile.TryGet<MotionBlur>(out motionBlur);
		volume.profile.TryGet<DepthOfField>(out depthOfField);

	}

	public void SetBrightness(float value)
	{
		colorAdjustments.postExposure.value = (value - 0.5f) * 3f;
	}
	public void SetMotionBlur(float value)
	{
		if (value < 0.01f) motionBlur.active = false;
		else motionBlur.active = true;
		motionBlur.intensity.value = value / 3f;
	}
	public void SetBlur(bool blur)
	{
		depthOfField.active = blur;
	}
}
