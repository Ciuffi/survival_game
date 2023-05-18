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
        Debug.Log("Initializing PlayerCharacterLibrary...");



        GameObject defaultCharacter = defaultPlayerPrefab;

        PlayerCharacterStats defaultStats = new PlayerCharacterStats(
            name: "Default",
            description: "Default Character",
            icon: Resources.Load<Sprite>("PlayerCharacters/SelectionPortrait/1"),
            rarity: Rarity.Common,
            price: 0,
            isLocked: false,
            maxHealth: 40,
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
            damageMultiplier: 1f,
            spreadMultiplier: 1,
            castTimeMultiplier: 1,
            comboWaitTimeMultiplier: 1,
            projectileSpeedMultiplier: 1,
            rangeMultiplier: 1,
            knockbackMultiplier: 1,
            thrownDamageMultiplier: 1,
            thrownSpeedMultiplier: 1,
            projectileSizeMultiplier: 1,
            meleeSizeMultiplier: 1,

            shootOpposideSide: false
        );

        defaultCharacter.AddComponent<StatComponent>().stat = defaultStats;
        AddCharacter(defaultCharacter);





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
