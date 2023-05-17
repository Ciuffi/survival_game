using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerCharactersLibrary
{
    private static Dictionary<string, GameObject> PlayerCharactersLibraryMap =
        new Dictionary<string, GameObject>();
    private static bool isInitialized = false;

    static GameObject defaultPlayerPrefab = Resources.Load<GameObject>(
        "PlayerCharacters/0_Default"
    );

    private static void AddCharacter(GameObject playerCharacter)
    {
        PlayerCharactersLibraryMap.Add(playerCharacter.name, playerCharacter);
    }

    static PlayerCharactersLibrary()
    {
        InitializeLibrary();
    }

    public static void InitializeLibrary()
    {
        if (isInitialized)
        {
            Debug.Log("InitializeLibrary has already been called.");
            return;
        }

        Debug.Log("Initializing AttackStatsLibrary...");

        //Value of stats - Individual Weapon -> Wpn Set -> Player

        //Global - All Weapons

        GameObject defaultCharacter = defaultPlayerPrefab;

        PlayerCharacterStats defaultStats = new PlayerCharacterStats(
            name: "Default",
            description: "Default Character",
            icon: Resources.Load<Sprite>("UI_Icons/DMG_up"),
            rarity: Rarity.Common
        );

        defaultCharacter.AddComponent<StatComponent>().stat = defaultStats;

        //common
        AddCharacter(defaultCharacter);

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
            throw new System.Exception("AttackStatsLibrary does not contain a stat named " + name);
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
