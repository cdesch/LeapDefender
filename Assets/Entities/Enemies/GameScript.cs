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

    void Start()
    {
        currentCooldown = 0;

        if (Terrain == null)
        {
            throw new ArgumentException("No terrain referenced...");
        }

        if (Enemies == null || Enemies.Count == 0)
        {
            throw new ArgumentException("No enemies referenced...");
        }
    }

    void Update()
    {
        // Decrease cooldown wit hcurrent time
        currentCooldown -= Time.deltaTime;

        // Spawn enemies
        if (currentCooldown <= 0)
        {
            // Reset cooldown
            currentCooldown = UnityEngine.Random.Range(MinCooldown, MaxCooldown);

            // Get a random location on the terrain border
            Vector3 edges = Terrain.transform.localScale;

            // Get a random enemy
            int eIndex = UnityEngine.Random.Range(0, Enemies.Count);

            Transform enemy = (Transform)Transform.Instantiate(Enemies[eIndex]);

            float randomX = UnityEngine.Random.Range(-1f, 1f);
            //if (randomX == 0) randomX = 1;

            float randomY = UnityEngine.Random.Range(-1f, 1f);
            //if (randomY == 0) randomY = 1;

            float randomZ = UnityEngine.Random.Range(-1f, 1f);
            //if (randomZ == 0) randomZ = 1;

            enemy.Translate(edges.x * randomX, edges.y * randomY, edges.z * randomZ);
        }
    }
}
