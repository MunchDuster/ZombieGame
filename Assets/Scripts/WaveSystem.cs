using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WaveSystem : MonoBehaviour
{
	public float startZombieHealth = 40;
	public float zombieHealthIncrease = 20;
	public float startZombieSpeed = 1;
	public float zombieSpeedIncrease = 0.3f;
	public float startZombieValue = 20;
	public float zombieValueIncrease = 20;
	public int maxZombiesIncrease = 1;
	public int startZombies = 6;
	public int zombiesIncrease = 2;
	public float spawnGapDecrease = 0.2f;
	public float minSpawnGap = 0.2f;
	public TextMeshProUGUI text;

	public UnityEvent OnWaveIncreased;

	public float waveGapTime = 3;
	public ZombieSpawner spawner;
	public int waveNo = 0;

	// Start is called before the first frame update
	void Start()
	{
		spawner.OnAllZombiesDead.AddListener(() => { StartCoroutine(OnWaveCompleted()); });

	}

	IEnumerator OnWaveCompleted()
	{
		yield return new WaitForSeconds(waveGapTime);
		waveNo++;
		OnWaveIncreased.Invoke();
		StartNextWave();
	}

	public void StartNextWave()
	{
		text.text = (waveNo + 1).ToString();
		spawner.zombieHealth = startZombieHealth + waveNo * zombieHealthIncrease;
		spawner.zombieSpeed = startZombieSpeed + waveNo * zombieSpeedIncrease;
		spawner.minSpawnGap = Mathf.Max(spawner.minSpawnGap - spawnGapDecrease, minSpawnGap);
		spawner.maxZombiesAtOnce += maxZombiesIncrease;
		spawner.zombieValue = startZombieValue + waveNo * zombieValueIncrease;
		spawner.SpawnRoundZombies(zombiesIncrease * waveNo + startZombies, waveNo);
	}
}
