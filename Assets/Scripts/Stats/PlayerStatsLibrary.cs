using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerStatsLibrary
{
    private static Dictionary<string, PlayerCharacterStats> PlayerStatsLibraryMap =
        new Dictionary<string, PlayerCharacterStats>();
    private static bool isInitialized = false;
    private static List<GameObject> statGameObjects = new List<GameObject>();

    public static void CreateStatGameObjects()
    {
        foreach (PlayerCharacterStats stat in getStats())
        {
            GameObject statObject = new GameObject(stat.name);
            statObject.AddComponent<StatComponent>().stat = stat;
            statObject.GetComponent<StatComponent>().stat.setContainer(statObject);
            GameObject.DontDestroyOnLoad(statObject);
            statGameObjects.Add(statObject);
        }
    }

    public static List<GameObject> GetStatGameObjects()
    {
        return statGameObjects;
    }

    private static void AddStat(PlayerCharacterStats stat)
    {
        PlayerStatsLibraryMap.Add(stat.name, stat);
    }

    static PlayerStatsLibrary()
    {
        InitializeLibrary();
        CreateStatGameObjects();
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        //COMMON STATS

        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 15,
                name: "First Aid 1",
                description: "Recover 15 HP",
                icon: Resources.Load<Sprite>("UI_Icons/FirstAid"),
                rarity: Rarity.Common
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 10,
                name: "Thick Hide 1",
                description: "Increase Max HP by 10",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Common
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.02f,
                name: "Eagle Eye 1",
                description: "All Crit Chance +2%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Common
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 12f,
                name: "Scanner 1",
                description: "All Crit Dmg +12%",
                icon: Resources.Load<Sprite>("UI_Icons/Scanner"),
                rarity: Rarity.Common
            )
        );

        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 0.5f,
                name: "Magnet 1",
                description: "Increase pickup range",
                icon: Resources.Load<Sprite>("UI_Icons/Magnet"),
                rarity: Rarity.Common
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.15f,
                name: "Boost 1",
                description: "Move speed +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Boost"),
                rarity: Rarity.Common
            )
        );

        //RARE STATS
        AddStat(
            new PlayerCharacterStats(
                health: 25,
                name: "First Aid 2",
                description: "Recover 25 HP",
                icon: Resources.Load<Sprite>("UI_Icons/FirstAid"),
                rarity: Rarity.Rare
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 15,
                name: "Thick Hide 2",
                description: "Increase Max HP by 15",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Rare
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.04f,
                name: "Eagle Eye 2",
                description: "All Crit Chance +4%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Rare
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 0.25f,
                name: "Scanner 2",
                description: "All Crit Dmg +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Scanner"),
                rarity: Rarity.Rare
            )
        );

        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 1f,
                name: "Magnet 2",
                description: "Increase pickup range",
                icon: Resources.Load<Sprite>("UI_Icons/Magnet"),
                rarity: Rarity.Rare
            )
        );

        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 1,
                name: "Protection 1",
                description: "Reduce damage taken by 1",
                icon: Resources.Load<Sprite>("UI_Icons/Protection"),
                rarity: Rarity.Rare
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 1.1f,
                name: "Training 1",
                description: "All Damage +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Training"),
                rarity: Rarity.Rare
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.92f,
                name: "Adrenaline 1",
                description: "All Cooldowns -8%",
                icon: Resources.Load<Sprite>("UI_Icons/Adrenaline"),
                rarity: Rarity.Rare
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 1.12f,
                name: "Strength 1",
                description: "All Knockback +12%",
                icon: Resources.Load<Sprite>("UI_Icons/Strength"),
                rarity: Rarity.Rare
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 1.08f,
                name: "Mass 1",
                description: "Melee, Nova size +8%",
                icon: Resources.Load<Sprite>("UI_Icons/Mass"),
                rarity: Rarity.Rare
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 1.1f,
                name: "Size 1",
                description: "Auto, SemiAuto, Shotgun, Explosive size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Size"),
                rarity: Rarity.Rare
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.25f,
                name: "Boost 2",
                description: "Move speed +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Boost"),
                rarity: Rarity.Rare
            )
        );

        AddStat(
           new PlayerCharacterStats(
               activeMultiplier: 1.15f,
               name: "Persistence 1",
               description: "All Attack duration +15%",
               icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
               rarity: Rarity.Rare
           )
       );
        ;

        AddStat(
            new PlayerCharacterStats(
                effectDuration: 0.5f,
                name: "Hourglass 1",
                description: "All Debuff duration +0.5s",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new PlayerCharacterStats(
                effectMultiplier: 1.15f,
                name: "Saboteur 1",
                description: "All Debuff Power +15%",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Rare
            )
        );
        ;

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.15f,
                name: "Multicast 1",
                description: "All Multicast chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Multicast"),
                rarity: Rarity.Epic
            )
        );

        //EPIC STATS

        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 50,
                name: "First Aid 3",
                description: "Recover 50 HP",
                icon: Resources.Load<Sprite>("UI_Icons/FirstAid"),
                rarity: Rarity.Epic
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 30,
                name: "Thick Hide 3",
                description: "Increase Max HP by 30",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Epic
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.09f,
                name: "Eagle Eye 3",
                description: "All Crit Chance +9%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Epic
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 0.40f,
                name: "Scanner 3",
                description: "All Crit Dmg +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Scanner"),
                rarity: Rarity.Epic
            )
        );

        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 2,
                name: "Protection 2",
                description: "Reduce Damage taken by 2",
                icon: Resources.Load<Sprite>("UI_Icons/Protection"),
                rarity: Rarity.Epic
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 1.2f,
                name: "Training 2",
                description: "All Damage +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Training"),
                rarity: Rarity.Epic
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.85f,
                name: "Adrenaline 2",
                description: "All Cooldowns -15%",
                icon: Resources.Load<Sprite>("UI_Icons/Adrenaline"),
                rarity: Rarity.Epic
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 1.25f,
                name: "Strength 2",
                description: "All Knockback +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Strength"),
                rarity: Rarity.Epic
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 1.15f,
                name: "Mass 2",
                description: "Melee, Nova size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Mass"),
                rarity: Rarity.Epic
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 1.2f,
                name: "Size 2",
                description: "Auto, SemiAuto, Shotgun, Explosive size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Size"),
                rarity: Rarity.Epic
            )
        );


        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.40f,
                name: "Boost 3",
                description: "Move speed +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Boost"),
                rarity: Rarity.Epic
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.25f,
                name: "Multicast 2",
                description: "All Multicast chance +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Multicast"),
                rarity: Rarity.Epic
            )
        );

        AddStat(
            new PlayerCharacterStats(
                effectDuration: 1f,
                name: "Hourglass 2",
                description: "All Debuff duration +1s",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new PlayerCharacterStats(
                effectMultiplier: 1.25f,
                name: "Saboteur 2",
                description: "All Debuff Power +25%",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Epic
            )
        );
        ;

        //LEGENDARY STATS


        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.15f,
                name: "Eagle Eye 4",
                description: "All Crit Chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Legendary
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 0.8f,
                name: "Scanner 4",
                description: "All Crit Dmg +80%",
                icon: Resources.Load<Sprite>("UI_Icons/Scanner"),
                rarity: Rarity.Legendary
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 1.4f,
                name: "Training 3",
                description: "All Damage +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Training"),
                rarity: Rarity.Legendary
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.75f,
                name: "Adrenaline 3",
                description: "All Cooldowns -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Adrenaline"),
                rarity: Rarity.Legendary
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 1.4f,
                name: "Strength 3",
                description: "All Knockback +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Strength"),
                rarity: Rarity.Legendary
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 1.25f,
                name: "Mass 3",
                description: "Melee, Nova size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Mass"),
                rarity: Rarity.Legendary
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 1.35f,
                name: "Size 3",
                description: "Auto, SemiAuto, Shotgun, Explosive size +35%",
                icon: Resources.Load<Sprite>("UI_Icons/Size"),
                rarity: Rarity.Legendary
            )
        );



        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.6f,
                name: "Boost 4",
                description: "Move speed +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Boost"),
                rarity: Rarity.Legendary
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.5f,
                name: "Multicast 3",
                description: "All Multicast chance +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Multicast"),
                rarity: Rarity.Legendary
            )
        );

        AddStat(
            new PlayerCharacterStats(
                effectDuration: 2f,
                name: "Hourglass 3",
                description: "All Debuff duration +2s",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new PlayerCharacterStats(
                effectMultiplier: 1.5f,
                name: "Saboteur 3",
                description: "All Debuff Power +50%",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Legendary
            )
        );
        ;

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

public class StatComponent : MonoBehaviour
{
    public PlayerCharacterStats stat;
}
