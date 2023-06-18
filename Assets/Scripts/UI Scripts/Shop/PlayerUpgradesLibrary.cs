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
                name: "Healthy 1",
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
                name: "Healthy 2",
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
                name: "Healthy 3",
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
                name: "Healthy 4",
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
                name: "Healthy 5",
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
                name: "Tough 1",
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
                name: "Tough 2",
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
                name: "Tough 3",
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
            speed: 0.15f,
               level: 1,
               price: 500,
               name: "Agile 1",
               description: "+0.15 base Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Boost"),
               rarity: Rarity.Rare
             );
        Speed1.name = Speed1Stats.name;
        Speed1.AddComponent<StatComponent>().stat = Speed1Stats;
        Speed1Stats.setContainer(Speed1);
        AddUpgrade(Speed1);

        GameObject Speed2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Speed2Stats = new PlayerCharacterStats(
            speed: 0.15f,
               level: 2,
               price: 1000,
               name: "Agile 2",
               description: "+0.3 base Speed",
               icon: Resources.Load<Sprite>("UI_Icons/Boost"),
               rarity: Rarity.Epic
             );
        Speed2.name = Speed2Stats.name;
        Speed2.AddComponent<StatComponent>().stat = Speed2Stats;
        Speed2Stats.setContainer(Speed2);
        AddUpgrade(Speed2);

        GameObject Speed3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Speed3Stats = new PlayerCharacterStats(
           speed: 0.2f,
               level: 3,
               price: 2000,
               name: "Agile 3",
               description: "+0.5 base Speed",
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
               name: "Mastery 1",
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
               name: "Mastery 2",
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
               name: "Mastery 3",
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
               name: "Mastery 4",
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
               name: "Mastery 5",
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
               name: "Accuracy 1",
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
               name: "Accuracy 2",
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
               name: "Accuracy 3",
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


        //XPGain
        GameObject XPGain1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats XPGain1Stats = new PlayerCharacterStats(
           xpGainMultiplier: 0.03f,
               level: 1,
               price: 800,
               name: "Talented 1",
               description: "+3% Experience",
               icon: Resources.Load<Sprite>("UI_Icons/Talented"),
               rarity: Rarity.Rare
             );
        XPGain1.name = XPGain1Stats.name;
        XPGain1.AddComponent<StatComponent>().stat = XPGain1Stats;
        XPGain1Stats.setContainer(XPGain1);

        AddUpgrade(XPGain1);

        GameObject XPGain2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats XPGain2Stats = new PlayerCharacterStats(
           xpGainMultiplier: 0.03f,
               level: 2,
               price: 1200,
               name: "Talented 2",
               description: "+6% Experience",
               icon: Resources.Load<Sprite>("UI_Icons/Talented"),
               rarity: Rarity.Epic
             );
        XPGain2.name = XPGain2Stats.name;
        XPGain2.AddComponent<StatComponent>().stat = XPGain2Stats;
        XPGain2Stats.setContainer(XPGain2);

        AddUpgrade(XPGain2);

        GameObject XPGain3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats XPGain3Stats = new PlayerCharacterStats(
           xpGainMultiplier: 0.04f,
               level: 3,
               price: 2000,
               name: "Talented 3",
               description: "+10% Experience",
               icon: Resources.Load<Sprite>("UI_Icons/Talented"),
               rarity: Rarity.Legendary
             );
        XPGain3.name = XPGain3Stats.name;
        XPGain3.AddComponent<StatComponent>().stat = XPGain3Stats;
        XPGain3Stats.setContainer(XPGain3);

        AddUpgrade(XPGain3);


        //GoldGain
        GameObject GoldGain1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats GoldGain1Stats = new PlayerCharacterStats(
           goldGainMultiplier: 0.02f,
               level: 1,
               price: 800,
               name: "Greedy 1",
               description: "+2% Gold",
               icon: Resources.Load<Sprite>("UI_Icons/Greedy"),
               rarity: Rarity.Rare
             );
        GoldGain1.name = GoldGain1Stats.name;
        GoldGain1.AddComponent<StatComponent>().stat = GoldGain1Stats;
        GoldGain1Stats.setContainer(GoldGain1);

        AddUpgrade(GoldGain1);

        GameObject GoldGain2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats GoldGain2Stats = new PlayerCharacterStats(
           goldGainMultiplier: 0.02f,
               level: 2,
               price: 1500,
               name: "Greedy 2",
               description: "+4% Gold",
               icon: Resources.Load<Sprite>("UI_Icons/Greedy"),
               rarity: Rarity.Epic
             );
        GoldGain2.name = GoldGain2Stats.name;
        GoldGain2.AddComponent<StatComponent>().stat = GoldGain2Stats;
        GoldGain2Stats.setContainer(GoldGain2);

        AddUpgrade(GoldGain2);

        GameObject GoldGain3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats GoldGain3Stats = new PlayerCharacterStats(
           goldGainMultiplier: 0.04f,
               level: 3,
               price: 2500,
               name: "Greedy 3",
               description: "+8% Gold",
               icon: Resources.Load<Sprite>("UI_Icons/Greedy"),
               rarity: Rarity.Legendary
             );
        GoldGain3.name = GoldGain3Stats.name;
        GoldGain3.AddComponent<StatComponent>().stat = GoldGain3Stats;
        GoldGain3Stats.setContainer(GoldGain3);

        AddUpgrade(GoldGain3);


        //Reroll
        GameObject Reroll1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Reroll1Stats = new PlayerCharacterStats(
           rerollTimes: 1,
               level: 1,
               price: 800,
               name: "Indecisive 1",
               description: "+1 Reroll",
               icon: Resources.Load<Sprite>("UI_Icons/Reroll"),
               rarity: Rarity.Rare
             );
        Reroll1.name = Reroll1Stats.name;
        Reroll1.AddComponent<StatComponent>().stat = Reroll1Stats;
        Reroll1Stats.setContainer(Reroll1);

        AddUpgrade(Reroll1);

        GameObject Reroll2 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Reroll2Stats = new PlayerCharacterStats(
           rerollTimes: 1,
               level: 2,
               price: 1000,
               name: "Indecisive 2",
               description: "+2 Rerolls",
               icon: Resources.Load<Sprite>("UI_Icons/Reroll"),
               rarity: Rarity.Rare
             );
        Reroll2.name = Reroll2Stats.name;
        Reroll2.AddComponent<StatComponent>().stat = Reroll2Stats;
        Reroll2Stats.setContainer(Reroll2);

        AddUpgrade(Reroll2);

        GameObject Reroll3 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Reroll3Stats = new PlayerCharacterStats(
           rerollTimes: 1,
               level: 3,
               price: 1200,
               name: "Indecisive 3",
               description: "+3 Rerolls",
               icon: Resources.Load<Sprite>("UI_Icons/Reroll"),
               rarity: Rarity.Epic
             );
        Reroll3.name = Reroll3Stats.name;
        Reroll3.AddComponent<StatComponent>().stat = Reroll3Stats;
        Reroll3Stats.setContainer(Reroll3);

        AddUpgrade(Reroll3);

        GameObject Reroll4 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Reroll4Stats = new PlayerCharacterStats(
           rerollTimes: 2,
               level: 4,
               price: 2000,
               name: "Indecisive 4",
               description: "+5 Rerolls",
               icon: Resources.Load<Sprite>("UI_Icons/Reroll"),
               rarity: Rarity.Legendary
             );
        Reroll4.name = Reroll4Stats.name;
        Reroll4.AddComponent<StatComponent>().stat = Reroll4Stats;
        Reroll4Stats.setContainer(Reroll4);

        AddUpgrade(Reroll4);


        //pickup
        GameObject Pickup1 = Object.Instantiate(defaultUpgradePrefab, parentObject.transform);
        PlayerCharacterStats Pickup1Stats = new PlayerCharacterStats(
           pickupRange: 0.5f,
               level: 1,
               price: 800,
               name: "Magnetic 1",
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
               name: "Magnetic 2",
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
               name: "Magnetic 3",
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