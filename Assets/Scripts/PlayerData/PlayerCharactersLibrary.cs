using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerCharactersLibrary
{
    private static Dictionary<string, GameObject> PlayerCharactersLibraryMap =
        new Dictionary<string, GameObject>();
    private static bool isInitialized = false;

    static GameObject defaultPlayerPrefab = Resources.Load<GameObject>(
        "PlayerCharacters/Default"
    );

    static GameObject parentObject = new GameObject("CharacterParent");

    private static void AddCharacter(GameObject playerCharacter)
    {
        PlayerCharactersLibraryMap.Add(playerCharacter.name, playerCharacter);
    }

    static PlayerCharactersLibrary()
    {
        InitializeLibrary();
        Object.DontDestroyOnLoad(parentObject);
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            Debug.Log("InitializeLibrary has already been called.");
            return;
        }
        Debug.Log("Initializing PlayerCharacterLibrary...");

        GameObject defaultCharacter = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats defaultStats = new PlayerCharacterStats(
            name: "Default",
            description: "Default Character",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/2"),
            rarity: Rarity.Common,
            price: 0,
            isLocked: false,
            maxHealth: 40,
            health: 40,
            defense: 0,
            speed: 0.025f,
            pickupRange: 2f,
            aimRangeAdditive: 0,

            shotsPerAttack: 0,
            comboLength: 0,
            shotsPerAttackMelee: 0,
            multicastChance: 0,
            shotgunSpread: 0,

           critChance: 0f,
           critDmg: 0f,
           damageMultiplier: 0,
           spreadMultiplier: 0,
           castTimeMultiplier: 0,
           comboWaitTimeMultiplier: 0,
           projectileSpeedMultiplier: 0,
           rangeMultiplier: 0,
           knockbackMultiplier: 0,
           thrownDamageMultiplier: 0,
           thrownSpeedMultiplier: 0,
           projectileSizeMultiplier: 0,
           meleeSizeMultiplier: 0,
           effectDuration: 0,
           effectMultiplier: 0,
           activeDuration: 0,
           activeMultiplier: 0,

            shootOpposideSide: false
        );

        defaultCharacter.name = defaultStats.name;
        defaultCharacter.AddComponent<StatComponent>().stat = defaultStats;
        AddCharacter(defaultCharacter);


        GameObject Scout = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats ScoutStats = new PlayerCharacterStats(
           name: "Scout",
           description: "Ding Dong",
           icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/5"),
           rarity: Rarity.Common,
           price: 450,
           isLocked: true,
           maxHealth: 30,
           health: 30,
           defense: 0,
           speed: 0.038f,
           pickupRange: 3f,
           aimRangeAdditive: 0

       );
        Scout.name = ScoutStats.name;
        Scout.AddComponent<StatComponent>().stat = ScoutStats;
        AddCharacter(Scout);

        GameObject Tank = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats TankStats = new PlayerCharacterStats(
            name: "Tank",
            description: "It's all Ogre now",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/1"),
            rarity: Rarity.Common,
            price: 450,
            isLocked: true,
            maxHealth: 50,
            health: 50,
            defense: 0,
            speed: 0.02f,
            pickupRange: 2.5f,

           damageMultiplier: 0.5f,
           castTimeMultiplier: 0.25f

        );
        Tank.name = TankStats.name;
        Tank.AddComponent<StatComponent>().stat = TankStats;
        AddCharacter(Tank);

        GameObject DemoMan = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats DemoManStats = new PlayerCharacterStats(
           name: "DemoMan",
           description: "KaBoom",
           icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/7"),
           rarity: Rarity.Rare,
           price: 800,
           isLocked: true,
           maxHealth: 40,
           health: 40,
           defense: 0,
           speed: 0.025f,
           pickupRange: 2.5f,
           aimRangeAdditive: 0,

           shotsPerAttack: 1,

           activeDuration: 0.2f
       );
        DemoMan.name = DemoManStats.name;
        DemoMan.AddComponent<StatComponent>().stat = DemoManStats;
        AddCharacter(DemoMan);

        GameObject Brawler = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats BrawlerStats = new PlayerCharacterStats(
            name: "Brawler",
            description: "Bloody Knuckles",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/3"),
            rarity: Rarity.Rare,
            price: 800,
            isLocked: true,
            maxHealth: 45,
            health: 45,
            defense: 0,
            speed: 0.028f,
            pickupRange: 2f,
            aimRangeAdditive: 0,

            comboLength: 1
        );
        Brawler.name = BrawlerStats.name;

        Brawler.AddComponent<StatComponent>().stat = BrawlerStats;
        AddCharacter(Brawler);


        GameObject Alchemist = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats AlchemistStats = new PlayerCharacterStats(
            name: "Alchemist",
            description: "Swish, Glug",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/4"),
            rarity: Rarity.Rare,
            price: 800,
            isLocked: true,
            maxHealth: 35,
            health: 35,
            defense: 0,
            speed: 0.03f,
            pickupRange: 3f,

           effectDuration: 1f,
           effectMultiplier: 0.5f
        );
        Alchemist.name = AlchemistStats.name;

        Alchemist.AddComponent<StatComponent>().stat = AlchemistStats;
        AddCharacter(Alchemist);

        GameObject SpaceMarine = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats SpaceMarineStats = new PlayerCharacterStats(
            name: "Space Marine",
            description: "Gravity? nah",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/6"),
            rarity: Rarity.Rare,
            price: 800,
            isLocked: true,
            maxHealth: 45,
            health: 45,
            defense: 0,
            speed: 0.025f,
            pickupRange: 2f,

           projectileSpeedMultiplier: -0.5f,
           rangeMultiplier: -0.5f,
           activeMultiplier: 0.5f
        );
        SpaceMarine.name = SpaceMarineStats.name;

        SpaceMarine.AddComponent<StatComponent>().stat = SpaceMarineStats;
        AddCharacter(SpaceMarine);


        GameObject Monk = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats MonkStats = new PlayerCharacterStats(
            name: "Monk",
            description: "Harness your energy",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/2"),
            rarity: Rarity.Epic,
            price: 1200,
            isLocked: true,
            maxHealth: 42,
            health: 42,
            defense: 0,
            speed: 0.03f,
            pickupRange: 2f,
            aimRangeAdditive: 0,

            shotsPerAttackMelee: 1
        );
        Monk.name = MonkStats.name;

        Monk.AddComponent<StatComponent>().stat = MonkStats;
        AddCharacter(Monk);


        GameObject Fractured = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats FracturedStats = new PlayerCharacterStats(
            name: "Fractured",
            description: "Tweakin' hard",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/7"),
            rarity: Rarity.Epic,
            price: 1200,
            isLocked: false,
            maxHealth: 30,
            health: 30,
            defense: 0,
            speed: 0.03f,
            pickupRange: 2f,

            multicastChance: 0.5f
        );
        Fractured.name = FracturedStats.name;

        Fractured.AddComponent<StatComponent>().stat = FracturedStats;
        AddCharacter(Fractured);


        GameObject Twins = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats TwinsStats = new PlayerCharacterStats(
            name: "Twins",
            description: "Watches your back",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/8"),
            rarity: Rarity.Legendary,
            price: 2500,
            isLocked: true,
            maxHealth: 40,
            health: 40,
            defense: 0,
            speed: 0.025f,
            pickupRange: 2f,

            shootOpposideSide: true
        );
        Twins.name = TwinsStats.name;

        Twins.AddComponent<StatComponent>().stat = TwinsStats;
        AddCharacter(Twins);

        Debug.Log("Finished initalizing PlayerCharacterLibrary");
        isInitialized = true;
    }

    public static GameObject getCharacter(string name)
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        if (PlayerCharactersLibraryMap.ContainsKey(name))
        {
            // Log the result before returning it.
            Debug.Log($"Found stat: {name} ");

            return PlayerCharactersLibraryMap[name];
        }
        else
        {
            throw new System.Exception("Player Character Library does not contain " + name);
        }
    }

    public static GameObject[] getCharacters()
    {
        if (!isInitialized)
        {
            InitializeLibrary();
        }

        return PlayerCharactersLibraryMap.Values.ToArray();
    }

}
