/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Handles loot pickup and removal on player collision.
*/

using UnityEngine;

/// <summary>
/// Handles loot collection by the player.
/// </summary>
public class LootBehaviour : MonoBehaviour
{
    /// <summary>
    /// Tracks whether the loot has already been collected.
    /// </summary>
    private bool isCollected = false;

    /// <summary>
    /// Called when another collider enters the trigger area.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Only collect once and only if the player enters trigger
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;

            Debug.LogWarning("You have recovered the remains of your fallen comrade.......  :(");

            // Remove the loot object from the scene
            Destroy(gameObject);
        }
    }
}
