using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class GrenadeBase : MonoBehaviour, IWeapon
{
	public TextMeshProUGUI ammo;
	public TextMeshProUGUI clip;


	public bool canUse;
	public bool CanUse()
	{
		return canUse;
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}
	bool thrown;
	public void AnimatorReady()
	{
		thrown = false;
		ammo.text = (amount > 0 ? (thrown ? 0 : 1) : 0).ToString();
		clip.text = amount.ToString();
	}

	public void ResetTrigger()
	{
		animator.ResetTrigger("Fire");

	}


	public int amount = 3;

	public Transform thrownGrenadeSpawnPoint;
	public Transform thrownGrenadePrefab;
	public Animator animator;
	public float speed;

	void Awake()
	{
		ammo.text = (amount > 0 ? (thrown ? 0 : 1) : 0).ToString();
		clip.text = amount.ToString();
	}

	void Update()
	{
		bool firePressed = Input.GetKey(KeyCode.Mouse0);
		if (firePressed && !thrown && amount > 0)
		{
			Fire();
		}
	}

	public void Fire()
	{
		animator.SetTrigger("Fire");
	}

	public void AnimatorFire()
	{
		thrown = true;
		Transform thrownGrenade = Instantiate(thrownGrenadePrefab, thrownGrenadeSpawnPoint.position, thrownGrenadeSpawnPoint.rotation);
		thrownGrenade.GetComponent<Rigidbody>().AddForce(thrownGrenade.forward * speed, ForceMode.VelocityChange);
		amount--;
		ammo.text = (amount > 0 ? (thrown ? 0 : 1) : 0).ToString();
		clip.text = amount.ToString();
	}
}