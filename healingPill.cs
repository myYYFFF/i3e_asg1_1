using UnityEngine;

public class healingPill : MonoBehaviour
{
    public int healAmount = 50;

    public void Collect(PlayerBehaviour player)
    {
        player.Heal(healAmount);
        Destroy(gameObject);
    }
}
