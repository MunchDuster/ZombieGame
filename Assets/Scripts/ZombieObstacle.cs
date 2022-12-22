using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class ZombieObstacle : MonoBehaviour
{
	public float speed = 0.1f;
	NavMeshObstacle navMesh;
	Vector3 oriPos;
	float a;

	// Start is called before the first frame update
	void Start()
	{
		navMesh = GetComponent<NavMeshObstacle>();
		oriPos = transform.position;
		a = Random.Range(0f, 2 * Mathf.PI);
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = oriPos + new Vector3(
			Mathf.Cos(a + speed * Time.timeSinceLevelLoad),
			0,
			Mathf.Sin(a + speed * Time.timeSinceLevelLoad)
		);
	}
}