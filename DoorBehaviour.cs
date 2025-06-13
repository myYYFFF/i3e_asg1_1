using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool isOpen = false;

    public void Interact()
    {
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
    }
}
