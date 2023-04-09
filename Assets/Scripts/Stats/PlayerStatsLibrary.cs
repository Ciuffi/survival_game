using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerStatsLibrary
{
    private static Dictionary<string, PlayerCharacterStats> PlayerStatsLibraryMap =
        new Dictionary<string, PlayerCharacterStats>();
    private static bool isInitialized = false;

    static PlayerStatsLibrary()
    {
        InitializeLibrary();
    }

    private static void AddStat(PlayerCharacterStats stat)
    {
        PlayerStatsLibraryMap.Add(stat.name, stat);
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        //COMMON STATS

            //Damage%
            AddStat(
                new PlayerCharacterStats(
                    damageMultiplier: 0.1f,
                    name: "Damage%",
                    description: "Increases damage by 10%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Attack Speed
            AddStat(
                new PlayerCharacterStats(
                    castTimeMultiplier: -0.1f,
                    name: "Attack Speed",
                    description: "Increases attack speed by 10%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Crit Chance
            AddStat(
                new PlayerCharacterStats(
                    critChance: 0.05f,
                    name: "Crit Chance",
                    description: "Increases critical hit chance by 5%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );



        //RARE STATS

            //melee swing
            AddStat(
                new PlayerCharacterStats(
                    comboLength: 1,
                    name: "MeleeSwing",
                    description: "Melee weapons emit +1 aftershock per swing",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

            //projectile 
            AddStat(
                new PlayerCharacterStats(
                    comboLength: 1,
                    name: "Projectile",
                    description: "Ranged weapons gain +1 projectile",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );





        //EPIC STATS

            //aftershock
            AddStat(
                new PlayerCharacterStats(
                    shotsPerAttackMelee: 1,
                    name: "Aftershock",
                    description: "Melee weapons emit +1 aftershock per swing",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Epic
                )
            );






        //LEGENDARY STATS

            //aftershock
            AddStat(
                new PlayerCharacterStats(
                    shotsPerAttackMelee: 2,
                    name: "Aftershock+",
                    description: "Melee weapons emit +2 aftershock per swing",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Legendary
                )
            );






        isInitialized = true;
    }

    public static PlayerCharacterStats getStat(string statName)
    {
        return PlayerStatsLibraryMap[statName];
    }

    public static PlayerCharacterStats[] getStats()
    {
        return PlayerStatsLibraryMap.Values.ToArray();
    }
}
