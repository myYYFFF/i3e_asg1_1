using UnityEngine;

public class LootSpawner : MonoBehaviour
{

    public GameObject lootPrefab;


    public Transform spawnPoint;

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
