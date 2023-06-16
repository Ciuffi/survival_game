using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    public int level;
    public float xp;
    public float nextXp;
    public float currentHealth;

    //public PlayerCharacterStats baseStats;
    public PlayerCharacterStats stats;

    public float Iframes;
    public float IFtimer;
    public bool canDamage;

    public LevelUpManager LevelManager;
    private Slider healthBar;
    private Color healthColor;
    private CoroutineQueue healthBarQueue;
    public GameObject StatContainer;

    public Animator animator;
    public Animator afterimageAnim;
    GameObject ComboManager;

    public GameObject Sprite;
    private SpriteRenderer spriteRend;
    private Material OGMaterial;
    public Material newMaterial;
    private bool resetMaterial = false;
    public float flashDuration;

    GameObject Camera;
    public float playerShakeTime,
        playerShakeStrength,
        playerShakeRotation;

    public GameObject weaponsList;

    private void MatchCharacter()
    {
        string storedName = PlayerPrefs.GetString("CharacterName");

        GameObject[] characters = PlayerCharactersLibrary.getCharacters();

        foreach (GameObject obj in characters)
        {
            if (obj.name == storedName)
            {
                stats = obj.GetComponent<StatComponent>().stat;
                currentHealth = stats.health;
                CalculatePlayerStats();
                MatchUpgrades();
                Debug.Log("Matched Upgrades");
                break;
            }
        }

    }

    private void MatchUpgrades()
    {
        string storedUpgradeNames = PlayerPrefs.GetString("PurchasedUpgrades");
        string[] upgradeNames = storedUpgradeNames.Split(',');

        // For each upgrade name, we retrieve the upgrade from the PlayerUpgradesLibrary
        // and add it to the player's stats.
        foreach (string upgradeName in upgradeNames)
        {
            GameObject upgradeObject = PlayerUpgradesLibrary.getUpgrade(upgradeName);
            Debug.Log(upgradeObject.name);

            if (upgradeObject != null)
            {
                PlayerCharacterStats upgradeStats = upgradeObject.GetComponent<StatComponent>().stat;
                AddStat(upgradeStats);
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (!canDamage)
        {
            return;
        }
        canDamage = false;
        float newHealth;
        if ((damageAmount - stats.defense) > 0)
        {
            newHealth = currentHealth - damageAmount + stats.defense;
        }
        else
        {
            newHealth = currentHealth;
        }
        animator.SetBool("TookDamage", true);
        afterimageAnim.SetBool("TookDamage", true);

        spriteRend.material = newMaterial;
        resetMaterial = true;
        healthBar.fillRect.GetComponent<Image>().color = Color.red;
        Camera
            .GetComponent<ScreenShakeController>()
            .StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);

        healthBarQueue.AddToQueue(
            BarHelper.RemoveFromBar(healthBar, currentHealth, newHealth, stats.maxHealth, 0.5f)
        );
        currentHealth = newHealth;
        if (currentHealth <= 0)
        {
            GameObject.FindObjectOfType<EndgameStatTracker>().EndGameStats();
            GameObject.FindObjectOfType<GameManager>().EndGame();
        }
    }

    void ResetMaterial()
    {
        healthBar.fillRect.GetComponent<Image>().color = healthColor;
        spriteRend.material = OGMaterial;
    }

    void Update()
    {
        if (IFtimer >= Iframes)
        {
            animator.SetBool("TookDamage", false);
            afterimageAnim.SetBool("TookDamage", false);
            if (resetMaterial)
            {
                ResetMaterial();
                resetMaterial = false;
            }
            canDamage = true;
        }

        if (canDamage == false)
        {
            IFtimer += Time.deltaTime;
        }
        else
        {
            IFtimer = 0f;
        }
    }

    public void GainXP(float xpAmount)
    {
        LevelManager.AddXP(xp, xpAmount + xp, nextXp);
        xp += xpAmount;
        if (xp >= nextXp)
        {
            nextXp = LevelManager.GetXpToNextLevel(level);
            LevelUp();
        }
    }

    public void LevelUp()
    {
        xp = 0;
        level = level + 1;
        LevelManager.LevelUp(level);
    }

    public void ResetStats(bool fullReset)
    {
        stats = new PlayerCharacterStats(stats);

        if (fullReset)
        {
            level = 1;
            xp = 0;
            nextXp = LevelManager.GetXpToNextLevel(level);
            currentHealth = stats.maxHealth;
            foreach (Transform trans in StatContainer.transform)
            {
                Destroy(trans.gameObject);
            }
            healthBarQueue.EmptyQueue();
            healthBarQueue.AddToQueue(
                BarHelper.ForceUpdateBar(healthBar, currentHealth, stats.maxHealth)
            );
            LevelManager.ResetXP();
        }
    }

    public void AddStat(PlayerCharacterStats stat)
    {
        var statsContainer = Instantiate(stat.statsContainer, StatContainer.transform);
        var statComponent = statsContainer.GetComponent<StatComponent>();
        statComponent.stat = stat;

        currentHealth += stat.health; // Add the health stat as a one-time heal

        CalculatePlayerStats();
    }

    public void CalculatePlayerStats()
    {
        ResetStats(false);

        if (StatContainer != null)
        {
            foreach (var statHolder in StatContainer.GetComponentsInChildren<StatComponent>())
            {
                stats.MergeStats(statHolder.stat);
            }
        }

        GetComponent<PlayerMovement>().SetAnimSpeed(stats.speed * (stats.speedMultiplier + 1), 1f); //change second value to be the default

        if (currentHealth > stats.maxHealth)
            currentHealth = stats.maxHealth;

        Debug.Log(stats.damageMultiplier);
        Debug.Log(stats.speed + "and Speed Multiplier: " + stats.speedMultiplier) ; 

        Debug.Log(healthBar);
        Debug.Log(healthBarQueue);

        Debug.Log(currentHealth);
        Debug.Log(stats.maxHealth);

        healthBarQueue.AddToQueue(BarHelper.ForceUpdateBar(healthBar, currentHealth, stats.maxHealth));

        CalculateWeaponStats(weaponsList);
    }

    void Start()
    {
        weaponsList = transform.Find("Weapons").gameObject;
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        OGMaterial = spriteRend.material;
        Camera = GameObject.FindWithTag("MainCamera");
        ComboManager = GameObject.FindWithTag("ComboManager");
        ComboManager.GetComponent<ComboTracker>().ResetCount();
        level = 1;
        xp = 0;
        LevelManager = GameObject.FindObjectOfType<LevelUpManager>();
        nextXp = LevelManager.GetXpToNextLevel(level);
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBarQueue = gameObject.AddComponent<CoroutineQueue>();
        healthBarQueue.StartQueue();
        healthColor = healthBar.fillRect.GetComponent<Image>().color;
        MatchCharacter();

    }

    private void CalculateWeaponStats(GameObject prefab)
    {
        // Get all child game objects of the prefab
        foreach (Transform child in prefab.transform)
        {
            // Check if the child game object has an attached Attack script
            Attack attackScript = child.gameObject.GetComponent<Attack>();
            if (attackScript != null)
            {
                // Invoke the "CalculateStats()" function if it exists
                attackScript.CalculateStats();
            }
        }
    }

    private GameObject[] GetGameObjectsInPrefab(GameObject prefab)
    {
        // Get all game objects in the prefab
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (Transform child in prefab.transform)
        {
            gameObjects.Add(child.gameObject);
        }
        // Return the game objects in the prefab
        return gameObjects.ToArray();
    }

    public float GetPlayerHpPercent()
    {
        return (float)currentHealth / stats.maxHealth;
    }
}
