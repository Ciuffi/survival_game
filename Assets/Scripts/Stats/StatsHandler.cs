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
    public PlayerCharacterStats baseStats;
    public PlayerCharacterStats stats;

    public float Iframes;
    public float IFtimer;
    public bool canDamage;

    public LevelUpManager LevelManager;
    private Slider healthBar;
    private Color healthColor;
    private CoroutineQueue healthBarQueue;
    private GameObject StatContainer;

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
        GameObject[] characters = Resources.LoadAll<GameObject>("PlayerCharacters");
        foreach (GameObject obj in characters)
        {
            if (obj.name == storedName)
            {
                baseStats = obj.GetComponent<PlayerCharacterStats>();
                break;
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
        stats = new PlayerCharacterStats(baseStats);

        if (fullReset)
        {
            level = 1;
            xp = 0;
            nextXp = LevelManager.GetXpToNextLevel(level);
            currentHealth = baseStats.maxHealth;
            foreach (Transform trans in StatContainer.transform)
            {
                Destroy(trans.gameObject);
            }
            healthBarQueue.EmptyQueue();
            healthBarQueue.AddToQueue(
                BarHelper.ForceUpdateBar(healthBar, currentHealth, baseStats.maxHealth)
            );
            LevelManager.ResetXP();
        }
    }

    public void AddStat(PlayerCharacterStats stat)
    {
        Instantiate(stat, StatContainer.transform);
        CalculatePlayerStats();
    }

    public void CalculatePlayerStats()
    {
        ResetStats(false);

        if (StatContainer != null)
        {
            foreach (var stat in StatContainer.GetComponentsInChildren<PlayerCharacterStats>())
            {
                stats.MergeStats(stat);
            }
        }

        GetComponent<PlayerMovement>().SetAnimSpeed(stats.speed, 0.038f); //change second value to be the default
        if (currentHealth > stats.maxHealth)
            currentHealth = stats.maxHealth;
        healthBarQueue.AddToQueue(
            BarHelper.ForceUpdateBar(healthBar, currentHealth, stats.maxHealth)
        );

        CalculateWeaponStats(weaponsList);
    }

    void Start()
    {
        MatchCharacter();

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

        CalculatePlayerStats();
    }

    private void CalculateWeaponStats(GameObject prefab)
    {
        // Get all child game objects of the prefab
        foreach (Transform child in prefab.transform)
        {
            Debug.Log(prefab.name);
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
