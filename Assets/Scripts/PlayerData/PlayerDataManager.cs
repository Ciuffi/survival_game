using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class PlayerDataManager : MonoBehaviour
{
    public int gold;
    public int unlockedStages; // This is an integer where each bit represents a stage, e.g. 00000011 means stages 1 and 2 are unlocked

    public HashSet<string> unlockedCharactersNames = new HashSet<string>();

    private PlayerInventory playerInventory;
    public List<TextMeshProUGUI> goldDisplay;
    CharSelectController charSelectController;

    public static PlayerDataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This line prevents the object from being destroyed between scenes
        }
        else
        {
            Destroy(gameObject);
        }

        playerInventory = PlayerInventory.Instance;
        goldDisplay.Add(GameObject.Find("playerGold").GetComponentInChildren<TextMeshProUGUI>());
        goldDisplay.Add(GameObject.Find("playerGold2").GetComponentInChildren<TextMeshProUGUI>());
        charSelectController = FindObjectOfType<CharSelectController>();

        LoadData();
    }

    public void LoadUnlocks()
    {
        for (int i = 0; i < charSelectController.characterPrefabs.Count; i++)
        {
            GameObject characterObject = charSelectController.characterPrefabs[i];
            if (characterObject == null)
            {
                Debug.LogError("CharacterObject at index " + i + " is null");
                continue;
            }

            StatComponent statComponent = characterObject.GetComponent<StatComponent>();

            if (statComponent == null)
            {
                Debug.LogError("StatComponent for CharacterObject at index " + i + " is null");
                continue;
            }

            PlayerCharacterStats character = statComponent.stat;

            if (character == null)
            {
                Debug.LogError("Stat for CharacterObject at index " + i + " is null");
                continue;
            }

            bool isUnlocked = unlockedCharactersNames.Contains(character.name);
            character.isLocked = !isUnlocked;
        }

        // Check and update unlocked state of stages
        StageButton[] stages = FindObjectsOfType<StageButton>();
        foreach (StageButton stage in stages)
        {
            bool isUnlocked = (unlockedStages & (1 << stage.stageID)) != 0;
            stage.isLocked = !isUnlocked;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Gold", gold);
        // Convert unlockedCharactersNames to a string
        string unlockedCharactersNamesString = string.Join(",", unlockedCharactersNames);
        PlayerPrefs.SetString("UnlockedCharactersNames", unlockedCharactersNamesString);
        
        PlayerPrefs.SetInt("UnlockedStages", unlockedStages);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        // Load the string from PlayerPrefs and convert it back to a HashSet
        string unlockedCharactersNamesString = PlayerPrefs.GetString("UnlockedCharactersNames", "");
        unlockedCharactersNames = new HashSet<string>(unlockedCharactersNamesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        
        unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);
    }

    public void IncrementGold()
    {
        int goldGained = PlayerPrefs.GetInt("incrementGold", 0);
        gold += goldGained;
        PlayerPrefs.SetInt("incrementGold", 0);
        SaveData();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        SaveData();
    }

    public void UnlockCharacter(GameObject character)
    {
        var characterStats = character.GetComponent<CharacterButton>().GetStats();
        if (characterStats != null)
        {
            unlockedCharactersNames.Add(characterStats.name);
        }
        SaveData();
    }

    public void UnlockNextStage(int currentStageID)
    {
        int nextStageID = currentStageID + 1;

        // Check if the next stage is locked
        if ((unlockedStages & (1 << nextStageID)) == 0)
        {
            // Unlock the next stage
            unlockedStages |= (1 << nextStageID);
            SaveData();
            Debug.Log("Unlocked stage " + nextStageID);
        }
        else
        {
            Debug.Log("Stage " + nextStageID + " is already unlocked");
        }
    }

    public void DecrementWeaponDurability()
    {
        playerInventory.DecrementWeaponDurability();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey("Gold");
        PlayerPrefs.DeleteKey("UnlockedStages");
        PlayerPrefs.DeleteKey("UnlockedCharactersNames");

        CharacterButton[] characters = FindObjectsOfType<CharacterButton>();

        foreach (CharacterButton c in characters)
        {
            if (c.stats != null)
            {
                PlayerCharacterStats character = c.stats;
                character.isLocked = true;
                string defaultName = "Default(Clone)";
                if (c.name.ToString() == defaultName)
                {
                    c.stats.isLocked = false;
                }
            }
        }

        StageButton[] stages = FindObjectsOfType<StageButton>();
        foreach (StageButton stage in stages)
        {
            stage.isLocked = true;
        }

        gold = 300;
        unlockedCharactersNames.Clear();
        unlockedStages = 1;

        SaveData();
    }

    private void Update()
    {
        string goldAmount = "$" + gold.ToString();
        for (int i = 0; i < goldDisplay.Count; i++)
        {
            goldDisplay[i].text = goldAmount;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)  // Check if the scene being loaded is the menu scene
        {
            goldDisplay.Clear();
            goldDisplay.Add(GameObject.Find("playerGold").GetComponentInChildren<TextMeshProUGUI>());
            goldDisplay.Add(GameObject.Find("playerGold2").GetComponentInChildren<TextMeshProUGUI>());
        }
    }
}
