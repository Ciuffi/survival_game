using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackStats : MonoBehaviour, Upgrade
{
    public string name;
    public string description;
    public Sprite icon;

    public float damage;
    public bool cantMove = false;
    public bool shootOppositeSide = false;
    public float critChance;
    public float critDmg;

    public float castTime;
    public float attackTime;
    public float recoveryTime;
    public float knockback;

    public float multicastChance;
    public float multicastWaitTime;
    public int multicastTimes;
    public float multicastAlphaFade;

    public int shotsPerAttack;
    public float spread;
    public float shotgunSpread;
    public float spray;
    public int sprayThreshold;
    public float speed;
    public float range;
    public int pierce;
    public float projectileSize;

    public int shotsPerAttackMelee;
    public int comboLength;
    public float comboWaitTime;
    public float comboAttackBuff;
    public float meleeShotsScaleUp;
    public float meleeSpacer;
    public float meleeSpacerGap;
    public bool swapAnimOnAttack = false;

    public float thrownDamage;
    public float thrownSpeed;

    public float shakeTime;
    public float shakeStrength;
    public float shakeRotation;

    public Vector3 scaleUP;
    public float meleeSize;
    public Rarity rarity;
    public float effectDuration;

    public float damageMultiplier;

    public float castTimeMultiplier;
    public float attackTimeMultiplier;
    public float recoveryTimeMultiplier;
    public float knockbackMultiplier;

    public float multicastChanceMultiplier;
    public float multicastWaitTimeMultiplier;

    public float spreadMultiplier;
    public float shotgunSpreadMultiplier;
    public float sprayMultiplier;
    public float speedMultiplier;
    public float rangeMultiplier;
    public float projectileSizeMultiplier;

    public float comboWaitTimeMultiplier;
    public float comboAttackBuffMultiplier;
    public float meleeShotsScaleUpMultiplier;
    public float meleeSpacerMultiplier;
    public float meleeSpacerGapMultiplier;

    public float thrownDamageMultiplier;
    public float thrownSpeedMultiplier;

    public float meleeSizeMultiplier;
    public float effectMultiplier;
    public bool weaponSet = false;

    //Constructor that takes in all the values and sets them to the variables while providing meaningful defaults
    public AttackStats(
        float damage = 0,
        float spread = 0,
        float shotgunSpread = 0,
        float spray = 0,
        int sprayThreshold = 0,
        float castTime = 0,
        float recoveryTime = 0,
        float range = 0,
        int shotsPerAttack = 0,
        int shotsPerAttackMelee = 0,
        float speed = 0,
        float knockback = 0,
        int pierce = 0,
        float critChance = 0,
        float critDmg = 0,
        bool shootOppositeSide = false,
        float projectileSize = 0,
        float meleeSize = 0,
        float multicastChance = 0,
        float multicastWaitTime = 0,
        int multicastTimes = 0,
        float multicastAlphaFade = 0,
        Vector3 scaleUP = new Vector3(),
        int comboLength = 0,
        float comboWaitTime = 0,
        float comboAttackBuff = 0,
        float meleeShotsScaleUp = 0,
        float meleeSpacer = 0,
        float meleeSpacerGap = 0,
        bool swapAnimOnAttack = false,
        float shakeTime = 0,
        float shakeStrength = 0,
        float shakeRotation = 0,
        float thrownDamage = 0,
        float throwSpeed = 0,
        bool cantMove = false,
        Rarity rarity = Rarity.Common,
        float effectDuration = 0,
        float effectMultiplier = 0,
        float damageMultiplier = 0,
        float castTimeMultiplier = 0,
        float attackTimeMultiplier = 0,
        float recoveryTimeMultiplier = 0,
        float knockbackMultiplier = 0,
        float multicastChanceMultiplier = 0,
        float multicastWaitTimeMultiplier = 0,
        float spreadMultiplier = 0,
        float shotgunSpreadMultiplier = 0,
        float sprayMultiplier = 0,
        float speedMultiplier = 0,
        float rangeMultiplier = 0,
        float projectileSizeMultiplier = 0,
        float comboWaitTimeMultiplier = 0,
        float comboAttackBuffMultiplier = 0,
        float meleeShotsScaleUpMultiplier = 0,
        float meleeSpacerMultiplier = 0,
        float meleeSpacerGapMultiplier = 0,
        float thrownDamageMultiplier = 0,
        float thrownSpeedMultiplier = 0,
        float meleeSizeMultiplier = 0,
        bool weaponSet = false,
        string name = "",
        string description = "",
        Sprite icon = null
    )
    {
        this.damage = damage;
        this.spread = spread;
        this.shotgunSpread = shotgunSpread;
        this.spray = spray;
        this.sprayThreshold = sprayThreshold;
        this.castTime = castTime;
        this.recoveryTime = recoveryTime;
        this.range = range;
        this.shotsPerAttack = shotsPerAttack;
        this.shotsPerAttackMelee = shotsPerAttackMelee;
        this.speed = speed;
        this.knockback = knockback;
        this.pierce = pierce;
        this.critChance = critChance;
        this.critDmg = critDmg;
        this.shootOppositeSide = shootOppositeSide;
        this.projectileSize = projectileSize;
        this.meleeSize = meleeSize;
        this.multicastChance = multicastChance;
        this.multicastWaitTime = multicastWaitTime;
        this.multicastTimes = multicastTimes;
        this.multicastAlphaFade = multicastAlphaFade;
        this.scaleUP = scaleUP;
        this.comboLength = comboLength;
        this.comboWaitTime = comboWaitTime;
        this.comboAttackBuff = comboAttackBuff;
        this.meleeShotsScaleUp = meleeShotsScaleUp;
        this.meleeSpacer = meleeSpacer;
        this.meleeSpacerGap = meleeSpacerGap;
        this.swapAnimOnAttack = swapAnimOnAttack;
        this.shakeTime = shakeTime;
        this.shakeStrength = shakeStrength;
        this.shakeRotation = shakeRotation;
        this.thrownDamage = thrownDamage;
        this.thrownSpeed = throwSpeed;
        this.cantMove = cantMove;
        this.rarity = rarity;
        this.effectDuration = effectDuration;
        this.effectMultiplier = effectMultiplier;
        this.damageMultiplier = damageMultiplier;
        this.castTimeMultiplier = castTimeMultiplier;
        this.attackTimeMultiplier = attackTimeMultiplier;
        this.recoveryTimeMultiplier = recoveryTimeMultiplier;
        this.knockbackMultiplier = knockbackMultiplier;
        this.multicastChanceMultiplier = multicastChanceMultiplier;
        this.multicastWaitTimeMultiplier = multicastWaitTimeMultiplier;
        this.spreadMultiplier = spreadMultiplier;
        this.shotgunSpreadMultiplier = shotgunSpreadMultiplier;
        this.sprayMultiplier = sprayMultiplier;
        this.speedMultiplier = speedMultiplier;
        this.rangeMultiplier = rangeMultiplier;
        this.projectileSizeMultiplier = projectileSizeMultiplier;
        this.comboWaitTimeMultiplier = comboWaitTimeMultiplier;
        this.comboAttackBuffMultiplier = comboAttackBuffMultiplier;
        this.meleeShotsScaleUpMultiplier = meleeShotsScaleUpMultiplier;
        this.meleeSpacerMultiplier = meleeSpacerMultiplier;
        this.meleeSpacerGapMultiplier = meleeSpacerGapMultiplier;
        this.thrownDamageMultiplier = thrownDamageMultiplier;
        this.thrownSpeedMultiplier = thrownSpeedMultiplier;
        this.meleeSizeMultiplier = meleeSizeMultiplier;
        this.weaponSet = weaponSet;
        this.name = name;
        this.description = description;
        this.icon = icon;
    }

    //shotsPerAttack and comboLength must be be one or greater.
    //
    private void FixUpStats()
    {
        this.shotsPerAttack = this.shotsPerAttack < 1 ? 1 : this.shotsPerAttack;
        this.shotsPerAttackMelee = this.shotsPerAttackMelee < 0 ? 0 : this.shotsPerAttackMelee;
        this.comboLength = this.comboLength < 1 ? 1 : this.comboLength;
        effectMultiplier = effectMultiplier == 0 ? 1 : effectMultiplier;
        damageMultiplier = damageMultiplier == 0 ? 1 : damageMultiplier;
        castTimeMultiplier = castTimeMultiplier == 0 ? 1 : castTimeMultiplier;
        attackTimeMultiplier = attackTimeMultiplier == 0 ? 1 : attackTimeMultiplier;
        recoveryTimeMultiplier = recoveryTimeMultiplier == 0 ? 1 : recoveryTimeMultiplier;
        knockbackMultiplier = knockbackMultiplier == 0 ? 1 : knockbackMultiplier;
        multicastChanceMultiplier = multicastChanceMultiplier == 0 ? 1 : multicastChanceMultiplier;
        multicastWaitTimeMultiplier =
            multicastWaitTimeMultiplier == 0 ? 1 : multicastWaitTimeMultiplier;
        spreadMultiplier = spreadMultiplier == 0 ? 1 : spreadMultiplier;
        shotgunSpreadMultiplier = shotgunSpreadMultiplier == 0 ? 1 : shotgunSpreadMultiplier;
        sprayMultiplier = sprayMultiplier == 0 ? 1 : sprayMultiplier;
        speedMultiplier = speedMultiplier == 0 ? 1 : speedMultiplier;
        rangeMultiplier = rangeMultiplier == 0 ? 1 : rangeMultiplier;
        projectileSizeMultiplier = projectileSizeMultiplier == 0 ? 1 : projectileSizeMultiplier;
        comboWaitTimeMultiplier = comboWaitTimeMultiplier == 0 ? 1 : comboWaitTimeMultiplier;
        comboAttackBuffMultiplier = comboAttackBuffMultiplier == 0 ? 1 : comboAttackBuffMultiplier;
        meleeShotsScaleUpMultiplier =
            meleeShotsScaleUpMultiplier == 0 ? 1 : meleeShotsScaleUpMultiplier;
        meleeSpacerMultiplier = meleeSpacerMultiplier == 0 ? 1 : meleeSpacerMultiplier;
        meleeSpacerGapMultiplier = meleeSpacerGapMultiplier == 0 ? 1 : meleeSpacerGapMultiplier;
        thrownDamageMultiplier = thrownDamageMultiplier == 0 ? 1 : thrownDamageMultiplier;
        thrownSpeedMultiplier = thrownSpeedMultiplier == 0 ? 1 : thrownSpeedMultiplier;
        meleeSizeMultiplier = meleeSizeMultiplier == 0 ? 1 : meleeSizeMultiplier;
        damage *= damageMultiplier;
        castTime *= castTimeMultiplier;
        recoveryTime *= recoveryTimeMultiplier;
        knockback *= knockbackMultiplier;
        multicastChance *= multicastChanceMultiplier;
        multicastWaitTime *= multicastWaitTimeMultiplier;
        spread *= spreadMultiplier;
        shotgunSpread *= shotgunSpreadMultiplier;
        spray *= sprayMultiplier;
        speed *= speedMultiplier;
        range *= rangeMultiplier;
        projectileSize *= projectileSizeMultiplier;
        comboWaitTime *= comboWaitTimeMultiplier;
        comboAttackBuff *= comboAttackBuffMultiplier;
        meleeShotsScaleUp *= meleeShotsScaleUpMultiplier;
        meleeSpacer *= meleeSpacerMultiplier;
        meleeSpacerGap *= meleeSpacerGapMultiplier;
        thrownDamage *= thrownDamageMultiplier;
        thrownSpeed *= thrownSpeedMultiplier;
        meleeSize *= meleeSizeMultiplier;
    }

    public AttackStats mergeInStats(AttackStats[] attackstats)
    {
        // loop over attackStats
        for (int i = 0; i < attackstats.Length; i++)
        {
            // merge in each attackStats
            this.mergeInStats(attackstats[i]);
        }
        return this;
    }

    public void mergeInStats(AttackStats attackStats)
    {
        this.damage += attackStats.damage;
        this.spread += attackStats.spread;
        this.shotgunSpread += attackStats.shotgunSpread;
        this.spray += attackStats.spray;
        this.sprayThreshold += attackStats.sprayThreshold;
        this.castTime += attackStats.castTime;
        this.recoveryTime += attackStats.recoveryTime;
        this.range += attackStats.range;
        this.shotsPerAttack += attackStats.shotsPerAttack;
        this.shotsPerAttackMelee += attackStats.shotsPerAttackMelee;
        this.speed += attackStats.speed;
        this.knockback += attackStats.knockback;
        this.pierce += attackStats.pierce;
        this.critChance += attackStats.critChance;
        this.critDmg += attackStats.critDmg;
        this.shootOppositeSide |= attackStats.shootOppositeSide;
        this.projectileSize += attackStats.projectileSize;
        this.meleeSize += attackStats.meleeSize;
        this.multicastChance += attackStats.multicastChance;
        this.multicastWaitTime += attackStats.multicastWaitTime;
        this.multicastTimes += attackStats.multicastTimes;
        this.multicastAlphaFade += attackStats.multicastAlphaFade;
        this.scaleUP += attackStats.scaleUP;
        this.comboLength += attackStats.comboLength;
        this.comboWaitTime += attackStats.comboWaitTime;
        this.comboAttackBuff += attackStats.comboAttackBuff;
        this.meleeShotsScaleUp += attackStats.meleeShotsScaleUp;
        this.meleeSpacer += attackStats.meleeSpacer;
        this.meleeSpacerGap += attackStats.meleeSpacerGap;
        this.swapAnimOnAttack |= attackStats.swapAnimOnAttack;
        this.shakeTime += attackStats.shakeTime;
        this.shakeStrength += attackStats.shakeStrength;
        this.shakeRotation += attackStats.shakeRotation;
        this.thrownDamage += attackStats.thrownDamage;
        this.thrownSpeed += attackStats.thrownSpeed;
        this.cantMove |= attackStats.cantMove;
        this.rarity = this.rarity.CompareRarity(attackStats.rarity);
        this.effectDuration += attackStats.effectDuration;
        this.effectMultiplier += attackStats.effectMultiplier;
        this.damageMultiplier += attackStats.damageMultiplier;
        this.castTimeMultiplier += attackStats.castTimeMultiplier;
        this.attackTimeMultiplier += attackStats.attackTimeMultiplier;
        this.recoveryTimeMultiplier += attackStats.recoveryTimeMultiplier;
        this.knockbackMultiplier += attackStats.knockbackMultiplier;
        this.multicastChanceMultiplier += attackStats.multicastChanceMultiplier;
        this.multicastWaitTimeMultiplier += attackStats.multicastWaitTimeMultiplier;
        this.spreadMultiplier += attackStats.spreadMultiplier;
        this.shotgunSpreadMultiplier += attackStats.shotgunSpreadMultiplier;
        this.sprayMultiplier += attackStats.sprayMultiplier;
        this.speedMultiplier += attackStats.speedMultiplier;
        this.rangeMultiplier += attackStats.rangeMultiplier;
        this.projectileSizeMultiplier += attackStats.projectileSizeMultiplier;
        this.comboWaitTimeMultiplier += attackStats.comboWaitTimeMultiplier;
        this.comboAttackBuffMultiplier += attackStats.comboAttackBuffMultiplier;
        this.meleeShotsScaleUpMultiplier += attackStats.meleeShotsScaleUpMultiplier;
        this.meleeSpacerMultiplier += attackStats.meleeSpacerMultiplier;
        this.meleeSpacerGapMultiplier += attackStats.meleeSpacerGapMultiplier;
        this.thrownDamageMultiplier += attackStats.thrownDamageMultiplier;
        this.thrownSpeedMultiplier += attackStats.thrownSpeedMultiplier;
        this.meleeSizeMultiplier += attackStats.meleeSizeMultiplier;
        FixUpStats();
    }

    public AttackStats MergeInPlayerStats(PlayerCharacterStats playerStats)
    {
        this.shotgunSpread += playerStats.shotgunSpread;
        this.shotsPerAttack += playerStats.shotsPerAttack;
        this.shotsPerAttackMelee += playerStats.shotsPerAttackMelee;
        this.critChance += playerStats.critChance;
        this.critDmg += playerStats.critDmg;
        this.multicastChance += playerStats.multicastChance;
        this.damageMultiplier += playerStats.damageMultiplier;
        this.shootOppositeSide |= playerStats.shootOpposideSide;
        this.comboLength += playerStats.comboLength;
        this.rangeMultiplier += playerStats.rangeMultiplier;
        this.comboWaitTime += playerStats.comboWaitTime;
        this.damageMultiplier += playerStats.damageMultiplier;
        this.castTimeMultiplier += playerStats.castTimeMultiplier;
        this.knockbackMultiplier += playerStats.knockbackMultiplier;
        this.spreadMultiplier += playerStats.spreadMultiplier;
        this.rangeMultiplier += playerStats.rangeMultiplier;
        this.projectileSizeMultiplier += playerStats.projectileSizeMultiplier;
        this.thrownDamageMultiplier += playerStats.thrownDamageMultiplier;
        this.thrownSpeedMultiplier += playerStats.thrownSpeedMultiplier;
        this.meleeSizeMultiplier += playerStats.meleeSizeMultiplier;
        FixUpStats();
        return this;
    }

    public static AttackStats MergeAttackStats(List<AttackStats> attackStatsList)
    {
        // Create a new AttackStats object to store the merged values
        AttackStats mergedAttackStats = new AttackStats();

        // Iterate through each AttackStats object in the list and merge the values
        foreach (AttackStats attackStats in attackStatsList)
        {
            mergedAttackStats.damage += attackStats.damage;
            mergedAttackStats.spread += attackStats.spread;
            mergedAttackStats.shotgunSpread += attackStats.shotgunSpread;
            mergedAttackStats.spray += attackStats.spray;
            mergedAttackStats.sprayThreshold += attackStats.sprayThreshold;
            mergedAttackStats.castTime += attackStats.castTime;
            mergedAttackStats.recoveryTime += attackStats.recoveryTime;
            mergedAttackStats.range += attackStats.range;
            mergedAttackStats.shotsPerAttack += attackStats.shotsPerAttack;
            mergedAttackStats.shotsPerAttackMelee += attackStats.shotsPerAttackMelee;
            mergedAttackStats.speed += attackStats.speed;
            mergedAttackStats.knockback += attackStats.knockback;
            mergedAttackStats.pierce += attackStats.pierce;
            mergedAttackStats.critChance += attackStats.critChance;
            mergedAttackStats.critDmg += attackStats.critDmg;
            mergedAttackStats.shootOppositeSide |= attackStats.shootOppositeSide;
            mergedAttackStats.projectileSize += attackStats.projectileSize;
            mergedAttackStats.meleeSize += attackStats.meleeSize;
            mergedAttackStats.multicastChance += attackStats.multicastChance;
            mergedAttackStats.multicastWaitTime += attackStats.multicastWaitTime;
            mergedAttackStats.multicastTimes += attackStats.multicastTimes;
            mergedAttackStats.multicastAlphaFade += attackStats.multicastAlphaFade;
            mergedAttackStats.scaleUP += attackStats.scaleUP;
            mergedAttackStats.comboLength += attackStats.comboLength;
            mergedAttackStats.comboWaitTime += attackStats.comboWaitTime;
            mergedAttackStats.comboAttackBuff += attackStats.comboAttackBuff;
            mergedAttackStats.meleeShotsScaleUp += attackStats.meleeShotsScaleUp;
            mergedAttackStats.meleeSpacer += attackStats.meleeSpacer;
            mergedAttackStats.meleeSpacerGap += attackStats.meleeSpacerGap;
            mergedAttackStats.swapAnimOnAttack |= attackStats.swapAnimOnAttack;
            mergedAttackStats.shakeTime += attackStats.shakeTime;
            mergedAttackStats.shakeStrength += attackStats.shakeStrength;
            mergedAttackStats.shakeRotation += attackStats.shakeRotation;
            mergedAttackStats.thrownDamage += attackStats.thrownDamage;
            mergedAttackStats.thrownSpeed += attackStats.thrownSpeed;
            mergedAttackStats.cantMove |= attackStats.cantMove;
            mergedAttackStats.rarity = mergedAttackStats.rarity.CompareRarity(attackStats.rarity);
            mergedAttackStats.effectDuration += attackStats.effectDuration;
            mergedAttackStats.effectMultiplier += attackStats.effectMultiplier;
            mergedAttackStats.damageMultiplier += attackStats.damageMultiplier;
            mergedAttackStats.castTimeMultiplier += attackStats.castTimeMultiplier;
            mergedAttackStats.attackTimeMultiplier += attackStats.attackTimeMultiplier;
            mergedAttackStats.recoveryTimeMultiplier += attackStats.recoveryTimeMultiplier;
            mergedAttackStats.knockbackMultiplier += attackStats.knockbackMultiplier;
            mergedAttackStats.multicastChanceMultiplier += attackStats.multicastChanceMultiplier;
            mergedAttackStats.multicastWaitTimeMultiplier +=
                attackStats.multicastWaitTimeMultiplier;
            mergedAttackStats.spreadMultiplier += attackStats.spreadMultiplier;
            mergedAttackStats.shotgunSpreadMultiplier += attackStats.shotgunSpreadMultiplier;
            mergedAttackStats.sprayMultiplier += attackStats.sprayMultiplier;
            mergedAttackStats.speedMultiplier += attackStats.speedMultiplier;
            mergedAttackStats.rangeMultiplier += attackStats.rangeMultiplier;
            mergedAttackStats.projectileSizeMultiplier += attackStats.projectileSizeMultiplier;
            mergedAttackStats.comboWaitTimeMultiplier += attackStats.comboWaitTimeMultiplier;
            mergedAttackStats.comboAttackBuffMultiplier += attackStats.comboAttackBuffMultiplier;
            mergedAttackStats.meleeShotsScaleUpMultiplier +=
                attackStats.meleeShotsScaleUpMultiplier;
            mergedAttackStats.meleeSpacerMultiplier += attackStats.meleeSpacerMultiplier;
            mergedAttackStats.meleeSpacerGapMultiplier += attackStats.meleeSpacerGapMultiplier;
            mergedAttackStats.thrownDamageMultiplier += attackStats.thrownDamageMultiplier;
            mergedAttackStats.thrownSpeedMultiplier += attackStats.thrownSpeedMultiplier;
            mergedAttackStats.meleeSizeMultiplier += attackStats.meleeSizeMultiplier;
            mergedAttackStats.FixUpStats();
        }
        return mergedAttackStats;
    }

    //Copy Constructor
    public AttackStats(AttackStats attackStats)
    {
        this.damage = attackStats.damage;
        this.spread = attackStats.spread;
        this.shotgunSpread = attackStats.shotgunSpread;
        this.spray = attackStats.spray;
        this.sprayThreshold = attackStats.sprayThreshold;
        this.castTime = attackStats.castTime;
        this.recoveryTime = attackStats.recoveryTime;
        this.range = attackStats.range;
        this.shotsPerAttack = attackStats.shotsPerAttack;
        this.shotsPerAttackMelee = attackStats.shotsPerAttackMelee;
        this.speed = attackStats.speed;
        this.knockback = attackStats.knockback;
        this.pierce = attackStats.pierce;
        this.critChance = attackStats.critChance;
        this.critDmg = attackStats.critDmg;
        this.shootOppositeSide = attackStats.shootOppositeSide;
        this.projectileSize = attackStats.projectileSize;
        this.meleeSize = attackStats.meleeSize;
        this.multicastChance = attackStats.multicastChance;
        this.multicastWaitTime = attackStats.multicastWaitTime;
        this.multicastTimes = attackStats.multicastTimes;
        this.multicastAlphaFade = attackStats.multicastAlphaFade;
        this.scaleUP = attackStats.scaleUP;
        this.comboLength = attackStats.comboLength;
        this.comboWaitTime = attackStats.comboWaitTime;
        this.comboAttackBuff = attackStats.comboAttackBuff;
        this.meleeShotsScaleUp = attackStats.meleeShotsScaleUp;
        this.meleeSpacer = attackStats.meleeSpacer;
        this.meleeSpacerGap = attackStats.meleeSpacerGap;
        this.swapAnimOnAttack = attackStats.swapAnimOnAttack;
        this.shakeTime = attackStats.shakeTime;
        this.shakeStrength = attackStats.shakeStrength;
        this.shakeRotation = attackStats.shakeRotation;
        this.thrownDamage = attackStats.thrownDamage;
        this.thrownSpeed = attackStats.thrownSpeed;
        this.cantMove = attackStats.cantMove;
        this.rarity = attackStats.rarity;
        this.effectDuration = attackStats.effectDuration;
        this.effectMultiplier = attackStats.effectMultiplier;
        this.damageMultiplier = attackStats.damageMultiplier;
        this.castTimeMultiplier = attackStats.castTimeMultiplier;
        this.attackTimeMultiplier = attackStats.attackTimeMultiplier;
        this.recoveryTimeMultiplier = attackStats.recoveryTimeMultiplier;
        this.knockbackMultiplier = attackStats.knockbackMultiplier;
        this.multicastChanceMultiplier = attackStats.multicastChanceMultiplier;
        this.multicastWaitTimeMultiplier = attackStats.multicastWaitTimeMultiplier;
        this.spreadMultiplier = attackStats.spreadMultiplier;
        this.shotgunSpreadMultiplier = attackStats.shotgunSpreadMultiplier;
        this.sprayMultiplier = attackStats.sprayMultiplier;
        this.speedMultiplier = attackStats.speedMultiplier;
        this.rangeMultiplier = attackStats.rangeMultiplier;
        this.projectileSizeMultiplier = attackStats.projectileSizeMultiplier;
        this.comboWaitTimeMultiplier = attackStats.comboWaitTimeMultiplier;
        this.comboAttackBuffMultiplier = attackStats.comboAttackBuffMultiplier;
        this.meleeShotsScaleUpMultiplier = attackStats.meleeShotsScaleUpMultiplier;
        this.meleeSpacerMultiplier = attackStats.meleeSpacerMultiplier;
        this.meleeSpacerGapMultiplier = attackStats.meleeSpacerGapMultiplier;
        this.thrownDamageMultiplier = attackStats.thrownDamageMultiplier;
        this.thrownSpeedMultiplier = attackStats.thrownSpeedMultiplier;
        this.meleeSizeMultiplier = attackStats.meleeSizeMultiplier;
        this.description = attackStats.description;
        this.name = attackStats.name;
        this.icon = attackStats.icon;
        this.weaponSet = attackStats.weaponSet;
    }

    public UpgradeType GetUpgradeType()
    {
        return weaponSet ? UpgradeType.WeaponSetStat : UpgradeType.WeaponStat;
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
