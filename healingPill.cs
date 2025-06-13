/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script lets the player heal when picking up a healing pill.
 */

using UnityEngine;

/// <summary>
/// Heals the player when collected.
/// </summary>
public class healingPill : MonoBehaviour
{
    /// <summary>
    /// How much health the pill gives.
    /// </summary>
    public int healAmount = 50;

    /// <summary>
    /// Called when the player picks up the healing pill.
    /// </summary>
    /// <param name="player">The player who collected it.</param>
    public void Collect(PlayerBehaviour player)
    {
        player.Heal(healAmount); // Heal the player
        Destroy(gameObject);     // Remove the pill from the scene
    }
}
