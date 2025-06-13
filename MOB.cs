using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class SlopeAwareChase : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float stopDistance = 1.5f;
    public LayerMask groundLayer;

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

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= stopDistance || !IsGrounded()) return;

        // Get direction and adjust to slope
        Vector3 moveDir = (player.position - transform.position).normalized;
        moveDir = AdjustToSlope(moveDir);

        // Move
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

        // Smooth look toward player
        Quaternion targetRot = Quaternion.LookRotation(new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
    }

    // Check if mob is on the ground
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.2f, groundLayer);
    }

    // Adjust movement to follow slope
    Vector3 AdjustToSlope(Vector3 direction)
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, groundLayer))
        {
            return Vector3.ProjectOnPlane(direction, hit.normal).normalized;
        }

        return direction;
    }
}
