/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Handles projectile collision and damage.
*/

using UnityEngine;

/// <summary>
/// Deals damage to enemies or objects on collision.
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Damage dealt on hit.
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Sets the projectile's damage.
    /// </summary>
    /// <param name="dmg">New damage value.</param>
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    /// <summary>
    /// Applies damage on collision and destroys projectile.
    /// </summary>
    /// <param name="collision">Object the projectile hits.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Try to get MobHealth component from collided object
        MobHealth mob = collision.collider.GetComponent<MobHealth>();
        if (mob != null)
        {
            // Deal damage to mob
            mob.TakeDamage(damage);
        }
        else
        {
            // Try to get DestructibleObject component
            DestructibleObject destructible = collision.collider.GetComponent<DestructibleObject>();
            if (destructible != null)
            {
                // Deal damage to destructible object
                destructible.TakeDamage(damage);
            }
        }

        // Destroy the projectile after impact
        Destroy(gameObject);
    }
}
