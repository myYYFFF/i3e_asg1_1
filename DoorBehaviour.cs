/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Controls door interaction—opens and closes the door when triggered.
*/

using UnityEngine;

/// <summary>
/// Handles door opening and closing when interacted with.
/// </summary>
public class DoorBehaviour : MonoBehaviour
{
    /// <summary>
    /// Tracks if the door is currently open.
    /// </summary>
    private bool isOpen = false;

    /// <summary>
    /// Toggles the door between open and closed states.
    /// </summary>
    public void Interact()
    {
        if (!isOpen)
        {
            // Open the door by rotating it
            transform.rotation = Quaternion.Euler(0, 90, 0);
            isOpen = true;
        }
        else
        {
            // Close the door by resetting rotation
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isOpen = false;
        }
    }
}
