using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public static List<Player> players;

	public Health health;
	public WeaponManager weaponManager;
	public PlayerClassController classController;

	void Start()
	{
		if (players == null)
		{
			players = new List<Player>();
		}
		players.Add(this);
	}

	void OnDisable()
	{
		players.Remove(this);
	}
}