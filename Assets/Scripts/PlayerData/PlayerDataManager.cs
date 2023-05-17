using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDataManager : MonoBehaviour
{
    public int gold;
    public int unlockedCharacters; // This is an integer where each bit represents a character, e.g. 00000101 means characters 1 and 3 are unlocked
    public int unlockedStages; // This is an integer where each bit represents a stage, e.g. 00000011 means stages 1 and 2 are unlocked

    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        LoadData();

        // Check and update unlocked state of characters
        PlayerCharacterStats[] characters = FindObjectsOfType<StatComponent>()
            .Select(s => s.GetComponent<PlayerCharacterStats>())
            .ToArray();
        foreach (PlayerCharacterStats character in characters)
        {
            bool isUnlocked =
                (unlockedCharacters & (1 << character.statsContainer.GetInstanceID())) != 0;
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

    private void SaveData()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("UnlockedCharacters", unlockedCharacters);
        PlayerPrefs.SetInt("UnlockedStages", unlockedStages);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        unlockedCharacters = PlayerPrefs.GetInt("UnlockedCharacters", 1);
        unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);
    }

    public void IncrementGold()
    {
        int goldGained = PlayerPrefs.GetInt("incrementGold", 0);
        gold += goldGained;
        PlayerPrefs.SetInt("incrementGold", 0);
        SaveData();
    }

    public void UnlockCharacter(PlayerCharacterStats character)
    {
        character.isLocked = false;
        unlockedCharacters |= (1 << character.statsContainer.GetInstanceID()); // Set the bit corresponding to this character to 1
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
        PlayerPrefs.DeleteKey("UnlockedCharacters");
        PlayerPrefs.DeleteKey("UnlockedStages");

        PlayerCharacterStats[] characters = FindObjectsOfType<StatComponent>()
            .Select(s => s.GetComponent<PlayerCharacterStats>())
            .ToArray();
        foreach (PlayerCharacterStats character in characters)
        {
            character.isLocked = true;
        }

        StageButton[] stages = FindObjectsOfType<StageButton>();
        foreach (StageButton stage in stages)
        {
            stage.isLocked = true;
        }

        gold = 0;
        unlockedCharacters = 1;
        unlockedStages = 1;

        SaveData();
    }
}
