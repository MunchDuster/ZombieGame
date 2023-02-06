using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class AutoFocs : MonoBehaviour
{
	DepthOfField depthOfField;
	public Transform camera;
	public LayerMask layerMask;

	public float defaultRaycastLength = 100;


	// Start is called before the first frame update
	void Start()
	{
		Volume volume = GetComponent<Volume>();
		volume.profile.TryGet<DepthOfField>(out depthOfField);
	}

	// Update is called once per frame
	void Update()
	{
		if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, 100, layerMask))
		{
			depthOfField.focusDistance.value = hit.distance;
		}
	}
}
