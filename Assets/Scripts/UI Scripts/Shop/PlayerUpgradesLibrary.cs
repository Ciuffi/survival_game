using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerUpgradesLibrary
{
    private static Dictionary<string, PlayerCharacterStats> UpgradesLibraryMap =
        new Dictionary<string, PlayerCharacterStats>();
    private static bool isInitialized = false;

    private static void AddUpgrade(PlayerCharacterStats upgrade)
    {
        UpgradesLibraryMap.Add(upgrade.name, upgrade);
    }

    static PlayerUpgradesLibrary()
    {
        InitializeLibrary();
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        //COMMON STATS
        AddUpgrade(
            new PlayerCharacterStats(
                maxHealth: 5,
                level: 1,
                price: 100,
                name: "Max HP 1",
                description: "+5 HP",
                icon: Resources.Load<Sprite>("UI_Icons/FirstAid"),
                rarity: Rarity.Rare
            )
        );

        AddUpgrade(
           new PlayerCharacterStats(
               maxHealth: 10,
               level: 2,
               price: 200,
               name: "Max HP 2",
               description: "+10 HP",
               icon: Resources.Load<Sprite>("UI_Icons/FirstAid"),
               rarity: Rarity.Epic
           )
       );

        AddUpgrade(
           new PlayerCharacterStats(
               maxHealth: 15,
               level: 3,
               price: 300,
               name: "Max HP 3",
               description: "+15 HP",
               icon: Resources.Load<Sprite>("UI_Icons/FirstAid"),
               rarity: Rarity.Legendary
           )
       );

        //Damage
        AddUpgrade(
           new PlayerCharacterStats(
               damageMultiplier: 0.1f,
               level: 1,
               price: 100,
               name: "Damage 1",
               description: "+10% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Damage"),
               rarity: Rarity.Rare
           )
       );

        AddUpgrade(
           new PlayerCharacterStats(
               damageMultiplier: 0.15f,
               level: 2,
               price: 200,
               name: "Damage 2",
               description: "+15% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Damage"),
               rarity: Rarity.Epic
           )
       );


        // ... Add other upgrades
        isInitialized = true;
    }

    public static PlayerCharacterStats getUpgrade(string upgradeName)
    {
        return UpgradesLibraryMap[upgradeName];
    }

    public static PlayerCharacterStats[] getUpgrades()
    {
        return UpgradesLibraryMap.Values.ToArray();
    }
}