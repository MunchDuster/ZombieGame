using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
	public static Crosshair instance;
	public GameObject HitMarker;
	public RawImage crosshair;

	// Start is called before the first frame update
	void Start()
	{
		instance = this;
	}

	public void HideCrossHair(bool hidden)
	{
		crosshair.enabled = !hidden;
	}

	public void ShowHitMarker()
	{
		if (hitMarker != null) StopCoroutine(hitMarker);
		hitMarker = ShowHitMarkerCoroutine();
		StartCoroutine(hitMarker);
	}

	private IEnumerator ShowHitMarkerCoroutine()
	{
		HitMarker.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		HitMarker.SetActive(false);
	}

	IEnumerator hitMarker;
}
