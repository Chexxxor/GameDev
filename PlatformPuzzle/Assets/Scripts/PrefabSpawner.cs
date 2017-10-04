using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
	public GameObject PrefabToSpawn;
	public float cubeStartDelay = 2f;
	public float cubeRepeatDelay = 2f;

	private void Start()
	{
		InvokeRepeating("SpawnPrefab", cubeStartDelay, cubeRepeatDelay);
	}

	void SpawnPrefab()
	{
		GameObject spawnedObject = Instantiate(PrefabToSpawn, transform.position, transform.rotation);
	}

}
