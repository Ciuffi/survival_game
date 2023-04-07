using System.Collections.Generic;
using UnityEngine;

public static class AttackLibrary
{
    private static Dictionary<string, AttackBuilder> attackBuilderDictionary =
        new Dictionary<string, AttackBuilder>();
    private static bool isInitialized = false;

    static AttackLibrary()
    {
        InitializeLibrary();
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
                new AttackStats(meleeSpacer: 2.5f),
                new AttackStats(comboWaitTime: -0.4f),
                new AttackStats(spread: -0.15f)
            };

        AttackBuilder AcidPool = new AttackBuilder()
            .SetAttackName("Acid Pool")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/AcidPool"))
            .SetBaseStats(new AttackStats(damage: 1, spread: 0.3f, castTime: 2.2f, critChance: 0.05f, critDmg: 1.5f, 
                                            multicastWaitTime: 0.25f, comboLength: 1, comboWaitTime: 0.6f, meleeShotsScaleUp: -0.1f,
                                            meleeSpacer: 1.5f, meleeSpacerGap: 4.5f, shakeTime: 0.1f, shakeStrength: 0.2f, shakeRotation: 0.5f,
                                            meleeSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Melee,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/hand2"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/hand2")
            )
            .SetUpgrades(AcidPoolUpgrades);
        attackBuilderDictionary.Add("Acid Pool", AcidPool);


