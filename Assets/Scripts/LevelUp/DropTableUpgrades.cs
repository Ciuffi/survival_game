using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTableUpgrades : MonoBehaviour
{
    public float playerStatChance = 0.25f;
    public float weaponSetStatChance = 0.25f;
    public float weaponStatChance = 0.50f;

    public float existingWeaponOrSetChance = 0.75f;
    public float newWeaponOrSetChance = 0.25f;

    [System.Serializable]
    public class RarityDropTable
    {
        public float[] dropRates; // Common, Rare, Epic, Legendary
    }

    public RarityDropTable[] guiltDropTables;
    public RarityDropTable[] lootDropTables;

}
