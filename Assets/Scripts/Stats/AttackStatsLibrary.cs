using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AttackStatsLibrary
{
    private static Dictionary<string, AttackStats> AttackStatsLibraryMap =
        new Dictionary<string, AttackStats>();
    private static bool isInitialized = false;
    private static List<GameObject> attackStatGameObjects = new List<GameObject>();

    public static void CreateStatGameObjects()
    {
        foreach (AttackStats stat in GetStats())
        {
            GameObject statObject = new GameObject(stat.name);
            statObject.AddComponent<AttackStatComponent>().stat = stat;
            attackStatGameObjects.Add(statObject);
        }
    }

    public static List<GameObject> GetStatGameObjects()
    {
        return attackStatGameObjects;
    }


    static AttackStatsLibrary()
    {
        InitializeLibrary();
        CreateStatGameObjects();
    }

    private static void AddStat(AttackStats stat)
    {
        AttackStatsLibraryMap.Add(stat.name, stat);
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        //Value of stats - Individual Weapon -> Wpn Set -> Player 


//Global - All Weapons

    //common
        AddStat(
             new AttackStats(
                 aimRangeAdditive: -0.75f,
                 name: "MLG 1",
                 description: "Decrease Aim Assist Range",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common

             )
         ); ;

        AddStat(
             new AttackStats(
                 coneAngle: -30f,
                 name: "Gamer 1",
                 description: "Decrease Aim Assist Width",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common

             )
         ); ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.06f,
                name: "Damage 1",
                description: "Damage +6%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        ); ;

        AddStat(
             new AttackStats(
                 critChance: 0.05f,
                 name: "Crit Chance 1",
                 description: "Crit Chance +5%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 critDmg: 0.25f,
                 name: "Crit Dmg 1",
                 description: "Crit Dmg +25%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 castTimeMultiplier: -0.08f,
                 name: "Haste 1",
                 description: "Charge time -8%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 spreadMultiplier: -0.15f,
                 name: "Glattt 1",
                 description: "Rate of Fire +15%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 shotgunSpreadMultiplier: 0.2f,
                 name: "Saw'd Off 1",
                 description: "Shotgun Spread +20%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
                
             )
         ); ;

        AddStat(
             new AttackStats(
                 sprayMultiplier: -0.4f,
                 name: "Steady 1",
                 description: "Projectile Spray -40%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 sprayMultiplier: 0.4f,
                 name: "Overheat 1",
                 description: "Projectile Spray +40%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common

             )
         ); ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.25f,
                name: "Velocity 1",
                description: "Projectile Speed +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common

            )
        ); ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.15f,
                name: "Reach 1",
                description: "Projectile Range +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common

            )
        ); ;

      //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.15f,
                name: "Quick Hands 1",
                description: "Cast Speed +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.2f,
                name: "Caster 1",
                description: "Attack Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common

            )
        ); ;

        AddStat(
             new AttackStats(
                 meleeSpacerMultiplier: -0.25f,
                 name: "Kamakazi 1",
                 description: "Attack Range -25%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common

             )
         ); ;




        //rare
        AddStat(
             new AttackStats(
                 aimRangeAdditive: -3f,
                 critChance: 0.15f,
                 name: "MLG 2",
                 description: "Decrease Aim Assist Range, Crit Chance +15%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
             new AttackStats(
                 coneAngle: -100f,
                 critDmg: 0.5f,
                 name: "Gamer 2",
                 description: "Decrease Aim Assist Width, Crit Dmg +50%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
            new AttackStats(
                damageMultiplier: 0.12f,
                name: "Damage 2",
                description: "Damage +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare
            )
        ); ;

        AddStat(
             new AttackStats(
                 critChance: 0.08f,
                 name: "Crit Chance 2",
                 description: "Crit Chance +8%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare
             )
         ); ;

        AddStat(
             new AttackStats(
                 critDmg: 0.40f,
                 name: "Crit Dmg 2",
                 description: "Crit Dmg +40%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare
             )
         ); ;

        AddStat(
             new AttackStats(
                 castTimeMultiplier: -0.15f,
                 name: "Haste 2",
                 description: "Charge time -15%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare
             )
         ); ;

        AddStat(
             new AttackStats(
                 spreadMultiplier: -0.35f,
                 name: "Glattt 2",
                 description: "Rate of Fire +35%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare
             )
         ); ;

        AddStat(
             new AttackStats(
                 shotgunSpreadMultiplier: 0.5f,
                 name: "Saw'd Off 2",
                 description: "Shotgun Spread +50%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
             new AttackStats(
                 sprayMultiplier: -0.8f,
                 name: "Steady 2",
                 description: "Projectile Spray -80%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare
             )
         ); ;

        AddStat(
             new AttackStats(
                 sprayMultiplier: 0.8f,
                 name: "Overheat 2",
                 description: "Projectile Spray +80%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
            new AttackStats(
                speedMultiplier: 0.5f,
                name: "Velocity 2",
                description: "Projectile Speed +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.3f,
                name: "Reach 2",
                description: "Projectile Range +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.35f,
                name: "Quick Hands 2",
                description: "Cast Speed +35%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 0.4f,
                name: "Caster 2",
                description: "Attack Range +40%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
             new AttackStats(
                 meleeSpacerMultiplier: -0.6f,
                 name: "Kamakazi 2",
                 description: "Attack Range -60%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;



        AddStat(
             new AttackStats(
                 aimRangeAdditive: 0.6f,
                 name: "Hacker 1",
                 description: "Increases Aim Assist Range",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;
        AddStat(
             new AttackStats(
                 coneAngle: 25f,
                 name: "AFK 1",
                 description: "Increases Aim Assist Width",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
             new AttackStats(
                 multicastChance: 0.15f,
                 name: "Multicast 1",
                 description: "Multicast chance +15%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
             new AttackStats(
                 shotsPerAttack: 1,
                 name: "Extra Round 1",
                 description: "+1 Projectile",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
             new AttackStats(
                 shotsPerAttack: 1,
                 name: "Extended Clip 1",
                 description: "+6 Projectiles",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
             new AttackStats(
                 pierce: 1,
                 name: "Pierce 1",
                 description: "Pierce through +1 enemy",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Rare

             )
         ); ;

        AddStat(
            new AttackStats(
                pierce: 5,
                name: "Puncture 1",
                description: "Pierce through +5 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                projectileSize: 0.25f,
                name: "Big Ammo 1",
                description: "Projectile Size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                projectileSize: 0.12f,
                name: "Big Gadget 1",
                description: "Projectile Size +12%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                comboLength: 1,
                name: "One More 1",
                description: "+1 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 1,
                name: "Aftershock 1",
                description: "Release an aftershock after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.15f,
                name: "Big Weapon 1",
                description: "Attack Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 0.7f,
                name: "Wave Master 1",
                description: "Aftershock Size +7%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 0.2f,
                name: "Ki Master 1",
                description: "Aftershock Range +20%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Rare

            )
        ); ;



        //epic
        AddStat(
            new AttackStats(
                damageMultiplier: 0.12f,
                name: "Damage 3",
                description: "Damage +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic
            )
        ); ;

        AddStat(
             new AttackStats(
                 critChance: 0.2f,
                 name: "Crit Chance 3",
                 description: "Crit Chance +20%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic
             )
         ); ;

        AddStat(
             new AttackStats(
                 critDmg: 1f,
                 name: "Crit Dmg 3",
                 description: "Crit Dmg +100%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic
             )
         ); ;

        AddStat(
             new AttackStats(
                 castTimeMultiplier: -0.35f,
                 name: "Haste 3",
                 description: "Charge time -35%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic
             )
         ); ;

        AddStat(
             new AttackStats(
                 spreadMultiplier: -0.5f,
                 shotsPerAttack: 5,
                 name: "Glattt 3",
                 description: "Rate of Fire +50%, +5 Projectiles",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic
             )
         ); ;

        AddStat(
             new AttackStats(
                 shotgunSpreadMultiplier: 0.5f,
                 shotsPerAttack: 1,
                 name: "Saw'd Off 3",
                 description: "Shotgun Spread +50%, +1 Projectile",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
            new AttackStats(
                speedMultiplier: 1f,
                name: "Velocity 3",
                description: "Projectile Speed +100%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                rangeMultiplier: 0.5f,
                name: "Reach 3",
                description: "Projectile Range +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        //melee
        AddStat(
            new AttackStats(
                comboWaitTimeMultiplier: -0.5f,
                name: "Quick Hands 3",
                description: "Cast Speed +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSpacerMultiplier: 1f,
                name: "Caster 3",
                description: "Attack Range +100%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;


        AddStat(
             new AttackStats(
                 shootOppositeSide: true,
                 name: "Double Trouble",
                 description: "Also Attack Behind (Doesn't stack)",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
             new AttackStats(
                 aimRangeAdditive: 1.5f,
                 name: "Hacker 2",
                 description: "Increases Aim Assist Range",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;
        AddStat(
             new AttackStats(
                 coneAngle: 60f,
                 name: "AFK 2",
                 description: "Increases Aim Assist Width",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
             new AttackStats(
                 multicastChance: 0.33f,
                 name: "Multicast 2",
                 description: "Multicast chance +33%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
             new AttackStats(
                 shotsPerAttack: 2,
                 name: "Extra Round 2",
                 description: "+3 Projectile",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
             new AttackStats(
                 shotsPerAttack: 1,
                 name: "Extended Clip 2",
                 description: "+15 Projectiles",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
             new AttackStats(
                 pierce: 1,
                 name: "Pierce 2",
                 description: "Pierce through +3 enemies",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Epic

             )
         ); ;

        AddStat(
            new AttackStats(
                pierce: 20,
                name: "Puncture 2",
                description: "Pierce through +20 enemies",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                projectileSize: 0.5f,
                name: "Big Ammo 2",
                description: "Projectile Size +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                projectileSize: 0.25f,
                name: "Big Gadget 2",
                description: "Projectile Size +25%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                comboLength: 2,
                name: "One More 2",
                description: "+2 Cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                shotsPerAttackMelee: 2,
                name: "Aftershock 2",
                description: "Release 2 aftershocks after each cast",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSizeMultiplier: 0.3f,
                name: "Big Weapon 2",
                description: "Attack Size +30%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeShotsScaleUpMultiplier: 0.15f,
                name: "Wave Master 2",
                description: "Aftershock Size +15%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;

        AddStat(
            new AttackStats(
                meleeSpacerGapMultiplier: 0.5f,
                name: "Ki Master 2",
                description: "Aftershock Range +50%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Epic

            )
        ); ;



    //legendary
        AddStat(
             new AttackStats(
                 shootOppositeSide: true,
                 damageMultiplier: 0.2f,
                 name: "Double Trouble 2",
                 description: "Also Attack Behind (Doesn't Stack), Damage +20%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;



        //Weapon Set - Automatic
        AddStat(
             new AttackStats(
                 pierce: 5,
                 shotsPerAttack: 15,
                 spreadMultiplier: -0.2f,
                 sprayMultiplier: -1f,
                 speedMultiplier: 0.3f,
                 rangeMultiplier: 0.3f,
                 aimRangeAdditive: 1f,
                 coneAngle: -15f,
                 name: "Auto Pro",
                 description: "All Automatic Weapons Enhanced",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;

        //Weapon Set - Semi-Auto
        AddStat(
             new AttackStats(
                 shotsPerAttack: 3,
                 castTimeMultiplier: -0.2f,
                 spreadMultiplier: -0.25f,
                 projectileSize: 0.4f,
                 name: "Semi-Auto Pro",
                 description: "All Semi-Automatic Weapons Enhanced",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;


        //Weapon Set - Shotgun
        AddStat(
             new AttackStats(
                 knockbackMultiplier: 0.5f,
                 shotsPerAttack: 2,
                 shotgunSpreadMultiplier: 0.3f,
                 speedMultiplier: 0.2f,
                 rangeMultiplier: 0.3f,
                 pierce: 5,
                 name: "Shotgun Pro",
                 description: "All Shotgun Weapons Enhanced",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;

        //Weapon Set - Explosive
        AddStat(
             new AttackStats(
                 shotsPerAttack: 1,
                 pierce: 5,
                 projectileSizeMultiplier: 0.4f,
                 castTimeMultiplier: -0.1f,
                 speedMultiplier: 0.5f,
                 rangeMultiplier: 0.2f,
                 name: "Explosive Pro",
                 description: "All Explosive Weapons Enhanced",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;


        //Weapon Set - Nova
        AddStat(
             new AttackStats(
                 comboLength: 1,
                 comboWaitTimeMultiplier: -0.3f,
                 castTimeMultiplier: -0.2f,
                 knockbackMultiplier: 0.4f,
                 meleeSizeMultiplier: 0.4f,
                 name: "AFK Pro",
                 description: "All Nova Weapons Enhanced",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;


        //Weapon Set - Melee
        AddStat(
             new AttackStats(
                 shotsPerAttackMelee: 1,
                 comboLength: 1,
                 knockbackMultiplier: 0.3f,
                 meleeSizeMultiplier: 0.25f,
                 name: "Melee Pro",
                 description: "All Melee Weapons Enhanced",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Legendary

             )
         ); ;



        isInitialized = true;
    }

    public static AttackStats GetStat(string name)
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        if (AttackStatsLibraryMap.ContainsKey(name))
        {
            return AttackStatsLibraryMap[name];
        }
        else
        {
            Debug.LogError("AttackStatsLibrary does not contain a stat named " + name);
            return null;
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