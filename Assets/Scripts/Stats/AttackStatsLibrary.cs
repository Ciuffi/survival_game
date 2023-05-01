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
        AddStat(
            new AttackStats(
                damageMultiplier: 0.1f,
                name: "Damage",
                description: "Increases damage by 10%",
                icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                rarity: Rarity.Common
            )
        ); ;

        AddStat(
             new AttackStats(
                 critChance: 0.05f,
                 name: "Crit Chance",
                 description: "Increases critical hit chance by 5%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
             )
         ); ;

        AddStat(
             new AttackStats(
                 knockbackMultiplier: 0.1f,
                 name: "Knockback",
                 description: "Increases knockback by 10%",
                 icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
                 rarity: Rarity.Common
                
             )
         ); ;


        //Weapon Set - Automatic


        //Weapon Set - Semi-Auto

        //Weapon Set - Shotgun
        //Weapon Set - Explosive
        //Weapon Set - Nova
        //Weapon Set - Melee

        


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