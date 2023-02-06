using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	public static PlayerSpawner instance;

	public static List<Player> players;
	public Transform spawnPointParent;

	public GameObject playerPrefab;

	void Start()
	{
		instance = this;
		players = new List<Player>();

		//Spawn players
		for (var i = 0; i < GameSettings.instance.playersSettings.Length; i++)
		{
			PlayerClass playerClass = GameSettings.instance.playersSettings[i].playerClass;

			Transform spawnPoint = spawnPointParent.GetChild(Random.Range(0, spawnPointParent.childCount));
			GameObject playerObject = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation, transform);
			Player player = playerObject.GetComponentInChildren<Player>();
			player.classController.PickClass(playerClass);

			if (GameSettings.instance.startsWithWeapons)
			{
				GameObject primaryWeapon = GameSettings.instance.playersSettings[i].primaryWeapon;
				if (primaryWeapon != null) player.weaponManager.PickupWeapon(primaryWeapon);
				GameObject secondaryWeapon = GameSettings.instance.playersSettings[i].secondaryWeapon;
				if (secondaryWeapon != null) player.weaponManager.PickupWeapon(secondaryWeapon);
				GameObject grenades = GameSettings.instance.playersSettings[i].grenades;
				if (grenades != null) player.weaponManager.PickupWeapon(grenades);
				GameObject melee = GameSettings.instance.playersSettings[i].melee;
				if (melee != null) player.weaponManager.PickupWeapon(melee);
			}
			players.Add(player);
		}
	}

	public void PlayerWon()
	{
		for (int i = 0; i < players.Count; i++)
		{
			players[i].ShowWinScreen();
		}
	}
}
