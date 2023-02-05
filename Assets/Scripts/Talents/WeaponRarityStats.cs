using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public struct WeaponRarityStats
{
    List<WeaponStatBoost> possibleRarityUpgrades;

    public WeaponRarityStats(List<WeaponStatBoost> possibleRarityUpgrades)
    {
        this.possibleRarityUpgrades = possibleRarityUpgrades;
    }
    public WeaponStatBoost RollStats(RarityTypes rarity)
    {
        WeaponStatBoost boost = new WeaponStatBoost();
        foreach (int _ in Enumerable.Range(0, ((int)rarity)))
        {
            int statIndex = Random.Range(0, possibleRarityUpgrades.Count);
            boost.merge(possibleRarityUpgrades[statIndex]);
            possibleRarityUpgrades.RemoveAt(statIndex);
        }
        return boost;
    }
}