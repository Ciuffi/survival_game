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
            statObject.GetComponent<StatComponent>().stat.statsContainer = statObject;
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
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxhealth: 10,
                name: "Thick Hide 1",
                description: "Increase Max HP by 10",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.02f,
                name: "Eagle Eye 1",
                description: "All weapons: Crit Chance +2%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.12f,
                name: "Scanner 1",
                description: "All weapons: Crit Dmg +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 0.5f,
                name: "Magnet 1",
                description: "Increase pickup range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speed: 0.0025f,
                name: "Boost 1",
                description: "Increase move speed by 5%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //RARE STATS
        AddStat(
            new PlayerCharacterStats(
                health: 25,
                name: "First Aid 2",
                description: "Recover 25 HP",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxhealth: 15,
                name: "Thick Hide 2",
                description: "Increase Max HP by 15",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.04f,
                name: "Eagle Eye 2",
                description: "All weapons: Crit Chance +4%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.25f,
                name: "Scanner 2",
                description: "All weapons: Crit Dmg +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 1f,
                name: "Magnet 2",
                description: "Increase pickup range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 1,
                name: "Protection 1",
                description: "Reduce all Damage taken by 1",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.08f,
                name: "Training 1",
                description: "All weapons: Damage +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.08f,
                name: "Adrenaline 1",
                description: "All weapons: Attack Speed +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 0.12f,
                name: "Strength 1",
                description: "All weapons: Knockback +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 0.08f,
                name: "Melee Size 1",
                description: "Melee: Attack size +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 0.1f,
                name: "Proj Size 1",
                description: "Ranged: Projectile size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //swing speed
        AddStat(
            new PlayerCharacterStats(
                comboWaitTime: -0.12f,
                name: "Cast Speed 1",
                description: "Melee: Cast Speed +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Rate of fire
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.10f,
                name: "Rate of Fire 1",
                description: "All weapons: Rate of Fire +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speed: 0.005f,
                name: "Boost 2",
                description: "Increase move speed by 12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //EPIC STATS

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxhealth: 30,
                name: "Thick Hide 3",
                description: "Increase Max HP by 30",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.09f,
                name: "Eagle Eye 3",
                description: "All weapons: Crit Chance +9%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.40f,
                name: "Scanner 3",
                description: "All weapons: Crit Dmg +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 2,
                name: "Protection 2",
                description: "Reduce all Damage taken by 2",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.15f,
                name: "Training 2",
                description: "All weapons: Damage +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.15f,
                name: "Adrenaline 2",
                description: "All weapons: Attack Speed +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 0.25f,
                name: "Strength 2",
                description: "All weapons: Knockback +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 0.15f,
                name: "Melee Size 2",
                description: "Melee: Attack size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 0.2f,
                name: "Proj Size 2",
                description: "Ranged: Projectile size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //swing speed
        AddStat(
            new PlayerCharacterStats(
                comboWaitTime: -0.25f,
                name: "Cast Speed 2",
                description: "Melee: Cast Speed +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Rate of fire
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.20f,
                name: "Rate of Fire 2",
                description: "All weapons: Rate of Fire +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //melee swing
        AddStat(
            new PlayerCharacterStats(
                comboLength: 2,
                name: "Another 2",
                description: "Melee: +2 Casts",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //projectile
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttack: 2,
                name: "Last Shot 2",
                description: "Ranged: +2 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //aftershock
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttackMelee: 1,
                name: "Aftershocker 1",
                description: "Melee: +1 Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speed: 0.008f,
                name: "Boost 3",
                description: "Increase move speed by 20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.2f,
                name: "Multicast",
                description: "Increase Multicast chance by 20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //LEGENDARY STATS


        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.15f,
                name: "Eagle Eye 4",
                description: "All weapons: Crit Chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.8f,
                name: "Scanner 4",
                description: "All weapons: Crit Dmg +80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.3f,
                name: "Training 3",
                description: "All weapons: Damage +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.25f,
                name: "Adrenaline 3",
                description: "All weapons: Attack Speed +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 0.4f,
                name: "Strength 3",
                description: "All weapons: Knockback +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 0.25f,
                name: "Melee Size 3",
                description: "Melee: Attack size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 0.35f,
                name: "Proj Size 3",
                description: "Ranged: Projectile size +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //swing speed
        AddStat(
            new PlayerCharacterStats(
                comboWaitTime: -0.50f,
                name: "Cast Speed 3",
                description: "Melee: Cast Speed +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Rate of fire
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.4f,
                name: "Rate of Fire 3",
                description: "All weapons: Rate of Fire +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //projectile
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttack: 4,
                name: "Last Shots",
                description: "Ranged: +4 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //aftershock
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttackMelee: 2,
                name: "Aftershocker 2",
                description: "Melee: +2 Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speed: 0.012f,
                name: "Boost 4",
                description: "Increase move speed by 40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
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
