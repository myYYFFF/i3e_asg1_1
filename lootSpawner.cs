/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Spawns loot prefab at a given spawn point or at this object's position.
*/

using UnityEngine;

/// <summary>
/// Handles spawning loot at a specified position.
/// </summary>
public class LootSpawner : MonoBehaviour
{
    /// <summary>
    /// The loot prefab to spawn.
    /// </summary>
    public GameObject lootPrefab;

    /// <summary>
    /// Optional spawn point; if null, uses this object's position.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Spawns the loot prefab at the spawn point or this object's position.
    /// </summary>
    public void SpawnLoot()
    {
        // Determine spawn position
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;

        // Create the loot prefab instance
        Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
    }
}
