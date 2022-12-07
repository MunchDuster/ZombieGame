using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public static List<Player> players;
	public Health health;

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