using UnityEngine;

public class gunBehaviour : MonoBehaviour
{
    public void Collect(PlayerBehaviour player)
    {
        if (player.gunPlayer != null)
        {
            player.gunPlayer.SetActive(true);
            player.SetGunCollected(true);
            Debug.Log("Weapon equipped!");
        }
        //else
        //{
            //Debug.Log("wrong.");
        //}

        Destroy(gameObject); // remove torch after pickup
    }
}
