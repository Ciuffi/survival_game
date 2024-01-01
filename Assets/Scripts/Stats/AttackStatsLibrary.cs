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
                unlockLevel: 18,
                description: "Decrease Aim Range, increase Crit Chance",
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
                unlockLevel: 18,
                description: "Decrease Aim Width, increase Crit Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Gamer"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.10f,
                name: "Damage 1",
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.02f,
                name: "Critical 1",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.2f,
                name: "Overkill 1",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.05f,
                name: "Haste 1",
                description: "Decrease Cooldown",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.10f,
                name: "Knockback 1",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.10f,
                name: "Rapid Fire 1",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/Glattt"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.12f,
                name: "Saw'd Off 1",
                description: "Increase Shotgun Spread",
                icon: Resources.Load<Sprite>("UI_Icons/Saw'dOff"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: -0.3f,
                name: "Steady 1",
                description: "Decrease Projectile Spray",
                icon: Resources.Load<Sprite>("UI_Icons/Steady"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 0.3f,
                name: "Overheat 1",
                description: "Increase Projectile Spray",
                icon: Resources.Load<Sprite>("UI_Icons/Overheat"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.10f,
                name: "Velocity 1",
                unlockLevel: 8,
                description: "Increase Projectile Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Velocity"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.12f,
               name: "Gravity 1",
                unlockLevel: 8,
               description: "Decrease Projectile Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Common
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.10f,
                name: "Reach 1",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Reach"),
                rarity: Rarity.Common
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.10f,
                spreadMultiplier: -0.05f,
                name: "Quick Hands 1",
                description: "Increase Attack speed",
                icon: Resources.Load<Sprite>("UI_Icons/QuickHands"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacer: 0.3f,
                meleeSpacerGap: 0.3f,
                name: "Extend 1",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Extend"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacer: -0.3f,
                meleeSpacerGap: -0.3f,
                name: "Kamakazi 1",
                description: "Decrease Range",
                icon: Resources.Load<Sprite>("UI_Icons/Kamakazi"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
           new AttackStats(
               aimRangeAdditive: 0.6f,
               name: "Hacker 1",
               description: "Increase Aim Range",
               icon: Resources.Load<Sprite>("UI_Icons/Hacker"),
               rarity: Rarity.Common
           )
       );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 35f,
                name: "AFK 1",
                description: "Increase Aim Width",
                icon: Resources.Load<Sprite>("UI_Icons/AFK"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSlow: true,
                slowPercentage: 0.9f,
                slowDuration: 1f,
                name: "Slow Down 1",
                unlockLevel: 2,
                description: "Attacks slow enemies",
                icon: Resources.Load<Sprite>("UI_Icons/SlowDown"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
            new AttackStats(
                isMagnet: true,
                magnetStrength: 0.25f,
                magnetDuration: 0.25f,
                name: "Magnetize 1",
                unlockLevel: 10,
                description: "Attacks are magnetic",
                icon: Resources.Load<Sprite>("UI_Icons/Magnetize"),
                rarity: Rarity.Common
            )
        );
        ;

        AddStat(
           new AttackStats(
               comboAttackBuffMultiplier: 0.05f,
               name: "C-combo 1",
               description: "Consecutive attacks deal more damage",
               icon: Resources.Load<Sprite>("UI_Icons/Charged"),
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
                unlockLevel: 18,
                description: "Decrease Aim Range, increase Crit Chance",
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
                unlockLevel: 18,
                description: "Decrease Aim Width, increase Crit Dmg",
                icon: Resources.Load<Sprite>("UI_Icons/Gamer"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.15f,
                name: "Damage 2",
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.05f,
                name: "Critical 2",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.40f,
                name: "Overkill 2",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.10f,
                name: "Haste 2",
                description: "Decrease Cooldown",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.20f,
                name: "Knockback 2",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.15f,
                name: "Rapid Fire 2",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/Glattt"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.25f,
                name: "Saw'd Off 2",
                description: "Increase Shotgun Spread",
                icon: Resources.Load<Sprite>("UI_Icons/Saw'dOff"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: -0.6f,
                name: "Steady 2",
                description: "Decrease Projectile Spray",
                icon: Resources.Load<Sprite>("UI_Icons/Steady"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                sprayMultiplier: 0.6f,
                name: "Overheat 2",
                description: "Increase Projectile Spray",
                icon: Resources.Load<Sprite>("UI_Icons/Overheat"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.20f,
                name: "Velocity 2",
                unlockLevel: 8,
                description: "Increase Projectile Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Velocity"),
                rarity: Rarity.Rare
            )
        );

        ;
        AddStat(
           new AttackStats(
               speedMultiplier: -0.22f,
               name: "Gravity 2",
                unlockLevel: 8,
               description: "Decrease Projectile Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Rare
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.15f,
                name: "Reach 2",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Reach"),
                rarity: Rarity.Rare
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.20f,
                spreadMultiplier: -0.10f,
                name: "Quick Hands 2",
                description: "Increase Attack speed",
                icon: Resources.Load<Sprite>("UI_Icons/QuickHands"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacer: 0.6f,
                meleeSpacerGap: 0.6f,
                name: "Extend 2",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Extend"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacer: -0.6f,
                meleeSpacerGap: -0.6f,
                name: "Kamakazi 2",
                description: "Decrease Range",
                icon: Resources.Load<Sprite>("UI_Icons/Kamakazi"),
                rarity: Rarity.Rare
            )
        );
        ;



        AddStat(
            new AttackStats(
                multicastChance: 0.15f,
                name: "Multicast 1",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.30f,
                name: "Multicast+ 1",
                description: "Increase Multicast chance",
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
                shotsPerAttack: 5,
                name: "Extended Clip 1",
                description: "+5 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 10,
                name: "Extended Clip+ 1",
                description: "+10 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 1,
                name: "Pierce 1",
                description: "Pierce through +1 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Pierce"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 8,
                name: "Puncture 1",
                description: "Pierce through +8 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Puncture"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.12f,
                name: "Big Ammo 1",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigAmmo"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.10f,
                name: "Big Gadget 1",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigGadget"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                name: "One More 1",
                description: "+1 Attack",
                icon: Resources.Load<Sprite>("UI_Icons/OneMore"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                name: "Aftershock 1",
                unlockLevel: 6,
                description: "Attacks release an Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/Aftershock"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.10f,
                name: "Big Weapon 1",
                description: "Increase Attack Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigWeapon"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUp: 0.08f,
                name: "Wave Surge 1",
                unlockLevel: 6,
                description: "Increase Aftershock size",
                icon: Resources.Load<Sprite>("UI_Icons/WaveSurge"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                activeMultiplier: 0.3f,
                name: "Persistence 1",
                unlockLevel: 9,
                description: "Increase Attack duration",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                activeDuration: 0.5f,
                name: "Persistence+ 1",
                description: "Increase Attack duration",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectDuration: 0.5f,
                name: "Hourglass 1",
                unlockLevel: 12,
                description: "Increase Effect duration",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectMultiplier: 0.25f,
                name: "Saboteur 1",
                unlockLevel: 12,
                description: "Increase Effect Power",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
           new AttackStats(
               aimRangeAdditive: 1f,
               name: "Hacker 2",
               description: "Increase Aim Range",
               icon: Resources.Load<Sprite>("UI_Icons/Hacker"),
               rarity: Rarity.Rare
           )
       );
        ;
        AddStat(
            new AttackStats(
                coneAngle: 60f,
                name: "AFK 2",
                description: "Increase Aim Width",
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
               dotTickRate: 2,
               name: "Ignite 1",
               unlockLevel: 4,
               description: "Attacks set enemies on Fire",
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
                dotTickRate: 2,
                name: "Cindershot 1",
                unlockLevel: 4,
                description: "Attacks set enemies on Fire",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 1,
                chainStatDecayPercent: 0.6f,
                chainRange: 2.5f,
                chainSpeed: 10f,
                name: "Chain 1",
                unlockLevel: 3,
                description: "Attacks jump to 1 more target",
                icon: Resources.Load<Sprite>("UI_Icons/Chain"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 2,
                chainStatDecayPercent: 0.2f,
                chainRange: 5f,
                chainSpeed: 5f,
                name: "Chain+ 1",
                unlockLevel: 3,
                description: "Attacks jump to 2 more targets",
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
                unlockLevel: 7,
                description: "Attacks create a lesser clone on hit",
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
                unlockLevel: 7,
                description: "Attacks create a lesser clone on hit",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isSlow: true,
                slowPercentage: 0.8f,
                slowDuration: 1.5f,
                name: "Slow Down 2",
                unlockLevel: 2,
                description: "Attacks slow enemies",
                icon: Resources.Load<Sprite>("UI_Icons/SlowDown"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
            new AttackStats(
                isMagnet: true,
                magnetStrength: 0.5f,
                magnetDuration: 0.5f,
                name: "Magnetize 2",
                unlockLevel: 10,
                description: "Attacks are magnetic",
                icon: Resources.Load<Sprite>("UI_Icons/Magnetize"),
                rarity: Rarity.Rare
            )
        );
        ;

        AddStat(
           new AttackStats(
               comboAttackBuffMultiplier: 0.08f,
               name: "C-combo 2",
               description: "Consecutive attacks deal more damage",
               icon: Resources.Load<Sprite>("UI_Icons/Charged"),
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
                unlockLevel: 18,
                description: "Decrease Aim Range, increase Crit Chance",
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
                unlockLevel: 18,
                description: "Decrease Aim Width, increase Crit Dmg",
                icon: Resources.Load<Sprite>("UI_Icons/Gamer"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.20f,
                name: "Damage 3",
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.10f,
                name: "Critical 3",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.6f,
                name: "Overkill 3",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.15f,
                name: "Haste 3",
                description: "Decrease Cooldown",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.30f,
                name: "Knockback 3",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.25f,
                name: "Rapid Fire 3",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/Glattt"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.4f,
                name: "Saw'd Off 3",
                description: "Increase Shotgun Spread",
                icon: Resources.Load<Sprite>("UI_Icons/Saw'dOff"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.3f,
                name: "Velocity 3",
                unlockLevel: 8,
                description: "Increase Projectile Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Velocity"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.35f,
               name: "Gravity 3",
                unlockLevel: 8,
               description: "Decrease Projectile Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Rare
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.3f,
                name: "Reach 3",
                description: "Increase Projectile Range",
                icon: Resources.Load<Sprite>("UI_Icons/Reach"),
                rarity: Rarity.Epic
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.35f,
                spreadMultiplier: -0.15f,
                name: "Quick Hands 3",
                description: "Increase Attack speed",
                icon: Resources.Load<Sprite>("UI_Icons/QuickHands"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacer: 1f,
                meleeSpacerGap: 1f,
                name: "Extend 3",
                description: "Increase Range",
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
                multicastChance: 0.30f,
                name: "Multicast 2",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.5f,
                name: "Multicast+ 2",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 2,
                name: "Extra Round 2",
                description: "+3 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtraRound"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 10,
                name: "Extended Clip 2",
                description: "+10 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 20,
                name: "Extended Clip+ 2",
                description: "+20 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 3,
                name: "Pierce 2",
                description: "Pierce through +3 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Pierce"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 15,
                name: "Puncture 2",
                description: "Pierce through +15 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Puncture"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.2f,
                name: "Big Ammo 2",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigAmmo"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.18f,
                name: "Big Gadget 2",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigGadget"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboLength: 2,
                name: "One More 2",
                description: "+2 Attacks",
                icon: Resources.Load<Sprite>("UI_Icons/OneMore"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                name: "Aftershock 2",
                unlockLevel: 6,
                description: "Attacks release an Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/Aftershock"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.18f,
                name: "Big Weapon 2",
                description: "Increase Attack Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigWeapon"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUp: 0.12f,
                name: "Wave Surge 2",
                unlockLevel: 6,
                description: "Increase Aftershock size",
                icon: Resources.Load<Sprite>("UI_Icons/WaveSurge"),
                rarity: Rarity.Epic
            )
        );
        ;


        AddStat(
           new AttackStats(
               activeMultiplier: 0.5f,
               name: "Persistence 2",
                unlockLevel: 9,
               description: "Increase Attack duration",
               icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
            new AttackStats(
                activeDuration: 1f,
                name: "Persistence+ 2",
                description: "Increase Attack duration",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectDuration: 1f,
                name: "Hourglass 2",
                unlockLevel: 12,
                description: "Increase Effect duration",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectMultiplier: 0.5f,
                name: "Saboteur 2",
                unlockLevel: 12,
                description: "Increase Effect Power",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
          new AttackStats(
              isDoT: true,
              dotDamage: 2,
              dotDuration: 8,
              dotTickRate: 0.8f,
              name: "Ignite 2",
              unlockLevel: 4,
              description: "Attacks set enemies on Fire",
              icon: Resources.Load<Sprite>("UI_Icons/Ignite"),
              rarity: Rarity.Epic
          )
      );
        ;

        AddStat(
            new AttackStats(
                isDoT: true,
                dotDamage: 2,
                dotDuration: 5,
                dotTickRate: 0.8f,
                name: "Cindershot 2",
                unlockLevel: 4,
                description: "Attacks set enemies on Fire",
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
                unlockLevel: 0,
                description: "Longer, quicker burn. (Doesn't stack)",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               isChain: true,
               chainTimes: 1,
               chainStatDecayPercent: 0.33f,
               chainRange: 3f,
               chainSpeed: 14f,
               name: "Chain 2",
               unlockLevel: 3,
               description: "Attacks jump to 1 more target",
               icon: Resources.Load<Sprite>("UI_Icons/Chain"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 4,
                chainStatDecayPercent: 0.12f,
                chainRange: 6f,
                chainSpeed: 7f,
                name: "Chain+ 2",
                unlockLevel: 3,
                description: "Attacks jump to 4 more targets",
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
                unlockLevel: 7,
                description: "Attacks create a lesser clone on hit",
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
                unlockLevel: 7,
                description: "Attacks create a lesser clone on hit",
                icon: Resources.Load<Sprite>("UI_Icons/Split"),
                rarity: Rarity.Epic
            )
        );
        ;

        AddStat(
           new AttackStats(
               isSlow: true,
               slowPercentage: 0.65f,
               slowDuration: 2f,
               name: "Slow Down 3",
                unlockLevel: 2,
               description: "Attacks slow enemies",
               icon: Resources.Load<Sprite>("UI_Icons/SlowDown"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
            new AttackStats(
                isMagnet: true,
                magnetStrength: 1f,
                magnetDuration: 0.5f,
                name: "Magnetize 3",
                unlockLevel: 10,
                description: "Attacks are magnetic",
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
                unlockLevel: 11,
               description: "Attacks stun enemies",
               icon: Resources.Load<Sprite>("UI_Icons/Concussive"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
           new AttackStats(
               isLifesteal: true,
               lifestealAmount: 0.25f,
               lifestealChance: 0.02f,
               name: "Infusion 1",
                unlockLevel: 13,
               description: "Chance to heal on hit",
               icon: Resources.Load<Sprite>("UI_Icons/Infusion"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
           new AttackStats(
               isLifesteal: true,
               lifestealAmount: 0.25f,
               lifestealChance: 0.04f,
               name: "Infusion+ 1",
                unlockLevel: 13,
               description: "Chance to heal on hit",
               icon: Resources.Load<Sprite>("UI_Icons/Infusion"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
           new AttackStats(
               isLifesteal: true,
               lifestealAmount: 0.25f,
               lifestealChance: 0.05f,
               name: "Infusion++ 1",
                unlockLevel: 13,
               description: "Chance to heal on hit",
               icon: Resources.Load<Sprite>("UI_Icons/Infusion"),
               rarity: Rarity.Epic
           )
       );
        ;

        AddStat(
           new AttackStats(
               comboAttackBuffMultiplier: 0.12f,
               name: "C-combo 3",
               description: "Consecutive attacks deal more damage",
               icon: Resources.Load<Sprite>("UI_Icons/Charged"),
               rarity: Rarity.Epic
           )
       );
        ;


        //legendary

        AddStat(
            new AttackStats(
                shootOppositeSide: true,
                damageMultiplier: -0.50f,
                name: "Double Trouble",
                description: "Attack again behind you. -50% Damage",
                icon: Resources.Load<Sprite>("UI_Icons/DoubleTrouble"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.3f,
                name: "Damage 4",
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Damage"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.15f,
                name: "Critical 4",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.75f,
                name: "Overkill 4",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.20f,
                name: "Haste 4",
                description: "Decrease Cooldown",
                icon: Resources.Load<Sprite>("UI_Icons/Haste"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.50f,
                name: "Knockback 4",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Knockback"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.7f,
                name: "Multicast 3",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 1f,
                name: "Multicast+ 3",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 5,
                name: "Extra Round 3",
                description: "+5 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtraRound"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 15,
                name: "Extended Clip 3",
                description: "+15 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 30,
                name: "Extended Clip+ 3",
                description: "+30 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedClip"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 5,
                name: "Pierce 3",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Pierce"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 25,
                name: "Puncture 3",
                description: "Pierce through +25 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Puncture"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.25f,
                name: "Big Ammo 3",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigAmmo"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.22f,
                name: "Big Gadget 3",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigGadget"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 2,
                name: "Aftershock 3",
                unlockLevel: 6,
                description: "Attacks release 2 Aftershocks",
                icon: Resources.Load<Sprite>("UI_Icons/Aftershock"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.20f,
                name: "Big Weapon 3",
                description: "Increase Attack Size",
                icon: Resources.Load<Sprite>("UI_Icons/BigWeapon"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
           new AttackStats(
               activeMultiplier: 0.75f,
               name: "Persistence 3",
                unlockLevel: 9,
               description: "Increase Attack duration",
               icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
            new AttackStats(
                activeDuration: 2f,
                name: "Persistence+ 3",
                description: "Increase Attack duration",
                icon: Resources.Load<Sprite>("UI_Icons/ActiveTime"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectDuration: 1.5f,
                name: "Hourglass 3",
                unlockLevel: 12,
                description: "Increase Effect duration",
                icon: Resources.Load<Sprite>("UI_Icons/EffectTime"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                effectMultiplier: 0.75f,
                name: "Saboteur 3",
                unlockLevel: 12,
                description: "Increase Effect Power",
                icon: Resources.Load<Sprite>("UI_Icons/EffectPower"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                isHoming: true,
                damage: -1,
                name: "Homing 1",
                unlockLevel: 5,
                description: "Projectiles follow enemies, -1 Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Homing"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
          new AttackStats(
              isDoT: true,
              dotDamage: 2,
              dotDuration: 10,
              dotTickRate: 0.75f,
              name: "Ignite 3",
                unlockLevel: 4,
              description: "Attacks set enemies on Fire",
              icon: Resources.Load<Sprite>("UI_Icons/Ignite"),
              rarity: Rarity.Legendary
          )
      );
        ;

        AddStat(
            new AttackStats(
                isDoT: true,
                dotDamage: 2,
                dotDuration: 6,
                dotTickRate: 0.7f,
                name: "Cindershot 3",
                unlockLevel: 4,
                description: "Attacks set enemies on Fire",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
            new AttackStats(
                dotDamage: 5,
                name: "Cindershot+ 3",
                unlockLevel: 0,
                description: "Burns like hell.",
                icon: Resources.Load<Sprite>("UI_Icons/Cindershot"),
                rarity: Rarity.Legendary
            )
        );
        ;

        AddStat(
           new AttackStats(
               isChain: true,
               chainTimes: 2,
               chainStatDecayPercent: 0.25f,
               chainRange: 4f,
               chainSpeed: 18f,
               name: "Chain 3",
                unlockLevel: 3,
               description: "Attacks jump to 2 more targets",
               icon: Resources.Load<Sprite>("UI_Icons/Chain"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
            new AttackStats(
                isChain: true,
                chainTimes: 5,
                chainStatDecayPercent: 0.06f,
                chainRange: 8f,
                chainSpeed: 10f,
                name: "Chain+ 3",
                unlockLevel: 3,
                description: "Attacks jump to 5 more targets",
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
                unlockLevel: 7,
                description: "Attacks create a lesser clone on hit",
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
                unlockLevel: 7,
                description: "Attacks create a lesser clone on hit",
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
                unlockLevel: 11,
               description: "Attacks stun enemies",
               icon: Resources.Load<Sprite>("UI_Icons/Concussive"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
           new AttackStats(
               isLifesteal: true,
               lifestealAmount: 0.3f,
               lifestealChance: 0.03f,
               name: "Infusion 2",
                unlockLevel: 13,
               description: "Chance to heal on hit",
               icon: Resources.Load<Sprite>("UI_Icons/Infusion"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
           new AttackStats(
               isLifesteal: true,
               lifestealAmount: 0.3f,
               lifestealChance: 0.05f,
               name: "Infusion+ 2",
                unlockLevel: 13,
               description: "Chance to heal on hit",
               icon: Resources.Load<Sprite>("UI_Icons/Infusion"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
           new AttackStats(
               isLifesteal: true,
               lifestealAmount: 0.4f,
               lifestealChance: 0.08f,
               name: "Infusion++ 2",
                unlockLevel: 13,
               description: "Chance to heal on hit",
               icon: Resources.Load<Sprite>("UI_Icons/Infusion"),
               rarity: Rarity.Legendary
           )
       );
        ;

        AddStat(
           new AttackStats(
               comboAttackBuffMultiplier: 0.20f,
               name: "C-combo 4",
               description: "Consecutive attacks deal more damage",
               icon: Resources.Load<Sprite>("UI_Icons/Charged"),
               rarity: Rarity.Legendary
           )
       );
        ;


        //Weapon Sets
        //common

        AddStat(
            new AttackStats(
                damageMultiplier: 0.12f,
                name: "Mastery 1",
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.04f,
                name: "Marksman 1",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.25f,
                name: "Brutality 1",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.05f,
                name: "Quickswap 1",
                description: "Decrease Cooldowns",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.15f,
                name: "Impact 1",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.12f,
                name: "RoF 1",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/RoF"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.15f,
                name: "Propulsion 1",
                description: "Increase Projectile Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Propulsion"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.15f,
               name: "Gravitation 1",
               description: "Decrease Projectile Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Gravity"),
               rarity: Rarity.Common,
               weaponSet: true
           )
       );
        ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.12f,
                name: "Scope 1",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Scope"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.20f,
                name: "Wide Barrel 1",
                description: "Increase Shotgun Spread",
                icon: Resources.Load<Sprite>("UI_Icons/WideBarrel"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.15f,
                spreadMultiplier: -0.10f,
                name: "Dexterity 1",
                description: "Increase Attack speed",
                icon: Resources.Load<Sprite>("UI_Icons/Dexterity"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.15f,
                meleeSpacerGapMultiplier: 0.08f,
                name: "Lunge 1",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Lunge"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: -0.25f,
                meleeSpacerGapMultiplier: -0.15f,
                name: "Implode 1",
                description: "Decrease Range",
                icon: Resources.Load<Sprite>("UI_Icons/Implode"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboAttackBuffMultiplier: 0.05f,
                name: "Charged Up 1",
                description: "Consecutive attacks deal more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Charged"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.12f,
                name: "Fire Rate 1",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/FireRate"),
                rarity: Rarity.Common,
                weaponSet: true
            )
        );
        ;


        //rare

        AddStat(
            new AttackStats(
                damageMultiplier: 0.20f,
                name: "Mastery 2",
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critChance: 0.08f,
                name: "Marksman 2",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.4f,
                name: "Brutality 2",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.10f,
                name: "Quickswap 2",
                description: "Decrease Cooldowns",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.25f,
                name: "Impact 2",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.20f,
                name: "RoF 2",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/RoF"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotgunSpreadMultiplier: 0.35f,
                name: "Wide Barrel 2",
                description: "Increase Shotgun Spread",
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
                description: "Increase Projectile Speed",
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
               description: "Decrease Projectile Speed",
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
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Scope"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.25f,
                spreadMultiplier: -0.15f,
                name: "Dexterity 2",
                description: "Increase Attack speed",
                icon: Resources.Load<Sprite>("UI_Icons/Dexterity"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.25f,
                meleeSpacerGapMultiplier: 0.15f,
                name: "Lunge 2",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Lunge"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: -0.4f,
                meleeSpacerGapMultiplier: -0.25f,
                name: "Implode 2",
                description: "Decrease Range",
                icon: Resources.Load<Sprite>("UI_Icons/Implode"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 1f,
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
                coneAngle: 40f,
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
                multicastChance: 0.25f,
                name: "Multi-cast 1",
                description: "Increase Multicast chance",
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
                description: "Pierce through +10 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PenetratingAmmo"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.15f,
                name: "High Caliber 1",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/HighCaliber"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.10f,
                name: "Size Up 1",
                description: "Increase Projectile Size",
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
                description: "Increase Attack Size",
                icon: Resources.Load<Sprite>("UI_Icons/Enlarge"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUp: 0.10f,
                name: "Wave Master 1",
                description: "Increase Aftershock size",
                icon: Resources.Load<Sprite>("UI_Icons/WaveMaster"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboAttackBuffMultiplier: 0.08f,
                name: "Charged Up 2",
                description: "Consecutive attacks deal more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Charged"),
                rarity: Rarity.Rare,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.22f,
                name: "Fire Rate 2",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/FireRate"),
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
                description: "Increase Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;
        AddStat(
            new AttackStats(
                critChance: 0.12f,
                name: "Marksman 3",
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.60f,
                name: "Brutality 3",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.15f,
                name: "Quickswap 3",
                description: "Decrease Cooldowns",
                icon: Resources.Load<Sprite>("UI_Icons/Quickswap"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                knockbackMultiplier: 0.4f,
                name: "Impact 3",
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.30f,
                name: "RoF 3",
                description: "Increase Rate of Fire",
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
                description: "Increase Shotgun Spread",
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
                description: "Increase Projectile Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Propulsion"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
           new AttackStats(
               speedMultiplier: -0.35f,
               name: "Gravitation 3",
               description: "Increase Projectile Speed",
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
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Scope"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.40f,
                spreadMultiplier: -0.20f,
                name: "Dexterity 3",
                description: "Increase Attack speed",
                icon: Resources.Load<Sprite>("UI_Icons/Dexterity"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.4f,
                meleeSpacerGapMultiplier: 0.20f,
                name: "Lunge 3",
                description: "Increase Range",
                icon: Resources.Load<Sprite>("UI_Icons/Lunge"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                aimRangeAdditive: 2f,
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
                coneAngle: 90f,
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
                multicastChance: 0.33f,
                name: "Multi-cast 2",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 3,
                name: "Bonus Round 2",
                description: "+3 Projectiles",
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
                description: "+12 Projectiles",
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
                pierce: 15,
                name: "Penetrating Ammo 2",
                description: "Pierce through +15 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/PenetratingAmmo"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.25f,
                name: "High Caliber 2",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/HighCaliber"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.20f,
                name: "Size Up 2",
                description: "Increase Projectile Size",
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
                description: "+1 Attack",
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
                description: "Attacks release an Aftershock",
                icon: Resources.Load<Sprite>("UI_Icons/After-shock"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.20f,
                name: "Enlarge 2",
                description: "Increase Attack Size",
                icon: Resources.Load<Sprite>("UI_Icons/Enlarge"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUp: 0.15f,
                name: "Wave Master 2",
                description: "Increase Aftershock size",
                icon: Resources.Load<Sprite>("UI_Icons/WaveMaster"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                comboAttackBuffMultiplier: 0.12f,
                name: "Charged Up 3",
                description: "Consecutive attacks deal more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Charged"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                spreadMultiplier: -0.3f,
                name: "Fire Rate 3",
                description: "Increase Rate of Fire",
                icon: Resources.Load<Sprite>("UI_Icons/FireRate"),
                rarity: Rarity.Epic,
                weaponSet: true
            )
        );
        ;

        //legendary

        AddStat(
            new AttackStats(
                shootOppositeSide: true,
                damageMultiplier: -0.5f,
                name: "Buddy System",
                description: "Attack again behind you, -50% Damage",
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
                description: "Increase Damage",
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
                description: "Increase Critical Chance",
                icon: Resources.Load<Sprite>("UI_Icons/EagleEye"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                critDmg: 0.8f,
                name: "Brutality 4",
                description: "Increase Critical Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Brutality"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                castTimeMultiplier: -0.2f,
                name: "Quickswap 4",
                description: "Decrease Cooldowns",
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
                description: "Increase Knockback",
                icon: Resources.Load<Sprite>("UI_Icons/Impact"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                multicastChance: 0.5f,
                name: "Multi-cast 3",
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Multi-cast"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                shotsPerAttack: 5,
                name: "Bonus Round 3",
                description: "+5 Projectiles",
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
                description: "+20 Projectiles",
                icon: Resources.Load<Sprite>("UI_Icons/ExtendedMag"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                pierce: 10,
                name: "Piercing Ammo 3",
                description: "Pierce through +10 enemies",
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
                projectileSizeMultiplier: 0.4f,
                name: "High Caliber 3",
                description: "Increase Projectile Size",
                icon: Resources.Load<Sprite>("UI_Icons/HighCaliber"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                projectileSizeMultiplier: 0.30f,
                name: "Size Up 3",
                description: "Increase Projectile Size",
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
                description: "Attacks release 2 Aftershocks",
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
                description: "+2 Attacks",
                icon: Resources.Load<Sprite>("UI_Icons/OnceMore"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.30f,
                name: "Enlarge 3",
                description: "Increase Attack Size",
                icon: Resources.Load<Sprite>("UI_Icons/Enlarge"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;


        AddStat(
            new AttackStats(
                comboAttackBuffMultiplier: 0.15f,
                name: "Charged Up 4",
                description: "Consecutive attacks deal more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Charged"),
                rarity: Rarity.Legendary,
                weaponSet: true
            )
        );
        ;

        //Weapon Set - Automatic

        AddStat(
            new AttackStats(
                shotsPerAttack: 3,
                spreadMultiplier: -0.05f,
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
                shotsPerAttack: 5,
                spreadMultiplier: -0.1f,
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
                pierce: 1,
                shotsPerAttack: 10,
                spreadMultiplier: -0.15f,
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
                castTimeMultiplier: -0.08f,
                projectileSizeMultiplier: 0.10f,
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
                pierce: 5,
                spreadMultiplier: -0.1f,
                projectileSizeMultiplier: 0.1f,
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
                shotsPerAttack: 1,
                pierce: 5,
                castTimeMultiplier: -0.12f,
                spreadMultiplier: -0.15f,
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
                shotgunSpreadMultiplier: 0.1f,
                knockback: 0.1f,
                damageMultiplier: 0.05f,
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
                shotsPerAttack: 1,
                damageMultiplier: 0.05f,
                rangeMultiplier: 0.12f,
                shotgunSpreadMultiplier: 0.10f,
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
                shotsPerAttack: 1,
                shotgunSpreadMultiplier: 0.15f,
                multicastWaitTimeMultiplier: -0.25f,
                damageMultiplier: 0.2f,
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
                shotgunSpreadMultiplier: 0.10f,
                projectileSizeMultiplier: 0.10f,
                damageMultiplier: 0.05f,
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
                shotgunSpreadMultiplier: 0.10f,
                projectileSizeMultiplier: 0.10f,
                spreadMultiplier: -0.12f,
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
                spreadMultiplier: -0.15f,
                shotgunSpreadMultiplier: 0.2f,
                projectileSizeMultiplier: 0.15f,
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
                knockbackMultiplier: 0.05f,
                meleeSizeMultiplier: 0.05f,
                damageMultiplier: 0.05f,
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
                damageMultiplier: 0.10f,
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
                spreadMultiplier: -0.15f,
                meleeSizeMultiplier: 0.15f,
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
                knockbackMultiplier: 0.05f,
                comboWaitTimeMultiplier: -0.05f,
                damageMultiplier: 0.05f,
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
                comboAttackBuffMultiplier: 0.04f,
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
                meleeShotsScaleUp: 0.10f,
                comboAttackBuffMultiplier: 0.04f,
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
