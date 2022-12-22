using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class NPCDamage : Weapon
{
	public float coolDown = 0.5f;
	public float attackDist = 1f;
	public UnityEvent OnHit;

	float coolDownStartTime;

	public override void Fire()
	{
		throw new System.NotImplementedException();
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad - coolDownStartTime < coolDown) return;
		if (Player.players.Count == 0) return;

		Player player = Player.players[0];
		if (player == null) return;

		float dist = Vector3.Distance(player.transform.position, transform.position);
		if (dist <= attackDist)
		{
			coolDownStartTime = Time.timeSinceLevelLoad;
			Debug.Log("player " + player);
			Debug.Log("player.health " + player.health);

			player.health.TakeDamage(Vector3.zero, null, this, transform, damage);

			Debug.Log("(Time.timeSinceLevelLoad - coolDownStartTime) " + (Time.timeSinceLevelLoad - coolDownStartTime));
			OnHit.Invoke();
		}
	}
}