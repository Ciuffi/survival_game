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
            description: "The OG",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/default"),
            characterSprite: Resources.Load<Sprite>("PlayerCharacters/Sprites/v3/player_v3"),
            characterAnimationController: Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/v3/PlayerMenuPreview"),
            rarity: Rarity.Common,
            price: 0,
            level: 0,
            isLocked: false,
            maxHealth: 40,
            health: 2,
            speed: 1.1f,
            pickupRange: 1.5f,
            rerollTimes: 3,
            baseRecoverySpeed: 5,

            shootOpposideSide: false
        ) ;

        defaultCharacter.name = defaultStats.name;
        defaultCharacter.AddComponent<StatComponent>().stat = defaultStats;
        AddCharacter(defaultCharacter);


        GameObject Witch = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats WitchStats = new PlayerCharacterStats(
           name: "Witch",
           description: "Shadow wizard money gang",
           icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/witch"),
           characterSprite: Resources.Load<Sprite>("PlayerCharacters/Sprites/v2/player_idle_2"),
           characterAnimationController: Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/v2/WitchPreview"),
           rarity: Rarity.Epic,
           price: 1600,
           level: 0,
           isLocked: true,
           maxHealth: 30,
           health: 30,
           defense: 0,
           speed: 1.3f,
           pickupRange: 2f,
           rerollTimes: 1,
           baseRecoverySpeed: 5,
           multicastChance: 0.5f,
           effectMultiplier: 0.5f
       );
        Witch.name = WitchStats.name;
        Witch.AddComponent<StatComponent>().stat = WitchStats;
        AddCharacter(Witch);


        GameObject AI = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats AIStats = new PlayerCharacterStats(
            name: "AI",
            description: "Beep boop",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/ai"),
            characterSprite: Resources.Load<Sprite>("PlayerCharacters/Sprites/robot/front"),
            characterAnimationController: Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/robot/robot_characterPreview"),
            rarity: Rarity.Legendary,
            price: 2000,
            level: 1,
            isLocked: true,
            maxHealth: 50,
            health: 50,
            defense: 0,
            speed: 1f,
            pickupRange: 1.25f,
            rerollTimes: 1,
            baseRecoverySpeed: 5,
            is360: true,
            damageMultiplier: 0.10f
        );
        AI.name = AIStats.name;
        AI.AddComponent<StatComponent>().stat = AIStats;
        AddCharacter(AI);

        GameObject Wildling = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats WildlingStats = new PlayerCharacterStats(
            name: "Wildling",
            description: "Charge straight in",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/ai"),
            characterSprite: Resources.Load<Sprite>("PlayerCharacters/Sprites/robot/front"),
            characterAnimationController: Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/robot/robot_characterPreview"),
            rarity: Rarity.Legendary,
            price: 2000,
            level: 2,
            isLocked: true,
            maxHealth: 50,
            health: 50,
            defense: 0.5f,
            speed: 1.25f,
            pickupRange: 1.3f,
            rerollTimes: 1,
            baseRecoverySpeed: 2,
            recoveryAmount: 1,
            isRevenge: true,
            revengeDamage: 18,
            damageMultiplier: -1.2f,
            critChance: 0.5f

        );
        Wildling.name = WildlingStats.name;
        Wildling.AddComponent<StatComponent>().stat = WildlingStats;
        AddCharacter(Wildling);


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
