using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackStats : Upgrade
{
    public string name;
    public string description;
    public Sprite icon;

    public float aimRange;
    public float damage;
    public bool cantMove = false;
    public bool shootOppositeSide = false;
    public float critChance;
    public float critDmg;

    public float castTime;
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
    public float activeDuration;

    public float aimRangeAdditive;
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
    public float activeMultiplier;
    public bool weaponSet = false;
    public bool isCone = false;
    public float coneAngle;

    public GameObject statsContainer;
    public string AttackName { get; set; }
    public WeaponSetType weaponSetType { get; set; }

    private Sprite defaultIcon = Resources.Load<Sprite>("UI_Icons/DMG_up");

    //Constructor that takes in all the values and sets them to the variables while providing meaningful defaults
    public AttackStats(
        float aimRange = 0,
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
        float projectileSizeMultiplier = 0,
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
        float activeDuration = 0,
        float effectDuration = 0,
        float effectMultiplier = 0,
        float activeMultiplier = 0,
        float aimRangeAdditive = 0,
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
        float comboWaitTimeMultiplier = 0,
        float comboAttackBuffMultiplier = 0,
        float meleeShotsScaleUpMultiplier = 0,
        float meleeSpacerMultiplier = 0,
        float meleeSpacerGapMultiplier = 0,
        float thrownDamageMultiplier = 0,
        float thrownSpeedMultiplier = 0,
        float meleeSizeMultiplier = 0,
        bool weaponSet = false,
        bool isCone = false,
        float coneAngle = 0,
        string name = "",
        string description = "",
        Sprite icon = null
    )
    {
        this.aimRange = aimRange;
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
        this.activeDuration = activeDuration;
        this.effectDuration = effectDuration;
        this.effectMultiplier = effectMultiplier;
        this.activeMultiplier = activeMultiplier;
        this.aimRangeAdditive = aimRangeAdditive;
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
        this.icon = icon == null ? defaultIcon : icon;
        this.isCone = isCone;
        this.coneAngle = coneAngle;
    }

    //shotsPerAttack and comboLength must be be one or greater.
    //
    public void FixUpStats()
    {
        this.coneAngle = this.coneAngle < 0f ? 0f : this.coneAngle;
        this.aimRange = this.aimRange < 0f ? 0f : this.aimRange;
        this.shotsPerAttack = this.shotsPerAttack < 1 ? 1 : this.shotsPerAttack;
        this.shotsPerAttackMelee = this.shotsPerAttackMelee < 0 ? 0 : this.shotsPerAttackMelee;
        this.comboLength = this.comboLength < 1 ? 1 : this.comboLength;
        this.effectMultiplier = this.effectMultiplier == 0 ? 1 : this.effectMultiplier;
        this.activeMultiplier = this.activeMultiplier == 0 ? 1 : this.activeMultiplier;
        this.damageMultiplier = this.damageMultiplier == 0 ? 1 : this.damageMultiplier;
        this.castTimeMultiplier = this.castTimeMultiplier == 0 ? 1 : this.castTimeMultiplier;
        this.attackTimeMultiplier = this.attackTimeMultiplier == 0 ? 1 : this.attackTimeMultiplier;
        this.recoveryTimeMultiplier = this.recoveryTimeMultiplier == 0 ? 1 : this.recoveryTimeMultiplier;
        this.knockbackMultiplier = this.knockbackMultiplier == 0 ? 1 : this.knockbackMultiplier;
        this.multicastChanceMultiplier = this.multicastChanceMultiplier == 0 ? 1 : this.multicastChanceMultiplier;
        this.multicastWaitTimeMultiplier =
            this.multicastWaitTimeMultiplier == 0 ? 1 : this.multicastWaitTimeMultiplier;
        this.spreadMultiplier = this.spreadMultiplier == 0 ? 1 : this.spreadMultiplier;
        this.shotgunSpreadMultiplier = this.shotgunSpreadMultiplier == 0 ? 1 : this.shotgunSpreadMultiplier;
        this.sprayMultiplier = this.sprayMultiplier == 0 ? 1 : this.sprayMultiplier;
        this.speedMultiplier = this.speedMultiplier == 0 ? 1 : this.speedMultiplier;
        this.rangeMultiplier = this.rangeMultiplier == 0 ? 1 : this.rangeMultiplier;
        this.projectileSize = this.projectileSize == 0 ? 1 : this.projectileSize;
        this.meleeSize = this.meleeSize == 0 ? 1 : this.meleeSize;
        this.projectileSizeMultiplier = this.projectileSizeMultiplier == 0 ? 1 : this.projectileSizeMultiplier;
        this.comboWaitTimeMultiplier = this.comboWaitTimeMultiplier == 0 ? 1 : this.comboWaitTimeMultiplier;
        this.comboAttackBuffMultiplier = this.comboAttackBuffMultiplier == 0 ? 1 : this.comboAttackBuffMultiplier;
        this.meleeShotsScaleUpMultiplier =
            this.meleeShotsScaleUpMultiplier == 0 ? 1 : this.meleeShotsScaleUpMultiplier;
        this.meleeSpacerMultiplier = this.meleeSpacerMultiplier == 0 ? 1 : this.meleeSpacerMultiplier;
        this.meleeSpacerGapMultiplier = this.meleeSpacerGapMultiplier == 0 ? 1 : this.meleeSpacerGapMultiplier;
        this.thrownDamageMultiplier = this.thrownDamageMultiplier == 0 ? 1 : this.thrownDamageMultiplier;
        this.thrownSpeedMultiplier = this.thrownSpeedMultiplier == 0 ? 1 : this.thrownSpeedMultiplier;
        this.meleeSizeMultiplier = this.meleeSizeMultiplier == 0 ? 1 : this.meleeSizeMultiplier;

    }

    public void ApplyMultiplier()
    {
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

        comboWaitTime *= comboWaitTimeMultiplier;
        comboAttackBuff *= comboAttackBuffMultiplier;
        meleeShotsScaleUp *= meleeShotsScaleUpMultiplier;
        meleeSpacer *= meleeSpacerMultiplier;
        meleeSpacerGap *= meleeSpacerGapMultiplier;
        thrownDamage *= thrownDamageMultiplier;
        thrownSpeed *= thrownSpeedMultiplier;

        meleeSize *= meleeSizeMultiplier;
        projectileSize *= projectileSizeMultiplier;
    }

    public AttackStats mergeInStats(AttackStats[] attackstats)
    {
        // loop over attackStats
        for (int i = 0; i < attackstats.Length; i++)
        {
            // check if attackStats is not null before merging
            if (attackstats[i] != null)
            {
                // merge in each attackStats
                this.mergeInStats(attackstats[i]);
            }
            else
            {
                Debug.LogError("attackStats is null at index " + i);
            }
        }
        return this;
    }

    public void mergeInStats(AttackStats attackStats)
    {
        this.aimRange += attackStats.aimRange;
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
        this.isCone |= attackStats.isCone;
        this.coneAngle += attackStats.coneAngle;
        this.rarity = this.rarity.CompareRarity(attackStats.rarity);
        this.activeDuration += attackStats.activeDuration;
        this.effectDuration += attackStats.effectDuration;
        this.effectMultiplier += attackStats.effectMultiplier;
        this.activeMultiplier += attackStats.activeMultiplier;
        this.aimRangeAdditive += attackStats.aimRangeAdditive;
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
    }

    public AttackStats MergeInPlayerStats(PlayerCharacterStats playerStats)
    {
        this.aimRangeAdditive += playerStats.aimRangeAdditive;
        this.shotgunSpread += playerStats.shotgunSpread;
        this.shotsPerAttack += playerStats.shotsPerAttack;
        this.shotsPerAttackMelee += playerStats.shotsPerAttackMelee;
        this.critChance += playerStats.critChance;
        this.critDmg += playerStats.critDmg;
        this.multicastChance += playerStats.multicastChance;
        this.shootOppositeSide |= playerStats.shootOpposideSide;
        this.comboLength += playerStats.comboLength;

        this.damageMultiplier += playerStats.damageMultiplier;
        this.rangeMultiplier += playerStats.rangeMultiplier;
        this.comboWaitTimeMultiplier += playerStats.comboWaitTimeMultiplier;
        this.damageMultiplier += playerStats.damageMultiplier;
        this.castTimeMultiplier += playerStats.castTimeMultiplier;
        this.knockbackMultiplier += playerStats.knockbackMultiplier;
        this.spreadMultiplier += playerStats.spreadMultiplier;
        this.projectileSizeMultiplier += playerStats.projectileSizeMultiplier;
        this.thrownDamageMultiplier += playerStats.thrownDamageMultiplier;
        this.thrownSpeedMultiplier += playerStats.thrownSpeedMultiplier;
        this.meleeSizeMultiplier += playerStats.meleeSizeMultiplier;

        this.activeDuration += playerStats.activeDuration;
        this.effectDuration += playerStats.effectDuration;
        this.effectMultiplier += playerStats.effectMultiplier;
        this.activeMultiplier += playerStats.activeMultiplier;

        return this;
    }

    //Copy Constructor
    public AttackStats(AttackStats attackStats)
    {
        this.aimRange = attackStats.aimRange;
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
        this.activeDuration = attackStats.activeDuration;
        this.effectDuration = attackStats.effectDuration;
        this.effectMultiplier = attackStats.effectMultiplier;
        this.activeMultiplier = attackStats.activeMultiplier;
        this.aimRangeAdditive = attackStats.aimRangeAdditive;
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
        this.isCone = attackStats.isCone;
        this.coneAngle = attackStats.coneAngle;
    }

    public AttackStats Clone()
    {

        return new AttackStats
        {
            name = this.name,
            description = this.description,
            icon = this.icon,

            aimRange = this.aimRange,
            damage = this.damage,
            cantMove = this.cantMove,
            shootOppositeSide = this.shootOppositeSide,
            critChance = this.critChance,
            critDmg = this.critDmg,

            castTime = this.castTime,
            recoveryTime = this.recoveryTime,
            knockback = this.knockback,

            multicastChance = this.multicastChance,
            multicastWaitTime = this.multicastWaitTime,
            multicastTimes = this.multicastTimes,
            multicastAlphaFade = this.multicastAlphaFade,

            shotsPerAttack = this.shotsPerAttack,
            spread = this.spread,
            shotgunSpread = this.shotgunSpread,
            spray = this.spray,
            sprayThreshold = this.sprayThreshold,
            speed = this.speed,
            range = this.range,
            pierce = this.pierce,
            projectileSize = this.projectileSize,

            shotsPerAttackMelee = this.shotsPerAttackMelee,
            comboLength = this.comboLength,
            comboWaitTime = this.comboWaitTime,
            comboAttackBuff = this.comboAttackBuff,
            meleeShotsScaleUp = this.meleeShotsScaleUp,
            meleeSpacer = this.meleeSpacer,
            meleeSpacerGap = this.meleeSpacerGap,
            swapAnimOnAttack = this.swapAnimOnAttack,

            thrownDamage = this.thrownDamage,
            thrownSpeed = this.thrownSpeed,

            shakeTime = this.shakeTime,
            shakeStrength = this.shakeStrength,
            shakeRotation = this.shakeRotation,

            scaleUP = this.scaleUP,
            meleeSize = this.meleeSize,
            rarity = this.rarity,
            effectDuration = this.effectDuration,
            activeDuration = this.activeDuration,

            aimRangeAdditive = this.aimRangeAdditive,
            damageMultiplier = this.damageMultiplier,

            castTimeMultiplier = this.castTimeMultiplier,
            attackTimeMultiplier = this.attackTimeMultiplier,
            recoveryTimeMultiplier = this.recoveryTimeMultiplier,
            knockbackMultiplier = this.knockbackMultiplier,

            multicastChanceMultiplier = this.multicastChanceMultiplier,
            multicastWaitTimeMultiplier = this.multicastWaitTimeMultiplier,

            spreadMultiplier = this.spreadMultiplier,
            shotgunSpreadMultiplier = this.shotgunSpreadMultiplier,
            sprayMultiplier = this.sprayMultiplier,
            speedMultiplier = this.speedMultiplier,
            rangeMultiplier = this.rangeMultiplier,
            projectileSizeMultiplier = this.projectileSizeMultiplier,

            comboWaitTimeMultiplier = this.comboWaitTimeMultiplier,
            comboAttackBuffMultiplier = this.comboAttackBuffMultiplier,
            meleeShotsScaleUpMultiplier = this.meleeShotsScaleUpMultiplier,
            meleeSpacerMultiplier = this.meleeSpacerMultiplier,
            meleeSpacerGapMultiplier = this.meleeSpacerGapMultiplier,

            thrownDamageMultiplier = this.thrownDamageMultiplier,
            thrownSpeedMultiplier = this.thrownSpeedMultiplier,

            meleeSizeMultiplier = this.meleeSizeMultiplier,
            effectMultiplier = this.effectMultiplier,
            activeMultiplier = this.activeMultiplier,
            weaponSet = this.weaponSet,
            isCone = this.isCone,
            coneAngle = this.coneAngle,

            statsContainer = this.statsContainer,
            AttackName = this.AttackName,
            weaponSetType = this.weaponSetType
        };
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
        return statsContainer.transform;
    }

    public void setContainer(GameObject container)
    {
        statsContainer = container;
    }
}
