using System.Collections.Generic;
using UnityEngine;

public class AttackBuilder
{
    private string attackName;
    private GameObject projectile;
    private AttackStats baseStats;
    private List<AttackStats> upgrades;
    private int rarity = 0;

    private Effect effect;
    private AttackTypes attackType;
    private GameObject meleeAttack;

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

    public AttackBuilder SetUpgrades(List<AttackStats> upgrades)
    {
        this.upgrades = upgrades;
        return this;
    }

    public AttackBuilder SetRarity(int rarity)
    {
        this.rarity = rarity;
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
        GameObject meleeAttack = null,
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
        this.meleeAttack = meleeAttack;
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

        if (baseStats == null)
        {
            throw new System.Exception("BaseStats is required and cannot be null.");
        }

        if (weaponSprite == null)
        {
            throw new System.Exception("WeaponSprite is required and cannot be null.");
        }

        if (thrownWeapon == null)
        {
            throw new System.Exception("ThrownWeapon is required and cannot be null.");
        }

        if (thrownSprite == null)
        {
            throw new System.Exception("ThrownSprite is required and cannot be null.");
        }
    }

    public GameObject Build()
    {
        ValidateRequiredFields();
        // Create a new GameObject to attach the Attack component to
        GameObject attackObject = new GameObject(
            string.IsNullOrEmpty(attackName) ? "Attack" : attackName
        );
        Attack attack = attackObject.AddComponent<Attack>();

        // Set the properties of the Attack component
        attack.projectile = projectile;
        attack.baseStats = baseStats;

        // Use the WeaponRarityStats class to upgrade the baseStats based on the rarity
        attack.baseStats = WeaponRarityStats.ApplyRarity(upgrades, rarity);

        attack.effect = effect;
        attack.attackType = attackType;
        attack.MeleeAttack = meleeAttack;
        attack.weaponSprite = weaponSprite;
        attack.isAutoAim = isAutoAim;
        attack.AutoAim = autoAim;
        attack.thrownWeapon = thrownWeapon;
        attack.thrownSprite = thrownSprite;
        attack.bulletCasing = bulletCasing;
        attack.MuzzleFlashPrefab = muzzleFlashPrefab;
        attack.muzzleFlashXOffset = muzzleFlashXOffset;
        attack.muzzleFlashYOffset = muzzleFlashYOffset;

        return attackObject;
    }
}
