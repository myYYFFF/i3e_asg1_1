using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Material defaultMaterial;
    public Material highlightMaterial;
    public float highlightDuration = 0.2f;

    private Renderer objectRenderer;

    void Start()
    {
        currentHealth = maxHealth;

        objectRenderer = GetComponentInChildren<Renderer>();
        if (objectRenderer != null && defaultMaterial != null)
        {
            objectRenderer.material = defaultMaterial;
        }
    }

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

    void HighlightOnHit()
    {
        if (objectRenderer != null && highlightMaterial != null)
        {
            objectRenderer.material = highlightMaterial;
            CancelInvoke(nameof(ResetMaterial));
            Invoke(nameof(ResetMaterial), highlightDuration);
        }
    }

    void ResetMaterial()
    {
        if (objectRenderer != null && defaultMaterial != null)
        {
            objectRenderer.material = defaultMaterial;
        }
    }
}
