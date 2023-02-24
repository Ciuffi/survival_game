using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStats : MonoBehaviour
{

    public float health,
        speed,
        damageMultiplier,
        critChance,
        critDmg,
        defense,
        shield;

    public int shotsPerAttack,
        meleeComboLength;

   public float multicastChance,
        castTimeMultiplier,
        meleeWaitTimeMultiplier,
        projectileSpeedMultiplier,
        rangeMultiplier,
        knockbackMultiplier,
        thrownDamageMultiplier,
        thrownSpeedMultiplier;



    public bool shootOpposideSide;

    public List<GameObject> startingWeapons;


}
