using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class SimpleChase : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Get direction toward the player
        Vector3 moveDir = (player.position - transform.position).normalized;

        // Move toward the player
        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);

        // Rotate to face the player (horizontal only)
        Vector3 lookDir = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
    }
}