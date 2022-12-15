using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthIndicator : MonoBehaviour
{
	public VolumeProfile volumeProfile;

	public Health health;

	public float minIntensity;
	public float maxIntensity;

	public Vignette vignette;
	// Start is called before the first frame update
	void Start()
	{
		volumeProfile.TryGet<Vignette>(out vignette);
	}

	// Update is called once per frame
	void Update()
	{
		vignette.intensity.value = 1f - health.GetHealthPercent();
	}
}
