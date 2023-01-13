using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : MonoBehaviour
{
	public enum WeaponType { Primary, Secondary, Tactical, Melee }
	public string weaponName;
	public bool canUse;
	public float damage = 20;
	public Texture icon;

	protected Animator animator;


	[HideInInspector]
	public TextMeshProUGUI ammoText;
	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public PlayerMoney playerMoney;
	[HideInInspector]
	public Transform normalItemPos;

	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public abstract void Fire();

	public void MakeUsable()
	{
		canUse = true;
	}

	public void MakeUnusable()
	{
		canUse = false;
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}
}