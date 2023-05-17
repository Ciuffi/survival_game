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
                        AttackStatsLibrary.GetStat("Mastery 1"),
                        AttackStatsLibrary.GetStat("Marksman 1"),
                        AttackStatsLibrary.GetStat("Brutality 1"),
                        AttackStatsLibrary.GetStat("Quickswap 1"),
                        AttackStatsLibrary.GetStat("RoF 1"),
                        AttackStatsLibrary.GetStat("Propulsion 1"),
                        AttackStatsLibrary.GetStat("Scope 1"),
                    }
                },
                {
                    Rarity.Rare,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 2"),
                        AttackStatsLibrary.GetStat("Marksman 2"),
                        AttackStatsLibrary.GetStat("Brutality 2"),
                        AttackStatsLibrary.GetStat("Quickswap 2"),
                        AttackStatsLibrary.GetStat("RoF 2"),
                        AttackStatsLibrary.GetStat("Propulsion 2"),
                        AttackStatsLibrary.GetStat("Scope 2"),
                        AttackStatsLibrary.GetStat("Vision 1"),
                        AttackStatsLibrary.GetStat("Awareness 1"),
                        AttackStatsLibrary.GetStat("Multi-cast 1"),
                        AttackStatsLibrary.GetStat("Extended Mag 1"),
                        AttackStatsLibrary.GetStat("Piercing Ammo 1"),
                        AttackStatsLibrary.GetStat("High Caliber 1"),
                        AttackStatsLibrary.GetStat("Auto Novice"),
                    }
                },
                {
                    Rarity.Epic,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 3"),
                        AttackStatsLibrary.GetStat("Marksman 3"),
                        AttackStatsLibrary.GetStat("Brutality 3"),
                        AttackStatsLibrary.GetStat("Quickswap 3"),
                        AttackStatsLibrary.GetStat("RoF 3"),
                        AttackStatsLibrary.GetStat("Propulsion 3"),
                        AttackStatsLibrary.GetStat("Scope 3"),
                        AttackStatsLibrary.GetStat("Vision 2"),
                        AttackStatsLibrary.GetStat("Awareness 2"),
                        AttackStatsLibrary.GetStat("Multi-cast 2"),
                        AttackStatsLibrary.GetStat("Extended Mag 2"),
                        AttackStatsLibrary.GetStat("Piercing Ammo 2"),
                        AttackStatsLibrary.GetStat("High Caliber 2"),
                        AttackStatsLibrary.GetStat("Auto Pro"),
                    }
                },
                {
                    Rarity.Legendary,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 4"),
                        AttackStatsLibrary.GetStat("Marksman 4"),
                        AttackStatsLibrary.GetStat("Brutality 4"),
                        AttackStatsLibrary.GetStat("Quickswap 4"),
                        AttackStatsLibrary.GetStat("Multi-cast 3"),
                        AttackStatsLibrary.GetStat("Extended Mag 3"),
                        AttackStatsLibrary.GetStat("Piercing Ammo 3"),
                        AttackStatsLibrary.GetStat("High Caliber 3"),
                        AttackStatsLibrary.GetStat("Double Trouble"),
                        AttackStatsLibrary.GetStat("Auto God"),
                    }
                },
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
                        AttackStatsLibrary.GetStat("Mastery 1"),
                        AttackStatsLibrary.GetStat("Marksman 1"),
                        AttackStatsLibrary.GetStat("Brutality 1"),
                        AttackStatsLibrary.GetStat("Quickswap 1"),
                        AttackStatsLibrary.GetStat("Impact 1"),
                        AttackStatsLibrary.GetStat("Propulsion 1"),
                        AttackStatsLibrary.GetStat("Scope 1"),
                    }
                },
                {
                    Rarity.Rare,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 2"),
                        AttackStatsLibrary.GetStat("Marksman 2"),
                        AttackStatsLibrary.GetStat("Brutality 2"),
                        AttackStatsLibrary.GetStat("Quickswap 2"),
                        AttackStatsLibrary.GetStat("Impact 2"),
                        AttackStatsLibrary.GetStat("Propulsion 2"),
                        AttackStatsLibrary.GetStat("Scope 2"),
                        AttackStatsLibrary.GetStat("Vision 1"),
                        AttackStatsLibrary.GetStat("Awareness 1"),
                        AttackStatsLibrary.GetStat("Multi-cast 1"),
                        AttackStatsLibrary.GetStat("Bonus Round 1"),
                        AttackStatsLibrary.GetStat("Penetrating Ammo 1"),
                        AttackStatsLibrary.GetStat("High Caliber 1"),
                        AttackStatsLibrary.GetStat("Semi-Auto Novice"),
                    }
                },
                {
                    Rarity.Epic,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 3"),
                        AttackStatsLibrary.GetStat("Marksman 3"),
                        AttackStatsLibrary.GetStat("Brutality 3"),
                        AttackStatsLibrary.GetStat("Quickswap 3"),
                        AttackStatsLibrary.GetStat("Impact 3"),
                        AttackStatsLibrary.GetStat("Propulsion 3"),
                        AttackStatsLibrary.GetStat("Scope 3"),
                        AttackStatsLibrary.GetStat("Vision 2"),
                        AttackStatsLibrary.GetStat("Awareness 2"),
                        AttackStatsLibrary.GetStat("Multi-cast 2"),
                        AttackStatsLibrary.GetStat("Bonus Round 2"),
                        AttackStatsLibrary.GetStat("Penetrating Ammo 2"),
                        AttackStatsLibrary.GetStat("High Caliber 2"),
                        AttackStatsLibrary.GetStat("Semi-Auto Pro"),
                    }
                },
                {
                    Rarity.Legendary,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 4"),
                        AttackStatsLibrary.GetStat("Marksman 4"),
                        AttackStatsLibrary.GetStat("Brutality 4"),
                        AttackStatsLibrary.GetStat("Quickswap 4"),
                        AttackStatsLibrary.GetStat("Multi-cast 3"),
                        AttackStatsLibrary.GetStat("Bonus Round 3"),
                        AttackStatsLibrary.GetStat("Penetrating Ammo 3"),
                        AttackStatsLibrary.GetStat("High Caliber 3"),
                        AttackStatsLibrary.GetStat("Double Trouble"),
                        AttackStatsLibrary.GetStat("Semi-Auto God"),
                    }
                },
            }
        },
        {
            WeaponSetType.Shotgun,
            new Dictionary<Rarity, List<AttackStats>>
            {
                {
                    Rarity.Common,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 1"),
                        AttackStatsLibrary.GetStat("Marksman 1"),
                        AttackStatsLibrary.GetStat("Brutality 1"),
                        AttackStatsLibrary.GetStat("Quickswap 1"),
                        AttackStatsLibrary.GetStat("Impact 1"),
                        AttackStatsLibrary.GetStat("Propulsion 1"),
                        AttackStatsLibrary.GetStat("Scope 1"),
                        AttackStatsLibrary.GetStat("Wide Barrel 1"),
                    }
                },
                {
                    Rarity.Rare,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 2"),
                        AttackStatsLibrary.GetStat("Marksman 2"),
                        AttackStatsLibrary.GetStat("Brutality 2"),
                        AttackStatsLibrary.GetStat("Quickswap 2"),
                        AttackStatsLibrary.GetStat("Impact 2"),
                        AttackStatsLibrary.GetStat("Propulsion 2"),
                        AttackStatsLibrary.GetStat("Scope 2"),
                        AttackStatsLibrary.GetStat("Wide Barrel 2"),
                        AttackStatsLibrary.GetStat("Vision 1"),
                        AttackStatsLibrary.GetStat("Awareness 1"),
                        AttackStatsLibrary.GetStat("Multi-cast 1"),
                        AttackStatsLibrary.GetStat("Bonus Round 1"),
                        AttackStatsLibrary.GetStat("Penetrating Ammo 1"),
                        AttackStatsLibrary.GetStat("High Caliber 1"),
                        AttackStatsLibrary.GetStat("Shotgun Novice"),
                    }
                },
                {
                    Rarity.Epic,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 3"),
                        AttackStatsLibrary.GetStat("Marksman 3"),
                        AttackStatsLibrary.GetStat("Brutality 3"),
                        AttackStatsLibrary.GetStat("Quickswap 3"),
                        AttackStatsLibrary.GetStat("Impact 3"),
                        AttackStatsLibrary.GetStat("Propulsion 3"),
                        AttackStatsLibrary.GetStat("Scope 3"),
                        AttackStatsLibrary.GetStat("Wide Barrel 3"),
                        AttackStatsLibrary.GetStat("Vision 2"),
                        AttackStatsLibrary.GetStat("Awareness 2"),
                        AttackStatsLibrary.GetStat("Multi-cast 2"),
                        AttackStatsLibrary.GetStat("Bonus Round 2"),
                        AttackStatsLibrary.GetStat("Penetrating Ammo 2"),
                        AttackStatsLibrary.GetStat("High Caliber 2"),
                        AttackStatsLibrary.GetStat("Shotgun Pro"),
                    }
                },
                {
                    Rarity.Legendary,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 4"),
                        AttackStatsLibrary.GetStat("Marksman 4"),
                        AttackStatsLibrary.GetStat("Brutality 4"),
                        AttackStatsLibrary.GetStat("Quickswap 4"),
                        AttackStatsLibrary.GetStat("Multi-cast 3"),
                        AttackStatsLibrary.GetStat("Bonus Round 3"),
                        AttackStatsLibrary.GetStat("Penetrating Ammo 3"),
                        AttackStatsLibrary.GetStat("High Caliber 3"),
                        AttackStatsLibrary.GetStat("Double Trouble"),
                        AttackStatsLibrary.GetStat("Shotgun God"),
                    }
                },
            }
        },
        {
            WeaponSetType.Explosive,
            new Dictionary<Rarity, List<AttackStats>>
            {
                {
                    Rarity.Common,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 1"),
                        AttackStatsLibrary.GetStat("Marksman 1"),
                        AttackStatsLibrary.GetStat("Brutality 1"),
                        AttackStatsLibrary.GetStat("Quickswap 1"),
                        AttackStatsLibrary.GetStat("Impact 1"),
                        AttackStatsLibrary.GetStat("Scope 1"),
                        AttackStatsLibrary.GetStat("RoF 1"),
                    }
                },
                {
                    Rarity.Rare,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 2"),
                        AttackStatsLibrary.GetStat("Marksman 2"),
                        AttackStatsLibrary.GetStat("Brutality 2"),
                        AttackStatsLibrary.GetStat("Quickswap 2"),
                        AttackStatsLibrary.GetStat("Impact 2"),
                        AttackStatsLibrary.GetStat("Scope 2"),
                        AttackStatsLibrary.GetStat("RoF 2"),
                        AttackStatsLibrary.GetStat("Vision 1"),
                        AttackStatsLibrary.GetStat("Awareness 1"),
                        AttackStatsLibrary.GetStat("Multi-cast 1"),
                        AttackStatsLibrary.GetStat("Bonus Round 1"),
                        AttackStatsLibrary.GetStat("Size Up 1"),
                        AttackStatsLibrary.GetStat("Explosive Novice"),
                    }
                },
                {
                    Rarity.Epic,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 3"),
                        AttackStatsLibrary.GetStat("Marksman 3"),
                        AttackStatsLibrary.GetStat("Brutality 3"),
                        AttackStatsLibrary.GetStat("Quickswap 3"),
                        AttackStatsLibrary.GetStat("Impact 3"),
                        AttackStatsLibrary.GetStat("Scope 3"),
                        AttackStatsLibrary.GetStat("RoF 3"),
                        AttackStatsLibrary.GetStat("Vision 2"),
                        AttackStatsLibrary.GetStat("Awareness 2"),
                        AttackStatsLibrary.GetStat("Multi-cast 2"),
                        AttackStatsLibrary.GetStat("Bonus Round 2"),
                        AttackStatsLibrary.GetStat("Size Up 2"),
                        AttackStatsLibrary.GetStat("Explosive Pro"),
                    }
                },
                {
                    Rarity.Legendary,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 4"),
                        AttackStatsLibrary.GetStat("Marksman 4"),
                        AttackStatsLibrary.GetStat("Brutality 4"),
                        AttackStatsLibrary.GetStat("Quickswap 4"),
                        AttackStatsLibrary.GetStat("Multi-cast 3"),
                        AttackStatsLibrary.GetStat("Bonus Round 3"),
                        AttackStatsLibrary.GetStat("Size Up 3"),
                        AttackStatsLibrary.GetStat("Double Trouble"),
                        AttackStatsLibrary.GetStat("Explosive God"),
                    }
                },
            }
        },
        {
            WeaponSetType.Nova,
            new Dictionary<Rarity, List<AttackStats>>
            {
                {
                    Rarity.Common,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 1"),
                        AttackStatsLibrary.GetStat("Marksman 1"),
                        AttackStatsLibrary.GetStat("Brutality 1"),
                        AttackStatsLibrary.GetStat("Quickswap 1"),
                        AttackStatsLibrary.GetStat("Impact 1"),
                        AttackStatsLibrary.GetStat("Dexterity 1"),
                        AttackStatsLibrary.GetStat("Lunge 1"),
                        AttackStatsLibrary.GetStat("Implode 1"),
                    }
                },
                {
                    Rarity.Rare,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 2"),
                        AttackStatsLibrary.GetStat("Marksman 2"),
                        AttackStatsLibrary.GetStat("Brutality 2"),
                        AttackStatsLibrary.GetStat("Quickswap 2"),
                        AttackStatsLibrary.GetStat("Impact 2"),
                        AttackStatsLibrary.GetStat("Dexterity 2"),
                        AttackStatsLibrary.GetStat("Lunge 2"),
                        AttackStatsLibrary.GetStat("Implode 2"),
                        AttackStatsLibrary.GetStat("Vision 1"),
                        AttackStatsLibrary.GetStat("Awareness 1"),
                        AttackStatsLibrary.GetStat("Multi-cast 1"),
                        AttackStatsLibrary.GetStat("Enlarge 1"),
                        AttackStatsLibrary.GetStat("Wave Master 1"),
                        AttackStatsLibrary.GetStat("Ki Master 1"),
                        AttackStatsLibrary.GetStat("AFK Novice"),
                    }
                },
                {
                    Rarity.Epic,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 3"),
                        AttackStatsLibrary.GetStat("Marksman 3"),
                        AttackStatsLibrary.GetStat("Brutality 3"),
                        AttackStatsLibrary.GetStat("Quickswap 3"),
                        AttackStatsLibrary.GetStat("Impact 3"),
                        AttackStatsLibrary.GetStat("Dexterity 3"),
                        AttackStatsLibrary.GetStat("Lunge 3"),
                        AttackStatsLibrary.GetStat("Vision 2"),
                        AttackStatsLibrary.GetStat("Awareness 2"),
                        AttackStatsLibrary.GetStat("Multi-cast 2"),
                        AttackStatsLibrary.GetStat("Enlarge 2"),
                        AttackStatsLibrary.GetStat("Wave Master 2"),
                        AttackStatsLibrary.GetStat("Ki Master 2"),
                        AttackStatsLibrary.GetStat("After-shock 1"),
                        AttackStatsLibrary.GetStat("Once More 1"),
                        AttackStatsLibrary.GetStat("Pro"),
                    }
                },
                {
                    Rarity.Legendary,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 4"),
                        AttackStatsLibrary.GetStat("Marksman 4"),
                        AttackStatsLibrary.GetStat("Brutality 4"),
                        AttackStatsLibrary.GetStat("Quickswap 4"),
                        AttackStatsLibrary.GetStat("Multi-cast 3"),
                        AttackStatsLibrary.GetStat("Enlarge 3"),
                        AttackStatsLibrary.GetStat("After-shock 2"),
                        AttackStatsLibrary.GetStat("Once More 2"),
                        AttackStatsLibrary.GetStat("God"),
                        AttackStatsLibrary.GetStat("Double Trouble"),
                    }
                },
            }
        },
        {
            WeaponSetType.Melee,
            new Dictionary<Rarity, List<AttackStats>>
            {
                {
                    Rarity.Common,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 1"),
                        AttackStatsLibrary.GetStat("Marksman 1"),
                        AttackStatsLibrary.GetStat("Brutality 1"),
                        AttackStatsLibrary.GetStat("Quickswap 1"),
                        AttackStatsLibrary.GetStat("Impact 1"),
                        AttackStatsLibrary.GetStat("Dexterity 1"),
                        AttackStatsLibrary.GetStat("Lunge 1"),
                        AttackStatsLibrary.GetStat("Implode 1"),
                        AttackStatsLibrary.GetStat("RoF 1"),
                    }
                },
                {
                    Rarity.Rare,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 2"),
                        AttackStatsLibrary.GetStat("Marksman 2"),
                        AttackStatsLibrary.GetStat("Brutality 2"),
                        AttackStatsLibrary.GetStat("Quickswap 2"),
                        AttackStatsLibrary.GetStat("Impact 2"),
                        AttackStatsLibrary.GetStat("Dexterity 2"),
                        AttackStatsLibrary.GetStat("Lunge 2"),
                        AttackStatsLibrary.GetStat("Implode 2"),
                        AttackStatsLibrary.GetStat("RoF 2"),
                        AttackStatsLibrary.GetStat("Vision 1"),
                        AttackStatsLibrary.GetStat("Awareness 1"),
                        AttackStatsLibrary.GetStat("Multi-cast 1"),
                        AttackStatsLibrary.GetStat("Enlarge 1"),
                        AttackStatsLibrary.GetStat("Wave Master 1"),
                        AttackStatsLibrary.GetStat("Ki Master 1"),
                        AttackStatsLibrary.GetStat("Melee Novice"),
                    }
                },
                {
                    Rarity.Epic,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 3"),
                        AttackStatsLibrary.GetStat("Marksman 3"),
                        AttackStatsLibrary.GetStat("Brutality 3"),
                        AttackStatsLibrary.GetStat("Quickswap 3"),
                        AttackStatsLibrary.GetStat("Impact 3"),
                        AttackStatsLibrary.GetStat("Dexterity 3"),
                        AttackStatsLibrary.GetStat("Lunge 3"),
                        AttackStatsLibrary.GetStat("RoF 3"),
                        AttackStatsLibrary.GetStat("Vision 2"),
                        AttackStatsLibrary.GetStat("Awareness 2"),
                        AttackStatsLibrary.GetStat("Multi-cast 2"),
                        AttackStatsLibrary.GetStat("Enlarge 2"),
                        AttackStatsLibrary.GetStat("Wave Master 2"),
                        AttackStatsLibrary.GetStat("Ki Master 2"),
                        AttackStatsLibrary.GetStat("After-shock 1"),
                        AttackStatsLibrary.GetStat("Once More 1"),
                        AttackStatsLibrary.GetStat("Melee Pro"),
                    }
                },
                {
                    Rarity.Legendary,
                    new List<AttackStats>
                    {
                        AttackStatsLibrary.GetStat("Mastery 4"),
                        AttackStatsLibrary.GetStat("Marksman 4"),
                        AttackStatsLibrary.GetStat("Brutality 4"),
                        AttackStatsLibrary.GetStat("Quickswap 4"),
                        AttackStatsLibrary.GetStat("Multi-cast 3"),
                        AttackStatsLibrary.GetStat("Enlarge 3"),
                        AttackStatsLibrary.GetStat("After-shock 2"),
                        AttackStatsLibrary.GetStat("Once More 2"),
                        AttackStatsLibrary.GetStat("Melee Pro"),
                        AttackStatsLibrary.GetStat("Double Trouble"),
                    }
                },
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
