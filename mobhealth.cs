/*
* Author: Mei Yifan
* Date: 13/6/2025
* Description: Manages mob health, damage, visual hit feedback, and death logic including score and loot spawning.
*/

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles health, damage, highlighting on hit, and death behavior for a mob.
/// </summary>
public class MobHealth : MonoBehaviour
{
    /// <summary>
    /// Maximum health of the mob.
    /// </summary>
    public int maxHealth = 30;

    /// <summary>
    /// Current health of the mob.
    /// </summary>
    private int currentHealth;

    /// <summary>
    /// UI slider to display mob health.
    /// </summary>
    public Slider healthSlider;

    /// <summary>
    /// Default material for the mob's renderer.
    /// </summary>
    public Material defaultMaterial;

    /// <summary>
    /// Material used to highlight mob when hit.
    /// </summary>
    public Material highlightMaterial;

    /// <summary>
    /// Duration to keep highlight material on hit.
    /// </summary>
    public float highlightDuration = 0.2f;

    /// <summary>
    /// Renderer component of the mob for material changes.
    /// </summary>
    private Renderer mobRenderer;

    /// <summary>
    /// Score awarded to the player upon mob death.
    /// </summary>
    [SerializeField] int scoreReward = 20;

    /// <summary>
    /// Initialize health and UI, set default material.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        mobRenderer = GetComponentInChildren<Renderer>();
        if (mobRenderer != null && defaultMaterial != null)
        {
            mobRenderer.material = defaultMaterial;
        }
    }

    /// <summary>
    /// Reduces health by given damage amount, updates UI, triggers hit highlight, and checks for death.
    /// </summary>
    /// <param name="amount">Damage to apply.</param>
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        HighlightOnHit();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Changes material to highlight the mob when hit, then resets after duration.
    /// </summary>
    void HighlightOnHit()
    {
        if (mobRenderer != null && highlightMaterial != null)
        {
            mobRenderer.material = highlightMaterial;
            CancelInvoke(nameof(ResetMaterial));
            Invoke(nameof(ResetMaterial), highlightDuration);
        }
    }

    /// <summary>
    /// Resets the mob's material back to the default.
    /// </summary>
    void ResetMaterial()
    {
        if (mobRenderer != null && defaultMaterial != null)
        {
            mobRenderer.material = defaultMaterial;
        }
    }

    /// <summary>
    /// Handles mob death: rewards player, spawns loot if available, and destroys mob object.
    /// </summary>
    void Die()
    {
        PlayerBehaviour player = FindObjectOfType<PlayerBehaviour>();
        if (player != null)
        {
            player.ModifyScore(scoreReward);
        }

        LootSpawner lootSpawner = GetComponent<LootSpawner>();
        if (lootSpawner != null)
        {
            lootSpawner.SpawnLoot();
        }

        Destroy(gameObject);
    }
}
