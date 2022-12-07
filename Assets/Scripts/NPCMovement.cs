using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
	public float maxTurnSpeed = 180;
	public AnimationCurve turnSpeedCurve;
	public float rotateSpeed = 180;

	public NavMeshAgent agent;
	public float closestPlayerCheckTime = 2.5f;

	public CharacterController controller;

	private bool isAlive = true;
	private Transform nearestPlayer;

	[HideInInspector] public bool stopWhenCanSeeEnemy;
	[HideInInspector] public float maxDistanceCanSeeEnemy;


	private void Start()
	{
		StartCoroutine(PlayerUpdateLoop());

		// agent.updatePosition = false;
		// agent.updateRotation = false;

	}
	IEnumerator PlayerUpdateLoop()
	{
		while (isAlive)
		{
			//Find the nearest player
			nearestPlayer = FindNearestPlayer();

			yield return new WaitForSeconds(closestPlayerCheckTime);
		}
	}
	//FixedUpdate is called every physics loop.
	private void FixedUpdate()
	{
		//if player exists
		if (nearestPlayer != null)
		{
			//agent.enabled = true;

			agent.SetDestination(nearestPlayer.position);

			Vector3 direction = (agent.nextPosition - transform.position).normalized;

			// controller.Move(direction);

			// RotateTowards(nearestPlayer.position);
		}
	}
	protected void RotateTowards(Vector3 point)
	{
		//Rotate to face player
		Vector3 direction = (point - transform.position).normalized;

		float unsignedAngle = Vector3.Angle(direction, transform.forward);
		float signedAngle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);

		float yTurn = -Mathf.Sign(signedAngle) * rotateSpeed * turnSpeedCurve.Evaluate(unsignedAngle / 180f);
		float yTurnClamped = Mathf.Clamp(yTurn, -maxTurnSpeed, maxTurnSpeed);

		Vector3 eulers = new Vector3(0, yTurnClamped * Time.fixedDeltaTime, 0);
		transform.Rotate(eulers);
	}

	Transform FindNearestPlayer()
	{
		if (Player.players == null || Player.players.Count == 0) return null;
		return Player.players[0].transform;
	}
}