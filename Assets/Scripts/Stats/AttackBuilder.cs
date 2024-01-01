using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackBuilder
{
    private string attackName;
    private int unlockLevel;
    private GameObject projectile;
    private AttackStats baseStats;
    private List<AttackStats> rarityUpgrades;
    private List<AttackStats> weaponUpgrades;
    private WeaponSetType? weaponSetType;
    private Rarity rarity = 0;

    private Effect effect;
    private AttackTypes attackType;

    private List<Sprite> weaponSprite;
    private List<Sprite> displaySprite;

    private bool isAutoAim;
    private GameObject autoAim;

    private GameObject thrownWeapon;
    private List<Sprite> thrownSprite;
    private GameObject bulletCasing;
    private List<GameObject> muzzleFlashPrefab;
    private float muzzleFlashXOffset;
    private float muzzleFlashYOffset;
    private string description;


    public AttackBuilder SetUnlockLevel(int level)
    {
        this.unlockLevel = level;
        return this;
    }

    public int GetUnlockLevel()
    {
        return unlockLevel;
    }

    public AttackBuilder SetProjectile(GameObject projectile)
    {
        this.projectile = projectile;
        return this;
    }

    public AttackBuilder SetBaseStats(AttackStats baseStats)
    {
        this.baseStats = baseStats;
        this.baseStats.FixUpStats();
        return this;
    }

    public AttackBuilder SetDescription(string description)
    {
        this.description = description;
        return this;
    }


    public AttackBuilder SetWeaponSetType(WeaponSetType weaponSetType)
    {
        this.weaponSetType = weaponSetType;
        return this;
    }

    public AttackBuilder SetRarityUpgrades(List<AttackStats> upgrades)
    {
        this.rarityUpgrades = upgrades;
        return this;
    }

    public AttackBuilder SetAttackName(string attackName)
    {
        this.attackName = attackName;
        //Debug.Log($"SetAttackName: attackName is now {this.attackName}");
        return this;
    }

    public AttackBuilder SetWeaponUpgrades(List<AttackStats> upgrades)
    {
        this.weaponUpgrades = new List<AttackStats>();

        foreach (var upgrade in upgrades)
        {
            AttackStats newUpgrade = new AttackStats(upgrade);
            newUpgrade.AttackName = this.attackName;
            // Create a GameObject for the upgrade and add it to a list:
            GameObject upgradeObject = AttackStatsLibrary.CreateStatGameObject(newUpgrade);
            this.weaponUpgrades.Add(newUpgrade);
        }

        //Debug.Log($"SetWeaponUpgrades: AttackName of weapon upgrades is {this.weaponUpgrades[0].AttackName}");
        //Debug.Log( $"Weapon Upgrades : {string.Join(", ", this.weaponUpgrades.Select(x => x == null ? "null" : x.ToString()))}");

        return this;
    }

    public AttackBuilder SetProperties(
        Effect effect = null,
        AttackTypes attackType = default,
        List<Sprite> weaponSprite = null,
        List<Sprite> displaySprite = null,
        bool isAutoAim = false,
        GameObject autoAim = null,
        GameObject thrownWeapon = null,
        List<Sprite> thrownSprite = null,
        GameObject bulletCasing = null,
        List<GameObject> muzzleFlashPrefab = null,
        float muzzleFlashXOffset = 0,
        float muzzleFlashYOffset = 0,
        float totalDamageDealt = 0
    )
    {
        this.effect = effect;
        this.attackType = attackType;
        this.weaponSprite = weaponSprite;
        this.displaySprite = displaySprite;
        this.isAutoAim = isAutoAim;
        this.autoAim = autoAim;
        this.thrownWeapon = thrownWeapon;
        this.thrownSprite = thrownSprite;
        this.bulletCasing = bulletCasing;
        this.muzzleFlashPrefab = muzzleFlashPrefab;
        this.muzzleFlashXOffset = muzzleFlashXOffset;
        this.muzzleFlashYOffset = muzzleFlashYOffset;
        return this;
    }

    private void ValidateRequiredFields()
    {
        if (string.IsNullOrEmpty(attackName))
        {
            throw new System.Exception("AttackName is required and cannot be null or empty.");
        }

        if (projectile == null)
        {
            Debug.LogWarning("Projectile is required and cannot be null.");
            projectile = Resources.Load<GameObject>("Projectiles/BasicBullet");
        }

        if (baseStats.Equals(null))
        {
            Debug.Log(attackName);
            throw new System.Exception("BaseStats is required and cannot be null.");
        }
        //if (baseStats == null)
        //{
        //Debug.Log(attackName);
        //throw new System.Exception("BaseStats is required and cannot be null.");
        //}

        if (weaponSprite == null)
        {
            throw new System.Exception("WeaponSprite is required and cannot be null.");
        }

        if (thrownWeapon == null)
        {
            Debug.LogWarning("ThrownWeapon is null.");
            //thrownWeapon = Resources.Load<GameObject>("Projectiles/WeaponThrown");
        }

        if (thrownSprite == null)
        {
            Debug.LogWarning("ThrownSprite is null.");
            //thrownSprite = Resources.Load<Sprite>("WeaponSprites/BurstRifle_02");
        }

        if (description == "")
        {
            throw new System.Exception("Description is required and cannot be null.");
        }
        if (weaponSetType == null)
        {
            throw new System.Exception("WeaponSetType is required and cannot be null.");
        }
    }

    public Attack Build(Rarity rarity)
    {
        ValidateRequiredFields();

        // Create a new instance of AttackStats for the Attack object
        AttackStats attackBaseStats = new AttackStats(baseStats);

        // Create a new Attack object
        GameObject attackObject = new GameObject(attackName);
        Attack attack = attackObject.AddComponent<Attack>();

        // Assign the copied AttackStats to the Attack object
        attack.baseStats = attackBaseStats;

        // Set the properties of the Attack component
        attack.name = attackName;
        attack.projectile = projectile;

        if (rarity > 0)
        {
            // Use the WeaponRarityStats class to upgrade the baseStats based on the rarity
            attack.baseStats.rarity = rarity;
            attack.baseStats.mergeInStats(WeaponRarityStats.ApplyRarity(rarityUpgrades, rarity));
        }

        attack.stats = attackBaseStats;
        attack.effect = effect;
        attack.attackType = attackType;
        attack.weaponSprite = weaponSprite;
        attack.displaySprite = displaySprite;
        if (thrownWeapon != null)
        {
            attack.thrownWeapon = thrownWeapon;
            attack.thrownWeapon.GetComponent<Projectile>().damage = attack.baseStats.thrownDamage;
        }
        //Debug.Log(attack.stats.thrownDamage);

        attack.thrownSprite = thrownSprite;
        attack.bulletCasing = bulletCasing;
        attack.MuzzleFlashPrefab = muzzleFlashPrefab;
        attack.muzzleFlashXOffset = muzzleFlashXOffset;
        attack.muzzleFlashYOffset = muzzleFlashYOffset;

        attack.weaponUpgrades = weaponUpgrades;

        attack.weaponSetType = (WeaponSetType)weaponSetType;
        attack.baseStats.description = description;

        if (attackType == AttackTypes.Shotgun)
        {
            attack.attackTime =
                 Mathf.RoundToInt(attack.baseStats.multicastChance) * attack.baseStats.multicastWaitTime;
        }
        else if (attackType == AttackTypes.Melee)
        {
            attack.attackTime =
                (attack.baseStats.comboLength - 1) * attack.baseStats.comboWaitTime
                + (attack.baseStats.shotsPerAttackMelee) * attack.baseStats.spread
                + Mathf.RoundToInt(attack.baseStats.multicastChance) * attack.baseStats.multicastWaitTime;
            // Add the definition for Melee attack type
        }
        else
        {
            attack.attackTime =
                attack.baseStats.spread * attack.baseStats.shotsPerAttack
                + Mathf.RoundToInt(attack.baseStats.multicastChance) * attack.baseStats.multicastWaitTime;
        }

        Debug.Log($"Finished building attack: {attackName}");
        return attack;
    }

    public Sprite GetThrownSprite(int rarity)
    {
        
        return thrownSprite[rarity / 2];
    }

    public Sprite GetDisplaySprite(int rarity)
    {

        return displaySprite[rarity / 2];
    }

    public string GetAttackName()
    {
        return attackName;
    }

    public AttackStats GetBaseStats()
    {
        return baseStats;
    }

    public string GetWeaponSet()
    {
        return weaponSetType.ToString();
    }

    public string GetDescription()
    {
        return description;
    }
}
