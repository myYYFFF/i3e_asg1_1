/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Heals the player when collected.
*/

using UnityEngine;

/// <summary>
/// Heals the player when the pill is collected.
/// </summary>
public class healingPill : MonoBehaviour
{
    /// <summary>
    /// Amount of health to restore.
    /// </summary>
    public int healAmount = 50;

    /// <summary>
    /// Heals the player and removes the pill from the scene.
    /// </summary>
    /// <param name="player">The player collecting the pill.</param>
    public void Collect(PlayerBehaviour player)
    {
        // Restore player health
        player.Heal(healAmount);

        // Remove pill from the scene
        Destroy(gameObject);
    }
}
