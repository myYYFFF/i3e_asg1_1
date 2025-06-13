using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int healAmount = 5;
    public int damageAmount = 5;
    public GameObject playerFlashLight;
    public GameObject gunPlayer;


    bool isInHealingArea = false;
    bool isInDamageArea = false;
    bool isDead = false;
    bool hasGun = false;


    float healTimer = 0f;
    float damageTimer = 0f;
    float interval = 1f;
    float interactionDistance = 5f;

    CoinBehaviour currentCoin;
    DoorBehaviour normalDoor;
    LockedDoorBehaviour lockedDoor;
    TorchlightBehaviour Torch;
    gunBehaviour gun;
    healingPill currentPill;
    bool canInteract = false;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform spwanPoint;
    [SerializeField] float fireStrength = 0f;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] private GameObject popupPanel;


    float fireCooldown = 0f;
    int totalScore = 0;

    void Update()
    {
        // Raycast for Collectibles
        RaycastHit hitInfo;
        Debug.DrawRay(spwanPoint.position, spwanPoint.forward * interactionDistance, Color.green);

        if (Physics.Raycast(spwanPoint.position, spwanPoint.forward, out hitInfo, interactionDistance))
        {
            if (hitInfo.collider.CompareTag("Collectible"))
            {
                if (currentCoin != null)
                    currentCoin.UnHighlight();
                
                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
                
                if(currentCoin != null)
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

        fireCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            OnFire();
            fireCooldown = fireRate;
        }



        if (isInHealingArea && currentHealth < maxHealth)
        {
            healTimer += Time.deltaTime;
            if (healTimer >= interval)
            {
                ModifyHealth(healAmount);
                healTimer = 0f;
            }
        }

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
        if ( currentHealth == maxHealth)
        {
            Debug.Log("You are at full health!");
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        //healing room
        if (other.CompareTag("healingArea"))
        {
            isInHealingArea = true;
            healTimer = 0f;
        }
        //lava 
        if (other.CompareTag("damageArea"))
        {
            isInDamageArea = true;
            damageTimer = 0f;
        }
        //coin
        if (other.CompareTag("Collectible"))
        {
            currentCoin = other.GetComponent<CoinBehaviour>();
            canInteract = true;
        }
        //torch
        if (other.CompareTag("Torch"))
        {
            Torch = other.GetComponent<TorchlightBehaviour>();
            canInteract = true;
        }
        //access door scripts
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
        //healing pill
        if (other.CompareTag("healingPill"))
        {
            currentPill = other.GetComponent<healingPill>();
            canInteract = true;
        }
        //gun
        if (other.CompareTag("gun"))
        {
            gun = other.GetComponent<gunBehaviour>();
            canInteract = true;
        }
        //endzone
        if (other.CompareTag("EndZone"))
        {
            EndGame();
        }
    }

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


    //gun collection restriciotn
    public void SetGunCollected(bool collected)
    {
        hasGun = collected;
    }

    //allow fire if gun collected
    void OnFire()
    {
        if (isDead || !hasGun) return; 

        GameObject newProjectile = Instantiate(projectile, spwanPoint.position, spwanPoint.rotation);
        Vector3 fireForce = spwanPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }


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

    public void ModifyScore(int amount)
    {
        if (isDead) return;

        totalScore += amount;
        Debug.Log("Score added: " + amount + " | Total Score: " + totalScore);
    }

    public int GetScore()
    {
        return totalScore;
    }
    


    // healingpill
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Healed by " + amount + ". Current Health: " + currentHealth);

        if (currentHealth == maxHealth)
        {
            Debug.Log("You are at full health!");
        }
    }

    
    
    
    void EndGame()
    {
        Debug.Log("You reached the end zone. Game Over!");

        Time.timeScale = 0f;

        if(popupPanel !=null)
        {

            popupPanel.SetActive(true);
        }
    }

}
