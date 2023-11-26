using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StatsHandler : MonoBehaviour
{
    Rigidbody2D rb;
    public int level;
    public float xp;
    public float nextXp;
    public float currentHealth;

    //public PlayerCharacterStats baseStats;
    public PlayerCharacterStats stats;
    public PlayerCharacterStats baseStats;

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

    public float damageCheckDuration = 0.05f;

    GameObject Camera;
    public float playerShakeTime,
        playerShakeStrength,
        playerShakeRotation;

    public GameObject weaponsList;
    private RerollHandler rerollHandler;
    private bool IsRecoveryCoroutineRunning = false;

    public GameObject DamagePopup;
    public GameObject HealVFX;
    public GameObject revengeVFX;

    private void MatchCharacter()
    {
        string storedName = PlayerPrefs.GetString("CharacterName");

        GameObject[] characters = PlayerCharactersLibrary.getCharacters();

        foreach (GameObject obj in characters)
        {
            if (obj.name == storedName)
            {

                stats = obj.GetComponent<StatComponent>().stat;
                baseStats = obj.GetComponent<StatComponent>().stat;
                currentHealth = stats.health;
                CalculatePlayerStats();

                // Updating sprite and animator.
                SpriteRenderer spriteRenderer = Sprite.GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = stats.characterSprite;

                if (storedName == "Witch")
                {
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/v2/Witch");
                    afterimageAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/v2/Witch");
                }
                else if (storedName == "AI")
                {
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/robot/robot_character");
                    afterimageAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/robot/robot_character");

                }
                else //default
                {
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/Player");
                    afterimageAnim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/Player");
                }
                break;
            }
        }

    }

    private void MatchUpgrades()
    {
        string storedUpgradeNames = PlayerPrefs.GetString("PurchasedUpgrades");
        string[] upgradeNames = storedUpgradeNames.Split(',');

        Debug.Log("Retrieved purchased upgrades: " + storedUpgradeNames);


        // For each upgrade name, we retrieve the upgrade from the PlayerUpgradesLibrary
        // and add it to the player's stats.
        foreach (string upgradeName in upgradeNames)
        {
            GameObject upgradeObject = PlayerUpgradesLibrary.getUpgrade(upgradeName);

            if (upgradeObject != null)
            {
                PlayerCharacterStats upgradeStats = upgradeObject.GetComponent<StatComponent>().stat;
                AddStat(upgradeStats);
                Debug.Log(upgradeStats.name);

            }
        }
    }

    private IEnumerator PassiveRecovery()
    {
        while (true)
        {
           if (stats.recoveryAmount != 0)
            {
                float recoverySpeed;

                if (stats.TotalRecoverySpeed < 0.5f)
                {
                    recoverySpeed = 0.5f;
                } else
                {
                    recoverySpeed = stats.TotalRecoverySpeed;
                }

                yield return new WaitForSeconds(recoverySpeed);
                AddHealth(stats.recoveryAmount);
            }
            else
            {
                IsRecoveryCoroutineRunning = false;
                yield break; 
            }
        }
    }

    public void AddHealth(float amount)
    {
        float newHealth;
        if (currentHealth + amount >= stats.maxHealth)
        {
            newHealth = stats.maxHealth;
        }
        else
        {
            newHealth = currentHealth + amount;
        }

        healthBarQueue.AddToQueue(
            BarHelper.AddToBar(healthBar, currentHealth, newHealth, stats.maxHealth, 0.4f)
        );
        currentHealth = newHealth;
        PopupNumber(amount);
    }

    public void PopupNumber(float number)
    {
        Vector3 popupPosition = rb.position;
        popupPosition.x = Random.Range(popupPosition.x - 0.05f, popupPosition.x + 0.05f);
        popupPosition.y = Random.Range(popupPosition.y, popupPosition.y + 0.1f);
        DamagePopupText damagePopup = Instantiate(DamagePopup, popupPosition, Quaternion.identity).GetComponent<DamagePopupText>();
        damagePopup.GetComponent<DamagePopupText>().Setup(number, false, true);

        Instantiate(HealVFX, popupPosition, Quaternion.identity);
    }

    private class DamageSource
    {
        public float DamageAmount;
        public GameObject Enemy;
    }

    private List<DamageSource> damageSourcesDuringIframes = new List<DamageSource>();
    private bool isEvaluatingDamage = false;

    public void TakeDamage(float damageAmount, GameObject enemy)
    {
        if (!canDamage)
        {
            return;
        }

        // Store potential damage sources
        damageSourcesDuringIframes.Add(new DamageSource { DamageAmount = damageAmount, Enemy = enemy });

        if (!isEvaluatingDamage)
        {
            isEvaluatingDamage = true;
            StartCoroutine(EvaluateDamageAfterDelay(damageCheckDuration));
        }
    }

    private IEnumerator EvaluateDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (damageSourcesDuringIframes.Count > 0)
        {
            var highestDamageSource = damageSourcesDuringIframes.OrderByDescending(d => d.DamageAmount).First();

            // Apply the highest damage
            float damageAmount = highestDamageSource.DamageAmount;
            GameObject enemy = highestDamageSource.Enemy;

            // Now handle the damage with the highest damage source
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
                BarHelper.RemoveFromBar(healthBar, currentHealth, newHealth, stats.maxHealth, 0.3f)
            );
            currentHealth = newHealth;
            if (currentHealth <= 0)
            {
                GameObject.FindObjectOfType<EndgameStatTracker>().EndGameStats();
                GameObject.FindObjectOfType<GameManager>().EndGame();
            }

            if (stats.isRevenge && enemy != null)
            {
                bool isCrit;
                float revengeDamageAmount = stats.revengeDamage;

                if (stats.critChance >= 1)
                {
                    isCrit = true;
                }
                else
                {
                    isCrit = UnityEngine.Random.Range(0f, 1f) < stats.critChance;
                }

                if (isCrit)
                {
                    // Apply critical damage multiplier
                    revengeDamageAmount *= stats.critDmg;
                }

                enemy.GetComponent<Enemy>().TakeDamage(revengeDamageAmount, isCrit);

                if (stats.isLifesteal)
                {
                    bool isLifesteal;
                    if (stats.lifestealChance >= 1)
                    {
                        isLifesteal = true;
                    }
                    else
                    {
                        isLifesteal = UnityEngine.Random.Range(0f, 1f) < stats.lifestealChance;
                    }

                    if (isLifesteal)
                    {
                        AddHealth(stats.lifestealAmount);
                    }
                }
            }
            // Clear the stored damage sources
            damageSourcesDuringIframes.Clear();
        }

        isEvaluatingDamage = false;
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
        float finalXP = xpAmount * (1 + stats.xpGainMultiplier);

        LevelManager.AddXP(xp, finalXP + xp, nextXp);
        xp += finalXP;
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
        float currentHealth = stats.health;
        stats = new PlayerCharacterStats(baseStats);
        stats.health = currentHealth;


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

        currentHealth += stat.health;
        if (stat.health != 0)
        {
            PopupNumber(stat.health); 
        }

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

        if (stats.rerollTimes != 0)
        {
            rerollHandler.AddReroll(stats.rerollTimes);
            stats.rerollTimes = 0;
            baseStats.rerollTimes = 0;
        }

        if (stats.swapTimes != 0)
        {
            rerollHandler.AddSwap(stats.swapTimes);
            stats.swapTimes = 0;
            baseStats.swapTimes = 0;
        }

        GetComponent<PlayerMovement>().SetAnimSpeed(stats.speed * (stats.speedMultiplier + 1), 1f); //change second value to be the default

        if (currentHealth > stats.maxHealth)
            currentHealth = stats.maxHealth;

        healthBarQueue.AddToQueue(BarHelper.ForceUpdateBar(healthBar, currentHealth, stats.maxHealth));

        CalculateWeaponStats(weaponsList);

        if (stats.recoveryAmount > 0)
        {
            if (!IsRecoveryCoroutineRunning)
            {
                StartCoroutine(PassiveRecovery());
                IsRecoveryCoroutineRunning = true;
            }
        }
        else if (IsRecoveryCoroutineRunning)
        {
            StopCoroutine("PassiveRecovery");
            IsRecoveryCoroutineRunning = false;
        }
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
        rerollHandler = GetComponentInChildren<RerollHandler>();
        rb = GetComponent<Rigidbody2D>();
        MatchCharacter();
        MatchUpgrades();
        if (stats.recoveryAmount != 0)
        {
            StartCoroutine(PassiveRecovery());
            IsRecoveryCoroutineRunning = true;
        }
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
