using System.Collections.Generic;
using UnityEngine;

public static class WeaponRarityStats
{
    public static AttackStats ApplyRarity(List<AttackStats> attackStatUpgrades, int rarity)
    {
        if (rarity < 1 || rarity > 6)
        {
            Debug.LogError("Invalid rarity value. Rarity should be between 1 and 6.");
            return null;
        }

        // Create a new AttackStats object to store the upgraded values
        AttackStats upgradedAttackStats = new AttackStats();

        // Create a copy of the attackStatUpgrades list to keep track of available upgrades
        List<AttackStats> availableUpgrades = new List<AttackStats>(attackStatUpgrades);

        // Iterate through each AttackStats upgrade based on the rarity
        for (int i = 0; i < rarity; i++)
        {
            // Select a random upgrade from the availableUpgrades list
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            AttackStats upgrade = availableUpgrades[randomIndex];

            // Merge the selected upgrade into the upgradedAttackStats
            upgradedAttackStats.mergeInStats(upgrade);

            // Remove the selected upgrade from the availableUpgrades list
            availableUpgrades.RemoveAt(randomIndex);
        }

        return upgradedAttackStats;
    }
}
