/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Allows the player to collect and equip a gun.
*/

using UnityEngine;

/// <summary>
/// Handles gun pickup and equips it to the player.
/// </summary>
public class gunBehaviour : MonoBehaviour
{
    /// <summary>
    /// Called when the player collects the gun.
    /// </summary>
    /// <param name="player">The player picking up the gun.</param>
    public void Collect(PlayerBehaviour player)
    {
        if (player.gunPlayer != null)
        {
            // Enable player's gun object
            player.gunPlayer.SetActive(true);

            // Set gun collected state
            player.SetGunCollected(true);

            Debug.Log("Weapon equipped!");
        }

        // Destroy the gun object from the scene
        Destroy(gameObject); // remove gun after pickup
    }
}
