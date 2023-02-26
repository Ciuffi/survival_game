using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBoost : MonoBehaviour, Upgrade
{
    public Sprite UiIcon;

    public float extraHealth;
    public float extraMaxHealth;
    public float extraSpeed;
    public float extraDamageMultipler;
    public float extraDefense;
    public float extraShield;
    public float extraCritChance;
    public float extraCritDmg;

    public int extraShotsPerAttack,
        extraShotsPerAttackMelee,
        extraMeleeComboLength;

    public float extraSpreadMultiplier,
        extraShotgunSpread,
        extraMulticastChance,
        extraCastTimeMultiplier,
        extraProjectileSpeedMultiplier,
        extraKnockbackMultiplier,
        extraMeleeWaitTimeMultiplier,
        extraThrownDamageMultiplier,
        extraThrownSpeedMultiplier,
        extraRangeMultiplier,
        extraPickupRange,
        extraProjectileSizeMultiplier,
        extraMeleeSizeMultiplier;


    public bool shootOppositeSide;

    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.StatBoost;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}