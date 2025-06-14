/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Manages destructible object health, visual hit feedback, and destruction on zero health.
*/

using UnityEngine;

/// <summary>
/// Represents an object that can take damage and be destroyed, with hit highlight effect.
/// </summary>
public class DestructibleObject : MonoBehaviour
{
    /// <summary>
    /// Maximum health of the destructible object.
    /// </summary>
    public int maxHealth = 100;

    /// <summary>
    /// Current health of the object.
    /// </summary>
    private int currentHealth;

    /// <summary>
    /// Default material applied when not hit.
    /// </summary>
    public Material defaultMaterial;

    /// <summary>
    /// Material applied briefly when the object is hit.
    /// </summary>
    public Material highlightMaterial;

    /// <summary>
    /// Duration in seconds for which the highlight material stays.
    /// </summary>
    public float highlightDuration = 0.2f;

    /// <summary>
    /// Renderer component for material changes.
    /// </summary>
    private Renderer objectRenderer;

    /// <summary>
    /// Initializes health and sets default material.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;

        objectRenderer = GetComponentInChildren<Renderer>();
        if (objectRenderer != null && defaultMaterial != null)
        {
            objectRenderer.material = defaultMaterial;
        }
    }

    /// <summary>
    /// Applies damage to the object, triggers highlight, and destroys if health reaches zero.
    /// </summary>
    /// <param name="amount">Amount of damage to apply.</param>
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
        else
        {
            HighlightOnHit();
        }
    }

    /// <summary>
    /// Temporarily switches material to highlight on damage.
    /// </summary>
    void HighlightOnHit()
    {
        if (objectRenderer != null && highlightMaterial != null)
        {
            objectRenderer.material = highlightMaterial;
            CancelInvoke(nameof(ResetMaterial));
            Invoke(nameof(ResetMaterial), highlightDuration);
        }
    }

    /// <summary>
    /// Resets the material back to default after highlight duration.
    /// </summary>
    void ResetMaterial()
    {
        if (objectRenderer != null && defaultMaterial != null)
        {
            objectRenderer.material = defaultMaterial;
        }
    }
}
