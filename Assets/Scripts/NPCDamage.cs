using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class NPCDamage : MonoBehaviour, IWeapon
{
	public float damage = 20;
	public float coolDown = 0.5f;
	public float attackDist = 1f;
	public UnityEvent OnHit;

	float coolDownStartTime;

	public bool CanUse()
	{
		throw new System.NotImplementedException();
	}

	public void Fire()
	{
		throw new System.NotImplementedException();
	}

	public void SetActive(bool active)
	{
		throw new System.NotImplementedException();
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad - coolDownStartTime < coolDown) return;
		if (Player.players.Count == 0) return;

		float dist = Vector3.Distance(Player.players[0].transform.position, transform.position);
		if (dist <= attackDist)
		{
			Player.players[0].health.TakeDamage(Vector3.zero, null, this, transform, damage);
			OnHit.Invoke();
			coolDownStartTime = Time.timeSinceLevelLoad;
		}
	}
}