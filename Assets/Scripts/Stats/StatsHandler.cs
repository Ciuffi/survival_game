using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    public int level;
    public float xp;
    public float nextXp;
    public float health;
    public float baseMaxHealth;

    public float maxHealth;
    public float speed;
    public float baseSpeed;
    public float damageMultipler, baseDamageMultiplier;
    public float critChance;
    public float baseCritChance;
    public float critDmg;
    public float baseCritDmg;

    public float defense; //flat damage decrease
    public float baseDefense;
    public float shield;
    public float baseShield;
    public float pickupRange,
        basePickupRange;

    public int shotsPerAttack,
        baseShotsPerAttack,
        shotsPerAttackMelee,
        baseShotsPerAttackMelee,
        meleeComboLength,
        baseMeleeComboLength;

    public float multicastChance,
        baseMulticastChance,
        castTimeMultiplier,
        baseCastTimeMultiplier,
        projectileSpeedMultiplier,
        baseProjectileSpeedMultiplier,
        knockbackMultiplier,
        baseKnockbackMultiplier,
        meleeWaitTimeMultiplier,
        baseMeleeWaitTimeMultiplier,
        thrownDamageMultiplier,
        baseThrownDamageMultiplier,
        thrownSpeedMultiplier,
        baseThrownSpeedMultiplier,
        rangeMultiplier,
        baseRangeMultiplier,
        projectileSizeMultiplier,
        baseProjectileSizeMultiplier,
        meleeSizeMultiplier,
        baseMeleeSizeMultiplier;

    public bool shootOppositeSide,
        baseShootOppositeSide;

    public List<StatBoost> stats;

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
    public float playerShakeTime, playerShakeStrength, playerShakeRotation;

    public GameObject weaponsList;
    PlayerCharacterStats characterStats;


    private void MatchCharacter()
    {
        string storedName = PlayerPrefs.GetString("CharacterName");
        GameObject[] characters = Resources.LoadAll<GameObject>("PlayerCharacters");

        foreach (GameObject obj in characters)
        {
            if (obj.name == storedName)
            {
                characterStats = obj.GetComponent<PlayerCharacterStats>();
                break;
            }
        }
    }


    public void InhereitStats()
    {

        // Get the selected character's stats from PlayerPrefs
        float health = characterStats.health;
        float speed = characterStats.speed;
        float damage = characterStats.damageMultiplier;
        float critChance = characterStats.critChance;
        float critDmg = characterStats.critDmg;
        float defense = characterStats.defense;
        float shield = characterStats.shield;
        int shotsPerAttack = characterStats.shotsPerAttack;
        int shotsPerAttackMelee = characterStats.shotsPerAttackMelee;
        int comboLength = characterStats.meleeComboLength;
        float multicast = characterStats.multicastChance;
        float castTime = characterStats.castTimeMultiplier;
        float projSpeed = characterStats.projectileSpeedMultiplier;
        float knockback = characterStats.knockbackMultiplier;
        float comboWaitTime = characterStats.meleeWaitTimeMultiplier;
        float thrownDmg = characterStats.thrownDamageMultiplier;
        float thrownSpd = characterStats.thrownSpeedMultiplier;
        float range = characterStats.rangeMultiplier;
        bool shootOpposite = characterStats.shootOpposideSide;
        float pickupRange = characterStats.pickupRange;
        float projSize = characterStats.projectileSizeMultiplier;
        float meleeSize = characterStats.meleeSizeMultiplier;

        // Assign the selected character's stats to the player's stats
        baseMaxHealth = maxHealth;
        baseSpeed = speed;
        baseDamageMultiplier = damage;
        baseCritChance = critChance;
        baseCritDmg = critDmg;
        baseDefense = defense;
        baseShield = shield;
        baseShotsPerAttack = shotsPerAttack;
        baseShotsPerAttackMelee = shotsPerAttackMelee;
        baseMeleeComboLength = comboLength;
        baseMulticastChance = multicast;
        baseCastTimeMultiplier = castTime;
        baseProjectileSpeedMultiplier = projSpeed;
        baseKnockbackMultiplier = knockback;
        baseMeleeWaitTimeMultiplier = comboWaitTime;
        baseThrownDamageMultiplier = thrownDmg;
        baseThrownSpeedMultiplier = thrownSpd;
        baseRangeMultiplier = range;
        baseShootOppositeSide = shootOpposite;
        basePickupRange = pickupRange;
        baseProjectileSizeMultiplier = projSize;
        baseMeleeSizeMultiplier = meleeSize;

    }


    public void TakeDamage(float damageAmount)
    {
        if (!canDamage)
        {
            return;

        }else
        {
            canDamage = false;
            float newHealth = health - damageAmount + defense;
            animator.SetBool("TookDamage", true);
            afterimageAnim.SetBool("TookDamage", true);

            spriteRend.material = newMaterial;
            healthBar.fillRect.GetComponent<Image>().color = Color.red;
            if (!resetMaterial)
            {
                StartCoroutine(ResetMaterial());
                resetMaterial = true;
            }

            Camera.GetComponent<ScreenShakeController>().StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);

            healthBarQueue.AddToQueue(BarHelper.RemoveFromBar(healthBar, health, newHealth, maxHealth, 0.5f));
            health = newHealth;
            if (health <= 0)
            {
                GameObject.FindObjectOfType<EndgameStatTracker>().OnPlayerDeath();
                GameObject.FindObjectOfType<GameManager>().EndGame();
            }
        } 
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);
        if (canDamage)
        {
            healthBar.fillRect.GetComponent<Image>().color = healthColor;
            spriteRend.material = OGMaterial;
            resetMaterial = false;
        }
        else
        {
            StartCoroutine(ResetMaterial());
        }
    }


    void Update()
    {

        if (IFtimer >= Iframes)
        {
            animator.SetBool("TookDamage", false);
            afterimageAnim.SetBool("TookDamage", false);
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
        speed = baseSpeed;
        maxHealth = baseMaxHealth;
        shield = baseShield;
        damageMultipler = baseDamageMultiplier;
        defense = baseDefense;
        critChance = baseCritChance;
        critDmg = baseCritDmg;
        multicastChance = baseMulticastChance;
        castTimeMultiplier = baseCastTimeMultiplier;
        shotsPerAttack = baseShotsPerAttack;
        shotsPerAttackMelee = baseShotsPerAttackMelee;
        projectileSpeedMultiplier = baseProjectileSpeedMultiplier;
        knockbackMultiplier = baseKnockbackMultiplier;
        meleeComboLength = baseMeleeComboLength;
        meleeWaitTimeMultiplier = baseMeleeWaitTimeMultiplier;
        thrownDamageMultiplier = baseThrownDamageMultiplier;
        thrownSpeedMultiplier = baseThrownSpeedMultiplier;
        rangeMultiplier = baseRangeMultiplier;
        shootOppositeSide = baseShootOppositeSide;
        pickupRange = basePickupRange;
        projectileSizeMultiplier = baseProjectileSizeMultiplier;
        meleeSizeMultiplier = baseMeleeSizeMultiplier;


        if (fullReset)
        {
            level = 1;
            xp = 0;
            nextXp = LevelManager.GetXpToNextLevel(level);
            health = maxHealth;
            foreach (Transform trans in StatContainer.transform)
            {
                Destroy(trans.gameObject);
            }
            healthBarQueue.EmptyQueue();
            healthBarQueue.AddToQueue(BarHelper.ForceUpdateBar(healthBar, health, maxHealth));
            LevelManager.ResetXP();
        }
    }



    public void AddStat(GameObject stat)
    {
        stat.transform.parent = StatContainer.transform;
        this.stats.Add(stat.GetComponent<StatBoost>());
        CalculateStats();
        // Find and invoke the "CalculateStats()" function in the Attack scripts of the prefab
        CalculateWeaponStats(weaponsList);

    }

    public void CalculateStats()
    {
        ResetStats(false);

        foreach (var stat in stats)
        {
            if (stat != null)
            {
                StatBoost sb = stat.GetComponent<StatBoost>();

                // Apply the stat's values
                health += sb.extraHealth;
                if (sb.extraHealth > 0)
                {
                    Destroy(stat);
                }
                maxHealth += sb.extraMaxHealth;
                speed += sb.extraSpeed;
                shield += sb.extraShield;
                damageMultipler += sb.extraDamageMultipler;
                defense += sb.extraDefense;
                critChance += sb.extraCritChance;
                critDmg += sb.extraCritDmg;
                multicastChance += sb.extraMulticastChance;
                castTimeMultiplier = (float)((castTimeMultiplier + sb.extraCastTimeMultiplier) > 0.1 ? (castTimeMultiplier + sb.extraCastTimeMultiplier) : 0.1);
                meleeComboLength += sb.extraMeleeComboLength;
                shotsPerAttack += sb.extraShotsPerAttack;
                shotsPerAttackMelee += sb.extraShotsPerAttackMelee;
                projectileSpeedMultiplier += sb.extraProjectileSpeedMultiplier;
                knockbackMultiplier += sb.extraKnockbackMultiplier;
                meleeWaitTimeMultiplier = (float)((meleeWaitTimeMultiplier + sb.extraMeleeWaitTimeMultiplier) > 0.1 ? (meleeWaitTimeMultiplier + sb.extraMeleeWaitTimeMultiplier) : 0.1);
                thrownDamageMultiplier += sb.extraThrownDamageMultiplier;
                thrownSpeedMultiplier += sb.extraThrownSpeedMultiplier;
                rangeMultiplier += sb.extraRangeMultiplier;
                if (!shootOppositeSide)
                {
                    shootOppositeSide = sb.shootOppositeSide;
                }
                pickupRange += sb.extraPickupRange;
                projectileSizeMultiplier += sb.extraProjectileSizeMultiplier;
                meleeSizeMultiplier += sb.extraMeleeSizeMultiplier;
            }
        }


        if (health > maxHealth) health = maxHealth;
        healthBarQueue.AddToQueue(BarHelper.ForceUpdateBar(healthBar, health, maxHealth));
    }

  

    void Start()
    {
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
        InhereitStats();

        //add extra stats?
        StatContainer = new List<Transform>(GetComponentsInChildren<Transform>()).Find(t =>
        {
            return t.name == "Stats";
        }).gameObject;
        new List<StatBoost>(StatContainer.GetComponentsInChildren<StatBoost>()).ForEach(a =>
        {
            AddStat(a.gameObject);
        });

        CalculateStats();
        CalculateWeaponStats(weaponsList);

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
}

