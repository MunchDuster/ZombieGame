

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;
public class Health : MonoBehaviour
{

	public float maxHealth = 100;
	public float value = 20;
	public Transform character;
	public Collider head;
	public UnityEvent OnDeath;
	public UnityEvent OnHit;

	public int healthRegen = 0;

	private bool canTakeDamage = true;
	private bool isDead = false;

	private float health;

	public static Health FindHealth(Transform target)
	{
		return target.gameObject.GetComponentInParent<Health>();
	}

	void Start()
	{
		health = maxHealth;
		StartCoroutine(Regen());
	}

	IEnumerator Regen()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			health = Mathf.Clamp(healthRegen + health, 0, maxHealth);

		}
	}

	public void Finish()
	{
		Destroy(gameObject);
	}

	//Damage from not bullet, but stil from Character
	public HitInfo TakeDamage(Vector3 point, Collider col, IWeapon weapon, Transform damager, float damage)
	{

		if (!canTakeDamage || isDead) return null;

		if (head != null && col == head) damage *= 2f;
		health -= damage;

		HitInfo hitInfo = new HitInfo(health <= 0, damage, weapon, damager, transform, point);

		if (health <= 0)
		{
			isDead = true;
			if (OnDeath != null) OnDeath.Invoke();
		}
		else
		{
			if (OnHit != null) OnHit.Invoke();
		}

		return hitInfo;
	}

	public void TakeHealth(float amount)
	{
		health = Mathf.Clamp(health + amount, 0, maxHealth);
	}
	public float GetHealthPercent()
	{
		return health / maxHealth;
	}
}