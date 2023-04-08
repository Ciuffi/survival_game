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

        AttackStats DamageBuff = new AttackStats(
            damageMultiplier: 0.1f,
            name: "Damage Buff",
            description: "Increases damage by 10%",
            icon: Resources.Load<Sprite>("Sprites/Icons/Stats/DamageBuff")
        );
        AddStat(DamageBuff);
        //knockback buff
        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.1f,
                name: "Knockback Buff",
                description: "Increases knockback by 10%",
                icon: Resources.Load<Sprite>("Sprites/Icons/Stats/KnockbackBuff")
            )
        );

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

    public static List<AttackStats> GetAllStats()
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        return AttackStatsLibraryMap.Values.ToList();
    }
}
