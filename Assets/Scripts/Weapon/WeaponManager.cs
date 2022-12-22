using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
	public TextMeshProUGUI clipAmmoText;
	public TextMeshProUGUI reserveAmmoText;
	public Transform player;
	public Transform hipFirePoint;
	public Transform aimFirePoint;
	public PlayerMoney playerMoney;
	public Transform recoilRotation;

	public List<Weapon> weapons = new List<Weapon>();

	int index = 0;

	public void UpdateIndex(int newIndex)
	{
		if (newIndex >= transform.childCount) return;
		if (!weapons[newIndex].canUse) return;
		weapons[index].SetActive(false);
		weapons[newIndex].SetActive(true);
		index = newIndex;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1)) UpdateIndex(0);
		else if (Input.GetKey(KeyCode.Alpha2)) UpdateIndex(1);
		else if (Input.GetKey(KeyCode.Alpha3)) UpdateIndex(2);
		else if (Input.GetKey(KeyCode.Alpha4)) UpdateIndex(3);
	}


	public void PickupWeapon(GameObject prefab)
	{
		if (HasWeapon(prefab)) return;

		int index = transform.childCount;

		GameObject instantiation = Instantiate(prefab, transform);
		Weapon weapon = instantiation.GetComponentInChildren<Weapon>();
		weapons.Add(weapon);

		//Setup specifically for gun 
		Gun gun = weapon as Gun;
		if (gun != null)
		{
			gun.clipAmmoText = clipAmmoText;
			gun.reserveAmmoText = reserveAmmoText;
			gun.character = player;
			gun.playerMoney = playerMoney;
			gun.recoiler.RecoilRotationTranform = recoilRotation;
			gun.aimer.targetNormalPosition = hipFirePoint;
			gun.aimer.targetAimingPosition = aimFirePoint;
		}



		UpdateIndex(index);
	}

	bool HasWeapon(GameObject prefab)
	{
		Weapon weapon = prefab.GetComponentInChildren<Weapon>();
		string pickupName = weapon.weaponName;

		for (int i = 0; i < weapons.Count; i++)
		{
			if (weapons[i].weaponName == pickupName) return true;
		}

		return false;
	}


}
