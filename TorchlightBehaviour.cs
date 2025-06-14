/*
* Author: Mei Yifan
* Date: 14/6/2025
* Description: Enables the player's flashlight when torchlight is collected and removes the torch object.
*/

using UnityEngine;

/// <summary>
/// Controls behavior when player picks up a torchlight.
/// </summary>
public class TorchlightBehaviour : MonoBehaviour
{
    /// <summary>
    /// Activates the player's flashlight and removes the torch object.
    /// </summary>
    /// <param name="player">The PlayerBehaviour script from the player.</param>
    public void Collect(PlayerBehaviour player)
    {
        if (player.playerFlashLight != null)
        {
            player.playerFlashLight.SetActive(true);
            Debug.Log("Flashlight enabled!");
        }
        else
        {
            Debug.LogWarning("Flashlight reference is missing from PlayerBehaviour.");
        }

        Destroy(gameObject); // remove torch after pickup
    }
}
