/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: Handles mob health, damage effects, and death with score rewards and loot spawning.
 */

using UnityEngine;
using UnityEngine.UI;

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
    /// UI slider to show mob health.
    /// </summary>
    public Slider healthSlider;

    /// <summary>
    /// Normal material for the mob.
    /// </summary>
    public Material defaultMaterial;

    /// <summary>
    /// Material to show when mob is hit.
    /// </summary>
    public Material highlightMaterial;

    /// <summary>
    /// How long the highlight stays after being hit.
    /// </summary>
    public float highlightDuration = 0.2f;

    /// <summary>
    /// Renderer of the mob to change material.
    /// </summary>
    private Renderer mobRenderer;

    /// <summary>
    /// Points player gets for killing this mob.
    /// </summary>
    [SerializeField] int scoreReward = 20;

    /// <summary>
    /// Setup health and material at start.
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
    /// Called when mob takes damage.
    /// Updates health, shows highlight, and checks if dead.
    /// </summary>
    /// <param name="amount">Damage amount.</param>
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
    /// Change material to highlight when hit, then reset after delay.
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
    /// Reset material back to normal.
    /// </summary>
    void ResetMaterial()
    {
        if (mobRenderer != null && defaultMaterial != null)
        {
            mobRenderer.material = defaultMaterial;
        }
    }

    /// <summary>
    /// Called when mob health is zero.
    /// Gives player score, spawns loot, and destroys mob.
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
