using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class GrenadeThrown : MonoBehaviour
{
	public bool explodeOnStart;
	public Transform character;
	public float timeBeforeExplode;

	public Transform fx;
	public float cameraShakeAmount;
	public UnityEvent OnExplode;

	[Space(10)]
	[Header("Blast settings")]
	public float blastRadius;
	public float damage;
	public Weapon grenadeBase;

	void Start()
	{
		StartCoroutine(WaitAndExplode());
	}

	IEnumerator WaitAndExplode()
	{
		yield return new WaitForSeconds(timeBeforeExplode);
		Explode();
	}

	public void Explode()
	{
		OnExplode.Invoke();
		if (CameraShake.instance != null) CameraShake.instance.AddShake(transform.position, cameraShakeAmount);
		Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

		List<Health> healths = new List<Health>();
		foreach (Collider c in colliders)
		{
			Health health = Health.FindHealth(c.transform);
			if (health != null && !healths.Contains(health))
			{
				healths.Add(health); float lerp = Vector3.Distance(health.transform.position, transform.position) / blastRadius;
				float damageDealt = Mathf.Lerp(damage, 0, lerp);
				HitInfo hitInfo = health.TakeDamage(Vector3.zero, c, grenadeBase, character, damageDealt);
				if (hitInfo.killedEnemy)
				{
					grenadeBase.playerMoney.AddMoney(health.value);
				}
			}
		}
		fx.SetParent(null);
		fx.rotation = Quaternion.identity;
		Destroy(gameObject);
	}
}
