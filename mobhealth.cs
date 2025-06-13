using UnityEngine;
using UnityEngine.UI;

public class MobHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    public Slider healthSlider;

    public Material defaultMaterial;
    public Material highlightMaterial;
    public float highlightDuration = 0.2f;

    private Renderer mobRenderer;

    [SerializeField] int scoreReward = 20;

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

    void HighlightOnHit()
    {
        if (mobRenderer != null && highlightMaterial != null)
        {
            mobRenderer.material = highlightMaterial;
            CancelInvoke(nameof(ResetMaterial));
            Invoke(nameof(ResetMaterial), highlightDuration);
        }
    }

    void ResetMaterial()
    {
        if (mobRenderer != null && defaultMaterial != null)
        {
            mobRenderer.material = defaultMaterial;
        }
    }

    void Die()
    {
        PlayerBehaviour player = FindObjectOfType<PlayerBehaviour>();
        if (player != null)
        {
            player.ModifyScore(scoreReward);
        }

        //loot 
        LootSpawner lootSpawner = GetComponent<LootSpawner>();
        if (lootSpawner != null)
        {
            lootSpawner.SpawnLoot();
        }

        Destroy(gameObject);
    }
}
