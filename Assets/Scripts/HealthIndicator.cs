using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
	public RawImage rawImage;

	public Health health;

	// Start is called before the first frame update
	void Start()
	{
		health.OnChange += UpdateVignette;
		health.OnDeath.AddListener(ResetVignette);
		rawImage.color = Color.clear;
	}

	void UpdateVignette()
	{
		rawImage.color = new Color(1f, 1f, 1f, 1f - health.GetHealthPercent());
	}
	void ResetVignette()
	{
		rawImage.color = new Color(1f, 1f, 1f, 0f);
	}
}
