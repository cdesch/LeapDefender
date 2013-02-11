using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{

	/// <summary>
	/// Spawn cooldown (seconds)
	/// </summary>
	public float MinCooldown, MaxCooldown;

	/// <summary>
	/// Terrain ref
	/// </summary>
	public Transform Terrain;

	/// <summary>
	/// Enemies that can be instanciated
	/// </summary>
	public List<Transform> Enemies;
	private float currentCooldown;

	void Start ()
	{
		currentCooldown = 0;

		if (Terrain == null) {
			throw new ArgumentException ("No terrain referenced...");
		}

		if (Enemies == null || Enemies.Count == 0) {
			throw new ArgumentException ("No enemies referenced...");
		}
	}

	void Update ()
	{
		// Decrease cooldown wit hcurrent time
		currentCooldown -= Time.deltaTime;

		// Spawn enemies
		if (currentCooldown <= 0) {
			// Reset cooldown
			currentCooldown = UnityEngine.Random.Range (MinCooldown, MaxCooldown);

			// Get a random location on the terrain border
			Vector3 randomSpawnLocation = Vector3.zero;
			Vector3 edges = Terrain.transform.localScale;
			
			// Choose a border : top, left, bot or right
			int randomBorder = UnityEngine.Random.Range (0, 4);
			
			if (randomBorder == 0) {
				randomSpawnLocation.x = (edges.x / 2) * UnityEngine.Random.Range (-1f, 1f);
				randomSpawnLocation.y = -edges.z / 2;
			} else if (randomBorder == 2) {
				randomSpawnLocation.x = (edges.x / 2) * UnityEngine.Random.Range (-1f, 1f);
				randomSpawnLocation.y = edges.z / 2;
			} else if (randomBorder == 1) {
				randomSpawnLocation.x = -edges.x / 2;
				randomSpawnLocation.y = (edges.z / 2) * UnityEngine.Random.Range (-1f, 1f);
			} else if (randomBorder == 3) {
				randomSpawnLocation.x = edges.x / 2;
				randomSpawnLocation.y = (edges.z / 2) * UnityEngine.Random.Range (-1f, 1f);
			}
			
			// Get a random enemy
			int eIndex = UnityEngine.Random.Range (0, Enemies.Count);

			Transform enemy = (Transform)Transform.Instantiate (Enemies [eIndex]);
			
			enemy.Translate (randomSpawnLocation);
		}
	}
}
