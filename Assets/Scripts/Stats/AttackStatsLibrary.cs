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
                critChance: 0.05f,
                name: "MLG 1",
                description: "Decrease Aim Range, Crit Chance +5%",
                icon: Resources.Load<Sprite>("UI_Icons/MLG"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                coneAngle: -30f,
                critDmg: 0.25f,
                name: "Gamer 1",
                description: "Decrease Aim Width, Crit Damage +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Gamer"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.12f,
                name: "Damage 1",
                description: "Damage +12%",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.03f,
                name: "Crit Chance 1",
                description: "Crit Chance +3%",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.20f,
                name: "Crit Dmg 1",
                description: "Crit Dmg +20%",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.12f,
                name: "Haste 1",
                description: "Cooldown -12%",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.12f,
                name: "Knockback 1",
                description: "Knockback +12%",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.10f,
                name: "Glattt 1",
                description: "Rate of Fire +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Glattt"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.15f,
                name: "Saw'd Off 1",
                description: "Shotgun Spread +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Saw'dOff"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: -0.3f,
                name: "Steady 1",
                description: "Projectile Spray -30%",
                icon: Resources.Load<Sprite>("UI_Icons/Steady"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 0.3f,
                name: "Overheat 1",
                description: "Projectile Spray +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Overheat"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.10f,
                name: "Velocity 1",
                description: "Projectile Speed +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Velocity"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.12f,
               name: "Gravity 1",
               description: "Projectile Speed -12%",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Common
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.10f,
                name: "Reach 1",
                description: "Range +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Reach"),
                rarity: Rarity.Common
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.12f,
                name: "Quick Hands 1",
                description: "Reduce time between attacks -12%",
                icon: Resources.Load<Sprite>("UI_Icons/QuickHands"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.15f,
                name: "Extend 1",
                description: "Range +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Extend"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: -0.20f,
                name: "Kamakazi 1",
                description: "Range -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Kamakazi"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
           new AttackStats(
               aimRangeAdditive: 0.75f,
               name: "Hacker 1",
               description: "Increases Aim Range",
               icon: Resources.Load<Sprite>("UI_Icons/Hacker"),
               rarity: Rarity.Common
           )
       );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 40f,
                name: "AFK 1",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/AFK"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSlow: true,
                slowPercentage: 0.85f,
                slowDuration: 1f,
                name: "Slow Down 1",
                description: "Slow for +1s",
                icon: Resources.Load<Sprite>("UI_Icons/SlowDown"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                isMagnet: true,
                magnetStrength: 0.5f,
                magnetDuration: 0.5f,
                name: "Magnetize 1",
                description: "On hit, pull target(s) in",
                icon: Resources.Load<Sprite>("UI_Icons/Magnetize"),
                rarity: Rarity.Common
            )
        );
        ;


        //rare
        AddStat(
            new AttackStats(
                aimRangeAdditive: -2f,
                critChance: 0.1f,
                name: "MLG 2",
                description: "Decrease Aim Range, Crit Chance +10%",
                icon: Resources.Load<Sprite>("UI_Icons/MLG"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                coneAngle: -60f,
                critDmg: 0.45f,
                name: "Gamer 2",
                description: "Decrease Aim Width, Crit Dmg +45%",
                icon: Resources.Load<Sprite>("UI_Icons/Gamer"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.25f,
                name: "Damage 2",
                description: "Damage +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.06f,
                name: "Crit Chance 2",
                description: "Crit Chance +6%",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.35f,
                name: "Crit Dmg 2",
                description: "Crit Dmg +35%",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.25f,
                name: "Haste 2",
                description: "Cooldown -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.25f,
                name: "Knockback 2",
                description: "Knockback +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.20f,
                name: "Glattt 2",
                description: "Rate of Fire +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Glattt"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.3f,
                name: "Saw'd Off 2",
                description: "Shotgun Spread +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Saw'dOff"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: -0.6f,
                name: "Steady 2",
                description: "Projectile Spray -60%",
                icon: Resources.Load<Sprite>("UI_Icons/Steady"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 0.6f,
                name: "Overheat 2",
                description: "Projectile Spray +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Overheat"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.20f,
                name: "Velocity 2",
                description: "Projectile Speed +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Velocity"),
                rarity: Rarity.Rare
            )
        );

        ;
        AddStat(
           new AttackStats(
               speedMultiplier: -0.20f,
               name: "Gravity 2",
               description: "Projectile Speed -20%",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Rare
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.15f,
                name: "Reach 2",
                description: "Range +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Reach"),
                rarity: Rarity.Rare
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.35f,
                name: "Quick Hands 2",
                description: "Reduce time between attacks -35%",
                icon: Resources.Load<Sprite>("UI_Icons/QuickHands"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.25f,
                name: "Extend 2",
                description: "Range +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Extend"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: -0.35f,
                name: "Kamakazi 2",
                description: "Range -35%",
                icon: Resources.Load<Sprite>("UI_Icons/Kamakazi"),
                rarity: Rarity.Rare
            )
        );
        ;

       

        AddStat(
            new AttackStats(
                multicastChance: 0.2f,
                name: "Multicast 1",
                description: "Multicast chance +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.30f,
                name: "Multicast+ 1",
                description: "Multicast chance +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                name: "Extra Round 1",
                description: "+1 Projectile",
                icon: Resources.Load<Sprite>("UI_Icons/ExtraRound"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 10,
                name: "Extended Clip 1",
                description: "+10 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 2,
                name: "Pierce 1",
                description: "Pierce through +2 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Pierce"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 10,
                name: "Puncture 1",
                description: "Pierce through +10 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Puncture"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.12f,
                name: "Big Ammo 1",
                description: "Projectile Size +12%",
                icon: Resources.Load<Sprite>("UI_Icons/BigAmmo"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.10f,
                name: "Big Gadget 1",
                description: "Projectile Size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/BigGadget"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                name: "One More 1",
                description: "+1 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/OneMore"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                name: "Aftershock 1",
                description: "Release an aftershock after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/Aftershock"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.10f,
                name: "Big Weapon 1",
                description: "Attack Size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/BigWeapon"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 0.08f,
                name: "Wave Surge 1",
                description: "Aftershock Size +8% per shock",
                icon: Resources.Load<Sprite>("UI_Icons/WaveSurge"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 0.12f,
                name: "Ki Surge 1",
                description: "Aftershock Range +12%",
                icon: Resources.Load<Sprite>("UI_Icons/KiSurge"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                activeMultiplier: 0.3f,
                name: "Persistence 1",
                description: "Attack duration +30%",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                activeDuration: 0.5f,
                name: "Persistence+ 1",
                description: "Attack duration +0.5s",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectDuration: 0.5f,
                name: "Hourglass 1",
                description: "Effect duration +0.5s",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectMultiplier: 0.25f,
                name: "Saboteur 1",
                description: "Effect Power +25%",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
           new AttackStats(
               aimRangeAdditive: 1.2f,
               name: "Hacker 2",
               description: "Increases Aim Range",
               icon: Resources.Load<Sprite>("UI_Icons/Hacker"),
               rarity: Rarity.Rare
           )
       );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 60f,
                name: "AFK 2",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/AFK"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
           new AttackStats(
               isDoT: true,
               dotDamage: 1,
               dotDuration: 5,
               dotTickRate: 1,
               name: "Ignite 1",
               description: "Ignite targets for 5 seconds",
               icon: Resources.Load<Sprite>("UI_Icons/Ignite"),
               rarity: Rarity.Rare
           )
       );
        ; 

        AddStat(
            new AttackStats(
                isDoT: true,
                dotDamage: 1,
                dotDuration: 3,
                dotTickRate: 1,
                name: "Cindershot 1",
                description: "Ignite targets for 3 seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 1,
                chainStatDecayPercent: 0.5f,
                chainRange: 3f,
                chainSpeed: 10f,
                name: "Chain 1",
                description: "Damage jumps to 1 more target",
                icon: Resources.Load<Sprite>("UI_Icons/Chain"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 2,
                chainStatDecayPercent: 0.3f,
                chainRange: 3.5f,
                chainSpeed: 5f,
                name: "Chain+ 1",
                description: "Damage jumps to 2 more targets",
                icon: Resources.Load<Sprite>("UI_Icons/Chain"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 0.20f,
                name: "Split 1",
                description: "On hit, create 1 lesser attack",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 0.3f,
                name: "Split+ 1",
                description: "On hit, create a lesser attack",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSlow: true,
                slowPercentage: 0.7f,
                slowDuration: 1.5f,
                name: "Slow Down 2",
                description: "Slow for +1.5s",
                icon: Resources.Load<Sprite>("UI_Icons/SlowDown"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isMagnet: true,
                magnetStrength: 1f,
                magnetDuration: 0.75f,
                name: "Magnetize 2",
                description: "On hit, pull target(s) in",
                icon: Resources.Load<Sprite>("UI_Icons/Magnetize"),
                rarity: Rarity.Rare
            )
        );
        ;




        //epic
        AddStat(
            new AttackStats(
                aimRangeAdditive: -4f,
                critChance: 0.15f,
                name: "MLG 3",
                description: "Decrease Aim Range, Crit Chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/MLG"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                coneAngle: -150f,
                critDmg: 0.70f,
                name: "Gamer 3",
                description: "Decrease Aim Width, Crit Dmg +70%",
                icon: Resources.Load<Sprite>("UI_Icons/Gamer"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.35f,
                name: "Damage 3",
                description: "Damage +35%",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.15f,
                name: "Crit Chance 3",
                description: "Crit Chance +15%",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.6f,
                name: "Crit Dmg 3",
                description: "Crit Dmg +60%",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.35f,
                name: "Haste 3",
                description: "Cooldown -35%",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.35f,
                name: "Knockback 3",
                description: "Knockback +35%",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.3f,
                name: "Glattt 3",
                description: "Rate of Fire +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Glattt"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.5f,
                name: "Saw'd Off 3",
                description: "Shotgun Spread +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Saw'dOff"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.3f,
                name: "Velocity 3",
                description: "Projectile Speed +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Velocity"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.35f,
               name: "Gravity 3",
               description: "Projectile Speed -35%",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Rare
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.3f,
                name: "Reach 3",
                description: "Projectile Range +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Reach"),
                rarity: Rarity.Epic
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.5f,
                name: "Quick Hands 3",
                description: "Reduce time between attacks -50%",
                icon: Resources.Load<Sprite>("UI_Icons/QuickHands"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.40f,
                name: "Extend 3",
                description: "Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Extend"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 2f,
                name: "Hacker 3",
                description: "Massively increase Aim Range",
                icon: Resources.Load<Sprite>("UI_Icons/Hacker"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                is360: true,
                name: "AFK 3",
                description: "360 Aim Vision",
                icon: Resources.Load<Sprite>("UI_Icons/AFK"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.33f,
                name: "Multicast 2",
                description: "Multicast chance +33%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.5f,
                name: "Multicast+ 2",
                description: "Multicast chance +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 3,
                name: "Extra Round 2",
                description: "+3 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtraRound"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 15,
                name: "Extended Clip 2",
                description: "+25 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 5,
                name: "Pierce 2",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Pierce"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 20,
                name: "Puncture 2",
                description: "Pierce through +20 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Puncture"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.2f,
                name: "Big Ammo 2",
                description: "Projectile Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/BigAmmo"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.18f,
                name: "Big Gadget 2",
                description: "Projectile Size +18%",
                icon: Resources.Load<Sprite>("UI_Icons/BigGadget"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 2,
                name: "One More 2",
                description: "+2 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/OneMore"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 2,
                name: "Aftershock 2",
                description: "Release 2 aftershocks after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/Aftershock"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.18f,
                name: "Big Weapon 2",
                description: "Attack Size +18%",
                icon: Resources.Load<Sprite>("UI_Icons/BigWeapon"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 0.15f,
                name: "Wave Surge 2",
                description: "Aftershock Size +15% per shock",
                icon: Resources.Load<Sprite>("UI_Icons/WaveSurge"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 0.25f,
                name: "Ki Surge 2",
                description: "Aftershock Range +25%",
                icon: Resources.Load<Sprite>("UI_Icons/KiSurge"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               activeMultiplier: 0.5f,
               name: "Persistence 2",
               description: "Attack duration +50%",
               icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
            new AttackStats(
                activeDuration: 1f,
                name: "Persistence+ 2",
                description: "Attack duration +1s",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectDuration: 1f,
                name: "Hourglass 2",
                description: "Effect duration +1s",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectMultiplier: 0.5f,
                name: "Saboteur 2",
                description: "Effect Power +50%",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                isHoming: true,
                name: "Homing 1",
                description: "Projectiles follow nearby enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Homing"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
          new AttackStats(
              isDoT: true,
              dotDamage: 1,
              dotDuration: 8,
              dotTickRate: 0.8f,
              name: "Ignite 2",
              description: "Ignite targets for 8 seconds",
              icon: Resources.Load<Sprite>("UI_Icons/Ignite"),
              rarity: Rarity.Epic
          )
      );
        ;

        AddStat(
            new AttackStats(
                isDoT: true,
                dotDamage: 1,
                dotDuration: 5,
                dotTickRate: 0.8f,
                name: "Cindershot 2",
                description: "Ignite targets for 5 seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                dotDuration: 10,
                dotTickRate: 0.75f,
                name: "Cindershot+ 2",
                description: "Longer, quicker burn. (Doesn't stack)",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               isChain: true,
               chainTimes: 2,
               chainStatDecayPercent: 0.33f,
               chainRange: 3.5f,
               chainSpeed: 14f,
               name: "Chain 2",
               description: "Damage jumps to 2 more targets",
               icon: Resources.Load<Sprite>("UI_Icons/Chain"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 3,
                chainStatDecayPercent: 0.2f,
                chainRange: 4f,
                chainSpeed: 7f,
                name: "Chain+ 2",
                description: "Damage jumps to 3 more targets",
                icon: Resources.Load<Sprite>("UI_Icons/Chain"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 0.33f,
                name: "Split 2",
                description: "On hit, create a lesser attack",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 0.5f,
                name: "Split+ 2",
                description: "On hit, create a lesser attack",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               isSlow: true,
               slowPercentage: 0.55f,
               slowDuration: 2f,
               name: "Slow Down 3",
               description: "Slow for +2s",
               icon: Resources.Load<Sprite>("UI_Icons/SlowDown"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
            new AttackStats(
                isMagnet: true,
                magnetStrength: 2f,
                magnetDuration: 1f,
                name: "Magnetize 3",
                description: "On hit, pull target(s) in",
                icon: Resources.Load<Sprite>("UI_Icons/Magnetize"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               isStun: true,
               stunDuration: 0.5f,
               name: "Concussive 1",
               description: "On hit, stun for 0.5s",
               icon: Resources.Load<Sprite>("UI_Icons/Concussive"),
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
                icon: Resources.Load<Sprite>("UI_Icons/DoubleTrouble"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.5f,
                name: "Damage 4",
                description: "Damage +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.25f,
                name: "Crit Chance 4",
                description: "Crit Chance +25%",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 1f,
                name: "Crit Dmg 4",
                description: "Crit Dmg +100%",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.5f,
                name: "Haste 4",
                description: "Cooldown -50%",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.50f,
                name: "Knockback 4",
                description: "Knockback +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.9f,
                name: "Multicast 3",
                description: "Multicast chance +75%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 1f,
                name: "Multicast+ 3",
                description: "Multicast chance +100%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 6,
                name: "Extra Round 3",
                description: "+6 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtraRound"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 35,
                name: "Extended Clip 3",
                description: "+35 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 15,
                name: "Pierce 3",
                description: "Pierce through +15 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Pierce"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 40,
                name: "Puncture 3",
                description: "Pierce through +40 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Puncture"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.25f,
                name: "Big Ammo 3",
                description: "Projectile Size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/BigAmmo"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.22f,
                name: "Big Gadget 3",
                description: "Projectile Size +22%",
                icon: Resources.Load<Sprite>("UI_Icons/BigGadget"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 3,
                name: "Aftershock 3",
                description: "Release 3 aftershocks after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/Aftershock"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.20f,
                name: "Big Weapon 3",
                description: "Attack Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/BigWeapon"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
           new AttackStats(
               activeMultiplier: 1f,
               name: "Persistence 3",
               description: "Attack duration +100%",
               icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
            new AttackStats(
                activeDuration: 2f,
                name: "Persistence+ 3",
                description: "Attack duration +2s",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectDuration: 1.5f,
                name: "Hourglass 3",
                description: "Effect duration +1.5s",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectMultiplier: 0.8f,
                name: "Saboteur 3",
                description: "Effect Power +80%",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                isHoming: true,
                name: "Homing 2",
                description: "Projectiles track nearby enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Homing"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
          new AttackStats(
              isDoT: true,
              dotDamage: 1,
              dotDuration: 10,
              dotTickRate: 0.75f,
              name: "Ignite 3",
              description: "Heavily ignite targets for 10 seconds",
              icon: Resources.Load<Sprite>("UI_Icons/Ignite"),
              rarity: Rarity.Legendary
          )
      );
        ;

        AddStat(
            new AttackStats(
                isDoT: true,
                dotDamage: 1,
                dotDuration: 6,
                dotTickRate: 0.7f,
                name: "Cindershot 3",
                description: "Heavily ignite targets for 6 seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                dotDamage: 4,
                name: "Cindershot+ 3",
                description: "Burns like hell.",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
           new AttackStats(
               isChain: true,
               chainTimes: 3,
               chainStatDecayPercent: 0.2f,
               chainRange: 4f,
               chainSpeed: 18f,
               name: "Chain 3",
               description: "Damage jumps to 3 more targets",
               icon: Resources.Load<Sprite>("UI_Icons/Chain"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 5,
                chainStatDecayPercent: 0.10f,
                chainRange: 5f,
                chainSpeed: 10f,
                name: "Chain+ 3",
                description: "Damage jumps to 5 more targets",
                icon: Resources.Load<Sprite>("UI_Icons/Chain"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 0.50f,
                name: "Split 3",
                description: "On hit, create another attack",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 0.60f,
                name: "Split+ 3",
                description: "On hit, create another attack",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
           new AttackStats(
               isStun: true,
               stunDuration: 1f,
               name: "Concussive 2",
               description: "On hit, stun for 1s",
               icon: Resources.Load<Sprite>("UI_Icons/Concussive"),
               rarity: Rarity.Legendary
           )
       );
        ;


        //Weapon Sets
        //common

        AddStat(
            new AttackStats(
                damageMultiplier: 0.15f,
                name: "Mastery 1",
                description: "Damage +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.02f,
                name: "Marksman 1",
                description: "Crit Chance +2%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
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
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.10f,
                name: "Quickswap 1",
                description: "Cooldowns -10%",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.10f,
                name: "Impact 1",
                description: "Knockback +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.08f,
                name: "RoF 1",
                description: "Rate of Fire +8%",
                icon: Resources.Load<Sprite>("UI_Icons/RoF"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.10f,
                name: "Propulsion 1",
                description: "Projectile Speed +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Propulsion"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.10f,
               name: "Gravitation 1",
               description: "Projectile Speed -10%",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Common,
               weaponSet: true
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.10f,
                name: "Scope 1",
                description: "Range +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Scope"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.15f,
                name: "Wide Barrel 1",
                description: "Shotgun Spread +15%",
                icon: Resources.Load<Sprite>("UI_Icons/WideBarrel"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.12f,
                name: "Dexterity 1",
                description: "Reduce time between attacks -12%",
                icon: Resources.Load<Sprite>("UI_Icons/Dexterity"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.10f,
                name: "Lunge 1",
                description: "Range +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Lunge"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: -0.25f,
                name: "Implode 1",
                description: "Range -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Implode"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        //rare

        AddStat(
            new AttackStats(
                damageMultiplier: 0.2f,
                name: "Mastery 2",
                description: "Damage +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.05f,
                name: "Marksman 2",
                description: "Crit Chance +4%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
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
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.20f,
                name: "Quickswap 2",
                description: "Cooldowns -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.20f,
                name: "Impact 2",
                description: "Knockback +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.15f,
                name: "RoF 2",
                description: "Rate of Fire +15%",
                icon: Resources.Load<Sprite>("UI_Icons/RoF"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.3f,
                name: "Wide Barrel 2",
                description: "Shotgun Spread +30%",
                icon: Resources.Load<Sprite>("UI_Icons/WideBarrel"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.2f,
                name: "Propulsion 2",
                description: "Projectile Speed +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Propulsion"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.20f,
               name: "Gravitation 2",
               description: "Projectile Speed -20%",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Rare,
               weaponSet: true
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.2f,
                name: "Scope 2",
                description: "Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Scope"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.20f,
                name: "Dexterity 2",
                description: "Reduce time between attacks -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Dexterity"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.20f,
                name: "Lunge 2",
                description: "Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Lunge"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: -0.4f,
                name: "Implode 2",
                description: "Range -40%",
                icon: Resources.Load<Sprite>("UI_Icons/Implode"),
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
                icon: Resources.Load<Sprite>("UI_Icons/Vision"),
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
                icon: Resources.Load<Sprite>("UI_Icons/Awareness"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.10f,
                name: "Multi-cast 1",
                description: "Multicast chance +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
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
                icon: Resources.Load<Sprite>("UI_Icons/BonusRound"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 6,
                name: "Extended Mag 1",
                description: "+6 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedMag"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 2,
                name: "Piercing Ammo 1",
                description: "Pierce through +2 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PiercingAmmo"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 5,
                name: "Penetrating Ammo 1",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PenetratingAmmo"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.10f,
                name: "High Caliber 1",
                description: "Projectile Size +10%",
                icon: Resources.Load<Sprite>("UI_Icons/HighCaliber"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.08f,
                name: "Size Up 1",
                description: "Projectile Size +8%",
                icon: Resources.Load<Sprite>("UI_Icons/SizeUp"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.20f,
                name: "Enlarge 1",
                description: "Attack Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Enlarge"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 0.06f,
                name: "Wave Master 1",
                description: "Aftershock Size +6% per shock",
                icon: Resources.Load<Sprite>("UI_Icons/WaveMaster"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 0.10f,
                name: "Ki Master 1",
                description: "Aftershock Range +10%",
                icon: Resources.Load<Sprite>("UI_Icons/KiMaster"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        //epic
        AddStat(
            new AttackStats(
                damageMultiplier: 0.3f,
                name: "Mastery 3",
                description: "Damage +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
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
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
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
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.30f,
                name: "Quickswap 3",
                description: "Cooldowns -30%",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.3f,
                name: "Impact 3",
                description: "Knockback +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.25f,
                name: "RoF 3",
                description: "Rate of Fire +25%",
                icon: Resources.Load<Sprite>("UI_Icons/RoF"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.4f,
                name: "Wide Barrel 3",
                description: "Shotgun Spread +40%",
                icon: Resources.Load<Sprite>("UI_Icons/WideBarrel"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.35f,
                name: "Propulsion 3",
                description: "Projectile Speed +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Propulsion"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.4f,
               name: "Gravitation 3",
               description: "Projectile Speed -30%",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Epic,
               weaponSet: true
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.35f,
                name: "Scope 3",
                description: "Range +35%",
                icon: Resources.Load<Sprite>("UI_Icons/Scope"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.30f,
                name: "Dexterity 3",
                description: "Reduce time between attacks -30%",
                icon: Resources.Load<Sprite>("UI_Icons/Dexterity"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.3f,
                name: "Lunge 3",
                description: "Range +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Lunge"),
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
                icon: Resources.Load<Sprite>("UI_Icons/Vision"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 40f,
                name: "Awareness 2",
                description: "Increases Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/Awareness"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.25f,
                name: "Multi-cast 2",
                description: "Multicast chance +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 2,
                name: "Bonus Round 2",
                description: "+2 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/BonusRound"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 15,
                name: "Extended Mag 2",
                description: "+15 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedMag"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 5,
                name: "Piercing Ammo 2",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PiercingAmmo"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 12,
                name: "Penetrating Ammo 2",
                description: "Pierce through +12 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PenetratingAmmo"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.18f,
                name: "High Caliber 2",
                description: "Projectile Size +18%",
                icon: Resources.Load<Sprite>("UI_Icons/HighCaliber"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.15f,
                name: "Size Up 2",
                description: "Projectile Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/SizeUp"),
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
                icon: Resources.Load<Sprite>("UI_Icons/OnceMore"),
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
                icon: Resources.Load<Sprite>("UI_Icons/After-shock"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.40f,
                name: "Enlarge 2",
                description: "Attack Size +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Enlarge"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 0.12f,
                name: "Wave Master 2",
                description: "Aftershock Size +12% per shock",
                icon: Resources.Load<Sprite>("UI_Icons/WaveMaster"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 0.20f,
                name: "Ki Master 2",
                description: "Aftershock Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/KiMaster"),
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
                icon: Resources.Load<Sprite>("UI_Icons/BuddySystem"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.40f,
                name: "Mastery 4",
                description: "Damage +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.2f,
                name: "Marksman 4",
                description: "Crit Chance +20%",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.7f,
                name: "Brutality 4",
                description: "Crit Dmg +70%",
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.4f,
                name: "Quickswap 4",
                description: "Cooldowns -40%",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.5f,
                name: "Impact 4",
                description: "Knockback +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.6f,
                name: "Multi-cast 3",
                description: "Multicast chance +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
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
                icon: Resources.Load<Sprite>("UI_Icons/BonusRound"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 30,
                name: "Extended Mag 3",
                description: "+30 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedMag"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 12,
                name: "Piercing Ammo 3",
                description: "Pierce through +12 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PiercingAmmo"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 40,
                name: "Penetrating Ammo 3",
                description: "Pierce through +40 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PenetratingAmmo"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.3f,
                name: "High Caliber 3",
                description: "Projectile Size +30%",
                icon: Resources.Load<Sprite>("UI_Icons/HighCaliber"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.25f,
                name: "Size Up 3",
                description: "Projectile Size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/SizeUp"),
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
                icon: Resources.Load<Sprite>("UI_Icons/After-shock"),
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
                icon: Resources.Load<Sprite>("UI_Icons/OnceMore"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.60f,
                name: "Enlarge 3",
                description: "Attack Size +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Enlarge"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Automatic

        AddStat(
            new AttackStats(
                shotsPerAttack: 5,
                spreadMultiplier: -0.08f,
                speedMultiplier: 0.1f,
                name: "Auto Novice",
                description: "All Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Automatic"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 8,
                spreadMultiplier: -0.15f,
                rangeMultiplier: 0.1f,
                name: "Auto Pro",
                description: "All Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Automatic"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 5,
                shotsPerAttack: 15,
                spreadMultiplier: -0.15f,
                rangeMultiplier: 0.1f,
                name: "Auto God",
                description: "All Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Automatic"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Semi-Auto

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.10f,
                projectileSizeMultiplier: 0.1f,
                name: "Semi-Auto Novice",
                description: "All Semi-Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Semi-Auto"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                spreadMultiplier: -0.15f,
                projectileSizeMultiplier: 0.10f,
                name: "Semi-Auto Pro",
                description: "All Semi-Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Semi-Auto"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 2,
                pierce: 10,
                castTimeMultiplier: -0.1f,
                projectileSizeMultiplier: 0.2f,
                name: "Semi-Auto God",
                description: "All Semi-Automatic Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Semi-Auto"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Shotgun

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.2f,
                speedMultiplier: 0.1f,
                damageMultiplier: 0.1f,
                name: "Shotgun Novice",
                description: "All Shotgun Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Shotgun"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.3f,
                rangeMultiplier: 0.15f,
                shotgunSpreadMultiplier: 0.2f,
                name: "Shotgun Pro",
                description: "All Shotgun Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Shotgun"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 2,
                shotgunSpreadMultiplier: 0.3f,
                multicastWaitTimeMultiplier: -0.25f,
                multicastChance: 0.5f,
                name: "Shotgun God",
                description: "All Shotgun Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Shotgun"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Explosive

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.15f,
                projectileSizeMultiplier: 0.15f,
                damageMultiplier: 0.1f,
                name: "Explosive Novice",
                description: "All Explosive Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Explosive"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                shotgunSpreadMultiplier: 0.15f,
                projectileSizeMultiplier: 0.15f,
                rangeMultiplier: 0.15f,
                pierce: 5,
                name: "Explosive Pro",
                description: "All Explosive Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Explosive"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 1,
                spreadMultiplier: -0.25f,
                shotgunSpreadMultiplier: 0.25f,
                projectileSizeMultiplier: 0.2f,
                damageMultiplier: 0.25f,
                name: "Explosive God",
                description: "All Explosive Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Explosive"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Nova

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.15f,
                meleeSizeMultiplier: 0.15f,
                name: "AFK Novice",
                description: "All Nova Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Nova"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                comboWaitTimeMultiplier: -0.2f,
                knockbackMultiplier: 0.10f,
                name: "AFK Pro",
                description: "All Nova Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Nova"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                meleeSpacer: 1.5f,
                meleeSpacerGap: 1f,
                meleeSizeMultiplier: 0.25f,
                name: "AFK God",
                description: "All Nova Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Nova"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Melee

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.12f,
                meleeSizeMultiplier: 0.12f,
                comboWaitTimeMultiplier: -0.1f,
                name: "Melee Novice",
                description: "All Melee Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Melee"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                comboWaitTimeMultiplier: -0.15f,
                name: "Melee Pro",
                description: "All Melee Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Melee"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                knockbackMultiplier: 0.2f,
                meleeSizeMultiplier: 0.2f,
                meleeShotsScaleUpMultiplier: 0.1f,
                name: "Melee God",
                description: "All Melee Weapons Enhanced",
                icon: Resources.Load<Sprite>("UI_Icons/Melee"),
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
