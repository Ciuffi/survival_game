using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackBuilder
{
    private string attackName;
    private GameObject projectile;
    private AttackStats baseStats;
    private List<AttackStats> rarityUpgrades;
    private List<AttackStats> weaponUpgrades;
    private WeaponSetType? weaponSetType;
    private Rarity rarity = 0;

    private Effect effect;
    private AttackTypes attackType;

    private Sprite weaponSprite;
    private bool isAutoAim;
    private GameObject autoAim;

    private GameObject thrownWeapon;
    private Sprite thrownSprite;
    private GameObject bulletCasing;
    private List<GameObject> muzzleFlashPrefab;
    private float muzzleFlashXOffset;
    private float muzzleFlashYOffset;
    private string description;

    public AttackBuilder SetProjectile(GameObject projectile)
    {
        this.projectile = projectile;
        return this;
    }

    public AttackBuilder SetBaseStats(AttackStats baseStats)
    {
        this.baseStats = baseStats;
        return this;
    }

    public AttackBuilder SetDescription(string description)
    {
        this.description = description;
        return this;
    }

    public AttackBuilder SetWeaponUpgrades(List<AttackStats> upgrades)
    {
        this.weaponUpgrades = upgrades;
        Debug.Log(
            $"Weapon Upgrades : {string.Join(", ", this.weaponUpgrades.Select(x => x == null ? "null" : x.ToString()))}"
        );
        //Debug.Log(upgrades);
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
        return this;
    }

    public AttackBuilder SetProperties(
        Effect effect = null,
        AttackTypes attackType = default,
        Sprite weaponSprite = null,
        bool isAutoAim = false,
        GameObject autoAim = null,
        GameObject thrownWeapon = null,
        Sprite thrownSprite = null,
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
            Debug.LogWarning("ThrownWeapon is required and cannot be null.");
            thrownWeapon = Resources.Load<GameObject>("Projectiles/WeaponThrown");
        }

        if (thrownSprite == null)
        {
            Debug.LogWarning("ThrownSprite is required and cannot be null.");
            thrownSprite = Resources.Load<Sprite>("WeaponSprites/BurstRifle_02");
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
        this.baseStats = new AttackStats(baseStats);

        //ValidateRequiredFields();
        GameObject attackObject = new GameObject(attackName);
        Attack attack = attackObject.AddComponent<Attack>();
        attack.baseStats = baseStats;
        attack.stats = baseStats;

        // Set the properties of the Attack component
        attack.name = attackName;
        attack.projectile = projectile;

        if (rarity > 0)
        {
            Debug.Log("rarity: " + rarity);
            // Use the WeaponRarityStats class to upgrade the baseStats based on the rarity
            attack.baseStats.mergeInStats(WeaponRarityStats.ApplyRarity(rarityUpgrades, rarity));
        }

        attack.effect = effect;
        attack.attackType = attackType;
        attack.weaponSprite = weaponSprite;
        if (thrownWeapon != null)
        {
            attack.thrownWeapon = thrownWeapon;
        }
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
                attack.baseStats.multicastTimes * attack.baseStats.multicastWaitTime;
        }
        else if (attackType == AttackTypes.Melee)
        {
            attack.attackTime =
                (attack.baseStats.comboLength - 1) * attack.baseStats.comboWaitTime
                + attack.baseStats.shotsPerAttackMelee * attack.baseStats.spread
                + attack.baseStats.multicastTimes * attack.baseStats.multicastWaitTime;
            // Add the definition for Melee attack type
        }
        else
        {
            attack.attackTime =
                attack.baseStats.spread * attack.baseStats.shotsPerAttack
                + attack.baseStats.multicastTimes * attack.baseStats.multicastWaitTime;
        }

        Debug.Log($"Finished building attack: {attackName}");
        return attack;
    }

    public Sprite GetThrownSprite()
    {
        return thrownSprite;
    }

    public string GetAttackName()
    {
        return attackName;
    }

    public AttackStats GetBaseStats()
    {
        return baseStats;
    }
}
