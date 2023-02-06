using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
	public static GameSettings instance;

	public PlayerSettings[] playersSettings = new PlayerSettings[] { };

	public void AddPlayer(PlayerSettings playerSettings)
	{
		List<PlayerSettings> players = new List<PlayerSettings>(playersSettings);
		players.Add(playerSettings);
		playersSettings = players.ToArray();
	}
	public void RemovePlayer(PlayerSettings playerSettings)
	{
		List<PlayerSettings> players = new List<PlayerSettings>(playersSettings);
		players.Remove(playerSettings);
		playersSettings = players.ToArray();
	}

	[Header("Testing")]
	public bool starterPlayer = false;
	public bool startsWithWeapons = false;
	public string playerName = "Jeff";
	public PlayerClass playerClass;
	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;
	public GameObject grenades;
	public GameObject melee;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		instance = this;

		if (starterPlayer)
		{
			AddPlayer(new PlayerSettings(playerClass, playerName, primaryWeapon, secondaryWeapon, grenades, melee));
		}

	}
}
