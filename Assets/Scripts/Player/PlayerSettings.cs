using UnityEngine;

public class PlayerSettings
{
	public PlayerClass playerClass;
	public string playerName;
	public Player inGamePlayer;
	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;
	public GameObject grenades;
	public GameObject melee;

	public PlayerSettings(PlayerClass playerClass, string playerName)
	{
		this.playerClass = playerClass;
		this.playerName = playerName;
	}
	public PlayerSettings(PlayerClass playerClass, string playerName, GameObject primaryWeaponPrefab, GameObject secondaryWeaponPrefab, GameObject grenades, GameObject meleePrefab)
	{
		this.playerClass = playerClass;
		this.playerName = playerName;
		this.primaryWeapon = primaryWeaponPrefab;
		this.secondaryWeapon = secondaryWeaponPrefab;
		this.grenades = grenades;
		this.melee = meleePrefab;
	}
}