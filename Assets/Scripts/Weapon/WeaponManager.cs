using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WeaponManager : MonoBehaviour
{
	public TextMeshProUGUI weaponNameText;
	public TextMeshProUGUI ammoText;
	public RawImage weaponIconImage;
	public Transform player;
	public Transform hipFirePoint;
	public Transform aimFirePoint;
	public PlayerMoney playerMoney;
	public Transform recoilRotation;
	public Transform normalItemPos;

	public List<Weapon> weapons = new List<Weapon>();

	int index = 0;

	public void UpdateIndex(int newIndex)
	{
		if (newIndex >= transform.childCount) return;
		if (!weapons[newIndex].canUse) return;
		weapons[index].SetActive(false);
		weapons[newIndex].SetActive(true);

		weaponNameText.text = weapons[newIndex].weaponName;
		weaponIconImage.texture = weapons[newIndex].icon;
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
		weapon.ammoText = ammoText;
		weapon.player = player;
		weapon.playerMoney = playerMoney;
		weapon.normalItemPos = normalItemPos;

		//Setup specifically for gun 
		Gun gun = weapon as Gun;
		if (gun != null)
		{
			if (gun.playerMoney == null) Debug.LogError("Gun money not assigned!");
			if (gun.recoiler == null) Debug.LogError("Gun recoiler not assigned!");
			if (gun.aimer == null) Debug.LogError("Gun aimer not assigned!");

			gun.playerMoney = playerMoney;
			gun.recoiler.RecoilRotationTranform = recoilRotation;
			gun.aimer.targetNormalPosition = hipFirePoint;
			gun.aimer.targetAimingPosition = aimFirePoint;
		}

		weapons.Add(weapon);

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
