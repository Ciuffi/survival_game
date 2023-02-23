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

    public int shotsPerAttack,
        baseShotsPerAttack,
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
        baseThrownSpeedMultiplier;

    public List<StatBoost> stats;

    public float Iframes;
    public float IFtimer;
    public bool canDamage;

    public LevelUpManager LevelManager;
    private Slider healthBar;
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
            if (!resetMaterial)
            {
                StartCoroutine(ResetMaterial());
                resetMaterial = true;
            }

            Camera.GetComponent<ScreenShakeController>().StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);

            healthBarQueue.AddToQueue(BarHelper.RemoveFromBar(healthBar, health, newHealth, maxHealth, 0.5f));
            health = newHealth;
            if (health <= 0) GameObject.FindObjectOfType<GameManager>().ResetGame();
        } 
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);
        if (canDamage)
        {
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
        projectileSpeedMultiplier = baseProjectileSpeedMultiplier;
        knockbackMultiplier = baseKnockbackMultiplier;
        meleeComboLength = baseMeleeComboLength;
        meleeWaitTimeMultiplier = baseMeleeWaitTimeMultiplier;
        thrownDamageMultiplier = baseThrownDamageMultiplier;
        thrownSpeedMultiplier = baseThrownSpeedMultiplier;


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


        // Create a HashSet to keep track of which stat names have already been applied
        HashSet<string> appliedStatNames = new HashSet<string>();

        foreach (var stat in stats)
        {
            // Get the name of the current stat
            string statName = stat.name;

            // Check if this stat's name has already been applied
            if (appliedStatNames.Contains(statName))
            {
                continue; // Skip over this stat, since its values have already been applied
            }

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
            projectileSpeedMultiplier += sb.extraProjectileSpeedMultiplier;
            knockbackMultiplier += sb.extraKnockbackMultiplier;
            meleeWaitTimeMultiplier = (float)((meleeWaitTimeMultiplier + sb.extraMeleeWaitTimeMultiplier) > 0.1 ? (meleeWaitTimeMultiplier + sb.extraMeleeWaitTimeMultiplier) : 0.1);
            thrownDamageMultiplier += sb.extraThrownDamageMultiplier;
            thrownSpeedMultiplier += sb.extraThrownSpeedMultiplier;

            // Add this stat's name to the HashSet of applied stat names
            appliedStatNames.Add(statName);
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
        health = maxHealth;
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

