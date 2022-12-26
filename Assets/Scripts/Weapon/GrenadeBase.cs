using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GrenadeBase : Weapon
{
	bool thrown;
	public void AnimatorReady()
	{
		thrown = false;
		ammoText.text = amount.ToString();
	}

	public void ResetTrigger()
	{
		animator.ResetTrigger("Fire");
	}


	public int amount = 3;

	public Transform thrownGrenadeSpawnPoint;
	public Transform thrownGrenadePrefab;
	public float speed;

	void Start()
	{
		ammoText.text = amount.ToString();
		transform.position = normalItemPos.position;
		transform.rotation = normalItemPos.rotation;
	}

	void Update()
	{
		bool firePressed = Input.GetKey(KeyCode.Mouse0);
		if (firePressed && !thrown && amount > 0)
		{
			Fire();
		}
	}

	public override void Fire()
	{
		animator.SetTrigger("Fire");
	}

	public void AnimatorFire()
	{
		thrown = true;
		Transform thrownGrenade = Instantiate(thrownGrenadePrefab, thrownGrenadeSpawnPoint.position, thrownGrenadeSpawnPoint.rotation);
		thrownGrenade.GetComponent<Rigidbody>().AddForce(thrownGrenade.forward * speed, ForceMode.VelocityChange);
		thrownGrenade.GetComponent<GrenadeThrown>().grenadeBase = this;
		amount--;
		ammoText.text = amount.ToString();
	}
}