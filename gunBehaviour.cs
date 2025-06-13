/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script lets the player pick up a gun and equips it.
 */

using UnityEngine;

/// <summary>
/// Lets the player collect and equip a gun.
/// </summary>
public class gunBehaviour : MonoBehaviour
{
    /// <summary>
    /// Called when the player collects the gun.
    /// </summary>
    /// <param name="player">The player who collected the gun.</param>
    public void Collect(PlayerBehaviour player)
    {
        // Check if the player has a gun to activate
        if (player.gunPlayer != null)
        {
            player.gunPlayer.SetActive(true); // Show the gun
            player.SetGunCollected(true);     // Mark gun as collected
            Debug.Log("Weapon equipped!");
        }

        // Remove the gun object from the world after pickup
        Destroy(gameObject);
    }
}