        // ClassicRifle
        List<AttackStats> ClassicRifleUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 5),
                new AttackStats(castTime: -0.5f),
                new AttackStats(thrownDamage: 8, throwSpeed: -0.3f),
                new AttackStats(shotsPerAttack: 10),
                new AttackStats(spread: -0.04f),
                new AttackStats(projectileSize: 0.75f, shakeStrength: 0.01f),
                new AttackStats(spray: 0.5f),
                new AttackStats(pierce: 1)
            };

        AttackBuilder ClassicRifle = new AttackBuilder()
            .SetAttackName("Classic Rifle")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet"))
            .SetBaseStats(new AttackStats(damage: 6, spread: 0.085f, spray: 1.2f, castTime: 2.1f,
                                            range: 7f, shotsPerAttack: 20, speed: 0.17f, knockback: 0.55f,
                                            pierce: 0, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                            shakeTime: 0.01f, shakeStrength: 0.02f, shakeRotation: 0.01f,
                                            thrownDamage: 10f, throwSpeed: 0.6f, projectileSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/Burst_03"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/BurstRifle_02"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetUpgrades(ClassicRifleUpgrades);
        attackBuilderDictionary.Add("Classic Rifle", ClassicRifle);


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
                new AttackStats(knockback: 1f, shakeTime: 0.05f)
            };

        AttackBuilder DoubleBarrel = new AttackBuilder()
           .SetAttackName("Double Barrel")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
           .SetBaseStats(new AttackStats(damage: 9, shotgunSpread: 40f, spray: 0, castTime: 1.7f,
                                           range: 2.5f, shotsPerAttack: 2, speed: 0.18f, knockback: 0.6f,
                                           pierce: 99, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           shakeTime: 0.1f, shakeStrength: 0.4f, shakeRotation: 0.05f,
                                           thrownDamage: 7, throwSpeed: 0.6f, projectileSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Shotgun,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shotgun_double_1"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/shotgun_double"),
               bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
               muzzleFlashPrefab: BigMuzzleFlash
           )
           .SetUpgrades(DoubleBarrelUpgrades);
        attackBuilderDictionary.Add("Double Barrel", DoubleBarrel);


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
                new AttackStats(comboWaitTime: -0.4f, spread: -0.1f)

            };
        AttackBuilder DrainScythe = new AttackBuilder()
           .SetAttackName("Drain Scythe")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DrainScythe"))
           .SetBaseStats(new AttackStats(damage: 4, spread: 0.2f, castTime: 2.2f, knockback: 0.3f, 
                                           critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 0.8f, meleeShotsScaleUp: -0.1f,
                                           meleeSpacer: 2f, meleeSpacerGap: 3.5f,
                                           shakeTime: 0.1f, shakeStrength: 0.5f, shakeRotation: 0.1f,
                                           thrownDamage: 9f, throwSpeed: 0.4f , meleeSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/scythe_01"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/Scythe_01")
           )
           .SetUpgrades(DrainScytheUpgrades);
        attackBuilderDictionary.Add("Drain Scythe", DrainScythe);


        //earth Shock
        List<AttackStats> EarthShockUpgrades = new List<AttackStats>
            {
              new AttackStats(damage: 3),
                new AttackStats(castTime: -0.5f),
                new AttackStats(comboLength: 1),
                new AttackStats(shotsPerAttack: 1, spread: -0.1f),
                new AttackStats(meleeSize: 0.6f),
                new AttackStats(meleeSpacer: 2.5f),
                new AttackStats(thrownDamage: 12, throwSpeed: 0.3f),
                new AttackStats(meleeShotsScaleUp: 0.1f, shakeTime: 0.05f)

            };
        AttackBuilder EarthShock = new AttackBuilder()
           .SetAttackName("Earth Shock")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Frogger"))
           .SetBaseStats(new AttackStats(damage: 3, spread: 0.22f, castTime: 2.1f, shotsPerAttackMelee: 2,
                                           knockback: 0.35f, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 0.3f, meleeShotsScaleUp: 0f,
                                           meleeSpacer: 1.5f, meleeSpacerGap: 3f,
                                           shakeTime: 0.1f, shakeStrength: 0.2f, shakeRotation: 1f,
                                           thrownDamage: 7f, throwSpeed: 0.5f , meleeSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/frog_gun_01"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/frog_01")
           )
           .SetUpgrades(EarthShockUpgrades);
        attackBuilderDictionary.Add("Earth Shock", EarthShock);


        //GravityGrab
        List<AttackStats> GravityGrabUpgrades = new List<AttackStats>
            {
              new AttackStats(damage: 5),
                new AttackStats(castTime: -0.5f),
                new AttackStats(comboLength: 1),
                new AttackStats(shotsPerAttack: 1),
                new AttackStats(meleeSize: 0.5f),
                new AttackStats(meleeSpacer: 1f, meleeSpacerGap: 1f),
                new AttackStats(knockback: 0.6f, shakeStrength: 0.1f),
                new AttackStats(meleeShotsScaleUp: 0.15f, shakeTime: 0.05f)

            };
        AttackBuilder GravityGrab = new AttackBuilder()
           .SetAttackName("Eldritch Grasp")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/GravityGrab"))
           .SetBaseStats(new AttackStats(damage: 3, spread: 0.3f, castTime: 2f, shotsPerAttackMelee: 0,
                                           knockback: 0f, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 0.6f, meleeShotsScaleUp: -0.1f,
                                           meleeSpacer: 0.5f, meleeSpacerGap: 2.5f,
                                           shakeTime: 0.1f, shakeStrength: 0.3f, shakeRotation: 1f,
                                           thrownDamage: 0, throwSpeed: 0f, meleeSize: 1f))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GrabHand_Dark_01"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/GrabHand_Dark_01")
           )
           .SetUpgrades(GravityGrabUpgrades);
        attackBuilderDictionary.Add("Eldritch Grasp", GravityGrab);


        // GatlingGun
        List<AttackStats> GatlingGunUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 4),
                new AttackStats(castTime: -0.5f),
                new AttackStats(thrownDamage: 8, throwSpeed: 0.4f),
                new AttackStats(shotsPerAttack: 30),
                new AttackStats(spread: -0.004f, spray: 0.02f),
                new AttackStats(projectileSize: 0.75f),
                new AttackStats(speed: 0.15f, spray: 0.04f),
                new AttackStats(pierce: 1)
            };

        AttackBuilder GatlingGun = new AttackBuilder()
            .SetAttackName("Gatling Gun")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Tiny"))
            .SetBaseStats(new AttackStats(damage: 3, spread: 0.018f, spray: 0.18f, castTime: 2.5f,
                                            range: 5.5f, shotsPerAttack: 100, speed: 0.2f, knockback: 0.32f,
                                            pierce: 0, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                            shakeTime: 0.01f, shakeStrength: 0.01f, shakeRotation: 0.01f,
                                            thrownDamage: 22f, throwSpeed: 0.2f, projectileSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/gatling_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Gatling_02"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: AutomaticMuzzleFlash
            )
            .SetUpgrades(GatlingGunUpgrades);
        attackBuilderDictionary.Add("Gatling Gun", GatlingGun);


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
                new AttackStats(meleeShotsScaleUp: 0.3f, shakeTime: 0.05f)

            };
        AttackBuilder GodHand = new AttackBuilder()
           .SetAttackName("God Hand")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/MeleeFist"))
           .SetBaseStats(new AttackStats(damage: 12, spread: 0.28f, castTime: 2.1f, shotsPerAttackMelee: 0,
                                           knockback: 0.8f, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 0.5f, meleeShotsScaleUp: 0f,
                                           meleeSpacer: 2f, meleeSpacerGap: 2f,
                                           shakeTime: 0.15f, shakeStrength: 0.7f, shakeRotation: 0.5f,
                                           thrownDamage: 0, throwSpeed: 0f, meleeSize: 1f))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/GodHand_01"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/GodHand_01")
           )
           .SetUpgrades(GodHandUpgrades);
        attackBuilderDictionary.Add("God Hand", GodHand);


        // ImpactGrenade
        List<AttackStats> ImpactGrenadeUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 6),
                new AttackStats(castTime: -0.6f),
                new AttackStats(castTime: 1f, damage: 10, projectileSize: 0.2f),
                new AttackStats(shotsPerAttack: 1),
                new AttackStats(spread: -0.03f, speed: 0.06f),
                new AttackStats(projectileSize: 0.5f, shakeStrength: 0.15f),
                new AttackStats(knockback: 0.7f, shakeRotation: 0.3f),
                new AttackStats(range: 2f, pierce: 2)
            };

        AttackBuilder ImpactGrenade = new AttackBuilder()
            .SetAttackName("Impact Grenade")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/grenade_impact"))
            .SetBaseStats(new AttackStats(damage: 2, spread: 0.6f, spray: 0f, castTime: 2.5f,
                                            range: 2f, shotsPerAttack: 1, speed: 0.08f, knockback: 0f,
                                            pierce: 0, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                            shakeTime: 0.1f, shakeStrength: 0.15f, shakeRotation: 0.1f,
                                            thrownDamage: 0f, throwSpeed: 0f, projectileSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_frag"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_frag"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetUpgrades(ImpactGrenadeUpgrades);
        attackBuilderDictionary.Add("Impact Grenade", ImpactGrenade);


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
            .SetBaseStats(new AttackStats(damage: 5, spread: 0.75f, spray: 0f, castTime: 2.5f,
                                            range: 0f, shotsPerAttack: 1, speed: 0f, knockback: 0.3f,
                                            pierce: 0, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                            shakeTime: 0.1f, shakeStrength: 0.15f, shakeRotation: 0.1f,
                                            thrownDamage: 0f, throwSpeed: 0f, projectileSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/mine_01"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/Mine_01"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetUpgrades(ImpactMineUpgrades);
        attackBuilderDictionary.Add("Impact Mine", ImpactMine);


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
                new AttackStats(thrownDamage: 12, throwSpeed: 0.3f)

            };
        AttackBuilder ImpactNova = new AttackBuilder()
           .SetAttackName("Impact Nova")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_impact"))
           .SetBaseStats(new AttackStats(damage: 13, spread: 0.35f, castTime: 2.2f, knockback: 0.7f,
                                           critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 0.75f, meleeShotsScaleUp: 0f,
                                           meleeSpacer: 0f, meleeSpacerGap: 0f,
                                           shakeTime: 0.1f, shakeStrength: 0.5f, shakeRotation: 0.1f,
                                           thrownDamage: 9f, throwSpeed: 0.5f, meleeSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_01"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_01")
           )
           .SetUpgrades(ImpactNovaUpgrades);
        attackBuilderDictionary.Add("Impact Nova", ImpactNova);


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
                new AttackStats(comboWaitTime: -0.3f, spread: -0.1f)

            };
        AttackBuilder LaserBeam = new AttackBuilder()
           .SetAttackName("Laser Beam")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/DoubleBeam"))
           .SetBaseStats(new AttackStats(damage: 15, spread: 0.3f, castTime: 2.5f, shotsPerAttackMelee: 0,
                                           knockback: 1f, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 0.6f, meleeShotsScaleUp: -0.1f,
                                           meleeSpacer: 3f, meleeSpacerGap: 3.8f,
                                           shakeTime: 0.1f, shakeStrength: 0.5f, shakeRotation: 0.5f,
                                           thrownDamage: 0, throwSpeed: 0f, meleeSize: 1f))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/gunFlash1_0"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/GunFlash5_0")
           )
           .SetUpgrades(LaserBeamUpgrades);
        attackBuilderDictionary.Add("Laser Beam", LaserBeam);


        // PainWheel
        List<AttackStats> PainWheelUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 4),
                new AttackStats(castTime: -0.5f),
                new AttackStats(speed: -0.05f),
                new AttackStats(shotsPerAttack: 1),
                new AttackStats(knockback: 0.4f, shakeRotation: 0.4f),
                new AttackStats(projectileSize: 0.5f, shakeStrength: 0.1f),
                new AttackStats(range: 3f),
                new AttackStats(pierce: 50, shakeTime: 0.05f)
            };

        AttackBuilder PainWheel = new AttackBuilder()
            .SetAttackName("Pain Wheel")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken"))
            .SetBaseStats(new AttackStats(damage: 5, spread: 0.4f, spray: 0f, castTime: 2.1f,
                                            range: 3f, shotsPerAttack: 1, speed: 0.15f, knockback: 0f,
                                            pierce: 50, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                            shakeTime: 0.1f, shakeStrength: 0.1f, shakeRotation: 0.1f,
                                            thrownDamage: 13f, throwSpeed: 0.6f, projectileSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shuriken_02"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/shuriken_1_2"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetUpgrades(PainWheelUpgrades);
        attackBuilderDictionary.Add("Pain Wheel", PainWheel);


        //PetrifyGrenade
        List<AttackStats> PetrifyGrenadeUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 8),
                new AttackStats(castTime: -0.4f),
                new AttackStats(pierce: 99),
                new AttackStats(shotsPerAttack: 1),
                new AttackStats(shotgunSpread: 40f),
                new AttackStats(projectileSize: 0.3f, shakeStrength: 0.2f),
                new AttackStats(range: 1.5f),
                new AttackStats(projectileSize: 0.3f, shakeTime: 0.05f)
            };

        AttackBuilder PetrifyGrenade = new AttackBuilder()
           .SetAttackName("Petrify Grenade")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/grenade_stun"))
           .SetBaseStats(new AttackStats(damage: 1, shotgunSpread: 65f, spray: 0, castTime: 1.9f,
                                           range: 1.5f, shotsPerAttack: 1, speed: 0.1f, knockback: 0f,
                                           pierce: 10, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           shakeTime: 0.1f, shakeStrength: 0.05f, shakeRotation: 0.05f,
                                           thrownDamage: 0, throwSpeed: 0f, projectileSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Shotgun,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/grenade_shock"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/grenade_shock"),
               muzzleFlashPrefab: PistolMuzzleFlash
           )
           .SetUpgrades(PetrifyGrenadeUpgrades);
        attackBuilderDictionary.Add("Petrify Grenade", PetrifyGrenade);


        //PetrifyNova
        List<AttackStats> PetrifyNovaUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 8),
                new AttackStats(castTime: -0.6f),
                new AttackStats(comboLength: 1),
                new AttackStats(shotsPerAttack: 1, damage: -1),
                new AttackStats(meleeSize: 0.5f, shakeRotation: 0.5f),
                new AttackStats(meleeSpacer: 2.5f),
                new AttackStats(meleeShotsScaleUp: 0.12f),
                new AttackStats(thrownDamage: 12, throwSpeed: 0.3f)

            };
        AttackBuilder PetrifyNova = new AttackBuilder()
           .SetAttackName("Petrify Nova")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Melee/Nova_Petrify"))
           .SetBaseStats(new AttackStats(damage: 2, spread: 0.5f, castTime: 2.3f, knockback: 0f,
                                           critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           comboLength: 1, comboWaitTime: 1f, meleeShotsScaleUp: 0f,
                                           meleeSpacer: 0f, meleeSpacerGap: 0f,
                                           shakeTime: 0.1f, shakeStrength: 0.5f, shakeRotation: 0.4f,
                                           thrownDamage: 11f, throwSpeed: 0.5f, meleeSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Melee,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/nova_03"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/Nova_03")
           )
           .SetUpgrades(PetrifyNovaUpgrades);
        attackBuilderDictionary.Add("Petrify Nova", PetrifyNova);


        // Revolver
        List<AttackStats> RevolverUpgrades = new List<AttackStats>
            {
                new AttackStats(damage: 9),
                new AttackStats(castTime: -0.4f),
                new AttackStats(thrownDamage: 12, throwSpeed: 0.25f),
                new AttackStats(shotsPerAttack: 2),
                new AttackStats(spread: -0.1f),
                new AttackStats(projectileSize: 0.5f, shakeStrength: 0.05f),
                new AttackStats(speed: 0.04f, spray: 0.5f, shakeRotation: 0.05f),
                new AttackStats(pierce: 1)
            };

        AttackBuilder Revolver = new AttackBuilder()
            .SetAttackName("Revolver")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_Magnum"))
            .SetBaseStats(new AttackStats(damage: 8, spread: 0.4f, spray: 1.8f, castTime: 2f,
                                            range: 6f, shotsPerAttack: 6, speed: 0.18f, knockback: 0.45f,
                                            pierce: 0, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                            shakeTime: 0.1f, shakeStrength: 0.1f, shakeRotation: 0.05f,
                                            thrownDamage: 6f, throwSpeed: 0.5f, projectileSize: 1))
            .SetRarity(0)
            .SetProperties(
                attackType: AttackTypes.Projectile,
                weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/pistol_magnum_1"),
                thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
                thrownSprite: Resources.Load<Sprite>("WeaponSprites/pistol_magnum"),
                bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
                muzzleFlashPrefab: PistolMuzzleFlash
            )
            .SetUpgrades(RevolverUpgrades);
        attackBuilderDictionary.Add("Revolver", Revolver);


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
                new AttackStats(knockback: 0.5f, shakeTime: 0.05f)
            };

        AttackBuilder Shotgun = new AttackBuilder()
           .SetAttackName("Shotgun")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/BasicBullet_wide"))
           .SetBaseStats(new AttackStats(damage: 9, shotgunSpread: 75f, spray: 0, castTime: 2.4f,
                                           range: 3.2f, shotsPerAttack: 4, speed: 0.15f, knockback: 0.55f,
                                           pierce: 99, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           shakeTime: 0.1f, shakeStrength: 0.35f, shakeRotation: 0.05f,
                                           thrownDamage: 8, throwSpeed: 0.5f, projectileSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Shotgun,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shotgun_01"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/shotgun_01"),
               bulletCasing: Resources.Load<GameObject>("WeaponVFX/BulletCasing"),
               muzzleFlashPrefab: BigMuzzleFlash
           )
           .SetUpgrades(ShotgunUpgrades);
        attackBuilderDictionary.Add("Shotgun", Shotgun);


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
                new AttackStats(knockback: 0.5f, shakeTime: 0.05f)
            };

        AttackBuilder Shuriken = new AttackBuilder()
           .SetAttackName("Shuriken")
           .SetProjectile(Resources.Load<GameObject>("Projectiles/Shuriken_small"))
           .SetBaseStats(new AttackStats(damage: 2, shotgunSpread: 100f, spray: 0, castTime: 2f,
                                           range: 2.8f, shotsPerAttack: 4, speed: 0.085f, knockback: 0f,
                                           pierce: 30, critChance: 0.05f, critDmg: 1.5f, multicastWaitTime: 0.25f,
                                           shakeTime: 0.05f, shakeStrength: 0.01f, shakeRotation: 0.01f,
                                           thrownDamage: 7, throwSpeed: 0.5f, projectileSize: 1))
           .SetRarity(0)
           .SetProperties(
               attackType: AttackTypes.Shotgun,
               weaponSprite: Resources.Load<Sprite>("WeaponSprites/Final/shuriken_01"),
               thrownWeapon: Resources.Load<GameObject>("Projectiles/WeaponThrown"),
               thrownSprite: Resources.Load<Sprite>("WeaponSprites/shuriken_2_2"),
               muzzleFlashPrefab: BigMuzzleFlash
           )
           .SetUpgrades(ShurikenUpgrades);
        attackBuilderDictionary.Add("Shuriken", Shuriken);


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

}
