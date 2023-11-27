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

        //Plump
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 5f,
                health: 5f,
                unlockLevel: 0,
                name: "Plump 1",
                description: "Max Health +5",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Plump"),
                rarity: Rarity.Common
            )
        );

        //Revenge
        AddStat(
            new PlayerCharacterStats(
                isRevenge: true,
                revengeDamage: 10,
                unlockLevel: 1,
                name: "Revenge 1",
                description: "Damage those who hurt you",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Revenge"),
                rarity: Rarity.Common
            )
        );

        //Regenerate
        AddStat(
            new PlayerCharacterStats(
                recoverySpeedAdditive: 0f,
                recoveryAmount: 1f,
                unlockLevel: 0,
                name: "Regenerate 1",
                description: "Recover health every few seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Regenerate"),
                rarity: Rarity.Common
            )
        );


        //rare
        //gold
        AddStat(
            new PlayerCharacterStats(
                goldGainMultiplier: 0.08f,
                name: "Greedy 2",
                unlockLevel: 0,
                description: "Gold +8%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Greedy"),
                rarity: Rarity.Rare
            )
        );

        //xp
        AddStat(
            new PlayerCharacterStats(
                xpGainMultiplier: 0.05f,
                name: "Talented 2",
                unlockLevel: 0,
                description: "Experience +5%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Talented"),
                rarity: Rarity.Rare
            )
        );

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.15f,
                damageMultiplier: -0.05f,
                name: "Lightweight 2",
                unlockLevel: 0,
                description: "Cooldowns -15%, Damage -5%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Rare
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.1f,
                damageMultiplier: 0.25f,
                name: "Heavyweight 2",
                unlockLevel: 0,
                description: "Cooldowns +10%, Damage +25%",
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
                unlockLevel: 1,
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
                unlockLevel: 1,
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
                unlockLevel: 1,
                description: "Weapon Throw: Speed -70%, Damage +50%, Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Toss"),
                rarity: Rarity.Rare
            )
        );

        //Plump
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 10f,
                health: 10f,
                name: "Plump 2",
                unlockLevel: 0,
                description: "Max Health +10",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Plump"),
                rarity: Rarity.Rare
            )
        );

        //Colossus
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.20f,
                speedMultiplier: -0.1f,
                name: "Colossus 1",
                unlockLevel: 0,
                description: "Damage +20%, Move Speed -10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Colossus"),
                rarity: Rarity.Rare
            )
        );

        //Kompact
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: -0.3f,
                meleeSizeMultiplier: -0.3f,
                rangeMultiplier: 0.2f,
                meleeSpacerGapMultiplier: 0.2f,
                damageMultiplier: 0.2f,
                name: "Kompact 1",
                unlockLevel: 0,
                description: "Attack size -30%, Damage & Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Kompact"),
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
                unlockLevel: 0,
                description: "Attack speed, Knockback +25%, Attack Range -40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Close"),
                rarity: Rarity.Rare
            )
        );

        //Hoverball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 0.5f,
                pierce: 5,
                projectileSizeMultiplier: -0.2f,
                meleeSizeMultiplier: -0.2f,
                name: "Hoverball 1",
                unlockLevel: 2,
                description: "Attack Duration +50%, Pierce +5, Size -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hover"),
                rarity: Rarity.Rare
            )
        );

        //Biggenball
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 0.15f,
                meleeSizeMultiplier: 0.15f,
                name: "Biggenball 1",
                unlockLevel: 0,
                description: "Attack, Projectile Size +15%",
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
                unlockLevel: 5,
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
                unlockLevel: 5,
                description: "Effect Power +50%, Effect Duration -1s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Doom"),
                rarity: Rarity.Rare
            )
        );

        //Lucky
        AddStat(
            new PlayerCharacterStats(
                rerollTimes: 3,
                name: "Lucky 1",
                unlockLevel: 0,
                description: "+3 Rerolls",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Lucky"),
                rarity: Rarity.Rare
            )
        );

        //Sleepy
        AddStat(
            new PlayerCharacterStats(
                isSlow: true,
                slowDuration: 2f,
                slowPercentage: 0.6f,
                knockbackMultiplier: -0.25f,
                name: "Sleepy 1",
                unlockLevel: 3,
                description: "Attacks slow for 2s, Knockback -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Sleepy"),
                rarity: Rarity.Rare
            )
        );

        //Revenge
        AddStat(
            new PlayerCharacterStats(
                isRevenge: true,
                revengeDamage: 15,
                unlockLevel: 1,
                name: "Revenge 2",
                description: "Damage those who hurt you",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Revenge"),
                rarity: Rarity.Rare
            )
        );

        //Regenerate
        AddStat(
            new PlayerCharacterStats(
                recoverySpeedAdditive: -0.2f,
                recoveryAmount: 1f,
                name: "Regenerate 2",
                unlockLevel: 0,
                description: "Recover health every few seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Regenerate"),
                rarity: Rarity.Rare
            )
        );

        //Lifesteal
        AddStat(
            new PlayerCharacterStats(
                isLifesteal: true,
                lifestealAmount: 0.25f,
                lifestealChance: 0.02f,
                name: "Lifesteal 1",
                unlockLevel: 0,
                description: "Chance to heal on hit",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Lifesteal"),
                rarity: Rarity.Rare
            )
        );



        //epic
        //gold
        AddStat(
            new PlayerCharacterStats(
                goldGainMultiplier: 0.10f,
                name: "Greedy 3",
                unlockLevel: 0,
                description: "Gold +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Greedy"),
                rarity: Rarity.Epic
            )
        );

        //xp
        AddStat(
            new PlayerCharacterStats(
                xpGainMultiplier: 0.1f,
                name: "Talented 3",
                unlockLevel: 0,
                description: "Experience +10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Talented"),
                rarity: Rarity.Epic
            )
        );

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.3f,
                damageMultiplier: -0.10f,
                name: "Lightweight 3",
                unlockLevel: 0,
                description: "Cooldowns -30%, Damage -10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Epic
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.2f,
                damageMultiplier: 0.5f,
                name: "Heavyweight 3",
                unlockLevel: 0,
                description: "Cooldowns +20%, Damage +50%",
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
                unlockLevel: 1,
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
                unlockLevel: 1,
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
                unlockLevel: 1,
                name: "Tosser 3",
                description: "Weapon Throw: Speed -90%, Damage +75%, Size +20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Toss"),
                rarity: Rarity.Epic
            )
        );

        //Plump
        AddStat(
            new PlayerCharacterStats(
                maxHealth: 15f,
                health: 15f,
                name: "Plump 3",
                unlockLevel: 0,
                description: "Max Health +15",
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
                unlockLevel: 0,
                description: "Damage +40%, Move Speed -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Colossus"),
                rarity: Rarity.Epic
            )
        );

        //Kompact
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: -0.4f,
                meleeSizeMultiplier: -0.4f,
                rangeMultiplier: 0.4f,
                meleeSpacerGapMultiplier: 0.40f,
                damageMultiplier: 0.4f,
                name: "Kompact 2",
                unlockLevel: 0,
                description: "Attack size -40%, Damage & Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Kompact"),
                rarity: Rarity.Epic
            )
        );

        //Overclock
        AddStat(
            new PlayerCharacterStats(
                spreadMultiplier: -0.25f,
                comboWaitTimeMultiplier: -0.25f,
                name: "Overclock 2",
                unlockLevel: 0,
                description: "Attack speed, Rate of fire +25%",
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
                unlockLevel: 0,
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
                unlockLevel: 0,
                description: "Attack speed, Knockback +35%, Attack Range -60%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Close"),
                rarity: Rarity.Epic
            )
        );

        //Hoverball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 0.75f,
                pierce: 10,
                projectileSizeMultiplier: -0.25f,
                meleeSizeMultiplier: -0.25f,
                unlockLevel: 2,
                name: "Hoverball 2",
                description: "Attack Duration +75%, Pierce +10, Size -25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hover"),
                rarity: Rarity.Epic
            )
        );

        //Biggenball
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: 0.25f,
                meleeSizeMultiplier: 0.25f,
                name: "Biggenball 2",
                unlockLevel: 0,
                description: "Attack, Projectile Size +25%",
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
                unlockLevel: 5,
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
                unlockLevel: 5,
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
                unlockLevel: 5,
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
                unlockLevel: 5,
                description: "Improve Chain Amount, Range and Speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Link"),
                rarity: Rarity.Epic
            )
        );

        //Mitosis
        AddStat(
            new PlayerCharacterStats(
                splitAmount: -1,
                splitStatPercentage: 0.6f,
                name: "Mitosis 2",
                unlockLevel: 5,
                description: "Split into 1 fewer attack, all splits gain +60% stats",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Mitosis"),
                rarity: Rarity.Epic
            )
        );

        //Lucky
        AddStat(
            new PlayerCharacterStats(
                rerollTimes: 5,
                name: "Lucky 2",
                unlockLevel: 0,
                description: "+5 Rerolls",
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
                unlockLevel: 3,
                description: "Attacks slow for 4s, Knockback -50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Sleepy"),
                rarity: Rarity.Epic
            )
        );

        //Visor
        AddStat(
            new PlayerCharacterStats(
                is360: true,
                name: "Visor 1",
                unlockLevel: 4,
                description: "360 Aim",
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
                unlockLevel: 4,
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
                castTimeMultiplier: -0.5f,
                pierce: 10,
                shotsPerAttack: -1000,
                shotsPerAttackMelee: -1000,
                comboLength: -1000,
                unlockLevel: 2,
                name: "Quarterback 1",
                description: "Weapon throw: Damage, Size +150%, Cooldowns -50%, Cannot attack",
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
                name: "Gasoline 1",
                unlockLevel: 3,
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
                damageMultiplier: -0.6f,
                projectileSizeMultiplier: -0.6f,
                meleeSizeMultiplier: -0.6f,
                name: "Clone 1",
                unlockLevel: 3,
                description: "Split amount +1, -60% Damage and Size",
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
                unlockLevel: 2,
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
                unlockLevel: 2,
                description: "Attacks pull targets in, Knockback +25%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Polarity"),
                rarity: Rarity.Epic
            )
        );

        //Glass
        AddStat(
           new PlayerCharacterStats(
               defense: -1f,
               critChance: 0.1f,
               critDmg: 0.5f,
               name: "Glass 1",
               unlockLevel: 1,
               description: "Crit Chance +10%, Crit Damage +50%, take +1 more Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Relics/Glass"),
               rarity: Rarity.Epic
           )
       );

        //Revenge
        AddStat(
            new PlayerCharacterStats(
                isRevenge: true,
                revengeDamage: 20,
                unlockLevel: 1,
                name: "Revenge 3",
                description: "Damage those who hurt you",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Revenge"),
                rarity: Rarity.Epic
            )
        );

        //Regenerate
        AddStat(
            new PlayerCharacterStats(
                recoverySpeedAdditive: -0.35f,
                recoveryAmount: 1.5f,
                name: "Regenerate 3",
                unlockLevel: 0,
                description: "Recover health every few seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Regenerate"),
                rarity: Rarity.Epic
            )
        );

        //Lifesteal
        AddStat(
            new PlayerCharacterStats(
                isLifesteal: true,
                lifestealAmount: 0.4f,
                lifestealChance: 0.03f,
                name: "Lifesteal 2",
                unlockLevel: 0,
                description: "Chance to heal on hit",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Lifesteal"),
                rarity: Rarity.Epic
            )
        );


        //legendary

        //Lightweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.10f,
                damageMultiplier: -0.4f,
                name: "Lightweight 4",
                unlockLevel: 0,
                description: "Cooldowns -40%, Damage -10%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Light"),
                rarity: Rarity.Legendary
            )
        );

        //Heavyweight
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: 0.25f,
                damageMultiplier: 0.7f,
                name: "Heavyweight 4",
                unlockLevel: 0,
                description: "Cooldowns +25%, Damage +70%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Heavy"),
                rarity: Rarity.Legendary
            )
        );

        //Gambler
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.25f,
                critDmg: 2f,
                damageMultiplier: -0.5f,
                unlockLevel: 1,
                name: "Gambler 4",
                description: "Crit Chance +25%, Crit Damage +200%, Damage -50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Gambler"),
                rarity: Rarity.Legendary
            )
        );


        //Colossus
        AddStat(
            new PlayerCharacterStats(
                damageMultiplier: 0.50f,
                speedMultiplier: -0.20f,
                name: "Colossus 3",
                unlockLevel: 0,
                description: "Damage +50%, Move Speed -20%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Colossus"),
                rarity: Rarity.Legendary
            )
        );

        //Kompact
        AddStat(
            new PlayerCharacterStats(
                projectileSizeMultiplier: -0.5f,
                meleeSizeMultiplier: -0.5f,
                rangeMultiplier: 0.5f,
                meleeSpacerGapMultiplier: 0.50f,
                damageMultiplier: 0.5f,
                name: "Kompact 3",
                unlockLevel: 0,
                description: "Attack size -50%, Damage & Range +50%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Kompact"),
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
                unlockLevel: 0,
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
                unlockLevel: 0,
                description: "Attack speed, Knockback +50%, Attack Range -75%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Close"),
                rarity: Rarity.Legendary
            )
        );

        //Hoverball
        AddStat(
            new PlayerCharacterStats(
                activeMultiplier: 1f,
                pierce: 15,
                projectileSizeMultiplier: -0.35f,
                meleeSizeMultiplier: -0.35f,
                unlockLevel: 2,
                name: "Hoverball 3",
                description: "Attack Duration +100%, Pierce +15, Size -35%",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hover"),
                rarity: Rarity.Legendary
            )
        );

        //Telekinesis
        AddStat(
            new PlayerCharacterStats(
                isHoming: true,
                name: "Telekinesis 2",
                unlockLevel: 3,
                description: "Projectiles follow enemies",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Telekinesis"),
                rarity: Rarity.Legendary
            )
        );

        //Quarterback
        AddStat(
            new PlayerCharacterStats(
                thrownDamageMultiplier: 2f,
                thrownWeaponSizeMultiplier: 2f,
                castTimeMultiplier: -0.6f,
                pierce: 20,
                shotsPerAttack: -1000,
                shotsPerAttackMelee: -1000,
                comboLength: -1000,
                unlockLevel: 2,
                name: "Quarterback 2",
                description: "Weapon throw: Damage, Size +200%, Cooldowns -60%, Cannot attack",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Quarterback"),
                rarity: Rarity.Legendary
            )
        );

        //Gasoline
        AddStat(
            new PlayerCharacterStats(
                isDoT: true,
                dotDuration: 10f,
                dotTickRate: 0.35f,
                dotDamage: 1,
                unlockLevel: 3,
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
                damageMultiplier: -0.50f,
                projectileSizeMultiplier: -0.50f,
                meleeSizeMultiplier: -0.50f,
                name: "Clone 2",
                unlockLevel: 3,
                description: "Split amount +1, -50% Damage and Size",
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
                unlockLevel: 2,
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
                unlockLevel: 2,
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
                unlockLevel: 4,
                description: "Attacks stun for 0.5s",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Stopsign"),
                rarity: Rarity.Legendary
            )
        );

        //Hindsight
        AddStat(
            new PlayerCharacterStats(
                shootOpposideSide: true,
                recoveryAdditive: 3f,
                name: "Hindsight 1",
                unlockLevel: 6,
                description: "Attack again behind you, exhausted after attacking",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Hindsight"),
                rarity: Rarity.Legendary
            )
        );

        //Glass
        AddStat(
           new PlayerCharacterStats(
               defense: -1f,
               critChance: 0.25f,
               critDmg: 0.5f,
               name: "Glass 2",
               unlockLevel: 1,
               description: "Crit Chance +25% & Crit Damage +50%, take +1 more Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Relics/Glass"),
               rarity: Rarity.Legendary
           )
       );



        //Older Stats

        //Common
        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 20,
                name: "First Aid 1",
                unlockLevel: 0,
                description: "Recover 20 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Common
            )
        );


        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 0.5f,
                name: "Magnet 1",
                unlockLevel: 0,
                description: "Increase Pickup Range",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Magnet"),
                rarity: Rarity.Common
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.12f,
                name: "Boost 1",
                unlockLevel: 0,
                description: "Increase Move speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Common
            )
        );

        //RARE STATS
        AddStat(
            new PlayerCharacterStats(
                health: 20,
                name: "First Aid 2",
                unlockLevel: 0,
                description: "Recover 30 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Rare
            )
        );

        //pickup range
        AddStat(
            new PlayerCharacterStats(
                pickupRange: 1f,
                name: "Magnet 2",
                unlockLevel: 0,
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
                unlockLevel: 0,
                description: "Reduce incoming Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Protection"),
                rarity: Rarity.Rare
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.08f,
                name: "Adrenaline 1",
                unlockLevel: 0,
                description: "Reduce all Cooldowns",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Adrenaline"),
                rarity: Rarity.Rare
            )
        );


        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.20f,
                name: "Boost 2",
                unlockLevel: 0,
                description: "Increase Move speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Rare
            )
        );


        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.12f,
                name: "Multicast 1",
                unlockLevel: 0,
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Multi"),
                rarity: Rarity.Rare
            )
        );

        //EPIC STATS

        //Healing
        AddStat(
            new PlayerCharacterStats(
                health: 40,
                name: "First Aid 3",
                unlockLevel: 0,
                description: "Recover 40 HP",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/FirstAid"),
                rarity: Rarity.Epic
            )
        );


        //defense
        AddStat(
            new PlayerCharacterStats(
                defense: 0.4f,
                name: "Protection 2",
                unlockLevel: 0,
                description: "Reduce incoming Damage",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Protection"),
                rarity: Rarity.Epic
            )
        );

        //Attack Speed
        AddStat(
            new PlayerCharacterStats(
                castTimeMultiplier: -0.15f,
                name: "Adrenaline 2",
                unlockLevel: 0,
                description: "Reduce all Cooldowns",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Adrenaline"),
                rarity: Rarity.Epic
            )
        );


        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.30f,
                name: "Boost 3",
                unlockLevel: 0,
                description: "Increase Move speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Epic
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.25f,
                name: "Multicast 2",
                unlockLevel: 0,
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Multi"),
                rarity: Rarity.Epic
            )
        );

        //LEGENDARY STATS

        //Crit Chance
        AddStat(
            new PlayerCharacterStats(
                critChance: 0.10f,
                critDmg: 0.25f,
                name: "Eagle Eye 4",
                unlockLevel: 1,
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
                unlockLevel: 0,
                description: "Reduce All Cooldowns",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Adrenaline"),
                rarity: Rarity.Legendary
            )
        );

        //move Speed
        AddStat(
            new PlayerCharacterStats(
                speedMultiplier: 0.5f,
                name: "Boost 4",
                unlockLevel: 0,
                description: "Increase Move speed",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Boost"),
                rarity: Rarity.Legendary
            )
        );

        //multicast
        AddStat(
            new PlayerCharacterStats(
                multicastChance: 0.5f,
                name: "Multicast 3",
                unlockLevel: 0,
                description: "Increase Multicast chance",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Multi"),
                rarity: Rarity.Legendary
            )
        );

        //Revenge
        AddStat(
            new PlayerCharacterStats(
                isRevenge: true,
                revengeDamage: 30,
                unlockLevel: 1,
                name: "Revenge 4",
                description: "Damage those who hurt you",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Revenge"),
                rarity: Rarity.Legendary
            )
        );

        //Regenerate
        AddStat(
            new PlayerCharacterStats(
                recoverySpeedAdditive: -1f,
                recoveryAmount: 1.5f,
                name: "Regenerate 4",
                unlockLevel: 0,
                description: "Recover health every few seconds",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Regenerate"),
                rarity: Rarity.Legendary
            )
        );

        //Lifesteal
        AddStat(
            new PlayerCharacterStats(
                isLifesteal: true,
                lifestealAmount: 0.75f,
                lifestealChance: 0.05f,
                name: "Lifesteal 3",
                unlockLevel: 0,
                description: "Chance to heal on hit",
                icon: Resources.Load<Sprite>("UI_Icons/Relics/Lifesteal"),
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
