/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Door that only opens if player score is at least 70.
*/

using UnityEngine;

/// <summary>
/// Controls a locked door that requires a minimum score to open.
/// </summary>
public class LockedDoorBehaviour : MonoBehaviour
{
    /// <summary>
    /// Tracks if the door is currently open.
    /// </summary>
    private bool isOpen = false;

    /// <summary>
    /// Toggles door state if player meets score requirement.
    /// </summary>
    /// <param name="player">Player interacting with the door.</param>
    public void Interact(PlayerBehaviour player)
    {
        // Check if player has enough score to open door
        if (player.GetScore() < 70)
        {
            Debug.Log("The door is locked. You need at least 70 score.");
            return;
        }

        if (!isOpen)
        {
            // Open door by rotating it
            transform.rotation = Quaternion.Euler(0, 90, 0);
            isOpen = true;
        }
        else
        {
            // Close door by resetting rotation
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isOpen = false;
        }

        Debug.Log("Locked door toggled!");
    }
}
