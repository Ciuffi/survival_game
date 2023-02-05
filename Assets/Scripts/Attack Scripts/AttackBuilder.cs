using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackBuilder : MonoBehaviour
{
    string attackName;
    float damage;
    float shotsPerAttack;
    float castTime;
    float recoveryTime;
    float knockback;
    float critChance;
    float critDamage;
    float scale;
    float shakeTime;
    float shakeStrength;
    float shakeRotation;
    float throwSpeed;
    AttackFunctions attackFunction;
    public GameObject projectile;
    // Projectile
    float pierce;
    float range;
    float spread;
    float speed;

    // Melee
    float initialSpacer;
    float comboSpacer;
    float comboWaitTime;

    // Stats
    List<WeaponStatBoost> starterStats;
    List<WeaponStatsTalents> possibleUpgrades;
    WeaponRarityStats weaponRarityStats;
    RarityWeights weaponRarityWeights = new RarityWeights(50, 30, 15, 5);
    RarityTypes rarity;
    // Sprite info
    Sprite thrownWeaponSprite;
    Sprite heldWeaponSprite;
    bool isMelee;

    public AttackBuilder(string attackName,
                                    float damage,
                                    float shotsPerAttack,
                                    float castTime,
                                    float recoveryTime, float knockback,
                                    float critChance,
                                    float critDamage,
                                    float scale,
                                    float shakeTime,
                                    float shakeStrength,
                                    float shakeRotation,
                                    float throwSpeed,
                                    GameObject projectile,
                                    AttackFunctions attackFunction,
                                    List<WeaponStatBoost> starterStats,
                                    List<WeaponStatsTalents> possibleUpgrades,
                                    WeaponRarityStats weaponRarityStats,
                                    Sprite thrownWeaponSprite,
                                    Sprite heldWeaponSprite
                                    )
    {
        this.attackName = attackName;
        this.damage = damage;
        this.shotsPerAttack = shotsPerAttack;
        this.castTime = castTime;
        this.recoveryTime = recoveryTime;
        this.knockback = knockback;
        this.critChance = critChance;
        this.critDamage = critDamage;
        this.scale = scale;
        this.shakeTime = shakeTime;
        this.shakeStrength = shakeStrength;
        this.shakeRotation = shakeRotation;
        this.throwSpeed = throwSpeed;
        this.projectile = projectile;
        this.starterStats = starterStats;
        this.possibleUpgrades = possibleUpgrades;
        this.weaponRarityStats = weaponRarityStats;
        this.thrownWeaponSprite = thrownWeaponSprite;
        this.heldWeaponSprite = heldWeaponSprite;
        this.attackFunction = attackFunction;
    }

    public AttackBuilder setRarity(RarityTypes type)
    {
        rarity = type;
        return this;
    }

    public AttackBuilder SetRangedAttack(float pierce, float range, float spread, float speed)
    {
        isMelee = false;
        this.pierce = pierce;
        this.range = range;
        this.spread = spread;
        this.speed = speed;
        return this;
    }

    public AttackBuilder SetMeleeAttack(float initialSpacer, float comboSpacer, float comboWaitTime)
    {
        isMelee = true;
        this.initialSpacer = initialSpacer;
        this.comboSpacer = comboSpacer;
        this.comboWaitTime = comboWaitTime;
        return this;
    }
}
