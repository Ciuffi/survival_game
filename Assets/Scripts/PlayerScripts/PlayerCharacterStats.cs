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
        shield,
        shotsPerAttack,
        meleeComboLength,
        multicastChance,
        castTimeMultiplier,
        projectileSpeedMultiplier,
        knockbackMultiplier,
        meleeWaitTimeMultiplier,
        thrownDamageMultiplier,
        thrownSpeedMultiplier;

    public List<GameObject> startingWeapons;
}
