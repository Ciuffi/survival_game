using System.Collections.Generic;
using UnityEngine;

public static class WeaponRarityStats
{
    public static AttackStats ApplyRarity(List<AttackStats> attackStatUpgrades, Rarity rarity)
    {
        //Debug.Log("Rarity: " + (int)rarity);

        // Create a new AttackStats object to store the upgraded values
        AttackStats upgradedAttackStats = new AttackStats();

        // Create a copy of the attackStatUpgrades list to keep track of available upgrades
        List<AttackStats> availableUpgrades = new List<AttackStats>(attackStatUpgrades);

        for (Rarity r = Rarity.Rare; r <= rarity; r++)
        {
            // Filter the availableUpgrades list based on rarity
            List<AttackStats> filteredUpgrades = availableUpgrades.FindAll(a => a.rarity == r);
            //Debug.Log("Filtered Upgrades (" + r + "): " + filteredUpgrades.Count);

            // Iterate through each AttackStats upgrade based on the rarity
            for (int i = 0; i < 2; i++)
            {
                // If there are no available upgrades of the current rarity, break the loop
                if (filteredUpgrades.Count == 0) break;

                // Select a random upgrade from the filteredUpgrades list
                int randomIndex = Random.Range(0, filteredUpgrades.Count);
                AttackStats upgrade = filteredUpgrades[randomIndex];

                // Merge the selected upgrade into the upgradedAttackStats
                upgradedAttackStats.mergeInStats(upgrade);
                //Debug.Log("Applied upgrade: " + upgrade + ", upgradedAttackStats: " + upgradedAttackStats);

                // Remove the selected upgrade from the availableUpgrades list
                availableUpgrades.Remove(upgrade);
                //Debug.Log("Filtered Upgrades after remove: " + filteredUpgrades.Count);

                // Also remove the upgrade from the filteredUpgrades list to avoid duplicates (unless it's the only upgrade)
                if (filteredUpgrades.Count > 1)
                {
                    filteredUpgrades.RemoveAt(randomIndex);
                    //Debug.Log(filteredUpgrades.Count);

                }
            }
        }

        return upgradedAttackStats;
    }
}
