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

        PlayerCharacterStats maxHealthBuff = new PlayerCharacterStats(
            maxhealth: 100,
            name: "Max Health Buff",
            description: "Increases max health by 100",
            icon: Resources.Load<Sprite>("Sprites/Icons/Stats/MaxHealthBuff")
        );
        AddStat(maxHealthBuff);
        //Damage buff
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.1f,
                name: "Damage Buff",
                description: "Increases damage by 10%",
                icon: Resources.Load<Sprite>("Sprites/Icons/Stats/DamageBuff")
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
