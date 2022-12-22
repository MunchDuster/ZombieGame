using UnityEngine;
using TMPro;

public abstract class Weapon : MonoBehaviour
{
	public string weaponName;
	public abstract void Fire();

	public bool canUse;

	public void MakeUsable()
	{
		canUse = true;
	}

	public void MakeUnusable()
	{
		canUse = false;
	}

	public float damage = 20;


	public Animator animator;


	public TextMeshProUGUI ammo;
	public TextMeshProUGUI clip;

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}


	public PlayerMoney playerMoney;
}