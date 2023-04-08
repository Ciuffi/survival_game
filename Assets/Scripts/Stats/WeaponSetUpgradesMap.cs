using System;
using System.Collections.Generic;

public static class WeaponSetUpgradeMap
{
    public static readonly Dictionary<
        WeaponSetType,
        Dictionary<Rarity, List<AttackStats>>
    > AttackStatsMap = new Dictionary<WeaponSetType, Dictionary<Rarity, List<AttackStats>>>
    {
        {
            WeaponSetType.Automatic,
            new Dictionary<Rarity, List<AttackStats>>
            {
                {
                    Rarity.Common,
                    new List<AttackStats>
                    {
                        new AttackStats(damage: 10, spread: 5, speed: 100, range: 50)
                    }
                },
                {
                    Rarity.Uncommon,
                    new List<AttackStats>
                    {
                        new AttackStats(damage: 12, spread: 4, speed: 110, range: 55)
                    }
                },
                // Add more rarities for the Automatic key as needed
            }
        },
        {
            WeaponSetType.SemiAuto,
            new Dictionary<Rarity, List<AttackStats>>
            {
                {
                    Rarity.Common,
                    new List<AttackStats>
                    {
                        new AttackStats(damage: 20, spread: 3, speed: 150, range: 60)
                    }
                },
                {
                    Rarity.Uncommon,
                    new List<AttackStats>
                    {
                        new AttackStats(damage: 22, spread: 2, speed: 160, range: 65)
                    }
                },
                // Add more rarities for the SemiAuto key as needed
            }
        },
    }; // Add more WeaponSetType keys with their respective AttackStats list

    static WeaponSetUpgradeMap()
    {
        foreach (WeaponSetType weaponSetType in Enum.GetValues(typeof(WeaponSetType)))
        {
            if (!AttackStatsMap.ContainsKey(weaponSetType))
            {
                throw new System.Exception(
                    $"AttackStatsMap is missing a sub-dictionary for WeaponSetType {weaponSetType}"
                );
            }

            Dictionary<Rarity, List<AttackStats>> rarityAttackStats = AttackStatsMap[weaponSetType];

            foreach (Rarity rarity in Enum.GetValues(typeof(Rarity)))
            {
                if (!rarityAttackStats.ContainsKey(rarity))
                {
                    throw new System.Exception(
                        $"AttackStatsMap is missing a rarity ({rarity}) entry for WeaponSetType {weaponSetType}"
                    );
                }

                List<AttackStats> attackStatsList = rarityAttackStats[rarity];

                if (attackStatsList == null || attackStatsList.Count == 0)
                {
                    throw new System.Exception(
                        $"AttackStatsMap has an empty list of AttackStats for WeaponSetType {weaponSetType} and rarity {rarity}"
                    );
                }
            }
        }
    }
}
