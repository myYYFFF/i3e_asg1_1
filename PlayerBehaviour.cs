/*
* Author: Mei Yifan
* Date: 14/6/2025
* Description: handles player logic including health, interaction, damage, healing, collectibles, gun firing, and endgame UI
*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// controls player actions like collecting items, firing gun, healing, taking damage, and ending the game
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    /// player health settings
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int healAmount = 5;
    public int damageAmount = 5;

    /// flashlight and gun
    public GameObject playerFlashLight;
    public GameObject gunPlayer;

    /// area and state flags
    bool isInHealingArea = false;
    bool isInDamageArea = false;
    bool isDead = false;
    bool hasGun = false;

    /// timers
    float healTimer = 0f;
    float damageTimer = 0f;
    float interval = 1f;
    float interactionDistance = 5f;

    /// collectible references
    CoinBehaviour currentCoin;
    DoorBehaviour normalDoor;
    LockedDoorBehaviour lockedDoor;
    TorchlightBehaviour Torch;
    gunBehaviour gun;
    healingPill currentPill;
    bool canInteract = false;

    /// projectile and firing
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spwanPoint;
    [SerializeField] float fireStrength = 0f;
    [SerializeField] float fireRate = 0.25f;
    float fireCooldown = 0f;

    /// UI and audio
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private AudioClip gunshotClip;
    private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSoundClip;

    int totalScore = 0;

    ///audio source
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.02f;
    }

    /// runs every frame to check player state and interaction
    void Update()
    {
        // raycast to detect coins
        RaycastHit hitInfo;
        Debug.DrawRay(spwanPoint.position, spwanPoint.forward * interactionDistance, Color.green);

        if (Physics.Raycast(spwanPoint.position, spwanPoint.forward, out hitInfo, interactionDistance))
        {
            if (hitInfo.collider.CompareTag("Collectible"))
            {
                if (currentCoin != null) currentCoin.UnHighlight();

                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
                if (currentCoin != null) currentCoin.Highlight();
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

        // gun fire logic
        fireCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            OnFire();
            fireCooldown = fireRate;
        }

        // healing over time
        if (isInHealingArea && currentHealth < maxHealth)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= interval)
            {
                ModifyHealth(healAmount);
                healTimer = 0f;
            }
        }

        // damage over time
        if (isInDamageArea && currentHealth > 0)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= interval)
            {
                ModifyHealth(-damageAmount);
                if (hurtSoundClip != null)
                {
                    audioSource.PlayOneShot(hurtSoundClip);
                }
                damageTimer = 0f;
            }
        }
    }

    /// change health by given amount
    void ModifyHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

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
            Debug.Log("You are at full health!");
    }

    /// triggered when player enters something
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("healingArea"))
        {
            isInHealingArea = true;
            healTimer = 0f;
        }

        if (other.CompareTag("damageArea"))
        {
            isInDamageArea = true;
            damageTimer = 0f;
        }

        if (other.CompareTag("Collectible"))
        {
            currentCoin = other.GetComponent<CoinBehaviour>();
            canInteract = true;
        }

        if (other.CompareTag("Torch"))
        {
            Torch = other.GetComponent<TorchlightBehaviour>();
            canInteract = true;
        }

        if (other.CompareTag("Door"))
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

        if (other.CompareTag("healingPill"))
        {
            currentPill = other.GetComponent<healingPill>();
            canInteract = true;
        }

        if (other.CompareTag("gun"))
        {
            gun = other.GetComponent<gunBehaviour>();
            canInteract = true;
        }

        if (other.CompareTag("EndZone"))
        {
            EndGame();
        }
    }

    /// triggered when player leaves a zone
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("healingArea"))
            isInHealingArea = false;

        if (other.CompareTag("damageArea"))
            isInDamageArea = false;

        if (other.CompareTag("Collectible"))
        {
            currentCoin = null;
            canInteract = false;
        }

        if (other.CompareTag("Door"))
        {
            lockedDoor = null;
            normalDoor = null;
            canInteract = false;
        }
    }

    /// set gun collected status
    public void SetGunCollected(bool collected)
    {
        hasGun = collected;
    }

    /// fire projectile if gun collected
    void OnFire()
    {
        if (isDead || !hasGun) return;

        GameObject newProjectile = Instantiate(projectile, spwanPoint.position, spwanPoint.rotation);
        Vector3 fireForce = spwanPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);

        if (gunshotClip != null)
        {
            audioSource.PlayOneShot(gunshotClip);
        }
    }

    /// handle interaction input
    void OnInteract()
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

    /// add score
    public void ModifyScore(int amount)
    {
        if (isDead) return;
        totalScore += amount;
        Debug.Log("Score added: " + amount + " | Total Score: " + totalScore);
    }

    /// get current score
    public int GetScore()
    {
        return totalScore;
    }

    /// heal instantly by given amount (like healing pill)
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Healed by " + amount + ". Current Health: " + currentHealth);

        if (currentHealth == maxHealth)
            Debug.Log("You are at full health!");
    }

    /// pause game and show popup when player reaches end zone
    void EndGame()
    {
        Debug.Log("You reached the end zone. Game Over!");
        Time.timeScale = 0f;

        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
    }
}
