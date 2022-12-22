using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WaveSystem : MonoBehaviour
{
	public static WaveSystem instance;
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

	public UnityEvent<int> OnWaveIncreased;

	public float waveGapTime = 3;
	public ZombieSpawner spawner;
	public int waveNo = 0;
	public bool startSpawningAtStart;

	// Start is called before the first frame update
	void Start()
	{
		instance = this;
		spawner.OnAllZombiesDead.AddListener(() => { StartCoroutine(OnWaveCompleted()); });
		if (startSpawningAtStart) StartNextWave();
	}

	IEnumerator OnWaveCompleted()
	{
		yield return new WaitForSeconds(waveGapTime);
		waveNo++;
		OnWaveIncreased.Invoke(waveNo);
		StartNextWave();
	}

	public void StartNextWave()
	{

		spawner.zombieHealth = startZombieHealth + waveNo * zombieHealthIncrease;
		spawner.zombieSpeed = startZombieSpeed + waveNo * zombieSpeedIncrease;
		spawner.minSpawnGap = Mathf.Max(spawner.minSpawnGap - spawnGapDecrease, minSpawnGap);
		spawner.maxZombiesAtOnce += maxZombiesIncrease;
		spawner.zombieValue = startZombieValue + waveNo * zombieValueIncrease;
		spawner.SpawnRoundZombies(zombiesIncrease * waveNo + startZombies, waveNo);
	}
}
