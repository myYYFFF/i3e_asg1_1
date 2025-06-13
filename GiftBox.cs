/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script makes a gift box spawn coins in a circle when hit by a projectile.
 */

using UnityEngine;

/// <summary>
/// Spawns coins and destroys itself when hit by a projectile.
/// </summary>
public class GiftBox : MonoBehaviour
{
    /// <summary>
    /// The coin prefab to spawn.
    /// </summary>
    public GameObject coin;

    /// <summary>
    /// How many coins to spawn.
    /// </summary>
    public int numberOfCoins = 5;

    /// <summary>
    /// Called when something hits the gift box.
    /// </summary>
    /// <param name="collision">The thing that hit the box.</param>
    void OnCollisionEnter(Collision collision)
    {
        // Check if it's hit by a projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Spawn coins in a circle
            for (int i = 0; i < numberOfCoins; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfCoins;
                Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 0.5f;
                Vector3 spawnPosition = transform.position + new Vector3(0, 0.2f, 0) + offset;

                Instantiate(coin, spawnPosition, Quaternion.identity);
            }

            // Destroy the projectile and the gift box
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

