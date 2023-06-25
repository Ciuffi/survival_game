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

        //gold
        AddStat(
            new PlayerCharacterStats(
                goldGainMultiplier: 0.05f,
                defense: -0.5f,
                name: "Greedy 1",
                description: "Gold +5%, Take +0.5 more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Greedy"),
                rarity: Rarity.Common
            )
        );

        //xp
        AddStat(
            new PlayerCharacterStats(
                xpGainMultiplier: 0.03f,
                damageMultiplier: -0.05f,
                name: "Talented 1",
                description: "Experience +3%, Damage -5%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Talented"),
                rarity: Rarity.Common
            )
        );

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.20f,
                damageMultiplier: -0.15f,
                name: "Lightweight 1",
                description: "Cooldowns -20%, Damage -15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Common
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.15f,
                damageMultiplier: 0.2f,
                name: "Heavyweight 1",
                description: "Cooldowns +15%, Damage +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Heavy"),
                rarity: Rarity.Common
            )
        );

        //Gambler
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.05f,
                critDmg: 0.5f,
                damageMultiplier: -0.15f,
                name: "Gambler 1",
                description: "Crit Chance +5%, Crit Damage +50%, Damage -15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gambler"),
                rarity: Rarity.Common
            )
        );

        //Pitcher
        AddStat(
            new PlayerCharacterStats(
                thrownSpeedMultiplier: 0.6f,
                thrownDamageMultiplier: 0.6f,
                name: "Pitcher 1",
                description: "Weapon Throw: Speed and Damage +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Pitch"),
                rarity: Rarity.Common
            )
        );

        //Tosser
        AddStat(
            new PlayerCharacterStats(
                thrownSpeedMultiplier: -0.4f,
                thrownDamageMultiplier: 0.3f,
                name: "Tosser 1",
                description: "Weapon Throw: Speed -40%, Damage +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Toss"),
                rarity: Rarity.Common
            )
        );

        //Plump
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 5f,
                health: 5f,
                name: "Plump 1",
                description: "Max Health +5",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Plump"),
                rarity: Rarity.Common
            )
        );



        //rare

        //gold
        AddStat(
            new PlayerCharacterStats(
                goldGainMultiplier: 0.10f,
                defense: -1f,
                name: "Greedy 2",
                description: "Gold +10%, Take +1 more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Greedy"),
                rarity: Rarity.Rare
            )
        );

        //xp
        AddStat(
            new PlayerCharacterStats(
                xpGainMultiplier: 0.05f,
                damageMultiplier: -0.10f,
                name: "Talented 2",
                description: "Experience +5%, Damage -10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Talented"),
                rarity: Rarity.Rare
            )
        );

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.3f,
                damageMultiplier: -0.22f,
                name: "Lightweight 2",
                description: "Cooldowns -30%, Damage -22%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Rare
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.22f,
                damageMultiplier: 0.3f,
                name: "Heavyweight 2",
                description: "Cooldowns +22%, Damage +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Heavy"),
                rarity: Rarity.Rare
            )
        );

        //Gambler
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.10f,
                critDmg: 1f,
                damageMultiplier: -0.25f,
                name: "Gambler 2",
                description: "Crit Chance +10%, Crit Damage +100%, Damage -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gambler"),
                rarity: Rarity.Rare
            )
        );


        //Pitcher
        AddStat(
            new PlayerCharacterStats(
                thrownSpeedMultiplier: 1f,
                thrownDamageMultiplier: 1f,
                thrownWeaponSizeMultiplier: 0.25f,
                name: "Pitcher 2",
                description: "Weapon Throw: Speed and Damage +100%, Size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Pitch"),
                rarity: Rarity.Rare
            )
        );

        //Tosser
        AddStat(
            new PlayerCharacterStats(
                thrownSpeedMultiplier: -0.7f,
                thrownDamageMultiplier: 0.5f,
                thrownWeaponSizeMultiplier: 0.15f,
                name: "Tosser 2",
                description: "Weapon Throw: Speed -70%, Damage +50%, Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Toss"),
                rarity: Rarity.Rare
            )
        );

        //Plump
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 15f,
                health: 15f,
                defense: -0.2f,
                name: "Plump 2",
                description: "Max Health +15, take +0.2 more Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Plump"),
                rarity: Rarity.Rare
            )
        );

        //Colossus
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.20f,
                speedMultiplier: -0.12f,
                name: "Colossus 1",
                description: "Damage +20%, Move Speed -12%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Colossus"),
                rarity: Rarity.Rare
            )
        );

        //Kompact
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: -0.5f,
                meleeSizeMultiplier: -0.5f,
                rangeMultiplier: 0.25f,
                meleeSpacerGapMultiplier: 0.15f,
                damageMultiplier: 0.25f,
                name: "Kompact 1",
                description: "Attack size -50%, Damage & Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Kompact"),
                rarity: Rarity.Rare
            )
        );

        //Overclock
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.25f,
                comboWaitTimeMultiplier: -0.25f,
                recoveryAdditive: 0.5f,
                name: "Overclock 1",
                description: "Attack speed, Rate of fire +25%, Attack recovery +0.5s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Overclock"),
                rarity: Rarity.Rare
            )
        );
        //Drill
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: -0.70f,
                damageMultiplier: 0.20f,
                rangeMultiplier: 0.20f,
                meleeSpacerMultiplier: 0.15f,
                meleeSpacerGapMultiplier: 0.15f,
                name: "Drill 1",
                description: "Knockback -70%, Damage & Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Drill"),
                rarity: Rarity.Rare
            )
        );

        //Close Combat
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 0.25f,
                spreadMultiplier: -0.25f,
                comboWaitTimeMultiplier: -0.25f,
                rangeMultiplier: -0.4f,
                meleeSpacerGapMultiplier: -0.35f,
                meleeSpacerMultiplier: -0.35f,
                name: "Close Combat 1",
                description: "Attack speed, Knockback +25%, Attack Range -40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Close"),
                rarity: Rarity.Rare
            )
        );

        //Hoverball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 0.5f,
                pierce: 15,
                projectileSizeMultiplier: -0.2f,
                meleeSizeMultiplier: -0.2f,
                name: "Hoverball 1",
                description: "Attack Duration +50%, Pierce +15, Size -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hover"),
                rarity: Rarity.Rare
            )
        );

        //Biggenball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 0.5f,
                projectileSizeMultiplier: 0.25f,
                meleeSizeMultiplier: 0.25f,
                damageMultiplier: -0.15f,
                name: "Biggenball 1",
                description: "Attack Duration +50%, Size +25%, Damage -15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Biggen"),
                rarity: Rarity.Rare
            )
        );

        //Curse
        AddStat(
            new PlayerCharacterStats(
                effectDuration: 1f,
                effectMultiplier: -0.5f,
                name: "Curse 1",
                description: "Effect Duration +1s, Effect Power -50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Curse"),
                rarity: Rarity.Rare
            )
        );

        //Doom
        AddStat(
            new PlayerCharacterStats(
                effectDuration: -1f,
                effectMultiplier: 0.5f,
                name: "Doom 1",
                description: "Effect Power +50%, Effect Duration -1s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Doom"),
                rarity: Rarity.Rare
            )
        );

        //Pyro
        AddStat(
            new PlayerCharacterStats(
                dotDamage: 3,
                name: "Pyro 1",
                description: "+3 Burn Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Pyro"),
                rarity: Rarity.Rare
            )
        );

        //Link
        AddStat(
            new PlayerCharacterStats(
                chainTimes: 1,
                chainRange: 0.5f,
                chainSpeed: 3,
                name: "Link 1",
                description: "Chain to +1 targets, increase chain Range and Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Link"),
                rarity: Rarity.Rare
            )
        );

        //Mitosis
        AddStat(
            new PlayerCharacterStats(
                splitAmount: -1,
                splitStatPercentage: 0.4f,
                name: "Mitosis 1",
                description: "Split into 1 fewer attack, all splits gain +50% stats",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Mitosis"),
                rarity: Rarity.Rare
            )
        );

        //Lucky
        AddStat(
            new PlayerCharacterStats(
                rerollTimes: 2,
                name: "Lucky 1",
                description: "+2 Rerolls",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Lucky"),
                rarity: Rarity.Rare
            )
        );

        //Sleepy
        AddStat(
            new PlayerCharacterStats(
                isSlow: true,
                slowDuration: 2f,
                slowPercentage: 0.5f,
                knockbackMultiplier: -0.25f,
                name: "Sleepy 1",
                description: "Attacks slow for 2s, Knockback -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Sleepy"),
                rarity: Rarity.Rare
            )
        );



        //epic

        //gold
        AddStat(
            new PlayerCharacterStats(
                goldGainMultiplier: 0.15f,
                defense: -1.5f,
                name: "Greedy 3",
                description: "Gold +15%, Take +1.5 more damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Greedy"),
                rarity: Rarity.Epic
            )
        );

        //xp
        AddStat(
            new PlayerCharacterStats(
                xpGainMultiplier: 0.1f,
                damageMultiplier: -0.15f,
                name: "Talented 3",
                description: "Experience +10%, Damage -15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Talented"),
                rarity: Rarity.Epic
            )
        );

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.5f,
                damageMultiplier: -0.3f,
                name: "Lightweight 3",
                description: "Cooldowns -50%, Damage -30%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Epic
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.3f,
                damageMultiplier: 0.5f,
                name: "Heavyweight 3",
                description: "Cooldowns +30%, Damage +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Heavy"),
                rarity: Rarity.Epic
            )
        );

        //Gambler
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.20f,
                critDmg: 2f,
                damageMultiplier: -0.40f,
                name: "Gambler 3",
                description: "Crit Chance +20%, Crit Damage +200%, Damage -40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gambler"),
                rarity: Rarity.Epic
            )
        );


        //Pitcher
        AddStat(
            new PlayerCharacterStats(
                thrownSpeedMultiplier: 1.5f,
                thrownDamageMultiplier: 1.5f,
                thrownWeaponSizeMultiplier: 0.3f,
                name: "Pitcher 3",
                description: "Weapon Throw: Speed and Damage +150%, Size +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Pitch"),
                rarity: Rarity.Epic
            )
        );

        //Tosser
        AddStat(
            new PlayerCharacterStats(
                thrownSpeedMultiplier: -0.9f,
                thrownDamageMultiplier: 0.75f,
                thrownWeaponSizeMultiplier: 0.2f,
                name: "Tosser 3",
                description: "Weapon Throw: Speed -90%, Damage +75%, Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Toss"),
                rarity: Rarity.Epic
            )
        );

        //Plump
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 20f,
                health: 20f,
                defense: -0.4f,
                name: "Plump 3",
                description: "Max Health +20, take +0.4 more Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Plump"),
                rarity: Rarity.Epic
            )
        );

        //Colossus
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.40f,
                speedMultiplier: -0.25f,
                name: "Colossus 2",
                description: "Damage +40%, Move Speed -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Colossus"),
                rarity: Rarity.Epic
            )
        );

        //Kompact
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: -0.75f,
                meleeSizeMultiplier: -0.75f,
                rangeMultiplier: 0.4f,
                meleeSpacerGapMultiplier: 0.20f,
                damageMultiplier: 0.4f,
                name: "Kompact 2",
                description: "Attack size -75%, Damage & Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Kompact"),
                rarity: Rarity.Epic
            )
        );

        //Overclock
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.35f,
                comboWaitTimeMultiplier: -0.35f,
                recoveryAdditive: 0.75f,
                name: "Overclock 2",
                description: "Attack speed, Rate of fire +35%, attack recovery +0.75s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Overclock"),
                rarity: Rarity.Epic
            )
        );
        //Drill
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: -1f,
                damageMultiplier: 0.3f,
                rangeMultiplier: 0.3f,
                meleeSpacerMultiplier: 0.2f,
                meleeSpacerGapMultiplier: 0.2f,
                name: "Drill 2",
                description: "Knockback -100%, Damage & Range +30%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Drill"),
                rarity: Rarity.Epic
            )
        );

        //Close Combat
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 0.35f,
                spreadMultiplier: -0.35f,
                comboWaitTimeMultiplier: -0.35f,
                rangeMultiplier: -0.6f,
                meleeSpacerGapMultiplier: -0.55f,
                meleeSpacerMultiplier: -0.55f,
                name: "Close Combat 2",
                description: "Attack speed, Knockback +35%, Attack Range -60%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Close"),
                rarity: Rarity.Epic
            )
        );

        //Hoverball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 0.75f,
                pierce: 25,
                projectileSizeMultiplier: -0.25f,
                meleeSizeMultiplier: -0.25f,
                name: "Hoverball 2",
                description: "Attack Duration +75%, Pierce +25, Size -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hover"),
                rarity: Rarity.Epic
            )
        );

        //Biggenball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 0.75f,
                projectileSizeMultiplier: 0.25f,
                meleeSizeMultiplier: 0.25f,
                damageMultiplier: -0.25f,
                name: "Biggenball 2",
                description: "Attack Duration +75%, Size +25%, Damage -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Biggen"),
                rarity: Rarity.Epic
            )
        );

        //Curse
        AddStat(
            new PlayerCharacterStats(
                effectDuration: 1.5f,
                effectMultiplier: -0.75f,
                name: "Curse 2",
                description: "Effect Duration +1.5s, Effect Power -75%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Curse"),
                rarity: Rarity.Epic
            )
        );

        //Doom
        AddStat(
            new PlayerCharacterStats(
                effectDuration: -1.5f,
                effectMultiplier: 0.75f,
                name: "Doom 2",
                description: "Effect Power +75%, Effect Duration -1.5s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Doom"),
                rarity: Rarity.Epic
            )
        );

        //Pyro
        AddStat(
            new PlayerCharacterStats(
                dotDamage: 6,
                name: "Pyro 2",
                description: "+6 Burn Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Pyro"),
                rarity: Rarity.Epic
            )
        );

        //Link
        AddStat(
            new PlayerCharacterStats(
                chainTimes: 2,
                chainRange: 0.5f,
                chainSpeed: 3,
                name: "Link 2",
                description: "Chain to +2 targets, increase chain Range and Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Link"),
                rarity: Rarity.Epic
            )
        );

        //Mitosis
        AddStat(
            new PlayerCharacterStats(
                splitAmount: -1,
                splitStatPercentage: 0.5f,
                name: "Mitosis 2",
                description: "Split into 1 fewer attack, all splits gain +60% stats",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Mitosis"),
                rarity: Rarity.Epic
            )
        );

        //Lucky
        AddStat(
            new PlayerCharacterStats(
                rerollTimes: 3,
                name: "Lucky 2",
                description: "+3 Rerolls",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Lucky"),
                rarity: Rarity.Epic
            )
        );

        //Sleepy
        AddStat(
            new PlayerCharacterStats(
                isSlow: true,
                slowDuration: 4f,
                slowPercentage: 0.5f,
                knockbackMultiplier: -0.5f,
                name: "Sleepy 2",
                description: "Attacks slow for 4s, Knockback -50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Sleepy"),
                rarity: Rarity.Epic
            )
        );

        //Visor
        AddStat(
            new PlayerCharacterStats(
                is360: true,
                aimRangeAdditive: -1f,
                name: "Visor 1",
                description: "360 Vision, Aim Range -1",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Visor"),
                rarity: Rarity.Epic
            )
        );

        //Telekinesis
        AddStat(
            new PlayerCharacterStats(
                isHoming: true,
                spreadMultiplier: -0.2f,
                name: "Telekinesis 1",
                description: "Projectiles follow enemies, Rate of Fire -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Telekinesis"),
                rarity: Rarity.Epic
            )
        );

        //Quarterback
        AddStat(
            new PlayerCharacterStats(
                thrownDamageMultiplier: 1.5f,
                thrownWeaponSizeMultiplier: 1.5f,
                castTimeMultiplier: -0.25f,
                pierce: 5,
                shotsPerAttack: -1000,
                shotsPerAttackMelee: -1000,
                comboLength: -1000,
                name: "Quarterback 1",
                description: "Weapon throw: Damage, Size +150%, Cooldowns -25%, Cannot attack",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Quarterback"),
                rarity: Rarity.Epic
            )
        );

        //Gasoline
        AddStat(
            new PlayerCharacterStats(
                isDoT: true,
                dotDuration: 10f,
                dotTickRate: 0.5f,
                dotDamage: 1,
                name: "Gasoline 1",
                description: "All attacks Ignite for 10 seconds and burn twice as quickly.",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gasoline"),
                rarity: Rarity.Epic
            )
        );

        //Clone
        AddStat(
            new PlayerCharacterStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 1f,
                damageMultiplier: -0.5f,
                projectileSizeMultiplier: -0.5f,
                meleeSizeMultiplier: -0.5f,
                name: "Clone 1",
                description: "Split amount +1, all Splits +100% stats, -50% Damage and Size",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Clone"),
                rarity: Rarity.Epic
            )
        );

        //Battery
        AddStat(
            new PlayerCharacterStats(
                isChain: true,
                chainTimes: 2,
                chainStatDecayPercent: 0.5f,
                chainRange: 2f,
                chainSpeed: 5f,
                name: "Battery 1",
                description: "Damage jumps to 2 more targets",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Battery"),
                rarity: Rarity.Epic
            )
        );

        //Polarity
        AddStat(
            new PlayerCharacterStats(
                isMagnet: true,
                magnetDuration: 0.5f,
                magnetStrength: 0.75f,
                knockbackMultiplier: 0.25f,
                name: "Polarity 1",
                description: "Attacks pull targets in, Knockback +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Polarity"),
                rarity: Rarity.Epic
            )
        );

        //Glass
        AddStat(
           new PlayerCharacterStats(
               defense: -3f,
               critChance: 0.1f,
               critDmg: 0.5f,
               name: "Glass 1",
               description: "Crit Chance +10%, Crit Damage +50%, take +3 more Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Relics/Glass"),
               rarity: Rarity.Epic
           )
       );


        //legendary

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.6f,
                damageMultiplier: -0.35f,
                name: "Lightweight 4",
                description: "Cooldowns -60%, Damage -35%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Legendary
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.35f,
                damageMultiplier: 0.6f,
                name: "Heavyweight 4",
                description: "Cooldowns +35%, Damage +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Heavy"),
                rarity: Rarity.Legendary
            )
        );

        //Gambler
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.25f,
                critDmg: 2.5f,
                damageMultiplier: -0.5f,
                name: "Gambler 4",
                description: "Crit Chance +25%, Crit Damage +250%, Damage -50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gambler"),
                rarity: Rarity.Legendary
            )
        );


        //Colossus
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.50f,
                speedMultiplier: -0.33f,
                name: "Colossus 3",
                description: "Damage +50%, Move Speed -33%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Colossus"),
                rarity: Rarity.Legendary
            )
        );

        //Kompact
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: -0.9f,
                meleeSizeMultiplier: -0.9f,
                rangeMultiplier: 0.6f,
                meleeSpacerGapMultiplier: 0.30f,
                damageMultiplier: 0.6f,
                name: "Kompact 3",
                description: "Attack size -90%, Damage & Range +60%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Kompact"),
                rarity: Rarity.Legendary
            )
        );

        //Overclock
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.5f,
                comboWaitTimeMultiplier: -0.5f,
                recoveryAdditive: 1f,
                name: "Overclock 3",
                description: "Attack speed, Rate of fire +50%, exhausted for +1s after attacking",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Overclock"),
                rarity: Rarity.Legendary
            )
        );
        //Drill
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: -1.2f,
                damageMultiplier: 0.4f,
                rangeMultiplier: 0.4f,
                meleeSpacerMultiplier: 0.3f,
                meleeSpacerGapMultiplier: 0.3f,
                name: "Drill 3",
                description: "Knockback -120%, Damage & Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Drill"),
                rarity: Rarity.Legendary
            )
        );

        //Close Combat
        AddStat(
            new PlayerCharacterStats(
                knockbackMultiplier: 0.5f,
                spreadMultiplier: -0.5f,
                comboWaitTimeMultiplier: -0.5f,
                rangeMultiplier: -0.75f,
                meleeSpacerGapMultiplier: -0.65f,
                meleeSpacerMultiplier: -0.65f,
                name: "Close Combat 3",
                description: "Attack speed, Knockback +50%, Attack Range -75%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Close"),
                rarity: Rarity.Legendary
            )
        );

        //Hoverball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 1f,
                pierce: 35,
                projectileSizeMultiplier: -0.35f,
                meleeSizeMultiplier: -0.35f,
                name: "Hoverball 3",
                description: "Attack Duration +100%, Pierce +35, Size -35%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hover"),
                rarity: Rarity.Legendary
            )
        );

        //Biggenball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 1f,
                projectileSizeMultiplier: 0.35f,
                meleeSizeMultiplier: 0.35f,
                damageMultiplier: -0.35f,
                name: "Biggenball 3",
                description: "Attack Duration +100%, Size +35%, Damage -35%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Biggen"),
                rarity: Rarity.Legendary
            )
        );

        //Curse
        AddStat(
            new PlayerCharacterStats(
                effectDuration: 2f,
                effectMultiplier: -0.9f,
                name: "Curse 3",
                description: "Effect Duration +2s, Effect Power -90%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Curse"),
                rarity: Rarity.Legendary
            )
        );

        //Doom
        AddStat(
            new PlayerCharacterStats(
                effectDuration: -2f,
                effectMultiplier: 1f,
                name: "Doom 3",
                description: "Effect Power +100%, Effect Duration -2s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Doom"),
                rarity: Rarity.Legendary
            )
        );

        //Pyro
        AddStat(
            new PlayerCharacterStats(
                dotDamage: 9,
                name: "Pyro 3",
                description: "+10 Burn Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Pyro"),
                rarity: Rarity.Legendary
            )
        );

        //Link
        AddStat(
            new PlayerCharacterStats(
                chainTimes: 2,
                chainRange: 1f,
                chainSpeed: 5,
                name: "Link 3",
                description: "Chain to +2 targets, increase chain Range and Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Link"),
                rarity: Rarity.Legendary
            )
        );

        //Mitosis
        AddStat(
            new PlayerCharacterStats(
                splitAmount: -1,
                splitStatPercentage: 0.75f,
                name: "Mitosis 3",
                description: "Split into 1 fewer attack, all splits gain +75% stats",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Mitosis"),
                rarity: Rarity.Legendary
            )
        );

        //Visor
        AddStat(
            new PlayerCharacterStats(
                is360: true,
                name: "Visor 2",
                description: "360 degree Vision",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Visor"),
                rarity: Rarity.Legendary
            )
        );

        //Telekinesis
        AddStat(
            new PlayerCharacterStats(
                isHoming: true,
                spreadMultiplier: -0.1f,
                name: "Telekinesis 2",
                description: "Projectiles follow enemies, Rate of Fire -10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Telekinesis"),
                rarity: Rarity.Legendary
            )
        );

        //Quarterback
        AddStat(
            new PlayerCharacterStats(
                thrownDamageMultiplier: 2f,
                thrownWeaponSizeMultiplier: 2f,
                castTimeMultiplier: -0.4f,
                pierce: 10,
                shotsPerAttack: -1000,
                shotsPerAttackMelee: -1000,
                comboLength: -1000,
                name: "Quarterback 2",
                description: "Weapon throw: Damage, Size +200%, Cooldowns -40%, Cannot attack",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Quarterback"),
                rarity: Rarity.Legendary
            )
        );

        //Gasoline
        AddStat(
            new PlayerCharacterStats(
                isDoT: true,
                dotDuration: 10f,
                dotTickRate: 0.33f,
                dotDamage: 1,
                name: "Gasoline 2",
                description: "All attacks Ignite for 10 seconds and burn twice as quickly.",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gasoline"),
                rarity: Rarity.Legendary
            )
        );

        //Clone
        AddStat(
            new PlayerCharacterStats(
                isSplit: true,
                splitAmount: 1,
                splitStatPercentage: 1f,
                damageMultiplier: -0.33f,
                projectileSizeMultiplier: -0.33f,
                meleeSizeMultiplier: -0.33f,
                name: "Clone 2",
                description: "Split amount +1, all Splits +100% stats, -33% Damage and Size",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Clone"),
                rarity: Rarity.Legendary
            )
        );

        //Battery
        AddStat(
            new PlayerCharacterStats(
                isChain: true,
                chainTimes: 3,
                chainStatDecayPercent: 0.4f,
                chainRange: 2f,
                chainSpeed: 5f,
                name: "Battery 2",
                description: "Damage jumps to 3 more targets",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Battery"),
                rarity: Rarity.Legendary
            )
        );

        //Polarity
        AddStat(
            new PlayerCharacterStats(
                isMagnet: true,
                magnetDuration: 0.5f,
                magnetStrength: 1f,
                knockbackMultiplier: 0.4f,
                name: "Polarity 2",
                description: "Attacks pull targets in, Knockback +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Polarity"),
                rarity: Rarity.Legendary
            )
        );


        //Stopsign
        AddStat(
            new PlayerCharacterStats(
                isStun: true,
                stunDuration: 0.5f,
                name: "Stopsign 1",
                description: "Attacks stun for 0.5s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Stopsign"),
                rarity: Rarity.Legendary
            )
        );

        //Hindsight
        AddStat(
            new PlayerCharacterStats(
                shootOpposideSide: true,
                recoveryAdditive: 0.5f,
                name: "Hindsight 1",
                description: "Attack again behind you, exhausted for +0.5s after attacking",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hindsight"),
                rarity: Rarity.Legendary
            )
        );

        //Glass
        AddStat(
           new PlayerCharacterStats(
               defense: -5f,
               critChance: 0.25f,
               critDmg: 0.5f,
               name: "Glass 2",
               description: "Crit Chance +25% & Crit Damage +50%, take +5 more Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Relics/Glass"),
               rarity: Rarity.Legendary
           )
       );



        //Older Stats

        //Common
        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 15,
                name: "First Aid 1",
                description: "Recover 15 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Common
            )
        );


        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 0.5f,
                name: "Magnet 1",
                description: "Pickup Range +0.5",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Magnet"),
                rarity: Rarity.Common
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.10f,
                name: "Boost 1",
                description: "Move speed +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Common
            )
        );

        //RARE STATS
        AddStat(
            new PlayerCharacterStats(
                health: 20,
                name: "First Aid 2",
                description: "Recover 20 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Rare
            )
        );

        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 1f,
                name: "Magnet 2",
                description: "Increase pickup range",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Magnet"),
                rarity: Rarity.Rare
            )
        );

        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 0.25f,
                name: "Protection 1",
                description: "Reduce incoming Damage by 0.25",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Protection"),
                rarity: Rarity.Rare
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.08f,
                name: "Adrenaline 1",
                description: "All Cooldowns -8%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Adrenaline"),
                rarity: Rarity.Rare
            )
        );


        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.20f,
                name: "Boost 2",
                description: "Move speed +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Rare
            )
        );


        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.12f,
                name: "Multicast 1",
                description: "All Multicast chance +12%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Multi"),
                rarity: Rarity.Rare
            )
        );

        //EPIC STATS

        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 35,
                name: "First Aid 3",
                description: "Recover 35 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Epic
            )
        );


        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 0.4f,
                name: "Protection 2",
                description: "Reduce incoming Damage by 0.4",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Protection"),
                rarity: Rarity.Epic
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.15f,
                name: "Adrenaline 2",
                description: "All Cooldowns -15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Adrenaline"),
                rarity: Rarity.Epic
            )
        );


        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.33f,
                name: "Boost 3",
                description: "Move speed +33%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Epic
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.25f,
                name: "Multicast 2",
                description: "All Multicast chance +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Multi"),
                rarity: Rarity.Epic
            )
        );

        //LEGENDARY STATS

        AddStat(
            new PlayerCharacterStats(
                health: 60,
                name: "First Aid 4",
                description: "Recover 60 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Legendary
            )
        );

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.10f,
                critDmg: 0.25f,
                name: "Eagle Eye 4",
                description: "Crit Chance +10%, Crit Damage +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Eye"),
                rarity: Rarity.Legendary
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.25f,
                name: "Adrenaline 3",
                description: "All Cooldowns -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Adrenaline"),
                rarity: Rarity.Legendary
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.5f,
                name: "Boost 4",
                description: "Move speed +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Legendary
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.5f,
                name: "Multicast 3",
                description: "All Multicast chance +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Multi"),
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
