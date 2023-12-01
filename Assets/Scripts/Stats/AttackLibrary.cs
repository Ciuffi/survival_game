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

            new AttackStats(rarity: Rarity.Legendary, comboWaitTime: -0.25f, comboAttackBuffMultiplier: 0.1f),
            new AttackStats(rarity: Rarity.Legendary, activeDuration: 1, damage: 1),
        };

        List<AttackStats> ConsecrateUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),

            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Persistence+ 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),
            AttackStatsLibrary.GetStat("C-combo 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Extend 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion 1"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Infusion 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
            AttackStatsLibrary.GetStat("Double Trouble"),
        };

        AttackBuilder Consecrate = new AttackBuilder()
            .SetAttackName("Consecrate")
            .SetUnlockLevel(3)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Consecrate"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("The light shall burn you.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 0f,
                    is360: true,
                    damage: 3,
                    spread: 0.7f,
                    castTime: 3f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.25f,
                    meleeShotsScaleUp: -0.25f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 2.2f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_consecrate_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_consecrate_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_consecrate_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_consecrate_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/nova_consecrate_1"),
                    Resources.Load<Sprite>("WeaponSprites/nova_consecrate_2"),
                    Resources.Load<Sprite>("WeaponSprites/nova_consecrate_3"),
                    Resources.Load<Sprite>("WeaponSprites/nova_consecrate_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_consecrate_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_consecrate_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_consecrate_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_consecrate_4")
                },

                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")
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

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 8, damage: 2),
            new AttackStats(rarity: Rarity.Legendary, spreadMultiplier: -0.2f, aimRangeAdditive: 2f, coneAngle: 30),
        };

        List<AttackStats> ClassicRifleUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
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
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            
            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
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
            AttackStatsLibrary.GetStat("Infusion 1"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Infusion 2"),
        };


        AttackBuilder ClassicRifle = new AttackBuilder()
            .SetAttackName("Classic Rifle")
            .SetUnlockLevel(0)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Good ol' trusty rifle.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    is360: false,
                    coneAngle: 40,
                    damage: 7,
                    spread: 0.085f,
                    spray: 1.35f,
                    castTime: 2f,
                    range: 6.5f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/rifle_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/rifle_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/rifle_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/rifle_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/rifle_1"),
                    Resources.Load<Sprite>("WeaponSprites/rifle_2"),
                    Resources.Load<Sprite>("WeaponSprites/rifle_3"),
                    Resources.Load<Sprite>("WeaponSprites/rifle_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/rifle_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/rifle_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/rifle_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/rifle_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
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
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
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
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast+ 1"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast+ 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion++ 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Multicast+ 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Infusion++ 2"),

        };

        AttackBuilder DoubleBarrel = new AttackBuilder()
            .SetAttackName("Double Barrel")
            .SetUnlockLevel(0)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Straight to the dome.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.75f,
                    is360: false,
                    coneAngle: 50,
                    damage: 10,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/doubleBarrel_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/doubleBarrel_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/doubleBarrel_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/doubleBarrel_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/doubleBarrel_1"),
                    Resources.Load<Sprite>("WeaponSprites/doubleBarrel_2"),
                    Resources.Load<Sprite>("WeaponSprites/doubleBarrel_3"),
                    Resources.Load<Sprite>("WeaponSprites/doubleBarrel_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/doubleBarrel_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/doubleBarrel_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/doubleBarrel_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/doubleBarrel_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
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
            new AttackStats(rarity: Rarity.Rare, meleeSpacerMultiplier: 0.2f, meleeSizeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, comboLength: 1, comboAttackBuffMultiplier: 0.12f),
            new AttackStats(rarity: Rarity.Epic, effectMultiplier: 0.6f),

            new AttackStats(rarity: Rarity.Legendary, shootOppositeSide: true),
            new AttackStats(rarity: Rarity.Legendary, effectDuration: 1f, damage: 3)
        };

        List<AttackStats> DrainScytheUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Implode 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Implode 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("C-combo 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("C-combo 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("C-combo 4"),

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
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };

        AttackBuilder DrainScythe = new AttackBuilder()
            .SetAttackName("Drain Scythe")
            .SetUnlockLevel(3)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DrainScythe"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Why you walkin' so slow?")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.7f,
                    is360: false,
                    coneAngle: 75f,
                    damage: 9,
                    spread: 0.55f,
                    castTime: 2f,
                    knockback: 0.3f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1f,
                    meleeSpacer: 1.25f,
                    meleeSpacerGap: 1.25f,
                    meleeShotsScaleUp: -0.20f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/scythe_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/scythe_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/scythe_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/scythe_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/scythe_1"),
                    Resources.Load<Sprite>("WeaponSprites/scythe_2"),
                    Resources.Load<Sprite>("WeaponSprites/scythe_3"),
                    Resources.Load<Sprite>("WeaponSprites/scythe_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/scythe_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/scythe_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/scythe_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/scythe_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")
            )
            .SetRarityUpgrades(DrainScytheRarity)
        .SetWeaponUpgrades(DrainScytheUpgrades);
        AddAttack(DrainScythe);

        //earth Shock
        List<AttackStats> EarthShockRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.2f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, spreadMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Epic, meleeSpacerGapMultiplier: -0.25f),

            new AttackStats(rarity: Rarity.Legendary, activeDuration: 0.2f),
            new AttackStats(rarity: Rarity.Legendary, damage: 5),

        };

        List<AttackStats> EarthShockUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),

            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Kamakazi 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
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
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("C-combo 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
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
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Infusion+ 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder EarthShock = new AttackBuilder()
            .SetAttackName("Earth Shock")
            .SetUnlockLevel(1)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/EarthShock"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Pound the ground.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 45f,
                    damage: 7,
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
                    meleeSpacer: 1.5f,
                    meleeSpacerGap: 1.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.2f,
                    shakeRotation: 1f,
                    thrownDamage: 7f,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/gauntlets_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/gauntlets_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/gauntlets_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/gauntlets_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/gauntlets_1"),
                    Resources.Load<Sprite>("WeaponSprites/gauntlets_2"),
                    Resources.Load<Sprite>("WeaponSprites/gauntlets_3"),
                    Resources.Load<Sprite>("WeaponSprites/gauntlets_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/gauntlets_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/gauntlets_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/gauntlets_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/gauntlets_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")


            )
            .SetRarityUpgrades(EarthShockRarity)
            .SetWeaponUpgrades(EarthShockUpgrades);
        AddAttack(EarthShock);



        //fire starter
        List<AttackStats> FireStarterRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, castTimeMultiplier: -0.1f),

            new AttackStats(rarity: Rarity.Epic, damage: 6, dotDamage: 2),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.3f, shotgunSpreadMultiplier: 0.2f, shakeStrength: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.4f, aimRangeAdditive: 1.5f, coneAngle: 30f),
            new AttackStats(rarity: Rarity.Legendary, dotDamage: 4),
        };

        List<AttackStats> FireStarterUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast+ 1"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Multicast+ 2"),
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
            AttackStatsLibrary.GetStat("Infusion 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Multicast+ 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot+ 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
        };

        AttackBuilder FireStarter = new AttackBuilder()
            .SetAttackName("Fire Starter")
            .SetUnlockLevel(4)
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/fireStarter_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/fireStarter_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/fireStarter_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/fireStarter_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/fireStarter_1"),
                    Resources.Load<Sprite>("WeaponSprites/fireStarter_2"),
                    Resources.Load<Sprite>("WeaponSprites/fireStarter_3"),
                    Resources.Load<Sprite>("WeaponSprites/fireStarter_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/fireStarter_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/fireStarter_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/fireStarter_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/fireStarter_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
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
            new AttackStats(rarity: Rarity.Rare, effectMultiplier: 0.5f, damage: 2),

            new AttackStats(rarity: Rarity.Epic, damage: 3, activeMultiplier: 0.4f),
            new AttackStats(rarity: Rarity.Epic, meleeSpacer: 1f, meleeSpacerGap: 0.5f, aimRangeAdditive: 1.5f, coneAngle: 30f),

            new AttackStats(rarity: Rarity.Legendary, effectDuration: 1f, damage: 2),
            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, spread: -0.1f)
        };

        List<AttackStats> GravityGrabUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),

            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Implode 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),


          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),

            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Implode 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
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
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Infusion++ 1"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Infusion++ 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder GravityGrab = new AttackBuilder()
            .SetAttackName("Eldritch Grasp")
            .SetUnlockLevel(4)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/GravityGrab"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Pull 'em in, maybe too in.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.25f,
                    is360: false,
                    coneAngle: 75,
                    damage: 10,
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
                    meleeSpacerGap: 1f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.3f,
                    shakeRotation: 1f,
                    thrownDamage: 8,
                    thrownSpeed: 0.42f,
                    isMagnet: true,
                    magnetStrength: 8f,
                    magnetDuration: 0.5f,
                    isSlow: true,
                    slowPercentage: 0.25f,
                    slowDuration: 1.5f,
                    comboAttackBuffMultiplier: 0.15f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/grasp_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grasp_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grasp_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grasp_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/grasp_1"),
                    Resources.Load<Sprite>("WeaponSprites/grasp_2"),
                    Resources.Load<Sprite>("WeaponSprites/grasp_3"),
                    Resources.Load<Sprite>("WeaponSprites/grasp_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/grasp_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grasp_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grasp_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grasp_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")

            )
            .SetRarityUpgrades(GravityGrabRarity)
                        .SetWeaponUpgrades(GravityGrabUpgrades);

        AddAttack(GravityGrab);

        // GatlingGun
        List<AttackStats> GatlingGunRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 1, shotsPerAttack: 10),
            new AttackStats(rarity: Rarity.Rare, projectileSizeMultiplier: 0.1f, speedMultiplier: 0.12f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 20),
            new AttackStats(rarity: Rarity.Epic, rangeMultiplier: 0.3f, aimRangeAdditive: 1f, coneAngle: 25f),

            new AttackStats(rarity: Rarity.Legendary, spreadMultiplier: -0.25f, sprayMultiplier: 0.2f),
            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 20, damage: 1),
        };

        List<AttackStats> GatlingGunUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extended Clip+ 1"),
            AttackStatsLibrary.GetStat("Pierce 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Chain 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip+ 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Infusion 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extended Clip+ 3"),
            AttackStatsLibrary.GetStat("Pierce 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Infusion 2"),
        };

        AttackBuilder GatlingGun = new AttackBuilder()
            .SetAttackName("Gatling Gun")
            .SetUnlockLevel(1)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Tiny"))
            .SetWeaponSetType(WeaponSetType.Automatic)
            .SetDescription("Spray 'n pray.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 45f,
                    damage: 1.5f,
                    spread: 0.016f,
                    spray: 0.3f,
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
                    thrownDamage: 15f,
                    thrownSpeed: 0.2f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/gatling_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/gatling_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/gatling_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/gatling_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/gatling_1"),
                    Resources.Load<Sprite>("WeaponSprites/gatling_2"),
                    Resources.Load<Sprite>("WeaponSprites/gatling_3"),
                    Resources.Load<Sprite>("WeaponSprites/gatling_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/gatling_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/gatling_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/gatling_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/gatling_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(GatlingGunRarity)
                    .SetWeaponUpgrades(GatlingGunUpgrades);

        AddAttack(GatlingGun);

        //GodHand
        List<AttackStats> GodHandRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 4),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 1f, meleeSpacerGap: 0.75f, meleeSizeMultiplier: 0.15f),

            new AttackStats(rarity: Rarity.Epic, castTimeMultiplier: -0.20f),
            new AttackStats(rarity: Rarity.Epic, comboLength: 1, comboAttackBuffMultiplier: 0.1f),

            new AttackStats(rarity: Rarity.Legendary, damage: 7),
            new AttackStats(rarity: Rarity.Legendary, meleeSizeMultiplier: 0.15f, knockbackMultiplier: 0.3f, shakeTime: 0.05f, shakeRotation: 0.5f),
        };

        List<AttackStats> GodHandUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),

            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Implode 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Implode 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("C-combo 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
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
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Infusion+ 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder GodHand = new AttackBuilder()
            .SetAttackName("God Hand")
            .SetUnlockLevel(5)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/MeleeFist"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Violence is the answer.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.6f,
                    is360: false,
                    coneAngle: 65f,
                    damage: 20,
                    spread: 0.6f,
                    castTime: 2f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.7f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1f,
                    meleeShotsScaleUp: -0.20f,
                    meleeSpacer: 1f,
                    meleeSpacerGap: 1f,
                    shakeTime: 0.15f,
                    shakeStrength: 0.7f,
                    shakeRotation: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/fist_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/fist_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/fist_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/fist_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/fist_1"),
                    Resources.Load<Sprite>("WeaponSprites/fist_2"),
                    Resources.Load<Sprite>("WeaponSprites/fist_3"),
                    Resources.Load<Sprite>("WeaponSprites/fist_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/fist_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/fist_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/fist_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/fist_4")
                }
            )
            .SetRarityUpgrades(GodHandRarity)
                    .SetWeaponUpgrades(GodHandUpgrades);

        AddAttack(GodHand);

        // FragGrenade
        List<AttackStats> FragGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.5f, shakeRotation: 0.3f),

            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.3f),
            new AttackStats(rarity: Rarity.Epic, damage: 5, aimRange: 1f, coneAngle: 20f),

            new AttackStats(rarity: Rarity.Legendary, isDoT: true, dotDamage: 5, dotDuration: 5f, dotTickRate: 1.5f),
            new AttackStats(rarity: Rarity.Legendary, damage:3, range: 1.5f, pierce: 10, aimRangeAdditive: 1f),
        };

        List<AttackStats> FragGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Reach 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
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
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Magnetize 2"),
            AttackStatsLibrary.GetStat("Reach 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
            AttackStatsLibrary.GetStat("Reach 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };

        AttackBuilder FragGrenade = new AttackBuilder()
            .SetAttackName("Frag Grenade")
            .SetUnlockLevel(0)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_impact"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Fire in the hole!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 30f,
                    damage: 3,
                    hasDeathrattle: true,
                    deathrattleDamage: 12,
                    spread: 1f,
                    spray: 0f,
                    castTime: 1.6f,
                    range: 1.35f,
                    shotsPerAttack: 1,
                    speed: 0.08f,
                    knockback: 0.10f,
                    deathrattleKnockback: 0.4f,
                    pierce: 15,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/grenade_frag_1"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_frag_2"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_frag_3"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_frag_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_frag_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_frag_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_frag_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_frag_4")
                },
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(FragGrenadeRarity)
            .SetWeaponUpgrades(FragGrenadeUpgrades);
        AddAttack(FragGrenade);

        // ImpactMine
        List<AttackStats> ImpactMineRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.5f, shakeRotation: 0.3f),

            new AttackStats(rarity: Rarity.Epic, damage: 5),
            new AttackStats(rarity: Rarity.Epic, spreadMultiplier: -0.25f, effectDuration: 1f),

            new AttackStats(rarity: Rarity.Legendary, isMagnet: true, magnetDuration: 0.5f, magnetStrength: 2f),
            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.25f, shotsPerAttack: 1, damage: 5),
        };

        List<AttackStats> ImpactMineUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };

        AttackBuilder ImpactMine = new AttackBuilder()
            .SetAttackName("Impact Mine")
            .SetUnlockLevel(3)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/mine_impact"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Lure 'em in and watch 'em fly.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 0.1f,
                    is360: true,
                    damage: 2,
                    hasDeathrattle: true,
                    deathrattleDamage: 12f,
                    spread: 1.6f,
                    spray: 0f,
                    castTime: 2.25f,
                    range: 9f,
                    shotsPerAttack: 1,
                    speed: 0f,
                    knockback: 0.15f,
                    deathrattleKnockback: 0.3f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.15f,
                    shakeRotation: 0.1f,
                    isSlow: true,
                    slowPercentage: 0.5f,
                    slowDuration: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/mine_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/mine_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/mine_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/mine_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/mine_1"),
                    Resources.Load<Sprite>("WeaponSprites/mine_2"),
                    Resources.Load<Sprite>("WeaponSprites/mine_3"),
                    Resources.Load<Sprite>("WeaponSprites/mine_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/mine_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/mine_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/mine_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/mine_4")
                },
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(ImpactMineRarity)
                    .SetWeaponUpgrades(ImpactMineUpgrades);

        AddAttack(ImpactMine);

        //ImpactNova
        List<AttackStats> ImpactNovaRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 2f, aimRangeAdditive: 1.5f),

            new AttackStats(rarity: Rarity.Epic, damage: 3),
            new AttackStats(rarity: Rarity.Epic, activeDuration: 0.5f),

            new AttackStats(rarity: Rarity.Legendary, meleeSizeMultiplier: 0.2f, shakeRotation: 0.5f),
            new AttackStats(rarity: Rarity.Legendary, comboLength: 1, comboAttackBuffMultiplier: 0.2f),
        };

        List<AttackStats> ImpactNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
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
            AttackStatsLibrary.GetStat("C-combo 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
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
            AttackStatsLibrary.GetStat("C-combo 3"),
            AttackStatsLibrary.GetStat("Infusion 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Infusion 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder ImpactNova = new AttackBuilder()
            .SetAttackName("Impact Nova")
            .SetUnlockLevel(1)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_impact"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Do I have to aim? No?")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    is360: true,
                    damage: 14,
                    spread: 0.4f,
                    castTime: 2.2f,
                    knockback: 0.6f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.5f,
                    meleeShotsScaleUp: -0.12f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 0.5f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.1f,
                    thrownDamage: 7f,
                    thrownSpeed: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_impact_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_impact_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_impact_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_impact_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/nova_impact_1"),
                    Resources.Load<Sprite>("WeaponSprites/nova_impact_3"),
                    Resources.Load<Sprite>("WeaponSprites/nova_impact_2"),
                    Resources.Load<Sprite>("WeaponSprites/nova_impact_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_impact_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_impact_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_impact_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_impact_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")
            )
            .SetRarityUpgrades(ImpactNovaRarity)
                    .SetWeaponUpgrades(ImpactNovaUpgrades);
        AddAttack(ImpactNova);

        //SuctionBeam
        List<AttackStats> SuctionBeamRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 5),
            new AttackStats(rarity: Rarity.Rare, projectileSizeMultiplier: 0.25f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, spread: -0.2f),
            new AttackStats(rarity: Rarity.Epic, aimRangeAdditive: 1f, coneAngle: 30f),

            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.3f, magnetStrength: 2f, magnetDuration: 0.5f),
            new AttackStats(rarity: Rarity.Legendary, castTimeMultiplier: -0.2f, knockback: 0.3f, damage: 5f)
        };

        List<AttackStats> SuctionBeamUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),

            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion++ 1"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Infusion++ 2"),
        };

        AttackBuilder SuctionBeam = new AttackBuilder()
            .SetAttackName("Suction Beam")
            .SetUnlockLevel(4)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/DoubleBeam"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Firin' mah lazer!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.25f,
                    is360: false,
                    coneAngle: 50f,
                    damage: 9,
                    spread: 1f,
                    castTime: 1.5f,
                    shotsPerAttack: 1,
                    spray: 2f,
                    range: 2f,
                    speed: 0.2f,
                    knockback: 0.4f,
                    pierce: 99,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.5f,
                    shakeRotation: 0.5f,
                    thrownDamage: 11,
                    thrownSpeed: 0.35f,
                    isMagnet: true,
                    magnetStrength: 3,
                    magnetDuration: 0.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/beam_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/beam_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/beam_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/beam_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/beam_1"),
                    Resources.Load<Sprite>("WeaponSprites/beam_2"),
                    Resources.Load<Sprite>("WeaponSprites/beam_3"),
                    Resources.Load<Sprite>("WeaponSprites/beam_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/beam_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/beam_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/beam_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/beam_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(SuctionBeamRarity)
                    .SetWeaponUpgrades(SuctionBeamUpgrades);

        AddAttack(SuctionBeam);


        // RPG
        List<AttackStats> RPGRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, projectileSizeMultiplier: 0.3f, damage: 4),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.4f, shakeRotation: 0.2f, speedMultiplier: 0.15f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Epic, spread: -0.25f, damage: 4),

            new AttackStats(rarity: Rarity.Legendary, isSplit: true, splitAmount: 2, splitStatPercentage: 0.5f),
            new AttackStats(rarity: Rarity.Legendary, damage: 2),
        };

        List<AttackStats> RPGUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
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
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Magnetize 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
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
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };

        AttackBuilder RPG = new AttackBuilder()
            .SetAttackName("RPG")
            .SetUnlockLevel(5)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/missile_frag"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Heat seeking missiles.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 25f,
                    damage: 5,
                    hasDeathrattle: true,
                    deathrattleDamage: 15,
                    spread: 2f,
                    spray: 0f,
                    castTime: 3f,
                    recoveryTime: 0f,
                    range: 7f,
                    shotsPerAttack: 1,
                    speed: 0.02f,
                    knockback: 0.05f,
                    deathrattleKnockback: 0.25f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.5f,
                    shakeStrength: 1f,
                    shakeRotation: 0.5f,
                    thrownDamage: 12f,
                    thrownSpeed: 0.25f,
                    isHoming: true
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/rpg_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/rpg_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/rpg_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/rpg_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/rpg_1"),
                    Resources.Load<Sprite>("WeaponSprites/rpg_2"),
                    Resources.Load<Sprite>("WeaponSprites/rpg_3"),
                    Resources.Load<Sprite>("WeaponSprites/rpg_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/rpg_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/rpg_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/rpg_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/rpg_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                muzzleFlashPrefab: BigMuzzleFlash

            )
            .SetRarityUpgrades(RPGRarity)
            .SetWeaponUpgrades(RPGUpgrades);
        AddAttack(RPG);

        // PainWheel
        List<AttackStats> PainWheelRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3),
            new AttackStats(rarity: Rarity.Rare, activeDuration: 1),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, speedMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Epic, knockback: 0.5f, shakeRotation: 0.4f),

            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.25f, damage: 5, shakeStrength: 0.05f),
            new AttackStats(rarity: Rarity.Legendary, speedMultiplier: -0.2f, rangeMultiplier: 0.2f, aimRangeAdditive: 1.5f, coneAngle: 45f),
        };

        List<AttackStats> PainWheelUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
  
            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
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
            AttackStatsLibrary.GetStat("Infusion 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Infusion 2"),
        };

        AttackBuilder PainWheel = new AttackBuilder()
            .SetAttackName("Pain Wheel")
            .SetUnlockLevel(2)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Keep on spinnin'")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.4f,
                    is360: false,
                    coneAngle: 35,
                    damage: 5,
                    spread: 0.85f,
                    spray: 0f,
                    castTime: 2f,
                    range: 3f,
                    shotsPerAttack: 1,
                    speed: 0.15f,
                    knockback: 0f,
                    pierce: 20,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/painWheel_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/painWheel_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/painWheel_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/painWheel_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/painWheel_1"),
                    Resources.Load<Sprite>("WeaponSprites/painWheel_2"),
                    Resources.Load<Sprite>("WeaponSprites/painWheel_3"),
                    Resources.Load<Sprite>("WeaponSprites/painWheel_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/painWheel_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/painWheel_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/painWheel_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/painWheel_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(PainWheelRarity)
                    .SetWeaponUpgrades(PainWheelUpgrades);

        AddAttack(PainWheel);

        //PetrifyGrenade
        List<AttackStats> PetrifyGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 5),
            new AttackStats(rarity: Rarity.Rare, rangeMultiplier: 0.5f, aimRangeAdditive: 1f, pierce: 10),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, aimRange: 1, coneAngle: 45f),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.1f, shotgunSpread: 15, shakeStrength: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, isSlow: true, slowPercentage: 0.5f, slowDuration: 7f),
            new AttackStats(rarity: Rarity.Legendary, damage: 5),

        };

        List<AttackStats> PetrifyGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),

            AttackStatsLibrary.GetStat("Multicast+ 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("Multicast+ 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Chain+ 2"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast+ 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Chain+ 3"),

        };

        AttackBuilder PetrifyGrenade = new AttackBuilder()
            .SetAttackName("Petrify Grenade")
            .SetUnlockLevel(4)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_stun"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Stay right where you are.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 35f,
                    damage: 1,
                    hasDeathrattle: true,
                    deathrattleDamage: 4,
                    shotgunSpread: 65f,
                    spray: 0,
                    castTime: 1.7f,
                    range: 1.3f,
                    shotsPerAttack: 1,
                    speed: 0.1f,
                    knockback: 0f,
                    deathrattleKnockback: 0f,
                    pierce: 10,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.05f,
                    isStun: true,
                    stunDuration: 4f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/grenade_shock_1"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_shock_2"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_shock_3"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_shock_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_shock_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_shock_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_shock_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_shock_4")
                },
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
            new AttackStats(rarity: Rarity.Legendary, activeDuration: 0.3f, damage: 2),
        };

        List<AttackStats> PetrifyNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
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
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder PetrifyNova = new AttackBuilder()
            .SetAttackName("Petrify Nova")
            .SetUnlockLevel(5)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_Petrify"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("Freeze!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.25f,
                    is360: true,
                    damage: 4,
                    spread: 0.4f,
                    castTime: 2.3f,
                    knockback: 0f,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    comboLength: 1,
                    comboWaitTime: 1.2f,
                    meleeShotsScaleUp: -0.20f,
                    meleeSpacer: 0.5f,
                    meleeSpacerGap: 1.5f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_petrify_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_petrify_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_petrify_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_petrify_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/nova_petrify_1"),
                    Resources.Load<Sprite>("WeaponSprites/nova_petrify_2"),
                    Resources.Load<Sprite>("WeaponSprites/nova_petrify_3"),
                    Resources.Load<Sprite>("WeaponSprites/nova_petrify_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_petrify_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_petrify_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_petrify_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_petrify_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")
            )
            .SetRarityUpgrades(PetrifyNovaRarity)
                    .SetWeaponUpgrades(PetrifyNovaUpgrades);

        AddAttack(PetrifyNova);

        // PlasmaPistol
        List<AttackStats> PlasmaPistolRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3, pierce: 7),
            new AttackStats(rarity: Rarity.Rare, speedMultiplier: 0.2f, rangeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, chainTimes: 1, chainStatDecayPercent: 0.20f),
            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shakeStrength: 0.05f),

            new AttackStats(rarity: Rarity.Legendary, damage: 5, spread: -0.15f, pierce: 5),
            new AttackStats(rarity: Rarity.Legendary, chainTimes: 1, chainStatDecayPercent: 0.1f, chainRange: 6, chainSpeed: 20f),
        };

        List<AttackStats> PlasmaPistolUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
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
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
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
            AttackStatsLibrary.GetStat("Infusion++ 1"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Infusion++ 2"),
        };

        AttackBuilder PlasmaPistol = new AttackBuilder()
            .SetAttackName("Plasma Pistol")
            .SetUnlockLevel(2)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/PlasmaBullet"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Lightning in your pocket.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.2f,
                    is360: false,
                    coneAngle: 30f,
                    damage: 14,
                    spread: 0.5f,
                    spray: 3f,
                    castTime: 1.75f,
                    range: 5f,
                    shotsPerAttack: 1,
                    speed: 0.125f,
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
                    chainTimes: 5,
                    chainStatDecayPercent: 0.30f,
                    chainRange: 4.5f,
                    chainSpeed: 15f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/plasmaPistol_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/plasmaPistol_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/plasmaPistol_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/plasmaPistol_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/plasmaPistol_1"),
                    Resources.Load<Sprite>("WeaponSprites/plasmaPistol_2"),
                    Resources.Load<Sprite>("WeaponSprites/plasmaPistol_3"),
                    Resources.Load<Sprite>("WeaponSprites/plasmaPistol_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/plasmaPistol_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/plasmaPistol_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/plasmaPistol_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/plasmaPistol_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetRarityUpgrades(PlasmaPistolRarity)
                    .SetWeaponUpgrades(PlasmaPistolUpgrades);
        AddAttack(PlasmaPistol);


        // Revolver
        List<AttackStats> RevolverRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 5),
            new AttackStats(rarity: Rarity.Rare, pierce: 2, speedMultiplier: 0.2f, rangeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 3, pierce: 2),
            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.3f, shakeStrength: 0.05f, rangeMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 3, damage: 5, spreadMultiplier: -0.25f),
            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.2f, sprayMultiplier: 0.25f, aimRangeAdditive: 1f, coneAngle: 30f),
        };

        List<AttackStats> RevolverUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
            AttackStatsLibrary.GetStat("Steady 2"),
            AttackStatsLibrary.GetStat("Overheat 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };

        AttackBuilder Revolver = new AttackBuilder()
            .SetAttackName("Revolver")
            .SetUnlockLevel(1)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Magnum"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("It's high noon.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 35f,
                    damage: 13,
                    spread: 0.42f,
                    spray: 1.8f,
                    castTime: 1.65f,
                    range: 5.5f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/revolver_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/revolver_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/revolver_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/revolver_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/revolver_1"),
                    Resources.Load<Sprite>("WeaponSprites/revolver_2"),
                    Resources.Load<Sprite>("WeaponSprites/revolver_3"),
                    Resources.Load<Sprite>("WeaponSprites/revolver_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/revolver_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/revolver_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/revolver_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/revolver_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(RevolverRarity)
                    .SetWeaponUpgrades(RevolverUpgrades);

        AddAttack(Revolver);

        //Shotgun
        List<AttackStats> ShotgunRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 5),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.35f, shotgunSpreadMultiplier: 0.10f, shakeTime: 0.05f),

            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.2f, castTime: -0.1f, shakeStrength: 0.05f),
            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shotgunSpreadMultiplier: 0.15f, coneAngle: 30),

            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.25f, speedMultiplier: 0.25f, aimRangeAdditive: 1f),
            new AttackStats(rarity: Rarity.Legendary, pierce: 10, damage: 8),
        };

        List<AttackStats> ShotgunUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Multicast+ 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Ammo 1"),
            AttackStatsLibrary.GetStat("Cindershot 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast+ 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast+ 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };


        AttackBuilder Shotgun = new AttackBuilder()
            .SetAttackName("Pump-Action")
            .SetUnlockLevel(1)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Up close and personal.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    damage: 8,
                    is360: false,
                    coneAngle: 65f,
                    shotgunSpread: 50f,
                    spray: 0,
                    castTime: 2.4f,
                    range: 3f,
                    shotsPerAttack: 5,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/shotgun_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/shotgun_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/shotgun_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/shotgun_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/shotgun_1"),
                    Resources.Load<Sprite>("WeaponSprites/shotgun_2"),
                    Resources.Load<Sprite>("WeaponSprites/shotgun_3"),
                    Resources.Load<Sprite>("WeaponSprites/shotgun_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/shotgun_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/shotgun_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/shotgun_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/shotgun_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
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
            new AttackStats(rarity: Rarity.Legendary, rangeMultiplier: 0.5f, speedMultiplier: -0.25f, damage: 3),
        };

        List<AttackStats> ShurikenUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Multicast+ 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Puncture 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence+ 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast+ 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Puncture 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast+ 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence+ 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Infusion 2"),

        };

        AttackBuilder Shuriken = new AttackBuilder()
            .SetAttackName("Shuriken")
            .SetUnlockLevel(2)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken_small"))
            .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetDescription("Dattebayo!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.7f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/shuriken_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/shuriken_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/shuriken_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/shuriken_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/shuriken_1"),
                    Resources.Load<Sprite>("WeaponSprites/shuriken_2"),
                    Resources.Load<Sprite>("WeaponSprites/shuriken_3"),
                    Resources.Load<Sprite>("WeaponSprites/shuriken_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/shuriken_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/shuriken_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/shuriken_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/shuriken_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                muzzleFlashPrefab: BigMuzzleFlash
            )
            .SetRarityUpgrades(ShurikenRarity)
                    .SetWeaponUpgrades(ShurikenUpgrades);

        AddAttack(Shuriken);

        // SMG
        List<AttackStats> SMGRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, shotsPerAttack: 10),
            new AttackStats(rarity: Rarity.Rare, rangeMultiplier: 0.25f, aimRangeAdditive: 1f, coneAngle: 30),

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 10, damage: 2),
            new AttackStats(rarity: Rarity.Epic, spreadMultiplier: -0.15f, sprayMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 5, pierce: 1, spreadMultiplier: -0.15f),
            new AttackStats(rarity: Rarity.Legendary, damage: 4, knockback: 0.25f, shakeRotation: 0.01f),
        };

        List<AttackStats> SMGUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Overheat 1"),
            AttackStatsLibrary.GetStat("Steady 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
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
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extended Clip 2"),
            AttackStatsLibrary.GetStat("Pierce 2"),
            AttackStatsLibrary.GetStat("Big Ammo 2"),
            AttackStatsLibrary.GetStat("Cindershot 2"),
            AttackStatsLibrary.GetStat("Chain 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Infusion 1"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Infusion 2"),
        };

        AttackBuilder SMG = new AttackBuilder()
            .SetAttackName("SMG")
            .SetUnlockLevel(0)
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/smg_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/smg_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/smg_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/smg_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/smg_1"),
                    Resources.Load<Sprite>("WeaponSprites/smg_2"),
                    Resources.Load<Sprite>("WeaponSprites/smg_3"),
                    Resources.Load<Sprite>("WeaponSprites/smg_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/smg_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/smg_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/smg_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/smg_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
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
            new AttackStats(rarity: Rarity.Rare, effectMultiplier: 0.25f, shotsPerAttack: 1),

            new AttackStats(rarity: Rarity.Epic, projectileSizeMultiplier: 0.2f),
            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, shotgunSpread: 35, damage: 2),

            new AttackStats(rarity: Rarity.Legendary, effectDuration: 2f, activeDuration: 1),
            new AttackStats(rarity: Rarity.Legendary, shotgunSpread: 25, damage: 2, projectileSizeMultiplier: 0.2f),
        };

        List<AttackStats> SmokeGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Saw'd Off 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Reach 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Saw'd Off 2"),
            AttackStatsLibrary.GetStat("Reach 2"),

            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("Multicast+ 1"),
            AttackStatsLibrary.GetStat("Extra Round 1"),
            AttackStatsLibrary.GetStat("Big Gadget 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Saw'd Off 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("Multicast+ 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Concussive 1"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast+ 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Split 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Chain+ 3"),

        };

        AttackBuilder SmokeGrenade = new AttackBuilder()
            .SetAttackName("Smoke Grenade")
            .SetUnlockLevel(0)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_slow"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Hotbox the world.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.8f,
                    is360: true,
                    damage: 1,
                    hasDeathrattle: true,
                    deathrattleDamage: 3,
                    shotgunSpread: 50f,
                    castTime: 2.1f,
                    range: 1f,
                    shotsPerAttack: 1,
                    speed: 0.08f,
                    knockback: 0f,
                    deathrattleKnockback: 0f,
                    pierce: 20,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.1f,
                    thrownDamage: 1,
                    isSlow: true,
                    slowPercentage: 0.7f,
                    slowDuration: 2f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_smoke_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_smoke_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_smoke_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_smoke_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/grenade_smoke_1"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_smoke_2"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_smoke_3"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_smoke_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_smoke_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_smoke_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_smoke_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_smoke_4")
                },
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

            new AttackStats(rarity: Rarity.Epic, shotsPerAttack: 1, damage: 5),
            new AttackStats(rarity: Rarity.Epic, aimRangeAdditive: 2f, coneAngle: 30f),

            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.25f, pierce: 15, shakeRotation: 0.1f),
            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, spreadMultiplier: -0.15f, damage: 10)
        };

        List<AttackStats> SniperUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Rapid Fire 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("Velocity 1"),
            AttackStatsLibrary.GetStat("Reach 1"),
            AttackStatsLibrary.GetStat("Gravity 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Rapid Fire 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Velocity 2"),
            AttackStatsLibrary.GetStat("Reach 2"),
            AttackStatsLibrary.GetStat("Gravity 2"),
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
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Gamer 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Velocity 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Gravity 3"),
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
            AttackStatsLibrary.GetStat("Infusion++ 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Extra Round 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Ammo 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Concussive 2"),
            AttackStatsLibrary.GetStat("Infusion++ 2"),
        };

        AttackBuilder Sniper = new AttackBuilder()
            .SetAttackName("Sniper Rifle")
            .SetUnlockLevel(2)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_long"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Cheers, mate.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    damage: 21,
                    is360: false,
                    coneAngle: 20f,
                    spread: 0.65f,
                    spray: 1.5f,
                    sprayThreshold: 1,
                    castTime: 1.85f,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/sniper_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/sniper_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/sniper_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/sniper_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/sniper_1"),
                    Resources.Load<Sprite>("WeaponSprites/sniper_2"),
                    Resources.Load<Sprite>("WeaponSprites/sniper_3"),
                    Resources.Load<Sprite>("WeaponSprites/sniper_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/sniper_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/sniper_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/sniper_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/sniper_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SniperRarity)
                    .SetWeaponUpgrades(SniperUpgrades);

        AddAttack(Sniper);

        // GravityCannon
        List<AttackStats> GravityCannonRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 2),
            new AttackStats(rarity: Rarity.Rare, effectMultiplier: 0.25f),

            new AttackStats(rarity: Rarity.Epic, effectDuration: 0.25f, damage: 2),
            new AttackStats(rarity: Rarity.Epic, rangeMultiplier: 0.15f, aimRangeAdditive: 1f, coneAngle: 20f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1),
            new AttackStats(rarity: Rarity.Legendary, damage: 2, effectDuration: 1f),
        };

        List<AttackStats> GravityCannonUpgrades = new List<AttackStats>
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
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),
            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Puncture 3"),
            AttackStatsLibrary.GetStat("Big Gadget 3"),
            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Saboteur 3"),
            AttackStatsLibrary.GetStat("Hourglass 3"),
            AttackStatsLibrary.GetStat("Homing 1"),
            AttackStatsLibrary.GetStat("Cindershot 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split 3"),
        };

        AttackBuilder GravityCannon = new AttackBuilder()
            .SetAttackName("Gravity Cannon")
            .SetUnlockLevel(6)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/SuctionCannon_Orb"))
            .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetDescription("Mini-black hole dispenser.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    damage: 4,
                    is360: false,
                    coneAngle: 40f,
                    spread: 3f,
                    spray: 0f,
                    castTime: 2.5f,
                    range: 3.5f,
                    shotsPerAttack: 1,
                    speed: 0.018f,
                    knockback: 0f,
                    pierce: 30,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.25f,
                    shakeStrength: 0.4f,
                    shakeRotation: 0.5f,
                    thrownDamage: 18f,
                    thrownSpeed: 0.12f,
                    isMagnet: true,
                    magnetStrength: 5,
                    magnetDuration: 1.5f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/cannon_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/cannon_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/cannon_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/cannon_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/cannon_1"),
                    Resources.Load<Sprite>("WeaponSprites/cannon_2"),
                    Resources.Load<Sprite>("WeaponSprites/cannon_3"),
                    Resources.Load<Sprite>("WeaponSprites/cannon_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/cannon_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/cannon_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/cannon_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/cannon_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing_Energy"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(GravityCannonRarity)
                    .SetWeaponUpgrades(GravityCannonUpgrades);

        AddAttack(GravityCannon);

        // SuctionGrenade
        List<AttackStats> SuctionGrenadeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, speedMultiplier: 0.2f, rangeMultiplier: 0.2f, pierce: 10, aimRangeAdditive: 1f, coneAngle: 30),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.55f, damage: 2, shakeStrength: 0.1f),

            new AttackStats(rarity: Rarity.Epic, damage: 4),
            new AttackStats(rarity: Rarity.Epic, activeDuration: 0.5f, effectMultiplier: 0.25f, effectDuration: 0.5f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttack: 1, damage: 4),
            new AttackStats(rarity: Rarity.Legendary, projectileSizeMultiplier: 0.15f, shakeRotation: 0.1f),
        };

        List<AttackStats> SuctionGrenadeUpgrades = new List<AttackStats>
        {
            //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Reach 1"),

            //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Haste 2"),
            AttackStatsLibrary.GetStat("Reach 2"),

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
            AttackStatsLibrary.GetStat("Chain+ 1"),

            //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Reach 3"),
            AttackStatsLibrary.GetStat("Rapid Fire 3"),
            AttackStatsLibrary.GetStat("MLG 3"),
            AttackStatsLibrary.GetStat("Hacker 3"),
            AttackStatsLibrary.GetStat("AFK 3"),
            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("Extra Round 2"),
            AttackStatsLibrary.GetStat("Big Gadget 2"),
            AttackStatsLibrary.GetStat("Persistence+ 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),

            //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Infusion+ 2"),
        };

        AttackBuilder SuctionGrenade = new AttackBuilder()
            .SetAttackName("Suction Grenade")
            .SetUnlockLevel(6)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_suction"))
            .SetWeaponSetType(WeaponSetType.Explosive)
            .SetDescription("Maintain a safe distance.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    is360: false,
                    coneAngle: 25f,
                    damage: 4,
                    hasDeathrattle: true,
                    deathrattleDamage: 8,
                    spread: 1.5f,
                    castTime: 2f,
                    range: 2f,
                    shotsPerAttack: 1,
                    speed: 0.1f,
                    knockback: 0f,
                    deathrattleKnockback: 0.15f,
                    pierce: 15,
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
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_magnet_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_magnet_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_magnet_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/grenade_magnet_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/grenade_magnet_1"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_magnet_2"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_magnet_3"),
                    Resources.Load<Sprite>("WeaponSprites/grenade_magnet_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_magnet_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_magnet_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_magnet_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/grenade_magnet_4")
                },
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SuctionGrenadeRarity)
                    .SetWeaponUpgrades(SuctionGrenadeUpgrades);
        AddAttack(SuctionGrenade);

        //SuctionNova
        List<AttackStats> SuctionNovaRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, damage: 3, meleeSpacer: 3f, aimRangeAdditive: 2f),
            new AttackStats(rarity: Rarity.Rare, knockback: 0.15f, effectMultiplier: 0.2f),

            new AttackStats(rarity: Rarity.Epic, meleeSizeMultiplier: 0.15f, shakeRotation: 0.1f),
            new AttackStats(rarity: Rarity.Epic, damage: 4, knockback: 0.1f),

            new AttackStats(rarity: Rarity.Legendary, comboLength: 1, damage: 6),
            new AttackStats(rarity: Rarity.Legendary, effectDuration: 1f),
        };

        List<AttackStats> SuctionNovaUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Haste 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
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
            AttackStatsLibrary.GetStat("Saboteur 1"),
            AttackStatsLibrary.GetStat("Hourglass 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Split 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),

          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
            AttackStatsLibrary.GetStat("Haste 3"),
            AttackStatsLibrary.GetStat("Knockback 3"),
            AttackStatsLibrary.GetStat("Quick Hands 3"),
            AttackStatsLibrary.GetStat("Extend 3"),

            AttackStatsLibrary.GetStat("MLG 2"),
            AttackStatsLibrary.GetStat("Hacker 3"),

            AttackStatsLibrary.GetStat("Multicast 2"),
            AttackStatsLibrary.GetStat("One More 2"),
            AttackStatsLibrary.GetStat("Big Weapon 2"),
            AttackStatsLibrary.GetStat("Wave Surge 2"),
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Saboteur 2"),
            AttackStatsLibrary.GetStat("Hourglass 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Split 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
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
            AttackStatsLibrary.GetStat("Infusion+ 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder SuctionNova = new AttackBuilder()
            .SetAttackName("Suction Nova")
            .SetUnlockLevel(6)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_gravity"))
            .SetWeaponSetType(WeaponSetType.Nova)
            .SetDescription("GET OVER HERE!")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.25f,
                    is360: true,
                    damage: 9,
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
                    magnetDuration: 0.5f,
                    isSlow: true,
                    slowPercentage: 0.2f,
                    slowDuration: 1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_magnet_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_magnet_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_magnet_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/nova_magnet_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/nova_magnet_1"),
                    Resources.Load<Sprite>("WeaponSprites/nova_magnet_2"),
                    Resources.Load<Sprite>("WeaponSprites/nova_magnet_3"),
                    Resources.Load<Sprite>("WeaponSprites/nova_magnet_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_magnet_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_magnet_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_magnet_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/nova_magnet_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")
            )
            .SetRarityUpgrades(SuctionNovaRarity)
        .SetWeaponUpgrades(SuctionNovaUpgrades);
        AddAttack(SuctionNova);

        //WindBlade
        List<AttackStats> WindBladeRarity = new List<AttackStats>
        {
            new AttackStats(rarity: Rarity.Rare, comboWaitTime: -0.2f, shakeRotation: 0.1f, comboAttackBuffMultiplier: 0.10f),
            new AttackStats(rarity: Rarity.Rare, meleeSpacer: 1.5f, coneAngle: 30f, aimRangeAdditive: 1f),

            new AttackStats(rarity: Rarity.Epic, damage: 6, meleeSizeMultiplier: 0.15f),
            new AttackStats(rarity: Rarity.Epic, comboWaitTimeMultiplier: 0.5f),

            new AttackStats(rarity: Rarity.Legendary, shotsPerAttackMelee: 1, shakeStrength: 0.02f),
            new AttackStats(rarity: Rarity.Legendary, spread: -0.1f, damage: 4),

        };

        List<AttackStats> WindBladeUpgrades = new List<AttackStats>
        {
          //common
            AttackStatsLibrary.GetStat("Damage 1"),
            AttackStatsLibrary.GetStat("Critical 1"),
            AttackStatsLibrary.GetStat("Overkill 1"),
            AttackStatsLibrary.GetStat("Knockback 1"),
            AttackStatsLibrary.GetStat("Quick Hands 1"),
            AttackStatsLibrary.GetStat("Extend 1"),
            AttackStatsLibrary.GetStat("Implode 1"),
            AttackStatsLibrary.GetStat("Hacker 1"),
            AttackStatsLibrary.GetStat("AFK 1"),
            AttackStatsLibrary.GetStat("Slow Down 1"),
            AttackStatsLibrary.GetStat("Magnetize 1"),
            AttackStatsLibrary.GetStat("C-combo 1"),

          //rare
            AttackStatsLibrary.GetStat("Damage 2"),
            AttackStatsLibrary.GetStat("Critical 2"),
            AttackStatsLibrary.GetStat("Overkill 2"),
            AttackStatsLibrary.GetStat("Knockback 2"),
            AttackStatsLibrary.GetStat("Quick Hands 2"),
            AttackStatsLibrary.GetStat("Extend 2"),
            AttackStatsLibrary.GetStat("Hacker 2"),
            AttackStatsLibrary.GetStat("AFK 2"),
            AttackStatsLibrary.GetStat("Implode 2"),

            AttackStatsLibrary.GetStat("Multicast 1"),
            AttackStatsLibrary.GetStat("One More 1"),
            AttackStatsLibrary.GetStat("Big Weapon 1"),
            AttackStatsLibrary.GetStat("Wave Surge 1"),
            AttackStatsLibrary.GetStat("Aftershock 1"),
            AttackStatsLibrary.GetStat("Persistence 1"),
            AttackStatsLibrary.GetStat("Ignite 1"),
            AttackStatsLibrary.GetStat("Chain+ 1"),
            AttackStatsLibrary.GetStat("Split+ 1"),
            AttackStatsLibrary.GetStat("Slow Down 2"),
            AttackStatsLibrary.GetStat("Magnetize 2"),
            AttackStatsLibrary.GetStat("C-combo 2"),
 
          //epic
            AttackStatsLibrary.GetStat("Damage 3"),
            AttackStatsLibrary.GetStat("Critical 3"),
            AttackStatsLibrary.GetStat("Overkill 3"),
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
            AttackStatsLibrary.GetStat("Aftershock 2"),
            AttackStatsLibrary.GetStat("Persistence 2"),
            AttackStatsLibrary.GetStat("Ignite 2"),
            AttackStatsLibrary.GetStat("Chain+ 2"),
            AttackStatsLibrary.GetStat("Split+ 2"),
            AttackStatsLibrary.GetStat("Slow Down 3"),
            AttackStatsLibrary.GetStat("Magnetize 3"),
            AttackStatsLibrary.GetStat("Infusion+ 1"),
            AttackStatsLibrary.GetStat("C-combo 3"),

          //legendary
            AttackStatsLibrary.GetStat("Damage 4"),
            AttackStatsLibrary.GetStat("Critical 4"),
            AttackStatsLibrary.GetStat("Overkill 4"),
            AttackStatsLibrary.GetStat("Haste 4"),

            AttackStatsLibrary.GetStat("Multicast 3"),
            AttackStatsLibrary.GetStat("Big Weapon 3"),
            AttackStatsLibrary.GetStat("Aftershock 3"),

            AttackStatsLibrary.GetStat("Double Trouble"),
            AttackStatsLibrary.GetStat("Persistence 3"),
            AttackStatsLibrary.GetStat("Ignite 3"),
            AttackStatsLibrary.GetStat("Chain+ 3"),
            AttackStatsLibrary.GetStat("Split+ 3"),
            AttackStatsLibrary.GetStat("Infusion+ 2"),
            AttackStatsLibrary.GetStat("C-combo 4"),
        };

        AttackBuilder WindBlade = new AttackBuilder()
            .SetAttackName("Wind Blade")
            .SetUnlockLevel(0)
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/KatanaSlash"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetDescription("Power of God and Anime.")
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.7f,
                    damage: 10,
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
                    comboWaitTime: 0.45f,
                    meleeSpacer: 1.2f,
                    meleeSpacerGap: 1.1f,
                    meleeShotsScaleUp: -0.2f,
                    shakeTime: 0.08f,
                    shakeStrength: 0.12f,
                    shakeRotation: 0.1f,
                    thrownDamage: 5,
                    thrownSpeed: 0.6f,
                    swapAnimOnAttack: true,
                    comboAttackBuffMultiplier: 0.10f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Final/katana_1"),
                    Resources.Load<Sprite>("WeaponSprites/Final/katana_2"),
                    Resources.Load<Sprite>("WeaponSprites/Final/katana_3"),
                    Resources.Load<Sprite>("WeaponSprites/Final/katana_4")
                },
                thrownSprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/katana_1"),
                    Resources.Load<Sprite>("WeaponSprites/katana_2"),
                    Resources.Load<Sprite>("WeaponSprites/katana_3"),
                    Resources.Load<Sprite>("WeaponSprites/katana_4")
                },
                displaySprite: new List<Sprite> {
                    Resources.Load<Sprite>("WeaponSprites/Display/katana_1"),
                    Resources.Load<Sprite>("WeaponSprites/Display/katana_2"),
                    Resources.Load<Sprite>("WeaponSprites/Display/katana_3"),
                    Resources.Load<Sprite>("WeaponSprites/Display/katana_4")
                },
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown")
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
