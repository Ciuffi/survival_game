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

        // AcidPool
        List<AttackStats> AcidPoolRarity = new List<AttackStats>
        {
            new AttackStats(damage: 2),
            new AttackStats(comboLength: 1, comboWaitTime: 0.25f),
            new AttackStats(shotsPerAttack: 1, meleeShotsScaleUp: -0.15f),
            new AttackStats(meleeSizeMultiplier: 0.3f, shakeTime: 0.05f),
            new AttackStats(meleeSpacer: 2f, aimRangeAdditive: 2f),
            new AttackStats(meleeSpacer: 1.5f, isCone: true, coneAngle: 120, aimRangeAdditive: 1.5f)
        };

        List<AttackStats> AcidPoolUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),

            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),

            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
 

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),

            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder AcidPool = new AttackBuilder()
            .SetAttackName("Acid Pool")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/AcidPool"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("GIT OUT MAH SWAMP!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 1,
                    spread: 0.3f,
                    castTime: 2.2f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.6f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 1.5f,
                    meleeSpacerGap: 4.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.2f,
                    shakeRotation: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/hand2"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/hand2")
            )
            .SetRarityUpgrades(AcidPoolRarity)
            .SetWeaponUpgrades(AcidPoolUpgrades);


        AddAttack(AcidPool);

        // ClassicRifle
        List<AttackStats> ClassicRifleRarity = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(shotsPerAttack: 7),
            new AttackStats(shotsPerAttack: 7),
            new AttackStats(spread: -0.04f, speedMultiplier: 0.2f),
            new AttackStats(projectileSizeMultiplier: 0.4f, shakeStrength: 0.01f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 30),
        };

        List<AttackStats> ClassicRifleUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        Debug.Log(
            $"Weapon Upgrades : {string.Join(", ", ClassicRifleUpgrades.Select(x => x == null ? "null" : x.ToString()))}"
        );

        AttackBuilder ClassicRifle = new AttackBuilder()
            .SetAttackName("Classic Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Good ol' trusty rifle.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.75f,
                    isCone: true,
                    coneAngle: 45,
                    damage: 6,
                    spread: 0.085f,
                    spray: 2f,
                    castTime: 2.1f,
                    range: 7f,
                    shotsPerAttack: 14,
                    speed: 0.17f,
                    knockback: 0.45f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.01f,
                    shakeStrength: 0.02f,
                    shakeRotation: 0.01f,
                    thrownDamage: 10f,
                    throwSpeed: 0.6f
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
            new AttackStats(damage: 9),
            new AttackStats(castTime: -0.3f, multicastWaitTime: -0.1f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(projectileSizeMultiplier: 0.3f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f, aimRangeAdditive: 1.5f),
            new AttackStats(aimRangeAdditive: 1.7f, coneAngle: 40f, shotgunSpread: 15)
        };

        List<AttackStats> DoubleBarrelUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder DoubleBarrel = new AttackBuilder()
            .SetAttackName("Double Barrel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("2 shots to the dome.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    isCone: true,
                    coneAngle: 60,
                    damage: 9,
                    shotgunSpread: 40f,
                    spray: 0,
                    castTime: 1.7f,
                    range: 2.5f,
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
                    throwSpeed: 0.6f
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
            .SetRarityUpgrades(DoubleBarrelUpgrades);
        AddAttack(DoubleBarrel);

        //drain scythe
        List<AttackStats> DrainScytheRarity = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(comboLength: 1, comboWaitTime: 0.4f),
            new AttackStats(shotsPerAttack: 1, meleeSizeMultiplier: -0.25f),
            new AttackStats(meleeSpacer: 2.5f, aimRangeAdditive: 2f),
            new AttackStats(thrownDamage: 12, throwSpeed: 0.3f),
            new AttackStats(coneAngle: 90f)
        };

        List<AttackStats> DrainScytheUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),

            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),

            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),

 

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
                        AttackStatsLibrary.GetStat("Knockback 3"),

            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
                        AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder DrainScythe = new AttackBuilder()
            .SetAttackName("Drain Scythe")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DrainScythe"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Slows enemies on hit.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    isCone: true,
                    coneAngle: 120f,
                    damage: 4,
                    spread: 0.2f,
                    castTime: 2.2f,
                    knockback: 0.3f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.8f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 2f,
                    meleeSpacerGap: 3.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.1f,
                    thrownDamage: 9f,
                    throwSpeed: 0.4f
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
            new AttackStats(damage: 3),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1, comboWaitTime: 0.5f),
            new AttackStats(shotsPerAttack: 1, spread: -0.1f),
            new AttackStats(meleeShotsScaleUp: 0.1f, shakeTime: 0.05f),
            new AttackStats(meleeSpacerMultiplier: 0.5f, aimRangeAdditive: 2f, coneAngle: 30),
        };

        List<AttackStats> EarthShockUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),

            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
                        AttackStatsLibrary.GetStat("Glattt 2"),


            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
                        AttackStatsLibrary.GetStat("Knockback 3"),
                        AttackStatsLibrary.GetStat("Glattt 3"),

            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
                        AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder EarthShock = new AttackBuilder()
            .SetAttackName("Earth Shock")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Frogger"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Shake the earth in a straight line.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.2f,
                    isCone: true,
                    coneAngle: 45f,
                    damage: 3,
                    spread: 0.22f,
                    castTime: 2.1f,
                    shotsPerAttackMelee: 2,
                    knockback: 0.35f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.3f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 1.5f,
                    meleeSpacerGap: 3f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.2f,
                    shakeRotation: 1f,
                    thrownDamage: 7f,
                    throwSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/frog_gun_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/frog_01")
            )
            .SetRarityUpgrades(EarthShockRarity)
            .SetWeaponUpgrades(EarthShockUpgrades);
        AddAttack(EarthShock);

        //GravityGrab
        List<AttackStats> GravityGrabRarity = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1, comboWaitTime: 0.4f),
            new AttackStats(knockback: 0.6f, shakeStrength: 0.1f),
            new AttackStats(shotsPerAttackMelee: 1, damage: -2, shakeTime: 0.05f),
            new AttackStats(meleeSpacer: 2f, aimRangeAdditive: 2f, coneAngle: 45f)
        };

        List<AttackStats> GravityGrabUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),

            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),

            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
 

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),

            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 2"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder GravityGrab = new AttackBuilder()
            .SetAttackName("Eldritch Grasp")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/GravityGrab"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Pull enemies in, maybe too in.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    isCone: true,
                    coneAngle: 75,
                    damage: 3,
                    spread: 0.3f,
                    castTime: 2f,
                    shotsPerAttackMelee: 0,
                    knockback: 0f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.6f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 2.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.3f,
                    shakeRotation: 1f,
                    thrownDamage: 0,
                    throwSpeed: 0f
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
            new AttackStats(shotsPerAttack: 20),
            new AttackStats(shotsPerAttack: 20),
            new AttackStats(spread: -0.004f, spray: 10f),
            new AttackStats(projectileSizeMultiplier: 0.25f, knockbackMultiplier: 0.25f),
            new AttackStats(shotsPerAttack: 20),
            new AttackStats(rangeMultiplier: 0.2f, aimRangeAdditive: 2f, coneAngle: 30f)
        };

        List<AttackStats> GatlingGunUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder GatlingGun = new AttackBuilder()
            .SetAttackName("Gatling Gun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Tiny"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Spray 'n pray.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.25f,
                    isCone: true,
                    coneAngle: 45f,
                    damage: 3,
                    spread: 0.018f,
                    spray: 10f,
                    castTime: 2.4f,
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
                    thrownDamage: 22f,
                    throwSpeed: 0.2f
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
            .SetRarityUpgrades(GatlingGunUpgrades);
        AddAttack(GatlingGun);

        //GodHand
        List<AttackStats> GodHandRarity = new List<AttackStats>
        {
            new AttackStats(damage: 10),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1, meleeShotsScaleUp: -0.1f),
            new AttackStats(meleeSpacer: 1.25f, meleeSpacerGap: 1.5f, aimRangeAdditive: 2f),
            new AttackStats(meleeSizeMultiplier: 0.3f, knockback: 0.3f, shakeTime: 0.05f, shakeRotation: 0.5f),
        };

        List<AttackStats> GodHandUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),

            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
                        AttackStatsLibrary.GetStat("Quick Hands 2"),


            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
                        AttackStatsLibrary.GetStat("Knockback 3"),
                        AttackStatsLibrary.GetStat("Quick Hands 3"),

            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder GodHand = new AttackBuilder()
            .SetAttackName("God Hand")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/MeleeFist"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Violence is the answer.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    isCone: true,
                    coneAngle: 120f,
                    damage: 12,
                    spread: 0.28f,
                    castTime: 2.1f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.7f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.5f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 2f,
                    meleeSpacerGap: 2f,
                    shakeTime: 0.15f,
                    shakeStrength: 0.7f,
                    shakeRotation: 0.5f,
                    thrownDamage: 0,
                    throwSpeed: 0f
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
            new AttackStats(castTime: 1f, damage: 10, projectileSizeMultiplier: 0.2f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(projectileSizeMultiplier: 0.3f, shakeStrength: 0.15f),
            new AttackStats(knockback: 0.7f, shakeRotation: 0.3f),
            new AttackStats(range: 2f, aimRangeAdditive: 2f),
            new AttackStats(isCone: true, coneAngle: 30f, pierce: 1)
        };

        List<AttackStats> ImpactGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder ImpactGrenade = new AttackBuilder()
            .SetAttackName("Impact Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_impact"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Explode on impact.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 2,
                    spread: 0.6f,
                    spray: 0f,
                    castTime: 2.5f,
                    range: 2f,
                    shotsPerAttack: 1,
                    speed: 0.08f,
                    knockback: 0f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.15f,
                    shakeRotation: 0.1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_frag"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(ImpactGrenadeRarity)
            .SetRarityUpgrades(ImpactGrenadeUpgrades);
        AddAttack(ImpactGrenade);

        // ImpactMine
        List<AttackStats> ImpactMineRarity = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.5f),
            new AttackStats(castTime: 1f, damage: 10, projectileSizeMultiplier: 0.2f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(knockback: 0.5f, shakeRotation: 0.3f),
            new AttackStats(pierce: 5, spread: 0.3f)
        };

        List<AttackStats> ImpactMineUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder ImpactMine = new AttackBuilder()
            .SetAttackName("Impact Mine")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/mine_impact"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Lure 'em in and watch 'em fly.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 0.5f,
                    damage: 5,
                    spread: 0.75f,
                    spray: 0f,
                    castTime: 2.5f,
                    range: 0f,
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
                    thrownDamage: 0f,
                    throwSpeed: 0f
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
            new AttackStats(damage: 9),
            new AttackStats(comboLength: 1, damage: -4),
            new AttackStats(meleeSizeMultiplier: 0.4f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2.5f, aimRangeAdditive: 2f, isCone: true, coneAngle: 45),
            new AttackStats(meleeShotsScaleUp: 0.12f),
            new AttackStats(aimRangeAdditive: 1.5f, meleeSpacer: 1.5f)
        };

        List<AttackStats> ImpactNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),

            AttackStatsLibrary.GetStat("Extend 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),

            AttackStatsLibrary.GetStat("Extend 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder ImpactNova = new AttackBuilder()
            .SetAttackName("Impact Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_impact"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Do I actually have to aim? No?")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 13,
                    spread: 0.35f,
                    castTime: 2.2f,
                    knockback: 0.6f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.75f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 0f,
                    meleeSpacerGap: 0f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.1f,
                    thrownDamage: 9f,
                    throwSpeed: 0.5f
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
            new AttackStats(damage: 5),
            new AttackStats(meleeSpacerGapMultiplier: 0.4f, meleeSpacerMultiplier: 0.4f, aimRangeAdditive: 1.5f),
            new AttackStats(shotsPerAttack: 1, spread: -0.1f),
            new AttackStats(castTime: 0.3f, meleeSizeMultiplier: 0.3f, damage: 10),
            new AttackStats(knockback: 0.8f, shakeTime: 0.05f, shakeRotation: 0.3f),
            new AttackStats(meleeShotsScaleUp: 0.15f, coneAngle: 45f)
        };

        List<AttackStats> LaserBeamUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),

            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
                        AttackStatsLibrary.GetStat("Quick Hands 2"),
                        AttackStatsLibrary.GetStat("Glattt 2"),

            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
                        AttackStatsLibrary.GetStat("Knockback 3"),
                        AttackStatsLibrary.GetStat("Quick Hands 3"),
                        AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
                        AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };
        AttackBuilder LaserBeam = new AttackBuilder()
            .SetAttackName("Laser Beam")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DoubleBeam"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Firin' mah lazer or whatever.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    isCone: true,
                    coneAngle: 45f,
                    damage: 15,
                    spread: 0.3f,
                    castTime: 2.5f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.8f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.6f,
                    meleeShotsScaleUp: -0.1f,
                    meleeSpacer: 3f,
                    meleeSpacerGap: 3.8f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.5f,
                    thrownDamage: 0,
                    throwSpeed: 0f
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
            new AttackStats(damage: 3),
            new AttackStats(speed: -0.05f, coneAngle: 45f),
            new AttackStats(shotsPerAttack: 1, spread: 0.4f),
            new AttackStats(knockback: 0.4f, shakeRotation: 0.4f),
            new AttackStats(projectileSizeMultiplier: 0.35f, shakeStrength: 0.1f),
            new AttackStats(range: 2f, aimRangeAdditive: 2f),
        };

        List<AttackStats> PainWheelUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder PainWheel = new AttackBuilder()
            .SetAttackName("Pain Wheel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Keep on spinnin', stay winnin'")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    isCone: true,
                    coneAngle: 40,
                    damage: 5,
                    spread: 0.4f,
                    spray: 0f,
                    castTime: 2.1f,
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
                    throwSpeed: 0.6f
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
            new AttackStats(damage: 3),
            new AttackStats(castTime: -0.4f),
            new AttackStats(shotsPerAttack: 1, shotgunSpread: 25f),
            new AttackStats(damage: 3),
            new AttackStats(projectileSizeMultiplier: 0.3f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f, aimRangeAdditive: 1.5f, pierce: 5),
            new AttackStats(projectileSizeMultiplier: 0.3f, shakeTime: 0.05f)
        };

        List<AttackStats> PetrifyGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder PetrifyGrenade = new AttackBuilder()
            .SetAttackName("Petrify Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_stun"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Explode and Freeze targets in place.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 1,
                    shotgunSpread: 65f,
                    spray: 0,
                    castTime: 1.9f,
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
                    shakeRotation: 0.05f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_shock"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(PetrifyGrenadeUpgrades);
        AddAttack(PetrifyGrenade);

        //PetrifyNova
        List<AttackStats> PetrifyNovaRarity = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(comboLength: 1, comboWaitTime: 0.3f),
            new AttackStats(shotsPerAttack: 1, damage: -2),
            new AttackStats(meleeSizeMultiplier: 0.3f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2f, aimRangeAdditive: 2f),
            new AttackStats(meleeSpacer: 1.5f, aimRangeAdditive: 1.5f),
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

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),

            AttackStatsLibrary.GetStat("Extend 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder PetrifyNova = new AttackBuilder()
            .SetAttackName("Petrify Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_Petrify"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Freeze targets around you.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 2,
                    spread: 0.5f,
                    castTime: 2.3f,
                    knockback: 0f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 0f,
                    meleeSpacerGap: 0f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.4f,
                    thrownDamage: 11f,
                    throwSpeed: 0.5f
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
        List<AttackStats> RevolverRarity = new List<AttackStats>
        {
            new AttackStats(damage: 9),
            new AttackStats(shotsPerAttack: 2),
            new AttackStats(shotsPerAttack: 2),
            new AttackStats(projectileSizeMultiplier: 0.5f, shakeStrength: 0.05f),
            new AttackStats(pierce: 1, speed: 0.05f),
            new AttackStats(range: 1f, aimRangeAdditive: 1.5f, coneAngle: 30f),
        };

        List<AttackStats> RevolverUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder Revolver = new AttackBuilder()
            .SetAttackName("Revolver")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Magnum"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("6 shots. For now.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    isCone: true,
                    coneAngle: 30f,
                    damage: 8,
                    spread: 0.4f,
                    spray: 1.8f,
                    castTime: 2f,
                    range: 5.5f,
                    shotsPerAttack: 6,
                    speed: 0.18f,
                    knockback: 0.5f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 6f,
                    throwSpeed: 0.5f
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
            new AttackStats(damage: 8),
            new AttackStats(damage: 8),
            new AttackStats(shotsPerAttack: 1, shotgunSpreadMultiplier: 0.2f),
            new AttackStats(projectileSizeMultiplier: 0.4f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f, aimRangeAdditive: 1.75f, coneAngle: 40),
            new AttackStats(knockback: 0.5f, shakeTime: 0.05f),
        };

        List<AttackStats> ShotgunUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };


        AttackBuilder Shotgun = new AttackBuilder()
            .SetAttackName("Shotgun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Ayy Shawttyyyy.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    damage: 9,
                    isCone: true,
                    coneAngle: 70f,
                    shotgunSpread: 75f,
                    spray: 0,
                    castTime: 2.4f,
                    range: 3.2f,
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
                    throwSpeed: 0.5f
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
            new AttackStats(damage: 2),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotgunSpread: 35f, aimRangeAdditive: 1, coneAngle: 25),
            new AttackStats(projectileSizeMultiplier: 0.4f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f, speed: 0.015f),
        };

        List<AttackStats> ShurikenUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder Shuriken = new AttackBuilder()
            .SetAttackName("Shuriken")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken_small"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("How quickly we spin.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    isCone: true,
                    coneAngle: 70f,
                    damage: 2,
                    shotgunSpread: 100f,
                    spray: 0,
                    castTime: 2f,
                    range: 2.8f,
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
                    thrownDamage: 7,
                    throwSpeed: 0.5f
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
            new AttackStats(damage: 4),
            new AttackStats(range: 1f, speed: 0.05f),
            new AttackStats(shotsPerAttack: 10),
            new AttackStats(spread: -0.01f),
            new AttackStats(projectileSizeMultiplier: 0.5f, shakeRotation: 0.02f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 30)
        };

        List<AttackStats> SMGUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder SMG = new AttackBuilder()
            .SetAttackName("SMG")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_SMG"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Lil' Uzi got me like!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    isCone: true,
                    coneAngle: 50f,
                    damage: 2,
                    spread: 0.04f,
                    spray: 25f,
                    castTime: 1.75f,
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
                    throwSpeed: 0.6f
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
            new AttackStats(damage: 2),
            new AttackStats(projectileSizeMultiplier: 0.25f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(speed: -0.25f, pierce: 5),
            new AttackStats(range: 1.25f, aimRangeAdditive: 2f)
        };

        List<AttackStats> SmokeGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder SmokeGrenade = new AttackBuilder()
            .SetAttackName("Smoke Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_slow"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Hotbox the world.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 1,
                    shotgunSpread: 50f,
                    castTime: 2.2f,
                    range: 1.75f,
                    shotsPerAttack: 1,
                    speed: 0.08f,
                    knockback: 0f,
                    pierce: 1,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.1f
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
            new AttackStats(damage: 10),
            new AttackStats(castTime: -0.2f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(knockback: 0.4f, shakeRotation: 0.4f),
            new AttackStats(projectileSizeMultiplier: 0.3f, shakeRotation: 0.1f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 30f),
            new AttackStats(shotsPerAttack: 1)
        };

        List<AttackStats> SniperUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder Sniper = new AttackBuilder()
            .SetAttackName("Sniper Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_long"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Cheers, mate.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.8f,
                    damage: 25,
                    isCone: true,
                    coneAngle: 30f,
                    spread: 0.6f,
                    spray: 1f,
                    sprayThreshold: 1,
                    castTime: 2f,
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
                    throwSpeed: 0.5f
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
            new AttackStats(damage: 3),
            new AttackStats(speed: -0.008f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(range: 1.5f, aimRangeAdditive: 1.5f),
            new AttackStats(projectileSizeMultiplier: 0.25f, shakeRotation: 0.1f),
            new AttackStats(throwSpeed: 5f),
            new AttackStats(coneAngle: 45f, aimRangeAdditive: 1f)
        };

        List<AttackStats> SuctionCannonUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder SuctionCannon = new AttackBuilder()
            .SetAttackName("Suction Cannon")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/SuctionCannon_Orb"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Fire a mini-black hole that sucks targets in.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.2f,
                    damage: 1,
                    isCone: true,
                    coneAngle: 40f,
                    spread: 1f,
                    spray: 2f,
                    sprayThreshold: 1,
                    castTime: 2.75f,
                    range: 4f,
                    shotsPerAttack: 1,
                    speed: 0.018f,
                    knockback: 0f,
                    pierce: 15,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 25f,
                    throwSpeed: 0.12f,
                    projectileSizeMultiplier: 0.8f
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
            new AttackStats(damage: 5),
            new AttackStats(speed: 0.1f, range: 1f, aimRangeAdditive: 1f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(range: 1f, aimRangeAdditive: 1f, pierce: 5),
            new AttackStats(projectileSizeMultiplier: 0.3f, shakeRotation: 0.1f),
            new AttackStats(knockback: 0.5f, shakeStrength: 0.1f),
        };

        List<AttackStats> SuctionGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("MLG 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder SuctionGrenade = new AttackBuilder()
            .SetAttackName("Suction Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_suction"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Explodes then sucks targets into the center.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 2,
                    spread: 0.75f,
                    castTime: 2.1f,
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
                    projectileSizeMultiplier: 1
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
            new AttackStats(damage: 7),
            new AttackStats(comboLength: 1, damage: -2),
            new AttackStats(meleeSizeMultiplier: 0.25f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2f, aimRangeAdditive: 2f),
            new AttackStats(meleeSpacer: 1.5f, aimRangeAdditive: 1.5f),
            new AttackStats(knockbackMultiplier: 0.25f)
        };

        List<AttackStats> SuctionNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),

            AttackStatsLibrary.GetStat("Extend 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),

            AttackStatsLibrary.GetStat("Extend 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder SuctionNova = new AttackBuilder()
            .SetAttackName("Suction Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_gravity"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Sucks in everything around you.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 3,
                    spread: 0.4f,
                    castTime: 2.5f,
                    knockback: 0.30f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 0.75f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 0f,
                    meleeSpacerGap: 0f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.5f,
                    thrownDamage: 13f,
                    throwSpeed: 0.6f
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
            new AttackStats(damage: 4),
            new AttackStats(comboWaitTime: -0.05f, castTime: -0.3f),
            new AttackStats(meleeSizeMultiplier: 0.3f, shakeStrength: 0.04f),
            new AttackStats(knockback: 0.4f, shakeRotation: 0.3f),
            new AttackStats(comboLength: -1, damage: 7),
            new AttackStats(meleeSpacer: 1.5f, coneAngle: 30f, aimRangeAdditive: 1f)
        };

        List<AttackStats> WindBladeUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Crit Chance 1"),
            AttackStatsLibrary.GetStat("Crit Dmg 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),

            AttackStatsLibrary.GetStat("Glattt 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),

            AttackStatsLibrary.GetStat("MLG 1"),
            AttackStatsLibrary.GetStat("Gamer 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Crit Chance 2"),
            AttackStatsLibrary.GetStat("Crit Dmg 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),

            AttackStatsLibrary.GetStat("Glattt 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Kamakazi 2"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Gamer 2"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Ki Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),

 

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Crit Chance 3"),
            AttackStatsLibrary.GetStat("Crit Dmg 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Glattt 3"),

            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Ki Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),


          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Crit Chance 4"),
            AttackStatsLibrary.GetStat("Crit Dmg 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Knockback 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),

        };

        AttackBuilder WindBlade = new AttackBuilder()
            .SetAttackName("Wind Blade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/KatanaSlash"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Power of God and Anime.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    damage: 5,
                    isCone: true,
                    coneAngle: 120f,
                    spread: 0.18f,
                    castTime: 2f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.35f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 3,
                    comboWaitTime: 0.3f,
                    meleeShotsScaleUp: 0f,
                    meleeSpacer: 2.5f,
                    meleeSpacerGap: 2.75f,
                    shakeTime: 0.08f,
                    shakeStrength: 0.12f,
                    shakeRotation: 0.1f,
                    thrownDamage: 5,
                    throwSpeed: 0.6f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/katana_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/katana_02")
            )
            .SetRarityUpgrades(WindBladeUpgrades);
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
