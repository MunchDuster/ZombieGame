using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
	public TextMeshProUGUI text;

	public float money = 200;

	void Start()
	{
		text.text = money.ToString();
	}

	public void AddMoney(float amount)
	{
		money += amount;
		text.text = money.ToString();
	}
}
