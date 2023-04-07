using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStats : MonoBehaviour
{

    public float health,
        speed,
        pickupRange,
        damageMultiplier,
        critChance,
        critDmg,
        defense,
        shield;
       

    public int shotsPerAttack,
        shotsPerAttackMelee,
        meleeComboLength;

    public float
        multicastChance,
        shotgunSpread,
        spreadMultiplier,
        castTimeMultiplier,
        meleeWaitTimeMultiplier,
        projectileSpeedMultiplier,
        rangeMultiplier,
        knockbackMultiplier,
        thrownDamageMultiplier,
        thrownSpeedMultiplier,
        projectileSizeMultiplier,
        meleeSizeMultiplier;



    public bool shootOpposideSide;


}
