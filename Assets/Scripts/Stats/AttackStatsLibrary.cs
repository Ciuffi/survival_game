using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AttackStatsLibrary
{
    private static Dictionary<string, AttackStats> AttackStatsLibraryMap =
        new Dictionary<string, AttackStats>();
    private static bool isInitialized = false;
    private static List<GameObject> attackStatGameObjects = new List<GameObject>();

    // In AttackStatsLibrary:
    public static GameObject CreateStatGameObject(AttackStats stat)
    {
        GameObject statObject = new GameObject(stat.name);
        statObject.AddComponent<AttackStatComponent>().stat = stat;
        stat.statsContainer = statObject; // set the statsContainer directly
        return statObject;
    }

    public static List<GameObject> GetStatGameObjects()
    {
        return attackStatGameObjects;
    }

    private static void AddStat(AttackStats stat)
    {
        //Debug.Log($"Creating AttackStat: {stat}");

        AttackStatsLibraryMap.Add(stat.name, stat);
    }

    static AttackStatsLibrary()
    {
        InitializeLibrary();
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            Debug.Log("InitializeLibrary has already been called.");
            return;
        }

        Debug.Log("Initializing AttackStatsLibrary...");

        //Value of stats - Individual Weapon -> Wpn Set -> Player

        //Global - All Weapons

        //common
        AddStat(
            new AttackStats(
                aimRangeAdditive: -1f,
                name: "MLG 1",
                description: "Decrease Aim Range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                coneAngle: -30f,
                name: "Gamer 1",
                description: "Decrease Aim Assist Width",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 1.06f,
                name: "Damage 1",
                description: "Damage +6%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.05f,
                name: "Crit Chance 1",
                description: "Crit Chance +5%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.25f,
                name: "Crit Dmg 1",
                description: "Crit Dmg +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.92f,
                name: "Haste 1",
                description: "Cooldown -8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.10f,
                name: "Knockback 1",
                description: "Knockback +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.88f,
                name: "Glattt 1",
                description: "Rate of Fire +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.15f,
                name: "Saw'd Off 1",
                description: "Shotgun Spread +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 0.6f,
                name: "Steady 1",
                description: "Projectile Spray -40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 1.4f,
                name: "Overheat 1",
                description: "Projectile Spray +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1.12f,
                name: "Velocity 1",
                description: "Projectile Speed +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 1.12f,
                name: "Reach 1",
                description: "Range +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: 0.88f,
                name: "Quick Hands 1",
                description: "Attack Speed +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 1.2f,
                name: "Extend 1",
                description: "Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.75f,
                name: "Kamakazi 1",
                description: "Range -25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        );
        ;

        //rare
        AddStat(
            new AttackStats(
                aimRangeAdditive: -3f,
                critChance: 0.10f,
                name: "MLG 2",
                description: "Decrease Aim Range, Crit Chance +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                coneAngle: -100f,
                critDmg: 0.3f,
                name: "Gamer 2",
                description: "Decrease Aim Width, Crit Dmg +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 1.12f,
                name: "Damage 2",
                description: "Damage +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.08f,
                name: "Crit Chance 2",
                description: "Crit Chance +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.40f,
                name: "Crit Dmg 2",
                description: "Crit Dmg +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.85f,
                name: "Haste 2",
                description: "Cooldown -15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.20f,
                name: "Knockback 2",
                description: "Knockback +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.75f,
                name: "Glattt 2",
                description: "Rate of Fire +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.3f,
                name: "Saw'd Off 2",
                description: "Shotgun Spread +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 0.2f,
                name: "Steady 2",
                description: "Projectile Spray -80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 1.8f,
                name: "Overheat 2",
                description: "Projectile Spray +80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1.25f,
                name: "Velocity 2",
                description: "Projectile Speed +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 1.25f,
                name: "Reach 2",
                description: "Range +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: 0.65f,
                name: "Quick Hands 2",
                description: "Attack Speed +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 1.4f,
                name: "Extend 2",
                description: "Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.4f,
                name: "Kamakazi 2",
                description: "Range -60%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 1f,
                name: "Hacker 1",
                description: "Increases Aim Range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 40f,
                name: "AFK 1",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.15f,
                name: "Multicast 1",
                description: "Multicast chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                name: "Extra Round 1",
                description: "+1 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 6,
                name: "Extended Clip 1",
                description: "+6 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 1,
                name: "Pierce 1",
                description: "Pierce through +1 enemy",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 5,
                name: "Puncture 1",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.25f,
                name: "Big Ammo 1",
                description: "Projectile Size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.15f,
                name: "Big Gadget 1",
                description: "Projectile Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                name: "One More 1",
                description: "+1 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                name: "Aftershock 1",
                description: "Release an aftershock after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 1.15f,
                name: "Big Weapon 1",
                description: "Attack Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 1.07f,
                name: "Wave Surge 1",
                description: "Aftershock Size +7%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 1.2f,
                name: "Ki Surge 1",
                description: "Aftershock Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        );
        ;

        //epic
        AddStat(
            new AttackStats(
                damageMultiplier: 1.12f,
                name: "Damage 3",
                description: "Damage +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.2f,
                name: "Crit Chance 3",
                description: "Crit Chance +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 1f,
                name: "Crit Dmg 3",
                description: "Crit Dmg +100%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.65f,
                name: "Haste 3",
                description: "Cooldown -35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.40f,
                name: "Knockback 3",
                description: "Knockback +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.6f,
                name: "Glattt 3",
                description: "Rate of Fire +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.5f,
                name: "Saw'd Off 3",
                description: "Shotgun Spread +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1.4f,
                name: "Velocity 3",
                description: "Projectile Speed +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 1.4f,
                name: "Reach 3",
                description: "Projectile Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: 0.5f,
                name: "Quick Hands 3",
                description: "Attack Speed +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 2f,
                name: "Extend 3",
                description: "Range +100%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 2f,
                name: "Hacker 2",
                description: "Increases Aim Range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 60f,
                name: "AFK 2",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.33f,
                name: "Multicast 2",
                description: "Multicast chance +33%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 3,
                name: "Extra Round 2",
                description: "+3 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 15,
                name: "Extended Clip 2",
                description: "+15 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 3,
                name: "Pierce 2",
                description: "Pierce through +3 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 20,
                name: "Puncture 2",
                description: "Pierce through +20 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.4f,
                name: "Big Ammo 2",
                description: "Projectile Size +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.3f,
                name: "Big Gadget 2",
                description: "Projectile Size +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 2,
                name: "One More 2",
                description: "+2 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 2,
                name: "Aftershock 2",
                description: "Release 2 aftershocks after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 1.3f,
                name: "Big Weapon 2",
                description: "Attack Size +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 1.15f,
                name: "Wave Surge 2",
                description: "Aftershock Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 1.5f,
                name: "Ki Surge 2",
                description: "Aftershock Range +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        );
        ;

        //legendary

        AddStat(
            new AttackStats(
                shootOppositeSide: true,
                name: "Double Trouble",
                description: "Also Attack Behind (Doesn't Stack)",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 1.80f,
                name: "Damage 4",
                description: "Damage +80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.5f,
                name: "Crit Chance 4",
                description: "Crit Chance +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 3f,
                name: "Crit Dmg 4",
                description: "Crit Dmg +300%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.25f,
                name: "Haste 4",
                description: "Cooldown -75%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.80f,
                name: "Knockback 4",
                description: "Knockback +80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 1f,
                name: "Multicast 3",
                description: "Multicast chance +100%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 5,
                name: "Extra Round 3",
                description: "+5 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 40,
                name: "Extended Clip 3",
                description: "+40 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 50,
                name: "Pierce 3",
                description: "Pierce through +10 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 99,
                name: "Puncture 3",
                description: "Pierce through +99 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.8f,
                name: "Big Ammo 3",
                description: "Projectile Size +80%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.55f,
                name: "Big Gadget 3",
                description: "Projectile Size +55%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 3,
                name: "Aftershock 3",
                description: "Release 3 aftershocks after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 1.6f,
                name: "Big Weapon 3",
                description: "Attack Size +60%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary
            )
        );
        ;

        //Weapon Sets

        //common

        AddStat(
            new AttackStats(
                damageMultiplier: 1.05f,
                name: "Mastery 1",
                description: "Damage +5%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.03f,
                name: "Marksman 1",
                description: "Crit Chance +3%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.15f,
                name: "Brutality 1",
                description: "Crit Dmg +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.95f,
                name: "Quickswap 1",
                description: "Cooldowns -5%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.08f,
                name: "Impact 1",
                description: "Knockback +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.89f,
                name: "RoF 1",
                description: "Rate of Fire +11%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1.10f,
                name: "Propulsion 1",
                description: "Projectile Speed +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 1.10f,
                name: "Scope 1",
                description: "Range +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.15f,
                name: "Wide Barrel 1",
                description: "Shotgun Spread +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: 0.88f,
                name: "Dexterity 1",
                description: "Attack Speed +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 1.15f,
                name: "Lunge 1",
                description: "Range +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.75f,
                name: "Implode 1",
                description: "Range -25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        //rare

        AddStat(
            new AttackStats(
                damageMultiplier: 1.1f,
                name: "Mastery 2",
                description: "Damage +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.05f,
                name: "Marksman 2",
                description: "Crit Chance +5%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.25f,
                name: "Brutality 2",
                description: "Crit Dmg +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.90f,
                name: "Quickswap 2",
                description: "Cooldowns -10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.15f,
                name: "Impact 2",
                description: "Knockback +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.88f,
                name: "RoF 2",
                description: "Rate of Fire +22%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.3f,
                name: "Wide Barrel 2",
                description: "Shotgun Spread +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1.2f,
                name: "Propulsion 2",
                description: "Projectile Speed +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 1.2f,
                name: "Scope 2",
                description: "Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: 0.80f,
                name: "Dexterity 2",
                description: "Attack Speed +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 1.25f,
                name: "Lunge 2",
                description: "Range +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.6f,
                name: "Implode 2",
                description: "Range -40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 0.6f,
                name: "Vision 1",
                description: "Increases Aim Range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 20f,
                name: "Awareness 1",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.08f,
                name: "Multi-cast 1",
                description: "Multicast chance +8%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                name: "Bonus Round 1",
                description: "+1 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 4,
                name: "Extended Mag 1",
                description: "+4 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 1,
                name: "Piercing Ammo 1",
                description: "Pierce through +1 enemy",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 3,
                name: "Penetrating Ammo 1",
                description: "Pierce through +3 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.20f,
                name: "High Caliber 1",
                description: "Projectile Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.1f,
                name: "Size Up 1",
                description: "Projectile Size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 1.10f,
                name: "Enlarge 1",
                description: "Attack Size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 1.04f,
                name: "Wave Master 1",
                description: "Aftershock Size +4%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 1.12f,
                name: "Ki Master 1",
                description: "Aftershock Range +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        //epic
        AddStat(
            new AttackStats(
                damageMultiplier: 1.2f,
                name: "Mastery 3",
                description: "Damage +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;
        AddStat(
            new AttackStats(
                critChance: 0.1f,
                name: "Marksman 3",
                description: "Crit Chance +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.50f,
                name: "Brutality 3",
                description: "Crit Dmg +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.85f,
                name: "Quickswap 3",
                description: "Cooldowns -15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.3f,
                name: "Impact 3",
                description: "Knockback +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.65f,
                name: "RoF 3",
                description: "Rate of Fire +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.4f,
                name: "Wide Barrel 3",
                description: "Shotgun Spread +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1.35f,
                name: "Propulsion 3",
                description: "Projectile Speed +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 1.35f,
                name: "Scope 3",
                description: "Range +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: 0.70f,
                name: "Dexterity 3",
                description: "Attack Speed +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 1.4f,
                name: "Lunge 3",
                description: "Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 1.25f,
                name: "Vision 2",
                description: "Increases Aim Range",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 50f,
                name: "Awareness 2",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.15f,
                name: "Multi-cast 2",
                description: "Multicast chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 2,
                name: "Bonus Round 2",
                description: "+2 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 10,
                name: "Extended Mag 2",
                description: "+10 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 2,
                name: "Piercing Ammo 2",
                description: "Pierce through +2 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 8,
                name: "Penetrating Ammo 2",
                description: "Pierce through +8 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.30f,
                name: "High Caliber 2",
                description: "Projectile Size +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.20f,
                name: "Size Up 2",
                description: "Projectile Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                name: "Once More 1",
                description: "+1 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                name: "After-shock 1",
                description: "Release an aftershock after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 1.20f,
                name: "Enlarge 2",
                description: "Attack Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 1.10f,
                name: "Wave Master 2",
                description: "Aftershock Size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 1.25f,
                name: "Ki Master 2",
                description: "Aftershock Range +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        //legendary

        AddStat(
            new AttackStats(
                shootOppositeSide: true,
                name: "Buddy System",
                description: "Also Attack Behind (Doesn't Stack)",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 1.50f,
                name: "Mastery 4",
                description: "Damage +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.3f,
                name: "Marksman 4",
                description: "Crit Chance +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 1f,
                name: "Brutality 4",
                description: "Crit Dmg +100%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.6f,
                name: "Quickswap 4",
                description: "Cooldowns -40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.5f,
                name: "Impact 4",
                description: "Knockback +60%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.6f,
                name: "Multi-cast 3",
                description: "Multicast chance +60%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 4,
                name: "Bonus Round 3",
                description: "+4 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 25,
                name: "Extended Mag 3",
                description: "+25 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 10,
                name: "Piercing Ammo 3",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 50,
                name: "Penetrating Ammo 3",
                description: "Pierce through +50 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.6f,
                name: "High Caliber 3",
                description: "Projectile Size +60%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 1.40f,
                name: "Size Up 3",
                description: "Projectile Size +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 2,
                name: "After-shock 2",
                description: "Release 2 aftershocks after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 2,
                name: "Once More 2",
                description: "+2 Casts",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 1.40f,
                name: "Enlarge 3",
                description: "Attack Size +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Automatic

        AddStat(
            new AttackStats(
                spreadMultiplier: 0.9f,
                speedMultiplier: 1.15f,
                aimRangeAdditive: 0.5f,
                coneAngle: 5f,
                name: "Auto Novice",
                description: "All Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 5,
                spreadMultiplier: 0.85f,
                rangeMultiplier: 1.15f,
                aimRangeAdditive: 0.7f,
                coneAngle: 10f,
                name: "Auto Pro",
                description: "All Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 1,
                shotsPerAttack: 10,
                spreadMultiplier: 0.8f,
                rangeMultiplier: 1.2f,
                aimRangeAdditive: 1.2f,
                coneAngle: 15f,
                name: "Auto God",
                description: "All Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Semi-Auto

        AddStat(
            new AttackStats(
                castTimeMultiplier: 0.95f,
                projectileSizeMultiplier: 1.05f,
                aimRangeAdditive: 0.6f,
                coneAngle: -5f,
                name: "Semi-Auto Novice",
                description: "All Semi-Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                spreadMultiplier: 0.85f,
                projectileSizeMultiplier: 1.10f,
                aimRangeAdditive: 0.9f,
                coneAngle: -10f,
                name: "Semi-Auto Pro",
                description: "All Semi-Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                pierce: 3,
                castTimeMultiplier: 0.8f,
                projectileSizeMultiplier: 1.2f,
                aimRangeAdditive: 1.5f,
                coneAngle: -15f,
                name: "Semi-Auto God",
                description: "All Semi-Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Shotgun

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.15f,
                speedMultiplier: 1.12f,
                aimRangeAdditive: 0.4f,
                coneAngle: 10f,
                name: "Shotgun Novice",
                description: "All Shotgun Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                rangeMultiplier: 1.15f,
                knockbackMultiplier: 1.15f,
                shotgunSpreadMultiplier: 1.15f,
                aimRangeAdditive: 0.75f,
                coneAngle: 15f,
                name: "Shotgun Pro",
                description: "All Shotgun Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 2,
                shotgunSpreadMultiplier: 1.3f,
                multicastWaitTimeMultiplier: 0.8f, 
                rangeMultiplier: 1.15f,
                aimRangeAdditive: 1f,
                coneAngle: 30f,
                name: "Shotgun God",
                description: "All Shotgun Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Explosive

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 1.10f,
                projectileSizeMultiplier: 1.10f,
                aimRangeAdditive: 0.4f,
                coneAngle: 15f,
                name: "Explosive Novice",
                description: "All Explosive Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                shotgunSpreadMultiplier: 1.15f,
                projectileSizeMultiplier: 1.10f,
                rangeMultiplier: 1.15f,
                aimRangeAdditive: 0.8f,
                coneAngle: 30f,
                name: "Explosive Pro",
                description: "All Explosive Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                spreadMultiplier: 0.75f,
                shotgunSpreadMultiplier: 1.25f,
                projectileSizeMultiplier: 1.2f,
                rangeMultiplier: 1.15f,
                aimRangeAdditive: 1.5f,
                coneAngle: 50f,
                name: "Explosive God",
                description: "All Explosive Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Nova

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.10f,
                meleeSizeMultiplier: 1.10f,
                name: "AFK Novice",
                description: "All Nova Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                comboWaitTimeMultiplier: 0.8f,
                knockbackMultiplier: 1.10f,
                name: "AFK Pro",
                description: "All Nova Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                meleeSizeMultiplier: 1.2f,
                name: "AFK God",
                description: "All Nova Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Melee

        AddStat(
            new AttackStats(
                knockbackMultiplier: 1.1f,
                meleeSizeMultiplier: 1.1f,
                aimRangeAdditive: 0.4f,
                coneAngle: 20f,
                name: "Melee Novice",
                description: "All Melee Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                comboWaitTimeMultiplier: 0.85f,
                aimRangeAdditive: 0.7f,
                coneAngle: 35f,
                name: "Melee Pro",
                description: "All Melee Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                knockbackMultiplier: 1.2f,
                meleeSizeMultiplier: 1.2f,
                meleeShotsScaleUpMultiplier: 1.1f,
                aimRangeAdditive: 1.2f,
                coneAngle: 50f,
                name: "Melee God",
                description: "All Melee Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        isInitialized = true;

        //foreach (var kvp in AttackStatsLibraryMap)
        //{
            //Debug.Log($"Library contains: {kvp.Key} => {kvp.Value}");
        //}

        Debug.Log("AttackStatsLibrary initialized.");
    }

    public static AttackStats GetStat(string name)
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        if (AttackStatsLibraryMap.ContainsKey(name))
        {
            // Log the result before returning it.
            //Debug.Log($"Found stat: {name} ");

            return AttackStatsLibraryMap[name];
        }
        else
        {
            throw new System.Exception("AttackStatsLibrary does not contain a stat named " + name);
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

public class AttackStatComponent : MonoBehaviour
{
    public AttackStats stat;
}
