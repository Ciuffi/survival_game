using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBoost : MonoBehaviour, Upgrade
{
    public float extraHealth;
    public float extraMaxHealth;
    public float extraSpeed;
    public float damageMultipler;
    public float extraDefense;
    public float extraShield;
    public float extraCritChance;
    public float extraCritDmg;
    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.StatBoost;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}