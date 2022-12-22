using UnityEngine;

public class PlayerSettings
{
	public PlayerClass playerClass;
	public string playerName;
	public Player inGamePlayer;
	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;

	public PlayerSettings(PlayerClass playerClass, string playerName)
	{
		this.playerClass = playerClass;
		this.playerName = playerName;
	}
	public PlayerSettings(PlayerClass playerClass, string playerName, GameObject primaryWeaponPrefab, GameObject secondaryWeaponPrefab)
	{
		this.playerClass = playerClass;
		this.playerName = playerName;
		this.primaryWeapon = primaryWeaponPrefab;
		this.secondaryWeapon = secondaryWeaponPrefab;
	}
}