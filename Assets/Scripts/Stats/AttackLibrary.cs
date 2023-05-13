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
        List<AttackStats> AcidPoolUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 2),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1, meleeShotsScaleUp: -0.15f),
            new AttackStats(meleeSize: 0.6f, shakeTime: 0.05f),
            new AttackStats(meleeSpacer: 2f, aimRangeAdditive:2f),
            new AttackStats(meleeSpacer: 1.5f, isCone: true, coneAngle: 120, aimRangeAdditive: 1.5f)
        };

        AttackBuilder AcidPool = new AttackBuilder()
            .SetAttackName("Acid Pool")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/AcidPool"))
            .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1,
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
                    shakeRotation: 0.5f,
                    meleeSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/hand2"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/hand2")
            )
            .SetRarityUpgrades(AcidPoolUpgrades);
        AddAttack(AcidPool);

        // ClassicRifle
        List<AttackStats> ClassicRifleRarity = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.5f),
            new AttackStats(thrownDamage: 8, throwSpeed: -0.3f),
            new AttackStats(shotsPerAttack: 10),
            new AttackStats(spread: -0.04f),
            new AttackStats(projectileSize: 0.75f, shakeStrength: 0.01f),
            new AttackStats(aimRangeAdditive: 2f),
            new AttackStats(coneAngle: 30),
            new AttackStats(pierce: 1)
        };

        List<AttackStats> ClassicRifleUpgrades = new List<AttackStats>
        {
            AttackStatsLibrary.GetStat("Knockback"),
            AttackStatsLibrary.GetStat("Damage"),
            AttackStatsLibrary.GetStat("Crit Chance")
        };

        AttackBuilder ClassicRifle = new AttackBuilder()
            .SetAttackName("Classic Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet"))
            .SetWeaponSetType(WeaponSetType.Automatic)
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
                    shotsPerAttack: 15,
                    speed: 0.17f,
                    knockback: 0.55f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.01f,
                    shakeStrength: 0.02f,
                    shakeRotation: 0.01f,
                    thrownDamage: 10f,
                    throwSpeed: 0.6f,
                    projectileSize: 1
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
        List<AttackStats> DoubleBarrelUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 9),
            new AttackStats(castTime: -0.3f),
            new AttackStats(thrownDamage: 11, throwSpeed: -0.3f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotgunSpread: 40f),
            new AttackStats(projectileSize: 1f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f),
            new AttackStats(knockback: 1f, shakeTime: 0.05f),
            new AttackStats(aimRangeAdditive: 2f),
            new AttackStats(aimRangeAdditive: 0.5f, coneAngle: 30f)
        };

        AttackBuilder DoubleBarrel = new AttackBuilder()
            .SetAttackName("Double Barrel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
                        .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    isCone: true,
                    coneAngle: 60,
                    damage: 9,
                    shotgunSpread: 40f,
                    spray: 0,
                    castTime: 1.7f,
                    range: 2.5f,
                    shotsPerAttack: 2,
                    speed: 0.18f,
                    knockback: 0.6f,
                    pierce: 99,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.4f,
                    shakeRotation: 0.05f,
                    thrownDamage: 7,
                    throwSpeed: 0.6f,
                    projectileSize: 1
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
            .SetRarityUpgrades(DoubleBarrelUpgrades);
        AddAttack(DoubleBarrel);


        //drain scythe
        List<AttackStats> DrainScytheUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1, damage: -2),
            new AttackStats(shotsPerAttack: 1, meleeShotsScaleUp: -0.15f),
            new AttackStats(meleeSize: 0.6f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2.5f),
            new AttackStats(thrownDamage: 12, throwSpeed: 0.3f),
            new AttackStats(aimRangeAdditive: 2f)
        };
        AttackBuilder DrainScythe = new AttackBuilder()
            .SetAttackName("Drain Scythe")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DrainScythe"))
                                    .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
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
                    throwSpeed: 0.4f,
                    meleeSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/scythe_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Scythe_01")
            )
            .SetRarityUpgrades(DrainScytheUpgrades);
        AddAttack(DrainScythe);

        //earth Shock
        List<AttackStats> EarthShockUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 3),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1, spread: -0.1f),
            new AttackStats(meleeSize: 0.6f),
            new AttackStats(thrownDamage: 12, throwSpeed: 0.3f),
            new AttackStats(meleeShotsScaleUp: 0.1f, shakeTime: 0.05f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 60),
        };
        AttackBuilder EarthShock = new AttackBuilder()
            .SetAttackName("Earth Shock")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Frogger"))
                                    .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    isCone: true,
                    coneAngle: 60f,
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
                    throwSpeed: 0.5f,
                    meleeSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/frog_gun_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/frog_01")
            )
            .SetRarityUpgrades(EarthShockUpgrades);
        AddAttack(EarthShock);

        //GravityGrab
        List<AttackStats> GravityGrabUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(meleeSize: 0.5f),
            new AttackStats(meleeSpacer: 1.5f, meleeSpacerGap: 1f),
            new AttackStats(knockback: 0.6f, shakeStrength: 0.1f),
            new AttackStats(meleeShotsScaleUp: 0.15f, shakeTime: 0.05f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 60f)
        };
        AttackBuilder GravityGrab = new AttackBuilder()
            .SetAttackName("Eldritch Grasp")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/GravityGrab"))
                                    .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.75f,
                    isCone: true,
                    coneAngle: 120,
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
                    throwSpeed: 0f,
                    meleeSize: 1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GrabHand_Dark_01"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/GrabHand_Dark_01")
            )
            .SetRarityUpgrades(GravityGrabUpgrades);
        AddAttack(GravityGrab);

        // GatlingGun
        List<AttackStats> GatlingGunUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 4),
            new AttackStats(castTime: -0.5f),
            new AttackStats(thrownDamage: 8, throwSpeed: -1f),
            new AttackStats(shotsPerAttack: 30),
            new AttackStats(spread: -0.004f, spray: 0.15f),
            new AttackStats(projectileSize: 0.75f),
            new AttackStats(speed: 0.15f, spray: 100f),
            new AttackStats(pierce: 1),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 30f)
        };

        AttackBuilder GatlingGun = new AttackBuilder()
            .SetAttackName("Gatling Gun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Tiny"))
                                    .SetWeaponSetType(WeaponSetType.Automatic)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.25f,
                    isCone: true,
                    coneAngle: 45f,
                    damage: 3,
                    spread: 0.018f,
                    spray: 10f,
                    castTime: 2.5f,
                    range: 5.5f,
                    shotsPerAttack: 100,
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
                    throwSpeed: 0.2f,
                    projectileSize: 1
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
        List<AttackStats> GodHandUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 10),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1, meleeShotsScaleUp: -0.1f),
            new AttackStats(meleeSize: 0.5f),
            new AttackStats(meleeSpacer: 1.25f, meleeSpacerGap: 1.5f),
            new AttackStats(knockback: 0.6f, shakeTime: 0.05f, shakeRotation: 0.5f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 60f)
        };
        AttackBuilder GodHand = new AttackBuilder()
            .SetAttackName("God Hand")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/MeleeFist"))
                                    .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    isCone: true,
                    coneAngle: 120f,
                    damage: 12,
                    spread: 0.28f,
                    castTime: 2.1f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.8f,
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
                    throwSpeed: 0f,
                    meleeSize: 1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GodHand_01"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/GodHand_01")
            )
            .SetRarityUpgrades(GodHandUpgrades);
        AddAttack(GodHand);

        // ImpactGrenade
        List<AttackStats> ImpactGrenadeUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 6),
            new AttackStats(castTime: -0.6f),
            new AttackStats(castTime: 1f, damage: 10, projectileSize: 0.2f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(spread: -0.03f, speed: 0.1f),
            new AttackStats(projectileSize: 0.5f, shakeStrength: 0.15f),
            new AttackStats(knockback: 0.7f, shakeRotation: 0.3f),
            new AttackStats(range: 2f, aimRangeAdditive: 2f),
            new AttackStats(isCone: true, coneAngle: 30f, pierce: 1)
        };

        AttackBuilder ImpactGrenade = new AttackBuilder()
            .SetAttackName("Impact Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_impact"))
                                    .SetWeaponSetType(WeaponSetType.Explosive)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
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
                    shakeRotation: 0.1f,
                    projectileSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_frag"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(ImpactGrenadeUpgrades);
        AddAttack(ImpactGrenade);

        // ImpactMine
        List<AttackStats> ImpactMineUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.5f),
            new AttackStats(castTime: 1f, damage: 10, projectileSize: 0.1f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(spread: -0.035f),
            new AttackStats(projectileSize: 0.4f, shakeStrength: 0.15f),
            new AttackStats(knockback: 0.5f, shakeRotation: 0.3f),
            new AttackStats(pierce: 4)
        };

        AttackBuilder ImpactMine = new AttackBuilder()
            .SetAttackName("Impact Mine")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/mine_impact"))
                                    .SetWeaponSetType(WeaponSetType.Explosive)

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
                    throwSpeed: 0f,
                    projectileSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/mine_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Mine_01"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(ImpactMineUpgrades);
        AddAttack(ImpactMine);

        //ImpactNova
        List<AttackStats> ImpactNovaUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 11),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboLength: 1, damage: -4),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(meleeSize: 0.4f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2.5f),
            new AttackStats(meleeShotsScaleUp: 0.12f),
            new AttackStats(thrownDamage: 12, throwSpeed: 0.3f),
            new AttackStats(aimRangeAdditive: 2f, meleeSpacer: 1.5f)
        };
        AttackBuilder ImpactNova = new AttackBuilder()
            .SetAttackName("Impact Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_impact"))
                                    .SetWeaponSetType(WeaponSetType.Nova)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 13,
                    spread: 0.35f,
                    castTime: 2.2f,
                    knockback: 0.7f,
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
                    throwSpeed: 0.5f,
                    meleeSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_01")
            )
            .SetRarityUpgrades(ImpactNovaUpgrades);
        AddAttack(ImpactNova);

        //LaserBeam
        List<AttackStats> LaserBeamUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 10),
            new AttackStats(castTime: -0.6f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(meleeSize: 0.4f),
            new AttackStats(meleeShotsScaleUp: 0.2f),
            new AttackStats(knockback: 0.8f, shakeTime: 0.05f, shakeRotation: 0.3f),
            new AttackStats(aimRangeAdditive: 2.25f)
        };
        AttackBuilder LaserBeam = new AttackBuilder()
            .SetAttackName("Laser Beam")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DoubleBeam"))
                                    .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2.5f,
                    isCone: true,
                    coneAngle: 60f,
                    damage: 15,
                    spread: 0.3f,
                    castTime: 2.5f,
                    shotsPerAttackMelee: 0,
                    knockback: 1f,
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
                    throwSpeed: 0f,
                    meleeSize: 1f
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/gunFlash1_0"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/gunFlash1_0")
            )
            .SetRarityUpgrades(LaserBeamUpgrades);
        AddAttack(LaserBeam);

        // PainWheel
        List<AttackStats> PainWheelUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 4),
            new AttackStats(castTime: -0.5f),
            new AttackStats(speed: -0.05f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(knockback: 0.4f, shakeRotation: 0.4f),
            new AttackStats(projectileSize: 0.5f, shakeStrength: 0.1f),
            new AttackStats(range: 2f, aimRangeAdditive: 2f),
            new AttackStats(coneAngle:45f, shakeTime: 0.05f),
        };

        AttackBuilder PainWheel = new AttackBuilder()
            .SetAttackName("Pain Wheel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken"))
                                    .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    isCone: true,
                    coneAngle: 45,
                    damage: 5,
                    spread: 0.4f,
                    spray: 0f,
                    castTime: 2.1f,
                    range: 3f,
                    shotsPerAttack: 1,
                    speed: 0.15f,
                    knockback: 0f,
                    pierce: 50,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.1f,
                    thrownDamage: 13f,
                    throwSpeed: 0.6f,
                    projectileSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shuriken_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shuriken_1"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(PainWheelUpgrades);
        AddAttack(PainWheel);

        //PetrifyGrenade
        List<AttackStats> PetrifyGrenadeUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 8),
            new AttackStats(castTime: -0.4f),
            new AttackStats(pierce: 99),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotgunSpread: 40f),
            new AttackStats(projectileSize: 0.3f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f, aimRangeAdditive: 1.5f),
            new AttackStats(projectileSize: 0.3f, shakeTime: 0.05f)
        };

        AttackBuilder PetrifyGrenade = new AttackBuilder()
            .SetAttackName("Petrify Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_stun"))
                                    .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 1,
                    shotgunSpread: 65f,
                    spray: 0,
                    castTime: 1.9f,
                    range: 1.5f,
                    shotsPerAttack: 1,
                    speed: 0.1f,
                    knockback: 0f,
                    pierce: 10,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.05f,
                    projectileSize: 1
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
        List<AttackStats> PetrifyNovaUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 8),
            new AttackStats(castTime: -0.6f),
            new AttackStats(comboLength: 1),
            new AttackStats(shotsPerAttack: 1, damage: -1),
            new AttackStats(meleeSize: 0.5f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2f),
            new AttackStats(meleeShotsScaleUp: 0.12f),
            new AttackStats(thrownDamage: 12, throwSpeed: 0.3f),
            new AttackStats(meleeSpacer: 1.5f, aimRangeAdditive: 2f),
        };
        AttackBuilder PetrifyNova = new AttackBuilder()
            .SetAttackName("Petrify Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_Petrify"))
                                    .SetWeaponSetType(WeaponSetType.Nova)
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
                    throwSpeed: 0.5f,
                    meleeSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_03"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_03")
            )
            .SetRarityUpgrades(PetrifyNovaUpgrades);
        AddAttack(PetrifyNova);

        // Revolver
        List<AttackStats> RevolverUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 9),
            new AttackStats(castTime: -0.4f),
            new AttackStats(thrownDamage: 12, throwSpeed: 0.25f),
            new AttackStats(shotsPerAttack: 2),
            new AttackStats(shotsPerAttack: 2),
            new AttackStats(projectileSize: 0.5f, shakeStrength: 0.05f),
            new AttackStats(speed: 0.04f, spray: 0.5f, shakeRotation: 0.05f),
            new AttackStats(pierce: 1),
            new AttackStats(aimRangeAdditive: 1.5f, coneAngle: 30f),

        };

        AttackBuilder Revolver = new AttackBuilder()
            .SetAttackName("Revolver")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Magnum"))
                                    .SetWeaponSetType(WeaponSetType.SemiAuto)
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
                    knockback: 0.45f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 6f,
                    throwSpeed: 0.5f,
                    projectileSize: 1
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
            .SetRarityUpgrades(RevolverUpgrades);
        AddAttack(Revolver);

        //Shotgun
        List<AttackStats> ShotgunUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 9),
            new AttackStats(castTime: -0.6f),
            new AttackStats(thrownDamage: 12, throwSpeed: -0.25f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotgunSpread: 35f),
            new AttackStats(projectileSize: 0.4f, shakeStrength: 0.2f),
            new AttackStats(range: 1.5f),
            new AttackStats(knockback: 0.5f, shakeTime: 0.05f),
            new AttackStats(aimRangeAdditive: 1.75f, coneAngle: 50)
        };

        AttackBuilder Shotgun = new AttackBuilder()
            .SetAttackName("Shotgun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
                                    .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.75f,
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
                    pierce: 99,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.35f,
                    shakeRotation: 0.05f,
                    thrownDamage: 8,
                    throwSpeed: 0.5f,
                    projectileSize: 1
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
            .SetRarityUpgrades(ShotgunUpgrades);
        AddAttack(Shotgun);

        //Shuriken
        List<AttackStats> ShurikenUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 2),
            new AttackStats(castTime: -0.5f),
            new AttackStats(thrownDamage: 8, throwSpeed: -0.2f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotgunSpread: 35f),
            new AttackStats(projectileSize: 0.4f, shakeStrength: 0.2f),
            new AttackStats(range: 1.6f, speed: 0.015f),
            new AttackStats(knockback: 0.5f, shakeTime: 0.05f),
            new AttackStats(aimRangeAdditive: 2f)

        };

        AttackBuilder Shuriken = new AttackBuilder()
            .SetAttackName("Shuriken")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken_small"))
                                    .SetWeaponSetType(WeaponSetType.Shotgun)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 2,
                    shotgunSpread: 100f,
                    spray: 0,
                    castTime: 2f,
                    range: 2.8f,
                    shotsPerAttack: 4,
                    speed: 0.085f,
                    knockback: 0f,
                    pierce: 30,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.01f,
                    shakeRotation: 0.01f,
                    thrownDamage: 7,
                    throwSpeed: 0.5f,
                    projectileSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shuriken_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shuriken_2"),
                muzzleFlashPrefab: BigMuzzleFlash
            )
            .SetRarityUpgrades(ShurikenUpgrades);
        AddAttack(Shuriken);

        // SMG
        List<AttackStats> SMGRarity = new List<AttackStats>
        {
            new AttackStats(damage: 4),
            new AttackStats(castTime: -0.5f),
            new AttackStats(range: 1f, speed: 0.05f),
            new AttackStats(shotsPerAttack: 10),
            new AttackStats(spread: -0.01f),
            new AttackStats(projectileSize: 0.5f, shakeRotation: 0.02f),
            new AttackStats(spray: 100f, coneAngle: 40f),
            new AttackStats(pierce: 1),
            new AttackStats(aimRangeAdditive: 2f)
        };

        AttackBuilder SMG = new AttackBuilder()
            .SetAttackName("SMG")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_SMG"))
                                    .SetWeaponSetType(WeaponSetType.Automatic)
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
                    knockback: 0.3f,
                    pierce: 0,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.01f,
                    shakeStrength: 0.01f,
                    shakeRotation: 0f,
                    thrownDamage: 6f,
                    throwSpeed: 0.6f,
                    projectileSize: 1
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
            .SetRarityUpgrades(SMGRarity);
        AddAttack(SMG);


        // Smoke Grenade
        List<AttackStats> SmokeGrenadeUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 2),
            new AttackStats(castTime: -0.6f),
            new AttackStats(projectileSize: 0.25f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(shotgunSpread: 20f),
            new AttackStats(range: 1.25f, shakeStrength: 0.1f),
            new AttackStats(speed: -0.25f, pierce: 5),
            new AttackStats(knockback: 0.25f),
            new AttackStats(aimRangeAdditive: 2f)
        };

        AttackBuilder SmokeGrenade = new AttackBuilder()
            .SetAttackName("Smoke Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_slow"))
                                    .SetWeaponSetType(WeaponSetType.Shotgun)    
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 1,
                    shotgunSpread: 50f,
                    castTime: 2.2f,
                    range: 1.75f,
                    shotsPerAttack: 3,
                    speed: 0.08f,
                    knockback: 0f,
                    pierce: 3,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.05f,
                    shakeStrength: 0.05f,
                    shakeRotation: 0.1f,
                    projectileSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Shotgun,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_smoke"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_smoke"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SmokeGrenadeUpgrades);
        AddAttack(SmokeGrenade);

        // Sniper
        List<AttackStats> SniperUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 15),
            new AttackStats(castTime: -0.5f),
            new AttackStats(critChance: 0.15f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(knockback: 0.4f, shakeRotation: 0.4f),
            new AttackStats(projectileSize: 0.5f, shakeRotation: 0.1f),
            new AttackStats(aimRangeAdditive: 2f, coneAngle: 30f),
            new AttackStats(pierce: 10, shakeTime: 0.05f)
        };

        AttackBuilder Sniper = new AttackBuilder()
            .SetAttackName("Sniper Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_long"))
                                    .SetWeaponSetType(WeaponSetType.SemiAuto)
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
                    knockback: 0.7f,
                    pierce: 5,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.4f,
                    shakeRotation: 0.1f,
                    thrownDamage: 10f,
                    throwSpeed: 0.5f,
                    projectileSize: 1
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
            .SetRarityUpgrades(SniperUpgrades);
        AddAttack(Sniper);

        // SuctionCannon
        List<AttackStats> SuctionCannonUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 3),
            new AttackStats(castTime: -0.6f),
            new AttackStats(speed: -0.008f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(range: 1.5f, aimRangeAdditive: 1.5f),
            new AttackStats(projectileSize: 0.3f, shakeRotation: 0.1f),
            new AttackStats(throwSpeed: 5f),
            new AttackStats(pierce: 50),
            new AttackStats(coneAngle: 45f, aimRangeAdditive: 1f)
        };

        AttackBuilder SuctionCannon = new AttackBuilder()
            .SetAttackName("Suction Cannon")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/SuctionCannon_Orb"))
                                    .SetWeaponSetType(WeaponSetType.SemiAuto)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 2f,
                    damage: 1,
                    isCone: true,
                    coneAngle: 45f,
                    spread: 1f,
                    spray: 2f,
                    sprayThreshold: 1,
                    castTime: 2.75f,
                    range: 4f,
                    shotsPerAttack: 1,
                    speed: 0.018f,
                    knockback: 0f,
                    pierce: 20,
                    critChance: 0.05f,
                    critDmg: 1.5f,
                    multicastWaitTime: 0.25f,
                    shakeTime: 0.1f,
                    shakeStrength: 0.1f,
                    shakeRotation: 0.05f,
                    thrownDamage: 25f,
                    throwSpeed: 0.12f,
                    projectileSize: 1
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
            .SetRarityUpgrades(SuctionCannonUpgrades);
        AddAttack(SuctionCannon);


        // SuctionGrenade
        List<AttackStats> SuctionGrenadeUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 5),
            new AttackStats(castTime: -0.6f),
            new AttackStats(speed: 0.1f, range: 1f),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(range: 1f, aimRangeAdditive: 1.5f),
            new AttackStats(projectileSize: 0.4f, shakeRotation: 0.1f),
            new AttackStats(knockback: 0.5f, shakeStrength: 0.1f),
            new AttackStats(pierce: 5)
        };

        AttackBuilder SuctionGrenade = new AttackBuilder()
            .SetAttackName("Suction Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Grenade/grenade_suction"))
                                    .SetWeaponSetType(WeaponSetType.Explosive)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 2,
                    spread: 0.6f,
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
                    projectileSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_magnet"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_magnet"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetRarityUpgrades(SuctionGrenadeUpgrades);
        AddAttack(SuctionGrenade);


        //SuctionNova
        List<AttackStats> SuctionNovaUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 7),
            new AttackStats(castTime: -0.6f),
            new AttackStats(comboLength: 1, damage: -2),
            new AttackStats(shotsPerAttack: 1),
            new AttackStats(meleeSize: 0.3f, shakeRotation: 0.5f),
            new AttackStats(meleeSpacer: 2f),
            new AttackStats(meleeSpacer: 1.5f, aimRangeAdditive: 2f),
            new AttackStats(thrownDamage: 5, throwSpeed: -0.22f)
        };
        AttackBuilder SuctionNova = new AttackBuilder()
            .SetAttackName("Suction Nova")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_gravity"))
                                    .SetWeaponSetType(WeaponSetType.Nova)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1f,
                    damage: 3,
                    spread: 0.4f,
                    castTime: 2.5f,
                    knockback: 0.35f,
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
                    throwSpeed: 0.6f,
                    meleeSize: 1
                )
            )
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_02")
            )
            .SetRarityUpgrades(SuctionNovaUpgrades);
        AddAttack(SuctionNova);


        //WindBlade
        List<AttackStats> WindBladeUpgrades = new List<AttackStats>
        {
            new AttackStats(damage: 4),
            new AttackStats(castTime: -0.5f),
            new AttackStats(comboWaitTime: -0.075f),
            new AttackStats(shotsPerAttack: 1, meleeShotsScaleUp: -0.1f),
            new AttackStats(meleeSize: 0.4f, shakeStrength: 0.04f),
            new AttackStats(meleeSpacer: 1.25f, meleeSpacerGap: 1.5f),
            new AttackStats(knockback: 0.4f, shakeRotation: 0.3f),
            new AttackStats(comboLength: -1, damage: 7, thrownDamage: 5),
            new AttackStats(meleeSpacer: 1.5f, aimRangeAdditive: 1.5f),
            new AttackStats(coneAngle: 60f, aimRangeAdditive: 1f)
        };
        AttackBuilder WindBlade = new AttackBuilder()
            .SetAttackName("Wind Blade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/KatanaSlash"))
                                    .SetWeaponSetType(WeaponSetType.Melee)
            .SetBaseStats(
                new AttackStats(
                    aimRange: 1.5f,
                    damage: 5,
                    isCone: true,
                    coneAngle: 120f,
                    spread: 0.18f,
                    castTime: 2f,
                    shotsPerAttackMelee: 0,
                    knockback: 0.4f,
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
                    throwSpeed: 0.6f,
                    meleeSize: 1f
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
