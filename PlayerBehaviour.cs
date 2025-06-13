/*
 * Author: Mei Yifan
 * Date: 13/6/2025
 * Description: Handles player health, interactions, shooting, scoring, and game state in the scene.
 */

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerBehaviour : MonoBehaviour
{
    /// <summary> Player's maximum health value. </summary>
    public int maxHealth = 100;

    /// <summary> Player's current health value. </summary>
    public int currentHealth = 100;

    /// <summary> Amount of health to restore per healing tick. </summary>
    public int healAmount = 5;

    /// <summary> Amount of damage to apply per damage tick. </summary>
    public int damageAmount = 5;

    /// <summary> Reference to the player's flashlight GameObject. </summary>
    public GameObject playerFlashLight;

    /// <summary> Reference to the player's gun GameObject. </summary>
    public GameObject gunPlayer;

    bool isInHealingArea = false;
    bool isInDamageArea = false;
    bool isDead = false;
    bool hasGun = false;

    float healTimer = 0f;
    float damageTimer = 0f;

    /// <summary> Time interval in seconds between damage/healing ticks. </summary>
    float interval = 1f;

    /// <summary> Maximum distance for interaction raycasting. </summary>
    float interactionDistance = 5f;

    CoinBehaviour currentCoin;
    DoorBehaviour normalDoor;
    LockedDoorBehaviour lockedDoor;
    TorchlightBehaviour Torch;
    gunBehaviour gun;
    healingPill currentPill;

    bool canInteract = false;

    [SerializeField]
    [Tooltip("Projectile prefab to spawn when firing.")]
    GameObject projectile;

    [SerializeField]
    [Tooltip("Transform where projectiles spawn.")]
    Transform spwanPoint;

    [SerializeField]
    [Tooltip("Force applied to fired projectiles.")]
    float fireStrength = 0f;

    [SerializeField]
    [Tooltip("Minimum time between shots in seconds.")]
    float fireRate = 0.25f;

    [SerializeField]
    [Tooltip("UI panel to display when the game ends.")]
    private GameObject popupPanel;

    float fireCooldown = 0f;
    int totalScore = 0;

    /// <summary>
    /// Called every frame. Handles shooting input, raycasting for collectibles,
    /// and applying healing or damage over time in designated areas.
    /// </summary>
    void Update()
    {
        // Raycast for collectibles in front of the player
        RaycastHit hitInfo;
        Debug.DrawRay(spwanPoint.position, spwanPoint.forward * interactionDistance, Color.green);

        if (Physics.Raycast(spwanPoint.position, spwanPoint.forward, out hitInfo, interactionDistance))
        {
            if (hitInfo.collider.CompareTag("Collectible"))
            {
                if (currentCoin != null)
                    currentCoin.UnHighlight();

                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();

                if (currentCoin != null)
                    currentCoin.Highlight();
            }
            else
            {
                if (currentCoin != null)
                {
                    currentCoin.UnHighlight();
                    currentCoin = null;
                }
            }
        }
        else
        {
            if (currentCoin != null)
            {
                currentCoin.UnHighlight();
                currentCoin = null;
            }
        }

        if (isDead) return;

        // Handle firing cooldown and shooting input
        fireCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            OnFire();
            fireCooldown = fireRate;
        }

        // Heal over time if inside healing area and not at max health
        if (isInHealingArea && currentHealth < maxHealth)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= interval)
            {
                ModifyHealth(healAmount);
                healTimer = 0f;
            }
        }

        // Damage over time if inside damage area and still alive
        if (isInDamageArea && currentHealth > 0)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= interval)
            {
                ModifyHealth(-damageAmount);
                damageTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Modifies current health by the given amount, clamping between 0 and maxHealth.
    /// Triggers death if health drops to zero or below.
    /// </summary>
    /// <param name="amount">Amount to add (positive) or subtract (negative) from current health.</param>
    void ModifyHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

#if UNITY_EDITOR
            Debug.Log("Player is dead. Exiting play mode.");
            EditorApplication.isPlaying = false;
#endif
        }

        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth == maxHealth)
        {
            Debug.Log("You are at full health!");
        }
    }

    /// <summary>
    /// Called when player enters a trigger collider.
    /// Tracks entering healing areas, damage areas, collectibles, doors, torches, healing pills, guns, and end zones.
    /// </summary>
    /// <param name="other">The other collider involved in the trigger.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("healingArea"))
        {
            isInHealingArea = true;
            healTimer = 0f;
        }
        else if (other.CompareTag("damageArea"))
        {
            isInDamageArea = true;
            damageTimer = 0f;
        }
        else if (other.CompareTag("Collectible"))
        {
            currentCoin = other.GetComponent<CoinBehaviour>();
            canInteract = true;
        }
        else if (other.CompareTag("Torch"))
        {
            Torch = other.GetComponent<TorchlightBehaviour>();
            canInteract = true;
        }
        else if (other.CompareTag("Door"))
        {
            if (other.TryGetComponent<LockedDoorBehaviour>(out var locked))
            {
                lockedDoor = locked;
            }
            else if (other.TryGetComponent<DoorBehaviour>(out var normal))
            {
                normalDoor = normal;
            }
            canInteract = true;
        }
        else if (other.CompareTag("healingPill"))
        {
            currentPill = other.GetComponent<healingPill>();
            canInteract = true;
        }
        else if (other.CompareTag("gun"))
        {
            gun = other.GetComponent<gunBehaviour>();
            canInteract = true;
        }
        else if (other.CompareTag("EndZone"))
        {
            EndGame();
        }
    }

    /// <summary>
    /// Called when player exits a trigger collider.
    /// Resets corresponding states and references.
    /// </summary>
    /// <param name="other">The other collider involved in the trigger.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("healingArea"))
            isInHealingArea = false;
        else if (other.CompareTag("damageArea"))
            isInDamageArea = false;
        else if (other.CompareTag("Collectible"))
        {
            currentCoin = null;
            canInteract = false;
        }
        else if (other.CompareTag("Door"))
        {
            lockedDoor = null;
            normalDoor = null;
            canInteract = false;
        }
    }

    /// <summary>
    /// Sets whether the player has collected a gun.
    /// </summary>
    /// <param name="collected">True if the gun is collected, false otherwise.</param>
    public void SetGunCollected(bool collected)
    {
        hasGun = collected;
    }

    /// <summary>
    /// Fires a projectile if the player has a gun and is not dead.
    /// </summary>
    void OnFire()
    {
        if (isDead || !hasGun) return;

        GameObject newProjectile = Instantiate(projectile, spwanPoint.position, spwanPoint.rotation);
        Vector3 fireForce = spwanPoint.forward * fireStrength;
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(fireForce);
        }
    }

    /// <summary>
    /// Handles interaction with objects the player is currently able to interact with.
    /// Calls appropriate Collect or Interact methods on detected objects.
    /// </summary>
    public void OnInteract()
    {
        if (currentCoin != null)
        {
            currentCoin.Collect(this);
            currentCoin = null;
        }
        else if (lockedDoor != null)
        {
            lockedDoor.Interact(this);
        }
        else if (normalDoor != null)
        {
            normalDoor.Interact();
        }
        else if (Torch != null)
        {
            Torch.Collect(this);
            Torch = null;
        }
        else if (currentPill != null)
        {
            currentPill.Collect(this);
            currentPill = null;
        }
        else if (gun != null)
        {
            gun.Collect(this);
            gun = null;
        }
    }

    /// <summary>
    /// Adds a given amount to the player's total score.
    /// Does nothing if the player is dead.
    /// </summary>
    /// <param name="amount">Score amount to add.</param>
    public void ModifyScore(int amount)
    {
        if (isDead) return;

        totalScore += amount;
    }

    /// <summary>
    /// Ends the game by displaying the popup panel and stopping play mode.
    /// </summary>
    void EndGame()
    {
        if (popupPanel != null)
            popupPanel.SetActive(true);

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
