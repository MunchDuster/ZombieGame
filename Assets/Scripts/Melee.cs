using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
	public float damage = 20;

	public bool canUse;
	public bool CanUse() { return canUse; }

	public Animator animator;
	public Transform triggerCollider;
	public float radius = 0.5f;
	public Transform character;

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public void Fire()
	{
		animator.SetTrigger("Fire");
		Collider[] colliders = Physics.OverlapSphere(triggerCollider.position, radius);

		List<Health> healths = new List<Health>();
		foreach (Collider c in colliders)
		{
			Health health = Health.FindHealth(c.transform);
			if (health != null && !healths.Contains(health))
			{
				healths.Add(health);
				health.TakeDamage(Vector3.zero, c, this, character, damage);

			}
		}
	}
}