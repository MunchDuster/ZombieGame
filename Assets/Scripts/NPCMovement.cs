using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
	public NavMeshAgent agent;
	public float closestPlayerCheckTime = 2.5f;

	private bool isAlive = true;
	private Transform nearestPlayer;

	private void Start()
	{
		StartCoroutine(PlayerUpdateLoop());
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
			agent.SetDestination(nearestPlayer.position);
		}
	}
	Transform FindNearestPlayer()
	{
		if (Player.players == null || Player.players.Count == 0) return null;
		return Player.players[0].transform;
	}
}