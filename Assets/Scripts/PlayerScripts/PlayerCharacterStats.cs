using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStats : MonoBehaviour, Upgrade
{
    public Sprite icon;
    public string name;
    public string description;
    public string characterName;
    public bool isLocked;

    // Base stats
    public float maxHealth,
        health,
        speed,
        pickupRange,
        damageMultiplier,
        critChance,
        critDmg,
        defense,
        shield;

    // Attack stats
    public int shotsPerAttack,
        shotsPerAttackMelee,
        comboLength;

    public float multicastChance,
        shotgunSpread,
        spreadMultiplier,
        castTimeMultiplier,
        comboWaitTime,
        projectileSpeedMultiplier,
        rangeMultiplier,
        knockbackMultiplier,
        thrownDamageMultiplier,
        thrownSpeedMultiplier,
        projectileSizeMultiplier,
        meleeSizeMultiplier;

    public bool shootOpposideSide;
    public Rarity rarity;

    // Merges the current PlayerCharacterStats with the provided PlayerCharacterStats
    public void MergeStats(PlayerCharacterStats other)
    {
        health += other.health;
        speed += other.speed;
        pickupRange += other.pickupRange;
        damageMultiplier += other.damageMultiplier;
        critChance += other.critChance;
        critDmg += other.critDmg;
        defense += other.defense;
        shield += other.shield;

        shotsPerAttack += other.shotsPerAttack;
        shotsPerAttackMelee += other.shotsPerAttackMelee;
        comboLength += other.comboLength;

        multicastChance += other.multicastChance;
        shotgunSpread += other.shotgunSpread;
        spreadMultiplier += other.spreadMultiplier;
        castTimeMultiplier += other.castTimeMultiplier;
        comboWaitTime += other.comboWaitTime;
        projectileSpeedMultiplier += other.projectileSpeedMultiplier;
        rangeMultiplier += other.rangeMultiplier;
        knockbackMultiplier += other.knockbackMultiplier;
        thrownDamageMultiplier += other.thrownDamageMultiplier;
        thrownSpeedMultiplier += other.thrownSpeedMultiplier;
        projectileSizeMultiplier += other.projectileSizeMultiplier;
        meleeSizeMultiplier += other.meleeSizeMultiplier;

        shootOpposideSide |= other.shootOpposideSide;
        this.rarity = this.rarity.CompareRarity(other.rarity);
    }

    // constructor that takes in a PlayerCharacterStats object and copies its values
    public PlayerCharacterStats(PlayerCharacterStats other)
    {
        health = other.health;
        speed = other.speed;
        pickupRange = other.pickupRange;
        damageMultiplier = other.damageMultiplier;
        critChance = other.critChance;
        critDmg = other.critDmg;
        defense = other.defense;
        shield = other.shield;

        shotsPerAttack = other.shotsPerAttack;
        shotsPerAttackMelee = other.shotsPerAttackMelee;
        comboLength = other.comboLength;

        multicastChance = other.multicastChance;
        shotgunSpread = other.shotgunSpread;
        spreadMultiplier = other.spreadMultiplier;
        castTimeMultiplier = other.castTimeMultiplier;
        comboWaitTime = other.comboWaitTime;
        projectileSpeedMultiplier = other.projectileSpeedMultiplier;
        rangeMultiplier = other.rangeMultiplier;
        knockbackMultiplier = other.knockbackMultiplier;
        thrownDamageMultiplier = other.thrownDamageMultiplier;
        thrownSpeedMultiplier = other.thrownSpeedMultiplier;
        projectileSizeMultiplier = other.projectileSizeMultiplier;
        meleeSizeMultiplier = other.meleeSizeMultiplier;

        shootOpposideSide = other.shootOpposideSide;
        rarity = rarity.CompareRarity(other.rarity);
    }

    //Constructor with smart defaults in parameters
    public PlayerCharacterStats(
        float health = 100,
        float speed = 5,
        float pickupRange = 2,
        float damageMultiplier = 1,
        float critChance = 0,
        float critDmg = 1,
        float defense = 0,
        float shield = 0,
        int shotsPerAttack = 1,
        int shotsPerAttackMelee = 1,
        int comboLength = 1,
        float multicastChance = 0,
        float shotgunSpread = 0,
        float spreadMultiplier = 1,
        float castTimeMultiplier = 1,
        float comboWaitTime = 0,
        float projectileSpeedMultiplier = 1,
        float rangeMultiplier = 1,
        float knockbackMultiplier = 1,
        float thrownDamageMultiplier = 1,
        float thrownSpeedMultiplier = 1,
        float projectileSizeMultiplier = 1,
        float meleeSizeMultiplier = 1,
        bool shootOpposideSide = false,
        Rarity rarity = Rarity.Common,
        string name = "Player Stats",
        string description = "Player Stats",
        float maxhealth = 0,
        Sprite icon = null
    )
    {
        this.health = health;
        this.speed = speed;
        this.pickupRange = pickupRange;
        this.damageMultiplier = damageMultiplier;
        this.critChance = critChance;
        this.critDmg = critDmg;
        this.defense = defense;
        this.shield = shield;

        this.shotsPerAttack = shotsPerAttack;
        this.shotsPerAttackMelee = shotsPerAttackMelee;
        this.comboLength = comboLength;

        this.multicastChance = multicastChance;
        this.shotgunSpread = shotgunSpread;
        this.spreadMultiplier = spreadMultiplier;
        this.castTimeMultiplier = castTimeMultiplier;
        this.comboWaitTime = comboWaitTime;
        this.projectileSpeedMultiplier = projectileSpeedMultiplier;
        this.rangeMultiplier = rangeMultiplier;
        this.knockbackMultiplier = knockbackMultiplier;
        this.thrownDamageMultiplier = thrownDamageMultiplier;
        this.thrownSpeedMultiplier = thrownSpeedMultiplier;
        this.projectileSizeMultiplier = projectileSizeMultiplier;
        this.meleeSizeMultiplier = meleeSizeMultiplier;

        this.shootOpposideSide = shootOpposideSide;
        this.rarity = rarity;
        this.name = name;
        this.description = description;
        this.maxHealth = maxhealth;
        this.icon = icon;
    }

    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.PlayerStats;
    }

    public Sprite GetUpgradeIcon()
    {
        return icon;
    }

    public string GetUpgradeName()
    {
        return name;
    }

    public string GetUpgradeDescription()
    {
        return description;
    }

    public Rarity GetRarity()
    {
        return rarity;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
