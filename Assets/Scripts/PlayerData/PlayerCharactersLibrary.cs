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
            isLocked: false,
            maxHealth: 40,
            health: 40,
            defense: 0,
            speed: 1f,
            pickupRange: 2f,
            aimRangeAdditive: 0,
            rerollTimes: 1,

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


        GameObject Witch = Object.Instantiate(defaultPlayerPrefab, parentObject.transform);
        PlayerCharacterStats WitchStats = new PlayerCharacterStats(
           name: "Witch",
           description: "Shadow wizard money gang",
           icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/witch"),
           characterSprite: Resources.Load<Sprite>("PlayerCharacters/Sprites/v2/player_idle_2"),
           characterAnimationController: Resources.Load<RuntimeAnimatorController>("PlayerCharacters/Sprites/v2/WitchPreview"),
           rarity: Rarity.Epic,
           price: 1500,
           isLocked: true,
           maxHealth: 30,
           health: 30,
           defense: 0,
           speed: 1.4f,
           pickupRange: 2.5f,
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
            price: 2500,
            isLocked: true,
            maxHealth: 50,
            health: 50,
            defense: 0,
            speed: 0.8f,
            pickupRange: 2f,
            isHoming: true,
            projectileSpeedMultiplier: -0.6f,
            castTimeMultiplier: 0.6f,
            projectileSizeMultiplier: 0.3f
        );
        AI.name = AIStats.name;
        AI.AddComponent<StatComponent>().stat = AIStats;
        AddCharacter(AI);


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
