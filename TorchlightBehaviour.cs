using UnityEngine;

/// <summary>
/// Handles picking up the torchlight and enabling the player's flashlight.
/// </summary>
public class TorchlightBehaviour : MonoBehaviour
{
    /// <summary>
    /// Called when the player collects the torchlight.
    /// Enables the player's flashlight if available and destroys the torch object.
    /// </summary>
    /// <param name="player">The player who collected the torchlight.</param>
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
