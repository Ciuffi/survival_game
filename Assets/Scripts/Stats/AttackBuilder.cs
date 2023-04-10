using System.Collections.Generic;
using UnityEngine;

public class AttackBuilder
{
    private string attackName;
    private GameObject projectile;
    private AttackStats baseStats;
    private List<AttackStats> rarityUpgrades;
    private List<AttackStats> weaponUpgrades;
    private WeaponSetType weaponSetType;
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
        this.baseStats.description = description;
        return this;
    }

    public AttackBuilder SetWeaponUpgrades(List<AttackStats> upgrades)
    {
        this.weaponUpgrades = upgrades;
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
            throw new System.Exception("Projectile is required and cannot be null.");
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

        //if (thrownWeapon == null)
        //{
        //throw new System.Exception("ThrownWeapon is required and cannot be null.");
        //}

        if (thrownSprite == null)
        {
            throw new System.Exception("ThrownSprite is required and cannot be null.");
        }
        if (baseStats.description == "")
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
        this.baseStats = new AttackStats(baseStats);

        //ValidateRequiredFields();
        GameObject attackObject = new GameObject(attackName);

        Attack attack = attackObject.AddComponent<Attack>();
        attack.baseStats = baseStats;

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
        attack.isAutoAim = isAutoAim;
        attack.AutoAim = autoAim;
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
        attack.weaponSetType = weaponSetType;

        
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
