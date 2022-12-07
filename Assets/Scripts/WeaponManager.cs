using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public IWeapon[] weapons;
	public Transform[] weaponTransforms;

	int index = 0;

	void Start()
	{
		weapons = new IWeapon[weaponTransforms.Length];
		for (int i = 0; i < weaponTransforms.Length; i++)
		{
			weapons[i] = weaponTransforms[i].GetComponent<IWeapon>();
		}
	}



	public void UpdateIndex(int newIndex)
	{
		if (newIndex >= transform.childCount) return;
		if (!weapons[newIndex].CanUse()) return;
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
}
