using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AttackStatsLibrary
{
    private static Dictionary<string, AttackStats> AttackStatsLibraryMap =
        new Dictionary<string, AttackStats>();
    private static bool isInitialized = false;

    static AttackStatsLibrary()
    {
        InitializeLibrary();
    }

    private static void AddStat(AttackStats stat)
    {
        AttackStatsLibraryMap.Add(stat.name, stat);
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        //Value of stats - Individual Weapon -> Wpn Set -> Player 
        //COMMON

        AddStat(
            new AttackStats(
                damageMultiplier: 0.1f,
                name: "Damage%",
                description: "Increases damage by 10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        ); ;

        AddStat(
             new AttackStats(
                 critChance: 0.05f,
                 name: "Crit Chance",
                 description: "Increases critical hit chance by 5%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 knockbackMultiplier: 0.1f,
                 name: "Knockback",
                 description: "Increases knockback by 10%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;




        isInitialized = true;
    }

    public static AttackStats GetStat(string name)
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        if (AttackStatsLibraryMap.ContainsKey(name))
        {
            return AttackStatsLibraryMap[name];
        }
        else
        {
            Debug.LogError("AttackStatsLibrary does not contain a stat named " + name);
            return null;
        }
    }

    public static AttackStats[] GetStats()
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        return AttackStatsLibraryMap.Values.ToArray();
    }
}
