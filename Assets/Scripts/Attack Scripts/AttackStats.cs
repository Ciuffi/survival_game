using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackStats
{
    public float damage;
    public float spread;
    public float shotgunSpread;
    public float spray;
    public int sprayThreshold;
    public float castTime;
    public float attackTime;
    public float recoveryTime;
    public float range;
    public int shotsPerAttack;
    public int shotsPerAttackMelee;
    public float speed;
    public float knockback;
    public int pierce;
    public float critChance;
    public float critDmg;
    public bool shootOppositeSide = false;
    public float projectileSize;
    public float meleeSize;
    public float multicastChance;
    public float multicastWaitTime;
    public int multicastTimes;
    public float multicastAlphaFade;
    public Vector3 scaleUP;
    public int comboLength;
    public float comboWaitTime;
    public float comboAttackBuff;
    public float meleeShotsScaleUp;
    public float meleeSpacer;
    public float meleeSpacerGap;
    public bool swapAnimOnAttack = false;
    public float shakeTime;
    public float shakeStrength;
    public float shakeRotation;
    public float thrownDamage;
    public float throwSpeed;
    public bool cantMove = false;

    //Constructor that takes in all the values and sets them to the variables while providing meaningful defaults
    public AttackStats(
        float damage = 0,
        float spread = 0,
        float shotgunSpread = 0,
        float spray = 0,
        int sprayThreshold = 0,
        float castTime = 0,
        float attackTime = 0,
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
        bool cantMove = false
    )
    {
        this.damage = damage;
        this.spread = spread;
        this.shotgunSpread = shotgunSpread;
        this.spray = spray;
        this.sprayThreshold = sprayThreshold;
        this.castTime = castTime;
        this.attackTime = attackTime;
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
        this.throwSpeed = throwSpeed;
        this.cantMove = cantMove;
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

    public AttackStats mergeInStats(AttackStats attackStats)
    {
        this.damage += attackStats.damage;
        this.spread += attackStats.spread;
        this.shotgunSpread += attackStats.shotgunSpread;
        this.spray += attackStats.spray;
        this.sprayThreshold += attackStats.sprayThreshold;
        this.castTime += attackStats.castTime;
        this.attackTime += attackStats.attackTime;
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
        this.throwSpeed += attackStats.throwSpeed;
        this.cantMove |= attackStats.cantMove;
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
            mergedAttackStats.attackTime += attackStats.attackTime;
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
            mergedAttackStats.throwSpeed += attackStats.throwSpeed;
            mergedAttackStats.cantMove |= attackStats.cantMove;
        }
        return mergedAttackStats;
    }
}
