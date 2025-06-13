/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: Spawns loot at a specified location when called.
 */

using UnityEngine;

/// <summary>
/// Spawns loot objects at a given spawn point or at the object's position.
/// </summary>
public class LootSpawner : MonoBehaviour
{
    /// <summary>
    /// The loot object to spawn.
    /// </summary>
    public GameObject lootPrefab;

    /// <summary>
    /// Where to spawn the loot. If not set, spawns at this object's position.
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Creates a loot object at the spawn point or this object's position.
    /// </summary>
    public void SpawnLoot()
    {
        if (lootPrefab == null)
        {
            Debug.Log("Loot prefab is not assigned.");
            return;
        }

        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
    }
}
