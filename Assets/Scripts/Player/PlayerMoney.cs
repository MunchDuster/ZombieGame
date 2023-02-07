using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
	public TextMeshProUGUI text;

	public float money = 200;
	public float displayUpdateSpeed = 50;

	void Start()
	{
		text.text = money.ToString();
	}

	public void AddMoney(float amount)
	{
		money += amount;
	}
	float displayMoney = 0;

	void Update()
	{
		if (displayMoney != money)
		{
			float difference = money - displayMoney;
			displayMoney += Mathf.Sign(difference) * Mathf.Min(Mathf.Abs(difference), displayUpdateSpeed);
			text.text = "$" + displayMoney.ToString("F0");
		}
	}
}
