using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public static List<Player> players;

	public UnityEvent OnWin;

	public Camera camera;
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

	public void ShowWinScreen()
	{
		OnWin.Invoke();
	}
}