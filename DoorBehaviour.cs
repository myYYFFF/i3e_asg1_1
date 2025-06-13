/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: This script controls a door that opens and closes when interacted with.
 */

using UnityEngine;

/// <summary>
/// Makes the door open and close when the player interacts with it.
/// </summary>
public class DoorBehaviour : MonoBehaviour
{
    /// <summary>
    /// Whether the door is currently open or not.
    /// </summary>
    private bool isOpen = false;

    /// <summary>
    /// Called when the player interacts with the door.
    /// It opens the door if it's closed, and closes it if it's open.
    /// </summary>
    public void Interact()
    {
        if (!isOpen)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); // open door
            isOpen = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // close door
            isOpen = false;
        }
    }
}
