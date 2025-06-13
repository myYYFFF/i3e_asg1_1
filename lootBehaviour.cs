using UnityEngine;

public class LootBehaviour : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;

            Debug.LogWarning("You have recovered the remains of your fallen comrade.......  :(");

            Destroy(gameObject); // Remove the loot after collection
        }
    }
}
