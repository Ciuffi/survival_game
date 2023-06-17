using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerUpgradesLibrary
{
    private static Dictionary<string, GameObject> UpgradesLibraryMap =
        new Dictionary<string, GameObject>();
    private static bool isInitialized = false;
    static GameObject parentObject = new GameObject("UpgradesParent");
    static GameObject defaultUpgradePrefab = Resources.Load<GameObject>(
        "PlayerCharacters/DefaultUpgrade");

    private static void AddUpgrade(GameObject upgradeObject)
    {
        UpgradesLibraryMap.Add(upgradeObject.name, upgradeObject);
    }

    static PlayerUpgradesLibrary()
    {
        InitializeLibrary();
        Object.DontDestroyOnLoad(parentObject);
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            return;
        }

        //HP
        GameObject MaxHp1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats MaxHp1Stats = new PlayerCharacterStats(
             maxHealth: 5,
             health: 5,
                level: 1,
                price: 300,
                name: "Health 1",
                description: "+5 HP",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Rare
             );
        MaxHp1.name = MaxHp1Stats.name;
        MaxHp1.AddComponent<StatComponent>().stat = MaxHp1Stats;
        MaxHp1Stats.setContainer(MaxHp1);

        AddUpgrade(MaxHp1);

        GameObject MaxHp2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats MaxHp2Stats = new PlayerCharacterStats(
             maxHealth: 5,
             health: 5,
                level: 2,
                price: 500,
                name: "Health 2",
                description: "+10 HP",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Rare
             );
        MaxHp2.name = MaxHp2Stats.name;
        MaxHp2.AddComponent<StatComponent>().stat = MaxHp2Stats;
        MaxHp2Stats.setContainer(MaxHp2);

        AddUpgrade(MaxHp2);

        GameObject MaxHp3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats MaxHp3Stats = new PlayerCharacterStats(
             maxHealth: 5,
             health: 5,
                level: 3,
                price: 1000,
                name: "Health 3",
                description: "+15 HP",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Epic
             );
        MaxHp3.name = MaxHp3Stats.name;
        MaxHp3.AddComponent<StatComponent>().stat = MaxHp3Stats;
        MaxHp3Stats.setContainer(MaxHp3);

        AddUpgrade(MaxHp3);

        GameObject MaxHp4 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats MaxHp4Stats = new PlayerCharacterStats(
             maxHealth: 5,
             health: 5,
                level: 4,
                price: 1000,
                name: "Health 4",
                description: "+20 HP",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Epic
             );
        MaxHp4.name = MaxHp4Stats.name;
        MaxHp4.AddComponent<StatComponent>().stat = MaxHp4Stats;
        MaxHp4Stats.setContainer(MaxHp4);

        AddUpgrade(MaxHp4);

        GameObject MaxHp5 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats MaxHp5Stats = new PlayerCharacterStats(
             maxHealth: 10,
             health: 10,
                level: 5,
                price: 2000,
                name: "Health 5",
                description: "+30 HP",
                icon: Resources.Load<Sprite>("UI_Icons/ThickHide"),
                rarity: Rarity.Legendary
             );
        MaxHp5.name = MaxHp5Stats.name;
        MaxHp5.AddComponent<StatComponent>().stat = MaxHp5Stats;
        MaxHp5Stats.setContainer(MaxHp5);

        AddUpgrade(MaxHp5);


        //defense
        GameObject Defense1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Defense1Stats = new PlayerCharacterStats(
             defense: 0.25f,
                level: 1,
                price: 800,
                name: "Armor 1",
                description: "Reduce incoming Damage by 0.25",
                icon: Resources.Load<Sprite>("UI_Icons/Protection"),
                rarity: Rarity.Rare
             );
        Defense1.name = Defense1Stats.name;
        Defense1.AddComponent<StatComponent>().stat = Defense1Stats;
        Defense1Stats.setContainer(Defense1);
        AddUpgrade(Defense1);

        GameObject Defense2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Defense2Stats = new PlayerCharacterStats(
             defense: 0.25f,
                level: 2,
                price: 1200,
                name: "Armor 2",
                description: "Reduce incoming Damage by 0.5",
                icon: Resources.Load<Sprite>("UI_Icons/Protection"),
                rarity: Rarity.Epic
             );
        Defense2.name = Defense2Stats.name;
        Defense2.AddComponent<StatComponent>().stat = Defense2Stats;
        Defense2Stats.setContainer(Defense2);
        AddUpgrade(Defense2);

        GameObject Defense3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Defense3Stats = new PlayerCharacterStats(
             defense: 0.4f,
                level: 3,
                price: 1500,
                name: "Armor 3",
                description: "Reduce incoming Damage by 1",
                icon: Resources.Load<Sprite>("UI_Icons/Protection"),
                rarity: Rarity.Legendary
             );
        Defense3.name = Defense3Stats.name;
        Defense3.AddComponent<StatComponent>().stat = Defense3Stats;
        Defense3Stats.setContainer(Defense3);
        AddUpgrade(Defense3);


        //moveSpeed
        GameObject Speed1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Speed1Stats = new PlayerCharacterStats(
            speed: 0.2f,
               level: 1,
               price: 500,
               name: "Speed 1",
               description: "+0.2 base Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Boost"),
               rarity: Rarity.Rare
             );
        Speed1.name = Speed1Stats.name;
        Speed1.AddComponent<StatComponent>().stat = Speed1Stats;
        Speed1Stats.setContainer(Speed1);
        AddUpgrade(Speed1);

        GameObject Speed2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Speed2Stats = new PlayerCharacterStats(
            speed: 0.2f,
               level: 2,
               price: 1000,
               name: "Speed 2",
               description: "+0.4 base Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Boost"),
               rarity: Rarity.Epic
             );
        Speed2.name = Speed2Stats.name;
        Speed2.AddComponent<StatComponent>().stat = Speed2Stats;
        Speed2Stats.setContainer(Speed2);
        AddUpgrade(Speed2);

        GameObject Speed3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Speed3Stats = new PlayerCharacterStats(
           speed: 0.4f,
               level: 3,
               price: 2000,
               name: "Speed 3",
               description: "+0.8 base Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Boost"),
               rarity: Rarity.Legendary
             );
        Speed3.name = Speed3Stats.name;
        Speed3.AddComponent<StatComponent>().stat = Speed3Stats;
        Speed3Stats.setContainer(Speed3);

        AddUpgrade(Speed3);

        //Damage
        GameObject Damage1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Damage1Stats = new PlayerCharacterStats(
              damageMultiplier: 0.05f,
               level: 1,
               price: 500,
               name: "Damage 1",
               description: "+5% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
               rarity: Rarity.Rare
             );
        Damage1.name = Damage1Stats.name;
        Damage1.AddComponent<StatComponent>().stat = Damage1Stats;
        Damage1Stats.setContainer(Damage1);

        AddUpgrade(Damage1);

        GameObject Damage2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Damage2Stats = new PlayerCharacterStats(
          damageMultiplier: 0.05f,
               level: 2,
               price: 500,
               name: "Damage 2",
               description: "+10% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
               rarity: Rarity.Rare
             );
        Damage2.name = Damage2Stats.name;
        Damage2.AddComponent<StatComponent>().stat = Damage2Stats;
        Damage2Stats.setContainer(Damage2);

        AddUpgrade(Damage2);

        GameObject Damage3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Damage3Stats = new PlayerCharacterStats(
            damageMultiplier: 0.10f,
               level: 3,
               price: 1200,
               name: "Damage 3",
               description: "+20% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
               rarity: Rarity.Epic
             );
        Damage3.name = Damage3Stats.name;
        Damage3.AddComponent<StatComponent>().stat = Damage3Stats;
        Damage3Stats.setContainer(Damage3);

        AddUpgrade(Damage3);

        GameObject Damage4 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Damage4Stats = new PlayerCharacterStats(
            damageMultiplier: 0.10f,
               level: 4,
               price: 1500,
               name: "Damage 4",
               description: "+30% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
               rarity: Rarity.Epic
             );
        Damage4.name = Damage4Stats.name;
        Damage4.AddComponent<StatComponent>().stat = Damage4Stats;
        Damage4Stats.setContainer(Damage4);
        AddUpgrade(Damage4);

        GameObject Damage5 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Damage5Stats = new PlayerCharacterStats(
            damageMultiplier: 0.20f,
               level: 5,
               price: 2000,
               name: "Damage 5",
               description: "+50% Damage",
               icon: Resources.Load<Sprite>("UI_Icons/Mastery"),
               rarity: Rarity.Legendary
             );
        Damage5.name = Damage5Stats.name;
        Damage5.AddComponent<StatComponent>().stat = Damage5Stats;
        Damage5Stats.setContainer(Damage5);
        AddUpgrade(Damage5);


        //Crit
        GameObject Crit1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Crit1Stats = new PlayerCharacterStats(
             critChance: 0.04f,
               level: 1,
               price: 800,
               name: "Critical 1",
               description: "+4% Critical Hit Chance",
               icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
               rarity: Rarity.Rare
             );
        Crit1.name = Crit1Stats.name;
        Crit1.AddComponent<StatComponent>().stat = Crit1Stats;
        Crit1Stats.setContainer(Crit1);

        AddUpgrade(Crit1);

        GameObject Crit2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Crit2Stats = new PlayerCharacterStats(
             critChance: 0.04f,
               level: 2,
               price: 1200,
               name: "Critical 2",
               description: "+8% Critical Hit Chance",
               icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
               rarity: Rarity.Epic
             );
        Crit2.name = Crit2Stats.name;
        Crit2.AddComponent<StatComponent>().stat = Crit2Stats;
        Crit2Stats.setContainer(Crit2);

        AddUpgrade(Crit2);

        GameObject Crit3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Crit3Stats = new PlayerCharacterStats(
              critChance: 0.07f,
               level: 3,
               price: 2000,
               name: "Critical 3",
               description: "+15% Critical Hit Chance",
               icon: Resources.Load<Sprite>("UI_Icons/CritChance"),
               rarity: Rarity.Legendary
             );
        Crit3.name = Crit3Stats.name;
        Crit3.AddComponent<StatComponent>().stat = Crit3Stats;
        Crit3Stats.setContainer(Crit3);
        AddUpgrade(Crit3);


        //CritDmg
        GameObject CritDmg1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats CritDmg1Stats = new PlayerCharacterStats(
             critDmg: 0.20f,
               level: 1,
               price: 500,
               name: "Overkill 1",
               description: "+20% Critical Hit Damage",
               icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
               rarity: Rarity.Rare
             );
        CritDmg1.name = CritDmg1Stats.name;
        CritDmg1.AddComponent<StatComponent>().stat = CritDmg1Stats;
        CritDmg1Stats.setContainer(CritDmg1);

        AddUpgrade(CritDmg1);

        GameObject CritDmg2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats CritDmg2Stats = new PlayerCharacterStats(
             critDmg: 0.20f,
               level: 2,
               price: 800,
               name: "Overkill 2",
               description: "+40% Critical Hit Damage",
               icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
               rarity: Rarity.Rare
             );
        CritDmg2.name = CritDmg2Stats.name;
        CritDmg2.AddComponent<StatComponent>().stat = CritDmg2Stats;
        CritDmg2Stats.setContainer(CritDmg2);
        AddUpgrade(CritDmg2);


        GameObject CritDmg3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats CritDmg3Stats = new PlayerCharacterStats(
             critDmg: 0.30f,
               level: 3,
               price: 1200,
               name: "Overkill 3",
               description: "+70% Critical Hit Damage",
               icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
               rarity: Rarity.Epic
             );
        CritDmg3.name = CritDmg3Stats.name;
        CritDmg3.AddComponent<StatComponent>().stat = CritDmg3Stats;
        CritDmg3Stats.setContainer(CritDmg3);
        AddUpgrade(CritDmg3);


        GameObject CritDmg4 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats CritDmg4Stats = new PlayerCharacterStats(
             critDmg: 0.30f,
               level: 4,
               price: 1500,
               name: "Overkill 4",
               description: "+100% Critical Hit Damage",
               icon: Resources.Load<Sprite>("UI_Icons/CritDamage"),
               rarity: Rarity.Legendary
             );
        CritDmg4.name = CritDmg4Stats.name;
        CritDmg4.AddComponent<StatComponent>().stat = CritDmg4Stats;
        CritDmg4Stats.setContainer(CritDmg4);
        AddUpgrade(CritDmg4);


        //pickup
        GameObject Pickup1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Pickup1Stats = new PlayerCharacterStats(
           pickupRange: 0.5f,
               level: 1,
               price: 800,
               name: "Magnet 1",
               description: "+0.5 base Pickup range",
               icon: Resources.Load<Sprite>("UI_Icons/Magnet"),
               rarity: Rarity.Rare
             );
        Pickup1.name = Pickup1Stats.name;
        Pickup1.AddComponent<StatComponent>().stat = Pickup1Stats;
        Pickup1Stats.setContainer(Pickup1);

        AddUpgrade(Pickup1);

        GameObject Pickup2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Pickup2Stats = new PlayerCharacterStats(
           pickupRange: 0.5f,
               level: 2,
               price: 1200,
               name: "Magnet 2",
               description: "+1 base Pickup range",
               icon: Resources.Load<Sprite>("UI_Icons/Magnet"),
               rarity: Rarity.Epic
             );
        Pickup2.name = Pickup2Stats.name;
        Pickup2.AddComponent<StatComponent>().stat = Pickup2Stats;
        Pickup2Stats.setContainer(Pickup2);

        AddUpgrade(Pickup2);

        GameObject Pickup3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Pickup3Stats = new PlayerCharacterStats(
           pickupRange: 1f,
               level: 3,
               price: 1500,
               name: "Magnet 3",
               description: "+2 base Pickup range",
               icon: Resources.Load<Sprite>("UI_Icons/Magnet"),
               rarity: Rarity.Legendary
             );
        Pickup3.name = Pickup3Stats.name;
        Pickup3.AddComponent<StatComponent>().stat = Pickup3Stats;
        Pickup3Stats.setContainer(Pickup3);

        AddUpgrade(Pickup3);


        //AimRange;
        GameObject AimRange1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats AimRange1Stats = new PlayerCharacterStats(
           aimRangeAdditive: 0.75f,
               level: 1,
               price: 500,
               name: "Vision 1",
               description: "Increase Aim Assist range",
               icon: Resources.Load<Sprite>("UI_Icons/Vision"),
               rarity: Rarity.Rare
             );
        AimRange1.name = AimRange1Stats.name;
        AimRange1.AddComponent<StatComponent>().stat = AimRange1Stats;
        AimRange1Stats.setContainer(AimRange1);

        AddUpgrade(AimRange1);

        //AimRange;
        GameObject AimRange2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats AimRange2Stats = new PlayerCharacterStats(
           aimRangeAdditive: 1.25f,
               level: 2,
               price: 1000,
               name: "Vision 2",
               description: "Massively increase Aim Assist range",
               icon: Resources.Load<Sprite>("UI_Icons/Vision"),
               rarity: Rarity.Epic
             );
        AimRange2.name = AimRange2Stats.name;
        AimRange2.AddComponent<StatComponent>().stat = AimRange2Stats;
        AimRange2Stats.setContainer(AimRange2);

        AddUpgrade(AimRange2);

        //ConeAngle
        GameObject ConeAngle1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats ConeAngle1Stats = new PlayerCharacterStats(
           coneAngle: 30f,
               level: 1,
               price: 500,
               name: "Awareness 1",
               description: "Increase Aim Assist width",
               icon: Resources.Load<Sprite>("UI_Icons/AFK"),
               rarity: Rarity.Rare
             );
        ConeAngle1.name = ConeAngle1Stats.name;
        ConeAngle1.AddComponent<StatComponent>().stat = ConeAngle1Stats;
        ConeAngle1Stats.setContainer(ConeAngle1);

        AddUpgrade(ConeAngle1);

        //ConeAngle
        GameObject ConeAngle2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats ConeAngle2Stats = new PlayerCharacterStats(
           coneAngle: 50f,
               level: 2,
               price: 1000,
               name: "Awareness 2",
               description: "Massively increase Aim Assist width",
               icon: Resources.Load<Sprite>("UI_Icons/AFK"),
               rarity: Rarity.Epic
             );
        ConeAngle2.name = ConeAngle2Stats.name;
        ConeAngle2.AddComponent<StatComponent>().stat = ConeAngle2Stats;
        ConeAngle2Stats.setContainer(ConeAngle2);

        AddUpgrade(ConeAngle2);


        // ... Add other upgrades
        isInitialized = true;
    }

    public static GameObject getUpgrade(string upgradeName)
    {
        return UpgradesLibraryMap[upgradeName];
    }

    public static GameObject[] getUpgrades()
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        return UpgradesLibraryMap.Values.ToArray();
    }

    public static void ResetLibrary()
    {
        isInitialized = false;

        // Delete old GameObjects
        foreach (Transform child in parentObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Clear the dictionary
        UpgradesLibraryMap.Clear();

        // Reinitialize
        InitializeLibrary();
    }
}