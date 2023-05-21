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
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 10,
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
                description: "Crit Chance +2%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 12f,
                name: "Scanner 1",
                description: "Crit Dmg +12%",
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
                speedMultiplier: 0.15f,
                name: "Boost 1",
                description: "Increase move speed by 15%",
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
                maxHealth: 15,
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
                description: "Crit Chance +4%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 0.25f,
                name: "Scanner 2",
                description: "Crit Dmg +25%",
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
                damageMultiplier: 1.08f,
                name: "Training 1",
                description: "Damage +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.92f,
                name: "Adrenaline 1",
                description: "Cooldowns -8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 1.12f,
                name: "Strength 1",
                description: "Knockback +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 1.08f,
                name: "Mass 1",
                description: "Melee, Nova size +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 1.1f,
                name: "Size 1",
                description: "Auto, SemiAuto, Shotgun, Explosive size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //swing speed
        AddStat(
            new PlayerCharacterStats(
                comboWaitTimeMultiplier: 0.88f,
                name: "Casting 1",
                description: "Melee, Nova Attack Speed +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //Rate of fire
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: 1.10f,
                name: "Rate of Fire 1",
                description: "Rate of Fire +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.20f,
                name: "Boost 2",
                description: "Increase move speed by 20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //melee swing
        AddStat(
            new PlayerCharacterStats(
                comboLength: 2,
                name: "Another 1",
                description: "Melee, Nova +1 Casts",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //projectile
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttack: 2,
                name: "Last Shot 1",
                description: "+1 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );

        //EPIC STATS

        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 50,
                name: "First Aid 3",
                description: "Recover 50 HP",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //max health
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 30,
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
                description: "Crit Chance +9%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 0.40f,
                name: "Scanner 3",
                description: "Crit Dmg +40%",
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
                damageMultiplier: 1.15f,
                name: "Training 2",
                description: "Damage +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.85f,
                name: "Adrenaline 2",
                description: "Cooldowns -15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 1.25f,
                name: "Strength 2",
                description: "Knockback +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 1.15f,
                name: "Mass 2",
                description: "Melee, Nova size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 1.2f,
                name: "Size 2",
                description: "Auto, SemiAuto, Shotgun, Explosive size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //swing speed
        AddStat(
            new PlayerCharacterStats(
                comboWaitTimeMultiplier: 0.75f,
                name: "Casting 2",
                description: "Melee, Nova Attack Speed +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //Rate of fire
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: 0.80f,
                name: "Rate of Fire 2",
                description: "Rate of Fire +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //melee swing
        AddStat(
            new PlayerCharacterStats(
                comboLength: 2,
                name: "Another 2",
                description: "Melee, Nova +2 Casts",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //projectile
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttack: 2,
                name: "Last Shot 2",
                description: "+2 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //aftershock
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttackMelee: 1,
                name: "Aftershocker 1",
                description: "+1 Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.30f,
                name: "Boost 3",
                description: "Increase move speed by 30%",
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
                description: "Crit Chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Crit Dmg
        AddStat(
            new PlayerCharacterStats(
                critDmg: 0.8f,
                name: "Scanner 4",
                description: "Crit Dmg +80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Damage%
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 1.3f,
                name: "Training 3",
                description: "Damage +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.75f,
                name: "Adrenaline 3",
                description: "Cooldowns -25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Knockback
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 1.4f,
                name: "Strength 3",
                description: "Knockback +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //melee Size
        AddStat(
            new PlayerCharacterStats(
                meleeSizeMultiplier: 1.25f,
                name: "Mass 3",
                description: "Melee, Nova size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //proj size
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 1.35f,
                name: "Size 3",
                description: "Auto, SemiAuto, Shotgun, Explosive size +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //swing speed
        AddStat(
            new PlayerCharacterStats(
                comboWaitTimeMultiplier: 0.50f,
                name: "Casting 3",
                description: "Melee, Nova Attack Speed +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //Rate of fire
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: 0.65f,
                name: "Rate of Fire 3",
                description: "Rate of Fire +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //projectile
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttack: 4,
                name: "Last Shots",
                description: "+4 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //aftershock
        AddStat(
            new PlayerCharacterStats(
                shotsPerAttackMelee: 2,
                name: "Aftershocker 2",
                description: "+2 Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.5f,
                name: "Boost 4",
                description: "Increase move speed by 50%",
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
