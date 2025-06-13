/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script lets the player collect a special loot when touched.
 */

using UnityEngine;

/// <summary>
/// Controls the behavior of a collectible loot item.
/// </summary>
public class LootBehaviour : MonoBehaviour
{
    /// <summary>
    /// True if the loot has already been picked up.
    /// </summary>
    private bool isCollected = false;

    /// <summary>
    /// Called when something enters the trigger zone.
    /// </summary>
    /// <param name="other">The object that entered.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Only collect if it's the player and not already collected
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true;

            // Show message in console
            Debug.LogWarning("You have recovered the remains of your fallen comrade.......  :(");

            // Remove the object from the scene
            Destroy(gameObject);
        }
    }
}
