/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: Controls destructible objects that can take damage, show hit highlights, and get destroyed.
 */

using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    /// <summary>
    /// Maximum health of the object.
    /// </summary>
    public int maxHealth = 100;

    /// <summary>
    /// Current health of the object.
    /// </summary>
    private int currentHealth;

    /// <summary>
    /// Default material of the object.
    /// </summary>
    public Material defaultMaterial;

    /// <summary>
    /// Material shown briefly when object is hit.
    /// </summary>
    public Material highlightMaterial;

    /// <summary>
    /// Duration to keep the highlight material.
    /// </summary>
    public float highlightDuration = 0.2f;

    /// <summary>
    /// Renderer component used to change materials.
    /// </summary>
    private Renderer objectRenderer;

    /// <summary>
    /// Initialize health and set the default material.
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
    /// Apply damage to the object, destroy it if health drops to zero or below.
    /// </summary>
    /// <param name="amount">Damage amount to apply.</param>
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
    /// Temporarily switch to highlight material to show damage effect.
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
    /// Reset material back to default after highlight duration.
    /// </summary>
    void ResetMaterial()
    {
        if (objectRenderer != null && defaultMaterial != null)
        {
            objectRenderer.material = defaultMaterial;
        }
    }
}
