using UnityEngine;

public class TorchlightBehaviour : MonoBehaviour
{
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
