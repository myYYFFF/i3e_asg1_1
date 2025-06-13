/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script allows a door to open only if the player has enough score.
 */

using UnityEngine;

/// <summary>
/// A door that can only be opened if the player has enough score.
/// </summary>
public class LockedDoorBehaviour : MonoBehaviour
{
    /// <summary>
    /// Tracks if the door is open.
    /// </summary>
    private bool isOpen = false;

    /// <summary>
    /// Called when the player tries to open the door.
    /// </summary>
    /// <param name="player">The player trying to open it.</param>
    public void Interact(PlayerBehaviour player)
    {
        // Check if player has enough score
        if (player.GetScore() < 70)
        {
            Debug.Log("The door is locked. You need at least 70 score.");
            return;
        }

        // Toggle door open/close
        if (!isOpen)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // Open
            isOpen = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Close
            isOpen = false;
        }

        Debug.Log("Locked door toggled!");
    }
}
