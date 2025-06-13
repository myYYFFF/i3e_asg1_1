/*
 * Author: [Mei Yifan]
 * Date: [13/6/2025]
 * Description: This script lets the projectile hurt enemies when it hits them.
 */

using UnityEngine;

/// <summary>
/// Makes the projectile do damage when it hits something.
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// How much damage this projectile does.
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Change the damage this projectile can do.
    /// </summary>
    /// <param name="dmg">The new damage amount.</param>
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    /// <summary>
    /// Happens when the projectile hits something.
    /// </summary>
    /// <param name="collision">Info about what was hit.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Try to get the MobHealth from what we hit
        MobHealth mob = collision.collider.GetComponent<MobHealth>();
        if (mob != null)
        {
            // Deal damage to it
            mob.TakeDamage(damage);
        }

        // Destroy the projectile after it hits
        Destroy(gameObject);
    }
}
