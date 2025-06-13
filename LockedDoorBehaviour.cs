using UnityEngine;

public class LockedDoorBehaviour : MonoBehaviour
{
    private bool isOpen = false;

    public void Interact(PlayerBehaviour player)
    {
        if (player.GetScore() < 70)
        {
            Debug.Log("The door is locked. You need at least 70 score.");
            return;
        }

        if (!isOpen)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            isOpen = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isOpen = false;
        }

        Debug.Log("Locked door toggled!");
    }
}
