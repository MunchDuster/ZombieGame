using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
	public RawImage rawImage;

	public Health health;

	// Start is called before the first frame update
	void Awake()
	{
		health.OnChange += UpdateVignette;
		health.OnDeath.AddListener(ResetVignette);
		Debug.Break();
		rawImage.color = Color.clear;
	}

	void UpdateVignette()
	{
		// Debug.Log("rawImage " + rawImage);
		Debug.Log("rawImage.color " + rawImage.color);
		// Debug.Log("health " + health);
		// Debug.Log("health.GetHealthPercent() " + health.GetHealthPercent());
		rawImage.color = new Color(1f, 1f, 1f, 1f - health.GetHealthPercent());
	}
	void ResetVignette()
	{
		rawImage.color = new Color(1f, 1f, 1f, 0f);
	}
}
