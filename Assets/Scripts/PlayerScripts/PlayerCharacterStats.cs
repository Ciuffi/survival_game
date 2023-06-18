using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacterStats : Upgrade
{
    public Sprite icon;
    public string description;
    public string name;
    public bool isLocked;
    public int price;
    public int level;

    // Base stats
    public float maxHealth,
        health,
        speed,
        pickupRange,
        damageAdditive,
        damageMultiplier,
        critChance,
        critDmg,
        defense,
        shield;

    // Attack stats
    public int shotsPerAttack,
        shotsPerAttackMelee,
        comboLength,
        rerollTimes,
        swapTimes;

    public float goldGainMultiplier,
        xpGainMultiplier,
        recoveryAdditive,
        aimRangeAdditive,
        coneAngle,
        multicastChance,
        shotgunSpread,
        speedMultiplier,
        spreadMultiplier,
        castTimeMultiplier,
        comboWaitTimeMultiplier,
        projectileSpeedMultiplier,
        rangeMultiplier,
        knockbackMultiplier,
        thrownDamageMultiplier,
        thrownSpeedMultiplier,
        projectileSizeMultiplier,
        meleeSizeMultiplier,
        effectMultiplier,
        effectDuration,
        activeDuration,
        activeMultiplier;

    public bool shootOpposideSide, isHoming, is360;

    public bool isDoT;
    public float dotDuration;
    public float dotDamage;
    public float dotTickRate;

    public bool isSplit;
    public int splitAmount;
    public float splitStatPercentage;

    public bool isChain;
    public int chainTimes;
    public float chainStatDecayPercent;
    public float chainRange;
    public float chainSpeed;

    public bool isMagnet;
    public float magnetStrength;
    public float magnetDuration;

    public bool isSlow;
    public float slowPercentage;
    public float slowDuration;

    public bool isStun;
    public float stunDuration;


    public Rarity rarity;
    public GameObject statsContainer;

    // Merges the current PlayerCharacterStats with the provided PlayerCharacterStats
    public void MergeStats(PlayerCharacterStats other)
    {
        rerollTimes += other.rerollTimes;
        other.rerollTimes = 0;
        swapTimes += other.swapTimes;
        other.swapTimes = 0;

        maxHealth += other.maxHealth;
        health += other.health;
        other.health = 0;
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

        aimRangeAdditive += other.aimRangeAdditive;
        coneAngle += other.coneAngle;

        multicastChance += other.multicastChance;
        shotgunSpread += other.shotgunSpread;
        spreadMultiplier += other.spreadMultiplier;
        castTimeMultiplier += other.castTimeMultiplier;
        comboWaitTimeMultiplier += other.comboWaitTimeMultiplier;
        projectileSpeedMultiplier += other.projectileSpeedMultiplier;
        rangeMultiplier += other.rangeMultiplier;
        knockbackMultiplier += other.knockbackMultiplier;
        thrownDamageMultiplier += other.thrownDamageMultiplier;
        thrownSpeedMultiplier += other.thrownSpeedMultiplier;
        projectileSizeMultiplier += other.projectileSizeMultiplier;
        meleeSizeMultiplier += other.meleeSizeMultiplier;
        speedMultiplier += other.speedMultiplier;

        activeDuration += other.activeDuration;
        effectDuration += other.effectDuration;
        effectMultiplier += other.effectMultiplier;
        activeMultiplier += other.activeMultiplier;

        damageAdditive += other.damageAdditive;
        goldGainMultiplier += other.goldGainMultiplier;
        xpGainMultiplier += xpGainMultiplier;
        recoveryAdditive += recoveryAdditive;
        is360 |= other.is360;
        isMagnet |= other.isMagnet;
        magnetStrength += other.magnetStrength;
        magnetDuration += other.magnetDuration;
        isSlow |= other.isSlow;
        slowDuration += other.slowDuration;
        slowPercentage += other.slowPercentage;
        isStun |= other.isStun;
        stunDuration += other.stunDuration;
        isDoT |= other.isDoT;
        dotDamage += other.dotDamage;
        dotDuration += other.dotDuration;
        dotTickRate += other.dotTickRate;
        isSplit |= other.isSplit;
        splitAmount += other.splitAmount;
        splitStatPercentage += other.splitStatPercentage;
        isChain |= other.isChain;
        chainTimes += other.chainTimes;
        chainStatDecayPercent += other.chainStatDecayPercent;
        chainRange += other.chainRange;
        chainSpeed += other.chainSpeed;


        shootOpposideSide |= other.shootOpposideSide;
        isHoming |= other.isHoming;

        this.rarity = this.rarity.CompareRarity(other.rarity);
    }

    // constructor that takes in a PlayerCharacterStats object and copies its values
    public PlayerCharacterStats(PlayerCharacterStats other)
    {
        if (other == null)
        {
            Debug.LogError("PlayerCharacterStats constructor: other parameter is null");
            return;
        }

        maxHealth = other.maxHealth;
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

        aimRangeAdditive = other.aimRangeAdditive;
        coneAngle = other.coneAngle;
        multicastChance = other.multicastChance;
        shotgunSpread = other.shotgunSpread;
        spreadMultiplier = other.spreadMultiplier;
        castTimeMultiplier = other.castTimeMultiplier;
        comboWaitTimeMultiplier = other.comboWaitTimeMultiplier;
        projectileSpeedMultiplier = other.projectileSpeedMultiplier;
        rangeMultiplier = other.rangeMultiplier;
        knockbackMultiplier = other.knockbackMultiplier;
        thrownDamageMultiplier = other.thrownDamageMultiplier;
        thrownSpeedMultiplier = other.thrownSpeedMultiplier;
        projectileSizeMultiplier = other.projectileSizeMultiplier;
        meleeSizeMultiplier = other.meleeSizeMultiplier;
        speedMultiplier = other.speedMultiplier;

        activeDuration = other.activeDuration;
        effectMultiplier = other.effectMultiplier;
        effectDuration = other.effectDuration;
        activeMultiplier = other.activeMultiplier;

        isHoming = other.isHoming;
        shootOpposideSide = other.shootOpposideSide;
        rarity = rarity.CompareRarity(other.rarity);

        rerollTimes = other.rerollTimes;
        swapTimes = other.swapTimes;
        damageAdditive = other.damageAdditive;
        goldGainMultiplier = other.goldGainMultiplier;
        xpGainMultiplier = other.xpGainMultiplier;
        recoveryAdditive = other.recoveryAdditive;
        is360 = other.is360;
        isMagnet = other.isMagnet;
        magnetStrength = other.magnetStrength;
        magnetDuration = other.magnetDuration;
        isSlow = other.isSlow;
        slowDuration = other.slowDuration;
        slowPercentage = other.slowPercentage;
        isStun = other.isStun;
        stunDuration = other.stunDuration;
        isDoT = other.isDoT;
        dotDamage = other.dotDamage;
        dotDuration = other.dotDuration;
        dotTickRate = other.dotTickRate;
        isSplit = other.isSplit;
        splitAmount = other.splitAmount;
        splitStatPercentage = other.splitStatPercentage;
        isChain = other.isChain;
        chainTimes = other.chainTimes;
        chainStatDecayPercent = other.chainStatDecayPercent;
        chainRange = other.chainRange;
        chainSpeed = other.chainSpeed;
    }

    //Constructor with smart defaults in parameters
    public PlayerCharacterStats(
        bool isLocked = false,
        int price = 0,
        int level = 0,
        float maxHealth = 0,
        float health = 0,
        float speed = 0f,
        float pickupRange = 0,
        float damageMultiplier = 0,
        float critChance = 0,
        float critDmg = 0,
        float defense = 0,
        float shield = 0,
        int shotsPerAttack = 0,
        int shotsPerAttackMelee = 0,
        int comboLength = 0,
        float aimRangeAdditive = 0,
        float coneAngle = 0,
        float multicastChance = 0,
        float shotgunSpread = 0,
        float spreadMultiplier = 0,
        float castTimeMultiplier = 0,
        float comboWaitTimeMultiplier = 0,
        float projectileSpeedMultiplier = 0,
        float rangeMultiplier = 0,
        float knockbackMultiplier = 0,
        float thrownDamageMultiplier = 0,
        float thrownSpeedMultiplier = 0,
        float projectileSizeMultiplier = 0,
        float meleeSizeMultiplier = 0,
        float activeDuration = 0,
        float effectDuration = 0,
        float effectMultiplier = 0,
        float activeMultiplier = 0,
        float speedMultiplier = 0,
        bool shootOpposideSide = false,
        bool isHoming = false,
        int rerollTimes = 0,
        int swapTimes = 0,
        float damageAdditive = 0,
        float goldGainMultiplier = 0,
        float xpGainMultiplier = 0,
        float recoveryAdditive = 0,
        bool is360 = false,
        bool isMagnet = false,
        float magnetStrength = 0,
        float magnetDuration = 0,
        bool isSlow = false,
        float slowPercentage = 0,
        float slowDuration = 0,
        bool isStun = false,
        float stunDuration = 0,
        bool isDoT = false,
        float dotDuration = 0,
        float dotDamage = 0,
        float dotTickRate = 0,
        bool isSplit = false,
        int splitAmount = 0,
        float splitStatPercentage = 0,
        bool isChain = false,
        int chainTimes = 0,
        float chainStatDecayPercent = 0,
        float chainRange = 0,
        float chainSpeed = 0,

        Rarity rarity = Rarity.Common,
        string name = "Player Stats",
        string description = "Player Stats",
        Sprite icon = null
    )
    {
        this.maxHealth = maxHealth;
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

        this.coneAngle = coneAngle;
        this.aimRangeAdditive = aimRangeAdditive;
        this.multicastChance = multicastChance;
        this.shotgunSpread = shotgunSpread;
        this.spreadMultiplier = spreadMultiplier;
        this.castTimeMultiplier = castTimeMultiplier;
        this.comboWaitTimeMultiplier = comboWaitTimeMultiplier;
        this.projectileSpeedMultiplier = projectileSpeedMultiplier;
        this.rangeMultiplier = rangeMultiplier;
        this.knockbackMultiplier = knockbackMultiplier;
        this.thrownDamageMultiplier = thrownDamageMultiplier;
        this.thrownSpeedMultiplier = thrownSpeedMultiplier;
        this.projectileSizeMultiplier = projectileSizeMultiplier;
        this.meleeSizeMultiplier = meleeSizeMultiplier;
        this.speedMultiplier = speedMultiplier;
        this.activeDuration = activeDuration;
        this.effectDuration = effectDuration;
        this.effectMultiplier = effectMultiplier;
        this.activeMultiplier = activeMultiplier;

        this.rerollTimes = rerollTimes;
        this.swapTimes = swapTimes;
        this.damageAdditive = damageAdditive;
        this.goldGainMultiplier = goldGainMultiplier;
        this.xpGainMultiplier = xpGainMultiplier;
        this.recoveryAdditive = recoveryAdditive;
        this.is360 = is360;
        this.isMagnet = isMagnet;
        this.magnetStrength = magnetStrength;
        this.magnetDuration = magnetDuration;
        this.isSlow = isSlow;
        this.slowDuration = slowDuration;
        this.slowPercentage = slowPercentage;
        this.isStun = isStun;
        this.stunDuration = stunDuration;
        this.isDoT = isDoT;
        this.dotDamage = dotDamage;
        this.dotDuration = dotDuration;
        this.dotTickRate = dotTickRate;
        this.isSplit = isSplit;
        this.splitAmount = splitAmount;
        this.splitStatPercentage = splitStatPercentage;
        this.isChain = isChain;
        this.chainTimes = chainTimes;
        this.chainStatDecayPercent = chainStatDecayPercent;
        this.chainRange = chainRange;
        this.chainSpeed = chainSpeed;

        this.shootOpposideSide = shootOpposideSide;
        this.isHoming = isHoming;
        this.rarity = rarity;
        this.name = name;
        this.description = description;
        this.price = price;
        this.isLocked = isLocked;
        this.level = level;
        this.icon = icon == null ? Resources.Load<Sprite>("UI_Icons/DMG_up") : icon;
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
        return statsContainer.transform;
    }

    public void setContainer(GameObject container)
    {
        statsContainer = container;
    }

    public string GetBaseName()
    {
        // Split by space and remove the last part (which is assumed to be the level)
        var splitName = name.Split(' ');
        return string.Join(" ", splitName.Take(splitName.Length - 1));
    }
}
