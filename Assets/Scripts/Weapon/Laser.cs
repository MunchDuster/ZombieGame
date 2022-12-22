using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	LayerMask layerMask;
	[SerializeField]
	LineRenderer lineRenderer;

	// Update is called once per frame
	void Update()
	{
		float length = GetLength();
		lineRenderer.SetPositions(new Vector3[]{
			transform.position,
			transform.position + transform.forward * length
		});
	}

	float GetLength()
	{
		if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
		{
			return hit.distance;
		}
		else
		{
			return 1000;
		}
	}
}
