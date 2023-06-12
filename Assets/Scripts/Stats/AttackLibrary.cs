using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AttackLibrary
{
    private static Dictionary<string, AttackBuilder> attackBuilderDictionary =
        new Dictionary<string, AttackBuilder>();
    private static bool isInitialized = false;

    static AttackLibrary()
    {
        InitializeLibrary();
    }

    private static void AddAttack(AttackBuilder attack)
    {
        attackBuilderDictionary.Add(attack.GetAttackName(), attack);
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        List<GameObject> AutomaticMuzzleFlash = new List<GameObject>
        {
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Fast_1"),
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Fast_2"),
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Fast_3"),
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Fast_4"),
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Fast_5")
        };

        List<GameObject> PistolMuzzleFlash = new List<GameObject>
        {
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Pistol")
        };

        List<GameObject> BigMuzzleFlash = new List<GameObject>
        {
            Resources.Load<GameObject>("WeaponVFX/MuzzleFlash_Big"),
        };

        // Consecrate
        List<AttackStats> ConsecrateRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 1),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.1f, activeDuration: 0.5f),

            new AttackStats(rarity: Rarity.Epic, comboLength: 1, effectDuration: 0.1f),
            new AttackStats(rarity: Rarity.Epic, damage: 1),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, meleeSizeMultiplier: -0.2f),
            new AttackStats(rarity: Rarity.Legendary, activeDuration: 1, damage: 1),
        };

        List<AttackStats> ConsecrateUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),

            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Persistence+ 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Extend 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder Consecrate = new AttackBuilder()
            .SetAttackName("Consecrate")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Consecrate"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("The light shall burn you.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    is360: true,
                    damage: 2,
                    spread: 0.7f,
                    castTime: 2.5f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.25f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 1f,
                    meleeSpacerGap: 2.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.2f,
                    shakeRotation: 0.5f,
                    thrownDamage: 5,
                    thrownSpeed: 0.65f,
                    cantMove: true,
                    isStun: true,
                    stunDuration: 0.025f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_04"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_04")
            )
            .SetRarityUpgrades(ConsecrateRarity)
            .SetWeaponUpgrades(ConsecrateUpgrades);

        AddAttack(Consecrate);

        // ClassicRifle
        List<AttackStats> ClassicRifleRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, spreadMultiplier: -0.12f, speedMultiplier: 0.1f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 7),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.12f, pierce: 1, shakeStrength: 0.01f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 13, damage: 3),
            new AttackStats(rarity: Rarity.Legendary, spreadMultiplier: -0.2f, aimRangeAdditive: 2f, coneAngle: 30),
        };

        List<AttackStats> ClassicRifleUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };


        AttackBuilder ClassicRifle = new AttackBuilder()
            .SetAttackName("Classic Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Good ol' trusty rifle.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    is360: false,
                    coneAngle: 40,
                    damage: 8,
                    spread: 0.085f,
                    spray: 1.35f,
                    castTime: 2f,
                    range: 7f,
                    shotsPerAttack: 15,
                    speed: 0.17f,
                    knockback: 0.45f,
                    pierce: 1,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.01f,
                    shakeStrength: 0.02f,
                    shakeRotation: 0.01f,
                    thrownDamage: 10f,
                    thrownSpeed: 0.6f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/Burst_03"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/BurstRifle_02"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(ClassicRifleRarity)
            .SetWeaponUpgrades(ClassicRifleUpgrades);

        AddAttack(ClassicRifle);

        //double barrel
        List<AttackStats> DoubleBarrelRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.12f),

            new AttackStats(rarity: Rarity.Epic, damage: 8),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.25f, speedMultiplier: 0.2f, shakeStrength: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.5f, aimRangeAdditive: 1.5f, coneAngle: 30f),
            new AttackStats(rarity: Rarity.Legendary, multicastChance: 1f, multicastWaitTime: -0.05f),
        };

        List<AttackStats> DoubleBarrelUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
   
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),

        };

        AttackBuilder DoubleBarrel = new AttackBuilder()
            .SetAttackName("Double Barrel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("2 shots to the dome.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.6f,
                    is360: false,
                    coneAngle: 50,
                    damage: 9,
                    shotgunSpread: 45f,
                    spray: 0,
                    castTime: 1.6f,
                    range: 2.25f,
                    shotsPerAttack: 2,
                    speed: 0.18f,
                    knockback: 0.55f,
                    pierce: 8,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.4f,
                    shakeRotation: 0.05f,
                    thrownDamage: 7,
                    thrownSpeed: 0.6f
                   
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shotgun_double_1"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shotgun_double"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: BigMuzzleFlash
            )
            .SetRarityUpgrades(DoubleBarrelRarity)
            .SetWeaponUpgrades(DoubleBarrelUpgrades);
        AddAttack(DoubleBarrel);

        //drain scythe
        List<AttackStats> DrainScytheRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, meleeSpacerMultiplier: 0.4f, meleeSizeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, comboLength: 1),
            new AttackStats(rarity: Rarity.Epic, effectMultiplier: 0.6f),

            new AttackStats(rarity: Rarity.Legendary, shootOppositeSide: true),
            new AttackStats(rarity: Rarity.Legendary, effectDuration: 1f)
        };

        List<AttackStats> DrainScytheUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),

            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder DrainScythe = new AttackBuilder()
            .SetAttackName("Drain Scythe")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DrainScythe"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Why you walkin' so slow?")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.7f,
                    is360: false,
                    coneAngle: 75f,
                    damage: 5,
                    spread: 0.55f,
                    castTime: 2f,
                    knockback: 0.3f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1f,
                    meleeShotsScaleUp: -0.15f,
                    meleeSpacer: 2f,
                    meleeSpacerGap: 2.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.1f,
                    thrownDamage: 9f,
                    thrownSpeed: 0.4f,
                    isSlow: true,
                    slowPercentage: 0.5f,
                    slowDuration: 2f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/scythe_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Scythe_01")
            )
            .SetRarityUpgrades(DrainScytheRarity)
        .SetWeaponUpgrades(DrainScytheUpgrades);
        AddAttack(DrainScythe);

        //earth Shock
        List<AttackStats> EarthShockRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 2),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.2f),

            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1, spreadMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Rare, meleeSpacerGapMultiplier: 0.2f, aimRangeAdditive: 1f, coneAngle: 25),

            new AttackStats(rarity: Rarity.Rare, activeMultiplier: 0.5f),
            new AttackStats(rarity: Rarity.Rare, damage: 6),

        };

        List<AttackStats> EarthShockUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),

            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),

            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder EarthShock = new AttackBuilder()
            .SetAttackName("Earth Shock")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/EarthShock"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Shake the earth in a straight line.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 45f,
                    damage: 5,
                    spread: 0.3f,
                    castTime: 2f,
                    shotsPerAttackMelee: 2,
                    knockback: 0.30f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.3f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 2.2f,
                    meleeSpacerGap: 2.2f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.2f,
                    shakeRotation: 1f,
                    thrownDamage: 7f,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/MeleeSlam_01"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Final/MeleeSlam_01")
            )
            .SetRarityUpgrades(EarthShockRarity)
            .SetWeaponUpgrades(EarthShockUpgrades);
        AddAttack(EarthShock);



        //fire starter
        List<AttackStats> FireStarterRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.1f),

            new AttackStats(rarity: Rarity.Epic, damage: 6),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.3f, shotgunSpreadMultiplier: 0.2f, shakeStrength: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.4f, aimRangeAdditive: 1.5f, coneAngle: 30f),
            new AttackStats(rarity: Rarity.Legendary, effectMultiplier: 1f),
        };

        List<AttackStats> FireStarterUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Cindershot+ 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Homing 2"),
            AttackStatsLibrary.GetStat("Cindershot+ 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder FireStarter = new AttackBuilder()
            .SetAttackName("Fire Starter")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/FireStarterBullet"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Does as advertised.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 40,
                    damage: 4,
                    shotgunSpread: 50f,
                    spray: 0,
                    castTime: 1.8f,
                    range: 1.5f,
                    shotsPerAttack: 1,
                    speed: 0.03f,
                    knockback: 0f,
                    pierce: 99,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.4f,
                    shakeRotation: 0.05f,
                    thrownDamage: 4,
                    thrownSpeed: 0.6f,
                    isDoT: true,
                    dotDamage: 1.5f,
                    dotDuration: 3f,
                    dotTickRate: 1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/fireStarter_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/fireStarter_01"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing_Energy"),
                muzzleFlashPrefab: BigMuzzleFlash
            )
            .SetRarityUpgrades(FireStarterRarity)
            .SetWeaponUpgrades(FireStarterUpgrades);
        AddAttack(FireStarter);





        //GravityGrab
        List<AttackStats> GravityGrabRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, knockback: 0.8f, shakeStrength: 0.05f),
            new AttackStats(rarity: Rarity.Rare, effectMultiplier: 0.5f),

            new AttackStats(rarity: Rarity.Epic, damage: 4),
            new AttackStats(rarity: Rarity.Epic, meleeSpacer: 2.5f, meleeSpacerGap: 1.5f, aimRangeAdditive: 1.5f, coneAngle: 30f),

            new AttackStats(rarity: Rarity.Legendary, effectDuration: 1f, damage: 2),
            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, spreadMultiplier: 0.6f)
        };

        List<AttackStats> GravityGrabUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),

            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),


          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),

            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
           
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),

            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 2"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
        };

        AttackBuilder GravityGrab = new AttackBuilder()
            .SetAttackName("Eldritch Grasp")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/GravityGrab"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Pull enemies in, maybe too in.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.25f,
                    is360: false,
                    coneAngle: 75,
                    damage: 4,
                    spread: 0.4f,
                    castTime: 2f,
                    shotsPerAttackMelee: 0,
                    knockback: 0f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.1f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 1.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.3f,
                    shakeRotation: 1f,
                    thrownDamage: 1,
                    thrownSpeed: 0f,
                    isMagnet: true,
                    magnetStrength: 8f,
                    magnetDuration: 0.5f,
                    isSlow: true,
                    slowPercentage: 0.25f,
                    slowDuration: 1.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GrabHand_Dark_01"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Final/GrabHand_Dark_01")
            )
            .SetRarityUpgrades(GravityGrabRarity)
                        .SetWeaponUpgrades(GravityGrabUpgrades);

        AddAttack(GravityGrab);

        // GatlingGun
        List<AttackStats> GatlingGunRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 20),
            new AttackStats(rarity: Rarity.Rare, projectileSizeMultiplier: 0.1f, speedMultiplier: 0.12f),

            new AttackStats(rarity: Rarity.Epic, damage: 1),
            new AttackStats(rarity: Rarity.Epic, rangeMultiplier: 0.3f, aimRangeAdditive: 1f, coneAngle: 25f),

            new AttackStats(rarity: Rarity.Legendary, spreadMultiplier: -0.25f, sprayMultiplier: 0.2f),
            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 20),
        };

        List<AttackStats> GatlingGunUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Homing 2"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder GatlingGun = new AttackBuilder()
            .SetAttackName("Gatling Gun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Tiny"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Spray 'n pray.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 45f,
                    damage: 3,
                    spread: 0.016f,
                    spray: 0.3f,
                    castTime: 2.2f,
                    range: 5.5f,
                    shotsPerAttack: 60,
                    speed: 0.2f,
                    knockback: 0.32f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.01f,
                    shakeStrength: 0.01f,
                    shakeRotation: 0.01f,
                    thrownDamage: 16f,
                    thrownSpeed: 0.2f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/gatling_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Gatling_02"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(GatlingGunRarity)
                    .SetWeaponUpgrades(GatlingGunUpgrades);

        AddAttack(GatlingGun);

        //GodHand
        List<AttackStats> GodHandRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 5),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 1.5f, meleeSpacerGap: 1f, aimRangeAdditive: 2f),

            new AttackStats(rarity: Rarity.Epic, castTimeMultiplier: -0.25f),
            new AttackStats(rarity: Rarity.Epic, comboLength: 1),

            new AttackStats(rarity: Rarity.Legendary, damage: 10),
            new AttackStats(rarity: Rarity.Legendary, meleeSizeMultiplier: 0.15f, knockbackMultiplier: 0.3f, shakeTime: 0.05f, shakeRotation: 0.5f),
        };

        List<AttackStats> GodHandUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder GodHand = new AttackBuilder()
            .SetAttackName("God Hand")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/MeleeFist"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Violence is the answer.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    is360: false,
                    coneAngle: 65f,
                    damage: 12,
                    spread: 0.6f,
                    castTime: 2f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.7f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 2f,
                    meleeSpacerGap: 1.5f,
                    shakeTime: 0.15f,
                    shakeStrength: 0.7f,
                    shakeRotation: 0.5f,
                    thrownDamage: 1,
                    thrownSpeed: 0f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GodHand_01"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Final/GodHand_01")
            )
            .SetRarityUpgrades(GodHandRarity)
                    .SetWeaponUpgrades(GodHandUpgrades);

        AddAttack(GodHand);

        // ImpactGrenade
        List<AttackStats> ImpactGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.5f, shakeRotation: 0.3f),

            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.3f),
            new AttackStats(rarity: Rarity.Epic, damage: 5, aimRange: 1f, coneAngle: 20f),

            new AttackStats(rarity: Rarity.Legendary, isDoT: true, dotDamage: 5, dotDuration: 5f, dotTickRate: 1.5f),
            new AttackStats(rarity: Rarity.Legendary, range: 1.5f, pierce: 10, aimRangeAdditive: 1f),
        };

        List<AttackStats> ImpactGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder ImpactGrenade = new AttackBuilder()
            .SetAttackName("Impact Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_impact"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Explode on impact.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.6f,
                    is360: false,
                    coneAngle: 20f,
                    damage: 10,
                    spread: 1f,
                    spray: 0f,
                    castTime: 2f,
                    range: 1.8f,
                    shotsPerAttack: 1,
                    speed: 0.08f,
                    knockback: 0.4f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.15f,
                    shakeRotation: 0.1f,
                    thrownDamage: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_frag"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(ImpactGrenadeRarity)
            .SetWeaponUpgrades(ImpactGrenadeUpgrades);
        AddAttack(ImpactGrenade);

        // ImpactMine
        List<AttackStats> ImpactMineRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.5f, shakeRotation: 0.3f),

            new AttackStats(rarity: Rarity.Epic, damage: 5),
            new AttackStats(rarity: Rarity.Epic, spreadMultiplier: -0.25f, effectDuration: 1f),

            new AttackStats(rarity: Rarity.Legendary, isMagnet: true, magnetDuration: 0.5f, magnetStrength: 4f),
            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.2f),
        };

        List<AttackStats> ImpactMineUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder ImpactMine = new AttackBuilder()
            .SetAttackName("Impact Mine")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/mine_impact"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Lure 'em in and watch 'em fly.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 0.5f,
                    is360: true,
                    damage: 12,
                    spread: 1.6f,
                    spray: 0f,
                    castTime: 2.25f,
                    range: 10f,
                    shotsPerAttack: 1,
                    speed: 0f,
                    knockback: 0.3f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.15f,
                    shakeRotation: 0.1f,
                    thrownDamage: 1f,
                    thrownSpeed: 0f,
                    isSlow: true,
                    slowPercentage: 0.5f,
                    slowDuration: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/mine_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Mine_01"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(ImpactMineRarity)
                    .SetWeaponUpgrades(ImpactMineUpgrades);

        AddAttack(ImpactMine);

        //ImpactNova
        List<AttackStats> ImpactNovaRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 3f, aimRangeAdditive: 1.5f),

            new AttackStats(rarity: Rarity.Epic, damage: 3),
            new AttackStats(rarity: Rarity.Epic, activeDuration: 0.5f),

            new AttackStats(rarity: Rarity.Legendary, meleeSizeMultiplier: 0.2f, shakeRotation: 0.5f),
            new AttackStats(rarity: Rarity.Legendary, comboLength: 1),
        };

        List<AttackStats> ImpactNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),

        };

        AttackBuilder ImpactNova = new AttackBuilder()
            .SetAttackName("Impact Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_impact"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Do I have to aim? No?")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    is360: true,
                    damage: 9,
                    spread: 0.4f,
                    castTime: 2.2f,
                    knockback: 0.6f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.5f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 0f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.1f,
                    thrownDamage: 7f,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_01")
            )
            .SetRarityUpgrades(ImpactNovaRarity)
                    .SetWeaponUpgrades(ImpactNovaUpgrades);
        AddAttack(ImpactNova);

        //LaserBeam
        List<AttackStats> LaserBeamRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 5),
            new AttackStats(rarity: Rarity.Rare, meleeSpacerGapMultiplier: 0.25f, meleeSpacerMultiplier: 0.4f, aimRangeAdditive: 1.5f),

            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1, spreadMultiplier: -0.2f),
            new AttackStats(rarity: Rarity.Rare, meleeShotsScaleUp: 0.15f, coneAngle: 60f),

            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.2f, meleeSizeMultiplier: 0.25f, damage: 5),
            new AttackStats(rarity: Rarity.Rare, knockbackMultiplier: 0.5f, shakeTime: 0.05f, shakeRotation: 0.3f),
        };

        List<AttackStats> LaserBeamUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
        };
        AttackBuilder LaserBeam = new AttackBuilder()
            .SetAttackName("Laser Beam")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DoubleBeam"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Firin' mah lazer or whatever.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.2f,
                    is360: false,
                    coneAngle: 50f,
                    damage: 11,
                    spread: 0.35f,
                    castTime: 1.7f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.65f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 4f,
                    meleeSpacerGap: 3f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.5f,
                    thrownDamage: 1,
                    thrownSpeed: 0f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/gunFlash1_0"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/gunFlash1_0")
            )
            .SetRarityUpgrades(LaserBeamRarity)
                    .SetWeaponUpgrades(LaserBeamUpgrades);

        AddAttack(LaserBeam);

        // PainWheel
        List<AttackStats> PainWheelRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, activeDuration: 1),

            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1, speedMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.5f, shakeRotation: 0.4f),

            new AttackStats(rarity: Rarity.Rare, projectileSizeMultiplier: 0.25f, damage: 3, shakeStrength: 0.05f),
            new AttackStats(rarity: Rarity.Rare, speedMultiplier: -0.2f, rangeMultiplier: 0.2f, aimRangeAdditive: 1.5f, coneAngle: 45f),
        };

        List<AttackStats> PainWheelUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
  
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence+ 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Homing 2"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder PainWheel = new AttackBuilder()
            .SetAttackName("Pain Wheel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Keep on spinnin'")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.4f,
                    is360: false,
                    coneAngle: 35,
                    damage: 6,
                    spread: 0.85f,
                    spray: 0f,
                    castTime: 2f,
                    range: 3f,
                    shotsPerAttack: 1,
                    speed: 0.15f,
                    knockback: 0f,
                    pierce: 35,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.1f,
                    thrownDamage: 13f,
                    thrownSpeed: 0.6f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shuriken_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shuriken_1"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(PainWheelRarity)
                    .SetWeaponUpgrades(PainWheelUpgrades);

        AddAttack(PainWheel);

        //PetrifyGrenade
        List<AttackStats> PetrifyGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, rangeMultiplier: 0.5f, aimRangeAdditive: 1f, pierce: 5),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, aimRange: 1, coneAngle: 45f),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.1f, shotgunSpread: 15, shakeStrength: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, isSlow: true, slowPercentage: 0.5f, slowDuration: 7f),
            new AttackStats(rarity: Rarity.Legendary, damage: 4),

        };

        List<AttackStats> PetrifyGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 2"),

        };

        AttackBuilder PetrifyGrenade = new AttackBuilder()
            .SetAttackName("Petrify Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_stun"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Freeze targets in place.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    is360: false,
                    coneAngle: 30f,
                    damage: 3,
                    shotgunSpread: 65f,
                    spray: 0,
                    castTime: 1.7f,
                    range: 1.5f,
                    shotsPerAttack: 1,
                    speed: 0.1f,
                    knockback: 0f,
                    pierce: 2,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.05f,
                    thrownDamage: 1,
                    isStun: true,
                    stunDuration: 4f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_shock"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(PetrifyGrenadeRarity)
                    .SetWeaponUpgrades(PetrifyGrenadeUpgrades);
        AddAttack(PetrifyGrenade);

        //PetrifyNova
        List<AttackStats> PetrifyNovaRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 2),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 2.5f, aimRangeAdditive: 1f),

            new AttackStats(rarity: Rarity.Epic, comboLength: 1, comboWaitTimeMultiplier: 0.25f),
            new AttackStats(rarity: Rarity.Epic, effectDuration: 1.5f),

            new AttackStats(rarity: Rarity.Legendary, meleeSizeMultiplier: 0.2f, shakeRotation: 0.5f),
            new AttackStats(rarity: Rarity.Legendary, activeDuration: 0.3f),
        };

        List<AttackStats> PetrifyNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder PetrifyNova = new AttackBuilder()
            .SetAttackName("Petrify Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_Petrify"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Freeze!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    is360: true,
                    damage: 3,
                    spread: 0.4f,
                    castTime: 2.3f,
                    knockback: 0f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.2f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 2f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.4f,
                    thrownDamage: 9f,
                    thrownSpeed: 0.5f,
                    isStun: true,
                    stunDuration: 3f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_03"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_03")
            )
            .SetRarityUpgrades(PetrifyNovaRarity)
                    .SetWeaponUpgrades(PetrifyNovaUpgrades);

        AddAttack(PetrifyNova);

        // Revolver
        List<AttackStats> PlasmaPistolRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3, pierce: 5),
            new AttackStats(rarity: Rarity.Rare, speedMultiplier: 0.2f, rangeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, chainTimes: 1, chainStatDecayPercent: 0.20f),
            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shakeStrength: 0.05f),

            new AttackStats(rarity: Rarity.Legendary, damage: 5, spread: -0.15f, pierce: 5),
            new AttackStats(rarity: Rarity.Legendary, chainStatDecayPercent: 0.1f, chainRange: 6, chainSpeed: 20f),
        };

        List<AttackStats> PlasmaPistolUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
        };

        AttackBuilder PlasmaPistol = new AttackBuilder()
            .SetAttackName("Plasma Pistol")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/PlasmaBullet"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Lightning in your pocket.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.2f,
                    is360: false,
                    coneAngle: 30f,
                    damage: 9,
                    spread: 0.5f,
                    spray: 3f,
                    castTime: 2f,
                    range: 5f,
                    shotsPerAttack: 1,
                    speed: 0.15f,
                    knockback: 0.25f,
                    pierce: 8,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 3f,
                    thrownSpeed: 0.65f,
                    isChain: true,
                    chainTimes: 4,
                    chainStatDecayPercent: 0.30f,
                    chainRange: 4.5f,
                    chainSpeed: 15f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/PlasmaWhip_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/PlasmaWhip_01"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(PlasmaPistolRarity)
                    .SetWeaponUpgrades(PlasmaPistolUpgrades);
        AddAttack(PlasmaPistol);


        // Revolver
        List<AttackStats> RevolverRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, pierce: 2, speedMultiplier: 0.2f, rangeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 3, pierce: 2),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.3f, shakeStrength: 0.05f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 3, damage: 5, spreadMultiplier: -0.25f),
            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.2f, sprayMultiplier: 0.25f, aimRangeAdditive: 1f, coneAngle: 30f),
        };

        List<AttackStats> RevolverUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder Revolver = new AttackBuilder()
            .SetAttackName("Revolver")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Magnum"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("6 shots- For now.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 35f,
                    damage: 10,
                    spread: 0.42f,
                    spray: 1.8f,
                    castTime: 1.65f,
                    range: 5f,
                    shotsPerAttack: 6,
                    speed: 0.18f,
                    knockback: 0.5f,
                    pierce: 2,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 6f,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/pistol_magnum_1"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/pistol_magnum"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(RevolverRarity)
                    .SetWeaponUpgrades(RevolverUpgrades);

        AddAttack(Revolver);

        //Shotgun
        List<AttackStats> ShotgunRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 4),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.35f, shotgunSpreadMultiplier: 0.10f, shakeTime: 0.05f),

            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.2f, multicastChance: 0.5f, shakeStrength: 0.05f),
            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shotgunSpreadMultiplier: 0.15f, coneAngle: 30),

            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.25f, speedMultiplier: 0.25f, aimRangeAdditive: 1f),
            new AttackStats(rarity: Rarity.Legendary, pierce: 10, damage: 6),
        };

        List<AttackStats> ShotgunUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };


        AttackBuilder Shotgun = new AttackBuilder()
            .SetAttackName("Shotgun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Up close and personal.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    damage: 10,
                    is360: false,
                    coneAngle: 65f,
                    shotgunSpread: 75f,
                    spray: 0,
                    castTime: 2f,
                    range: 3f,
                    shotsPerAttack: 4,
                    speed: 0.15f,
                    knockback: 0.55f,
                    pierce: 10,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.35f,
                    shakeRotation: 0.05f,
                    thrownDamage: 8,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shotgun_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shotgun_01"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: BigMuzzleFlash
            )
            .SetRarityUpgrades(ShotgunRarity)
            .SetWeaponUpgrades(ShotgunUpgrades);
        AddAttack(Shotgun);

        //Shuriken
        List<AttackStats> ShurikenRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 2),
            new AttackStats(rarity: Rarity.Rare, activeDuration: 1),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shotgunSpread: 25f, aimRangeAdditive: 1, coneAngle: 30),
            new AttackStats(rarity: Rarity.Epic, pierce: 10),

            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.35f, pierce: 15, shakeStrength: 0.2f),
            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.5f, speedMultiplier: -0.25f, damage: 2),
        };

        List<AttackStats> ShurikenUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence+ 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Homing 2"),
            AttackStatsLibrary.GetStat("Chain 3"),

        };

        AttackBuilder Shuriken = new AttackBuilder()
            .SetAttackName("Shuriken")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken_small"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Dattebayo!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.6f,
                    is360: false,
                    coneAngle: 60f,
                    damage: 3,
                    shotgunSpread: 100f,
                    spray: 0,
                    castTime: 1.6f,
                    range: 2.2f,
                    shotsPerAttack: 3,
                    speed: 0.085f,
                    knockback: 0f,
                    pierce: 15,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.01f,
                    shakeRotation: 0.01f,
                    thrownDamage: 5,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shuriken_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shuriken_2"),
                muzzleFlashPrefab: BigMuzzleFlash
            )
            .SetRarityUpgrades(ShurikenRarity)
                    .SetWeaponUpgrades(ShurikenUpgrades);

        AddAttack(Shuriken);

        // SMG
        List<AttackStats> SMGRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, rangeMultiplier: 0.25f, aimRangeAdditive: 1f, coneAngle: 30),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 10),
            new AttackStats(rarity: Rarity.Epic, spreadMultiplier: -0.15f, sprayMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 20, spreadMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Legendary, damage: 3, knockback: 0.25f, shakeRotation: 0.01f),
        };

        List<AttackStats> SMGUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),


            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),

        };

        AttackBuilder SMG = new AttackBuilder()
            .SetAttackName("SMG")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_SMG"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Lil' Uzi Vertical")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.6f,
                    is360: false,
                    coneAngle: 40f,
                    damage: 4,
                    spread: 0.04f,
                    spray: 1.4f,
                    castTime: 1.5f,
                    range: 5f,
                    shotsPerAttack: 30,
                    speed: 0.15f,
                    knockback: 0.35f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.01f,
                    shakeStrength: 0.01f,
                    shakeRotation: 0f,
                    thrownDamage: 6f,
                    thrownSpeed: 0.6f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/smg_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/SMG_01"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(SMGRarity)
        .SetWeaponUpgrades(SMGUpgrades);
        AddAttack(SMG);

        // Smoke Grenade
        List<AttackStats> SmokeGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, activeDuration: 1),
            new AttackStats(rarity: Rarity.Rare, effectMultiplier: 0.5f, damage: 1),

            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.2f),
            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shotgunSpread: 20),

            new AttackStats(rarity: Rarity.Legendary, effectDuration: 2f, activeDuration: 1),
            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, shotgunSpread: 40),
        };

        List<AttackStats> SmokeGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Concussive 1"),


            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),

        };

        AttackBuilder SmokeGrenade = new AttackBuilder()
            .SetAttackName("Smoke Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_slow"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Hotbox the world.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    is360: true,
                    damage: 2,
                    shotgunSpread: 50f,
                    castTime: 2.1f,
                    range: 1.75f,
                    shotsPerAttack: 1,
                    speed: 0.08f,
                    knockback: 0f,
                    pierce: 5,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.1f,
                    thrownDamage: 1,
                    isSlow: true,
                    slowPercentage: 0.25f,
                    slowDuration: 2f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_smoke"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_smoke"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SmokeGrenadeRarity)
                    .SetWeaponUpgrades(SmokeGrenadeUpgrades);

        AddAttack(SmokeGrenade);

        // Sniper
        List<AttackStats> SniperRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.12f),

            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1, damage: 5),
            new AttackStats(rarity: Rarity.Rare, aimRangeAdditive: 2f, coneAngle: 30f),

            new AttackStats(rarity: Rarity.Rare, projectileSizeMultiplier: 0.25f, pierce: 15, shakeRotation: 0.1f),
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1, spreadMultiplier: -0.15f)
        };

        List<AttackStats> SniperUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Homing 2"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
        };

        AttackBuilder Sniper = new AttackBuilder()
            .SetAttackName("Sniper Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_long"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Cheers, mate.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    damage: 18,
                    is360: false,
                    coneAngle: 20f,
                    spread: 0.65f,
                    spray: 1.5f,
                    sprayThreshold: 1,
                    castTime: 1.9f,
                    range: 8f,
                    shotsPerAttack: 1,
                    speed: 0.2f,
                    knockback: 0.8f,
                    pierce: 10,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.4f,
                    shakeRotation: 0.1f,
                    thrownDamage: 10f,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/longshot_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/LongShotRifle_02"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SniperRarity)
                    .SetWeaponUpgrades(SniperUpgrades);

        AddAttack(Sniper);

        // SuctionCannon
        List<AttackStats> SuctionCannonRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 2),
            new AttackStats(rarity: Rarity.Rare, effectMultiplier: 0.4f),

            new AttackStats(rarity: Rarity.Epic, speedMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Epic, rangeMultiplier: 0.1f, aimRangeAdditive: 1f, coneAngle: 20f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Legendary, damage: 2, effectDuration: 1f),
        };

        List<AttackStats> SuctionCannonUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
          
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Homing 2"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder SuctionCannon = new AttackBuilder()
            .SetAttackName("Suction Cannon")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/SuctionCannon_Orb"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Fire a mini-black hole that sucks targets in.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    damage: 2,
                    is360: false,
                    coneAngle: 40f,
                    spread: 1.2f,
                    spray: 0f,
                    castTime: 2.2f,
                    range: 3.5f,
                    shotsPerAttack: 1,
                    speed: 0.018f,
                    knockback: 0f,
                    pierce: 30,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 21f,
                    thrownSpeed: 0.12f,
                    isMagnet: true,
                    magnetStrength: 5,
                    magnetDuration: 1.15f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GravCannon_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/gravCannon_01"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing_Energy"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SuctionCannonRarity)
                    .SetWeaponUpgrades(SuctionCannonUpgrades);

        AddAttack(SuctionCannon);

        // SuctionGrenade
        List<AttackStats> SuctionGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, speedMultiplier: 0.3f, rangeMultiplier: 0.2f, pierce: 10, aimRangeAdditive: 1f, coneAngle: 30),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.55f, shakeStrength: 0.1f),

            new AttackStats(rarity: Rarity.Epic, damage: 4),
            new AttackStats(rarity: Rarity.Epic, activeDuration: 0.5f, effectMultiplier: 0.25f, effectDuration: 0.5f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.15f, shakeRotation: 0.1f),
        };

        List<AttackStats> SuctionGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
   
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence+ 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder SuctionGrenade = new AttackBuilder()
            .SetAttackName("Suction Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_suction"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Explodes then sucks targets into the center.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 25f,
                    damage: 5,
                    spread: 1.5f,
                    castTime: 2f,
                    range: 1.5f,
                    shotsPerAttack: 1,
                    speed: 0.1f,
                    knockback: 0f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.05f,
                    thrownDamage: 1,
                    isMagnet: true,
                    magnetStrength: 7,
                    magnetDuration: 1.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_magnet"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_magnet"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SuctionGrenadeRarity)
                    .SetWeaponUpgrades(SuctionGrenadeUpgrades);
        AddAttack(SuctionGrenade);

        //SuctionNova
        List<AttackStats> SuctionNovaRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 2),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.15f, effectMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, meleeSizeMultiplier: 0.15f, shakeRotation: 0.1f),
            new AttackStats(rarity: Rarity.Epic, meleeSpacer: 3.5f, aimRangeAdditive: 2f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, damage: 4),
            new AttackStats(rarity: Rarity.Legendary, effectDuration: 1f),
        };

        List<AttackStats> SuctionNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Hacker 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder SuctionNova = new AttackBuilder()
            .SetAttackName("Suction Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_gravity"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("GET OVER HERE!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    is360: true,
                    damage: 3,
                    spread: 0.35f,
                    castTime: 2.1f,
                    knockback: 0.30f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.2f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 1f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.5f,
                    thrownDamage: 11f,
                    thrownSpeed: 0.6f,
                    isMagnet: true,
                    magnetStrength: 4,
                    magnetDuration: 1,
                    isSlow: true,
                    slowPercentage: 0.2f,
                    slowDuration: 1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_02")
            )
            .SetRarityUpgrades(SuctionNovaRarity)
        .SetWeaponUpgrades(SuctionNovaUpgrades);
        AddAttack(SuctionNova);

        //WindBlade
        List<AttackStats> WindBladeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, comboWaitTime: -0.2f, shakeRotation: 0.1f),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 1.5f, coneAngle: 30f, aimRangeAdditive: 1f),

            new AttackStats(rarity: Rarity.Epic, comboLength: -1, damage: 8, meleeSizeMultiplier: 0.15f),
            new AttackStats(rarity: Rarity.Epic, comboWaitTimeMultiplier: 0.5f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttackMelee: 1, shakeStrength: 0.02f),
            new AttackStats(rarity: Rarity.Legendary, spreadMultiplier: -0.2f),

        };

        List<AttackStats> WindBladeUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
        };

        AttackBuilder WindBlade = new AttackBuilder()
            .SetAttackName("Wind Blade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/KatanaSlash"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Power of God and Anime.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.7f,
                    damage: 8,
                    is360: false,
                    coneAngle: 75f,
                    spread: 0.3f,
                    castTime: 1.85f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.35f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 3,
                    comboWaitTime: 0.9f,
                    meleeSpacer: 2f,
                    meleeSpacerGap: 1.7f,
                    shakeTime: 0.08f,
                    shakeStrength: 0.12f,
                    shakeRotation: 0.1f,
                    thrownDamage: 5,
                    thrownSpeed: 0.6f,
                    swapAnimOnAttack: true
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/katana_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/katana_02")
            )
            .SetRarityUpgrades(WindBladeRarity)
                    .SetWeaponUpgrades(WindBladeUpgrades);

        AddAttack(WindBlade);

        isInitialized = true;
    }

    public static AttackBuilder GetAttackBuilder(string attackName)
    {
        if (attackBuilderDictionary.ContainsKey(attackName))
        {
            return attackBuilderDictionary[attackName];
        }
        else
        {
            return null;
        }
    }

    public static Attack[] GetAttacks()
    {
        return attackBuilderDictionary.Values.Select(x => x.Build(0)).ToArray();
    }

    public static AttackBuilder[] getAttackBuilders()
    {
        return attackBuilderDictionary.Values.ToArray();
    }
}
