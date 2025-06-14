/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Spawns coins when hit by a projectile and then destroys itself.
*/

using UnityEngine;

/// <summary>
/// Spawns multiple coins in a circular pattern when hit by a projectile.
/// </summary>
public class GiftBox : MonoBehaviour
{
    /// <summary>
    /// Coin prefab to be spawned.
    /// </summary>
    public GameObject coin;

    /// <summary>
    /// Number of coins to spawn.
    /// </summary>
    public int numberOfCoins = 5;

    /// <summary>
    /// Called when another object collides with this one.
    /// </summary>
    /// <param name="collision">The collision data.</param>
    void OnCollisionEnter(Collision collision)
    {
        // Check if hit by a projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            for (int i = 0; i < numberOfCoins; i++)
            {
                // Calculate spawn angle and position in a circle
                float angle = i * Mathf.PI * 2 / numberOfCoins;
                Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 0.5f;
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.2f, 0) + offset;

                // Spawn coin at calculated position
                Instantiate(coin, spawnPosition, Quaternion.identity);
            }

            // Destroy the projectile
            Destroy(collision.gameObject);

            // Destroy the gift box
            Destroy(gameObject);
        }
    }
}
