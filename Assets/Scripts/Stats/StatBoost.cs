using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBoost : MonoBehaviour, Upgrade
{
    public float extraHealth;
    public float extraMaxHealth;
    public float extraSpeed;
    public float damageMultipler;
    public float exraDefense;
    public float exraShield;
    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.StatBoost;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}