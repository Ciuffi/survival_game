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
                    health: 20,
                    name: "Healing",
                    description: "Recover 20 HP",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Damage%
            AddStat(
                new PlayerCharacterStats(
                    damageMultiplier: 0.1f,
                    name: "Damage",
                    description: "All weapons: Damage +10%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Attack Speed
            AddStat(
                new PlayerCharacterStats(
                    castTimeMultiplier: -0.1f,
                    name: "Attack Speed",
                    description: "All weapons: Attack Speed +10%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Crit Chance
            AddStat(
                new PlayerCharacterStats(
                    critChance: 0.05f,
                    name: "Crit Chance",
                    description: "All weapons: Crit Chance +5%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Knockback
            AddStat(
                new PlayerCharacterStats(
                    knockbackMultiplier: 0.15f,
                    name: "Knockback",
                    description: "All weapons: Knockback +15%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //Proj Range
            AddStat(
                new PlayerCharacterStats(
                    rangeMultiplier: 0.25f,
                    name: "Range Up",
                    description: "All weapons: Projectile Range +25%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Common
                )
            );

            //pickup range
            AddStat(
                new PlayerCharacterStats(
                    pickupRange: 1f,
                    name: "Magnet",
                    description: "Increase pickup range",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

      //RARE STATS

        //max health
        AddStat(
                new PlayerCharacterStats(
                    maxhealth: 15,
                    name: "Max HP",
                    description: "Increase Max HP by 15",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

             //defense
            AddStat(
                new PlayerCharacterStats(
                    defense: 1,
                    name: "Armor",
                    description: "Reduce all Damage taken by 1",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

            //melee Size
            AddStat(
                new PlayerCharacterStats(
                    meleeSizeMultiplier: 0.15f,
                    name: "Melee Size",
                    description: "Melee: Attack size +15%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

            //proj size
            AddStat(
                new PlayerCharacterStats(
                    projectileSizeMultiplier: 0.20f,
                    name: "Proj Size",
                    description: "Ranged: Projectile size +20%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

            //swing speed
            AddStat(
                new PlayerCharacterStats(
                    comboWaitTime: -0.3f,
                    name: "Melee Speed",
                    description: "Multi-hit Melee attacks finish faster",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

            //Rate of fire
            AddStat(
                new PlayerCharacterStats(
                    spreadMultiplier: -0.2f,
                    name: "Rate of Fire",
                    description: "All weapons: Rate of Fire +20%",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );

            //shotgun spread
            AddStat(
                new PlayerCharacterStats(
                    shotgunSpread: 30f,
                    name: "Shotgun Spread",
                    description: "Shotguns: Projectile Spread +30 degrees",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Rare
                )
            );



      //EPIC STATS

        //melee swing
        AddStat(
                new PlayerCharacterStats(
                    comboLength: 1,
                    name: "MeleeSwing",
                    description: "Melee weapons attack +1 times",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Epic
                )
            );

        //projectile 
        AddStat(
            new PlayerCharacterStats(
                comboLength: 1,
                name: "Projectile",
                description: "Ranged weapons gain +1 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //aftershock
        AddStat(
                new PlayerCharacterStats(
                    shotsPerAttackMelee: 1,
                    name: "Aftershock",
                    description: "Melee weapons emit +1 Aftershock per swing",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Epic
                )
            );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speed: 0.006f,
                name: "Speed",
                description: "Increase move speed by 15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.25f,
                name: "Multicast",
                description: "Increase Multicast chance by 25%",
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
                    description: "Melee weapons emit +2 Aftershock per swing",
                    icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                    rarity: Rarity.Legendary
                )
            );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.5f,
                name: "Multicast+",
                description: "Increase Multicast chance by 50%",
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

public class StatComponent : MonoBehaviour
{
    public PlayerCharacterStats stat;
}
