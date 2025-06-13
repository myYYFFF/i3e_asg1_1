/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: Makes the enemy chase and face the player smoothly.
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class SimpleChase : MonoBehaviour
{
    /// <summary>
    /// The player to chase.
    /// </summary>
    public Transform player;

    /// <summary>
    /// How fast the enemy moves toward the player.
    /// </summary>
    public float speed = 5f;

    /// <summary>
    /// Rigidbody component for movement.
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Called before the first frame update, sets up Rigidbody and finds the player if not assigned.
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
    /// Called on a fixed timer, moves and rotates enemy to chase the player.
    /// </summary>
    void FixedUpdate()
    {
        if (player == null) return;

        // Get direction toward the player and normalize it
        Vector3 moveDir = (player.position - transform.position).normalized;

        // Move the enemy in the direction of the player
        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);

        // Calculate direction to look at, keeping same height (y)
        Vector3 lookDir = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;

        // Smoothly rotate to face the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
    }
}
