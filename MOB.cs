/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Makes an enemy chase the player using Rigidbody movement and rotate to face the player.
*/

using UnityEngine;

/// <summary>
/// Simple AI that chases the player with Rigidbody movement and rotates to face the player horizontally.
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class SimpleChase : MonoBehaviour
{
    /// <summary>
    /// Reference to the player transform to chase.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Movement speed toward the player.
    /// </summary>
    public float speed = 5f;

    /// <summary>
    /// Rigidbody component for physics-based movement.
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Initialization: cache Rigidbody and find player if not assigned.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found.transform;
        }
    }

    /// <summary>
    /// Physics update: move toward and rotate to face the player.
    /// </summary>
    void FixedUpdate()
    {
        if (player == null) return;

        // Calculate normalized direction vector toward the player
        Vector3 moveDir = (player.position - transform.position).normalized;

        // Move Rigidbody toward the player
        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);

        // Calculate look direction on horizontal plane only
        Vector3 lookDir = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;

        // Smoothly rotate to face the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
    }
}
