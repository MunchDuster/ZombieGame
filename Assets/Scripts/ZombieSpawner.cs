using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ZombieSpawner : MonoBehaviour
{
	public float speedVariance = 0.25f;
	public int maxZombiesAtOnce;
	public float minSpawnGap;

	int zombiesSpawned;

	[System.Serializable]
	public struct Spawnable
	{
		public GameObject zombiePrefab;
		public float chance;
		public int minWave;
		public float speedMultiplier;
		public float healthMultiplier;
		public float damageMultiplier;
	}
	public Spawnable[] zombiePrefabs;
	public Transform zombieParent;

	[HideInInspector] public float zombieSpeed;
	[HideInInspector] public float zombieHealth;
	[HideInInspector] public float zombieValue;

	int spawnedInRound = 0;
	int zombiesAlive = 0;
	int spawnTarget = 0;

	public UnityEvent OnAllZombiesDead;

	public List<Transform> spawnPointParents;

	public void AddSpawnPointParent(Transform spawnPoint)
	{
		spawnPointParents.Add(spawnPoint);
	}
	int wave;
	public void SpawnRoundZombies(int target, int wave)
	{
		this.wave = wave;
		spawnedInRound = 0;
		zombiesAlive = 0;
		spawnTarget = target;
		StartCoroutine(Round());

	}

	public void ZombieDied()
	{
		zombiesAlive--;
	}

	IEnumerator Round()
	{
		while (spawnedInRound < spawnTarget)
		{
			yield return new WaitForSeconds(minSpawnGap);

			while (zombiesAlive >= maxZombiesAtOnce)
			{
				yield return new WaitForSeconds(1);
			}

			Spawn();
		}

		while (zombiesAlive > 0)
		{
			yield return new WaitForSeconds(1);
		}

		OnAllZombiesDead.Invoke();
	}

	void Spawn()
	{
		spawnedInRound++;
		zombiesAlive++;

		int spawnPointParent = spawnPointParents.Count > 1 ? Random.Range(0, spawnPointParents.Count) : 0;
		int spawnPointIndex = Random.Range(0, spawnPointParents[spawnPointParent].childCount);
		Transform spawnPoint = spawnPointParents[spawnPointParent].GetChild(spawnPointIndex);

		Vector3 position = spawnPoint.position;
		Quaternion rotation = spawnPoint.rotation;

		Spawnable spawn = ChooseZombiePrefab();
		GameObject zombie = Instantiate(spawn.zombiePrefab, position, rotation, zombieParent);
		Health health = zombie.GetComponent<Health>();
		health.OnDeath.AddListener(ZombieDied);
		health.maxHealth = zombieHealth * spawn.healthMultiplier;
		health.TakeHealth(100000);
		health.value = zombieValue;
		float speed = zombieSpeed * spawn.speedMultiplier + Random.Range(-speedVariance, speedVariance);
		zombie.GetComponent<NavMeshAgent>().speed = speed;
		zombie.GetComponent<Animator>().SetFloat("speed", speed);

	}

	Spawnable ChooseZombiePrefab()
	{
		if (wave == 0)
		{
			return zombiePrefabs[0];
		}
		else
		{
			float ranNum = Random.Range(0f, 1f);
			for (int i = 0; i < zombiePrefabs.Length; i++)
			{
				if (wave < zombiePrefabs[i].minWave) break;
				if (ranNum <= zombiePrefabs[i].chance) return zombiePrefabs[i];
			}
			return zombiePrefabs[0];
		}
	}
}
