using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	public static PlayerSpawner instance;
	public Transform spawnPointParent;

	public GameObject playerPrefab;

	void Start()
	{
		instance = this;

		//Spawn players
		for (var i = 0; i < GameSettings.instance.playersSettings.Length; i++)
		{
			PlayerClass playerClass = GameSettings.instance.playersSettings[i].playerClass;
			GameObject primaryWeapon = GameSettings.instance.playersSettings[i].primaryWeapon;
			GameObject secondaryWeapon = GameSettings.instance.playersSettings[i].secondaryWeapon;
			Transform spawnPoint = spawnPointParent.GetChild(Random.Range(0, spawnPointParent.childCount));
			GameObject playerObject = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation, transform);
			Player player = playerObject.GetComponentInChildren<Player>();
			player.classController.PickClass(playerClass);
			player.weaponManager.PickupWeapon(secondaryWeapon);
			player.weaponManager.PickupWeapon(primaryWeapon);
		}
	}
}
