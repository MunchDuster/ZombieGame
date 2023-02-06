using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveNumberDisplay : MonoBehaviour
{
	public TextMeshProUGUI text;

	void Start()
	{
		WaveSystem.OnInstanceCreated += AddWaveSystemListener;
	}

	void AddWaveSystemListener()
	{
		WaveSystem.instance.OnWaveIncreased.AddListener(UpdateText);
	}

	void UpdateText(int waveNo)
	{
		text.text = (waveNo + 1).ToString();
	}
}
