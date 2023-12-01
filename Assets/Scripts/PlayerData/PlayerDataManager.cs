using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class PlayerDataManager : MonoBehaviour
{
    public int playerLevel;
    public int[] LevelArray;
    public int[] StageXpArray;
    public int currentXp;
    public int xpGained;
    
    public int gold;
    public int unlockedStages; // This is an integer where each bit represents a stage, e.g. 00000011 means stages 1 and 2 are unlocked

    public HashSet<string> unlockedCharactersNames = new HashSet<string>();
    public HashSet<string> purchasedUpgrades = new HashSet<string>();

    private PlayerInventory playerInventory;
    public List<TextMeshProUGUI> goldDisplay;
    TextMeshProUGUI playerLevelText;
    CharSelectController charSelectController;
    public Transform upgradeButtonParent;
    public static PlayerDataManager Instance { get; private set; }
    public Dictionary<string, int> upgradeIndices = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerDataManager instance created");
        }
        else
        {
            Debug.Log("Duplicate PlayerDataManager instance detected, destroying");
            Destroy(gameObject);
        }

        playerInventory = PlayerInventory.Instance;
        goldDisplay = new List<TextMeshProUGUI>
        {
            GameObject.Find("playerGold").GetComponentInChildren<TextMeshProUGUI>(),
            GameObject.Find("playerGold2").GetComponentInChildren<TextMeshProUGUI>(),
            GameObject.Find("playerGold3").GetComponentInChildren<TextMeshProUGUI>()
        };
        charSelectController = FindObjectOfType<CharSelectController>();
        upgradeButtonParent = GameObject.Find("Canvas_Upgrades/PlayerUpgradesScrollView/Viewport/Content").transform;
        playerLevelText = GameObject.Find("PlayerLevelText").GetComponent<TextMeshProUGUI>();
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("UnlockedStages", unlockedStages);

        // Convert unlockedCharactersNames to a string
        string unlockedCharactersNamesString = string.Join(",", unlockedCharactersNames);
        PlayerPrefs.SetString("UnlockedCharactersNames", unlockedCharactersNamesString);

        // Save purchasedUpgrades
        string purchasedUpgradesString = string.Join(",", purchasedUpgrades);
        PlayerPrefs.SetString("PurchasedUpgrades", purchasedUpgradesString);
        Debug.Log("Saved purchased upgrades: " + purchasedUpgradesString);

        // Save upgradeIndices
        string upgradeIndicesString = string.Join(",", upgradeIndices.Select(kv => kv.Key + ":" + kv.Value.ToString()));
        PlayerPrefs.SetString("UpgradeIndices", upgradeIndicesString);

        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("CurrentXP", currentXp);

        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);

        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 0);
        currentXp = PlayerPrefs.GetInt("CurrentXP", 0); 

        string unlockedCharactersNamesString = PlayerPrefs.GetString("UnlockedCharactersNames", "");
        unlockedCharactersNames = new HashSet<string>(unlockedCharactersNamesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

        string purchasedUpgradesString = PlayerPrefs.GetString("PurchasedUpgrades", "");
        purchasedUpgrades = new HashSet<string>(purchasedUpgradesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        Debug.Log("Retrieved purchased upgrades: " + purchasedUpgradesString);

        string upgradeIndicesString = PlayerPrefs.GetString("UpgradeIndices", "");
        var upgradeIndicesArray = upgradeIndicesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var upgradeIndex in upgradeIndicesArray)
        {
            var keyValue = upgradeIndex.Split(':');
            if (keyValue.Length == 2)
            {
                string upgradeName = keyValue[0];
                int index;
                if (int.TryParse(keyValue[1], out index))
                {
                    upgradeIndices[upgradeName] = index;
                }
            }
        }
    }


    public void PurchaseUpgrade(string upgradeName, int price, int index)
    {
        if (gold >= price && !purchasedUpgrades.Contains(upgradeName))
        {
            gold -= price;
            purchasedUpgrades.Add(upgradeName);

            // Update the upgrade index for the specific upgrade
            upgradeIndices[upgradeName] = index;

            SaveData();

            var upgradeButtonDictionary = FindObjectOfType<PlayerUpgradeManager>().upgradeButtonDictionary;

            // Trigger the upgrade UI reset and update immediately
            if (upgradeButtonDictionary.TryGetValue(upgradeName, out var button))
            {
                button.ResetUpgradeUI();
                button.SetUpgrade(index);
            }
        }
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
        int originalGold = gold;
        gold += amount;
        StartCoroutine(CountUpGold(originalGold, gold, 1.5f)); // count up over 1 second
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


    public void ResetData()
    {
        PlayerPrefs.DeleteKey("Gold");
        PlayerPrefs.DeleteKey("UnlockedStages");
        PlayerPrefs.DeleteKey("UnlockedCharactersNames");
        PlayerPrefs.DeleteKey("PurchasedUpgrades");
        PlayerPrefs.DeleteKey("UpgradeIndices");
        PlayerPrefs.DeleteKey("PlayerLevel");
        PlayerPrefs.DeleteKey("CurrentXP");

        AddMoney addMoneyComponent = FindObjectOfType<AddMoney>();
        if (addMoneyComponent != null)
        {
            addMoneyComponent.ResetCooldown();
        }

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

        gold = 420;
        unlockedCharactersNames.Clear();
        unlockedStages = 1;
        purchasedUpgrades.Clear();  // Also clear the in-memory HashSet of purchased upgrades
        for (int i = 0; i < upgradeButtonParent.childCount; i++)
        {
            var button = upgradeButtonParent.GetChild(i).GetComponent<PlayerUpgradeButton>();
            button.SetUpgrade(0); // Set the upgrade to the first one after resetting
            string upgradeName = button.buttonId;
            upgradeIndices[upgradeName] = 0;
            button.ResetUpgradeUI();

        }

        playerLevel = 0;
        currentXp = 0;

        //reset lootbox pool
        ShopLootBox[] lootBoxes = FindObjectsOfType<ShopLootBox>();
        foreach (ShopLootBox lootBox in lootBoxes)
        {
            lootBox.RefreshLootboxPool();
        }

        //reset character pool
        CharSelectController charSelectController = FindObjectOfType<CharSelectController>();
        if (charSelectController != null)
        {
            charSelectController.RefreshCharacterSelection();
        }

        //reset player upgrades
        PlayerUpgradeManager upgradeManager = FindObjectOfType<PlayerUpgradeManager>();
        if (upgradeManager != null)
        {
            upgradeManager.RefreshUpgradeButtons();
        }

        SaveData();
    }

    public void ProcessEndGameStats(int enemiesKilled, int goldGained, int guiltReached, WeaponStats weaponStats, int stage)
    {
        // Process gold
        AddGold(goldGained);

        // Process XP
        CalculateAndApplyXp(guiltReached, stage);

        // Process weapon stats if needed

        CheckLevelUp();
    }

    private void CalculateAndApplyXp(int guilt, int stage)
    {
        int baseXp = GetBaseXpForCurrentStage(stage); // Implement this based on your game's logic
        int xpMultiplier = guilt + 1;
        int xpEarned = baseXp * xpMultiplier;
        Debug.Log("xp +" + xpEarned);
        currentXp += xpEarned;
        Debug.Log("currentXP " + currentXp);
    }

    private int GetBaseXpForCurrentStage(int stage)
    {
        // Implement logic to fetch base XP for the current stage
        return StageXpArray[stage];
    }

    private void CheckLevelUp()
    {
        while (currentXp >= LevelArray[playerLevel])
        {
            currentXp -= LevelArray[playerLevel];
            playerLevel++;
            Debug.Log("currentLevel " + playerLevel);
            // Additional level-up logic (e.g., rewards, notifications) goes here
        }
    }

    private void Update()
    {
        string goldAmount = "$" + gold.ToString();
        for (int i = 0; i < goldDisplay.Count; i++)
        {
            goldDisplay[i].text = goldAmount;
        }

        string levelText = "Level " + playerLevel.ToString();
        playerLevelText.text = levelText;
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
            goldDisplay.Add(GameObject.Find("playerGold3").GetComponentInChildren<TextMeshProUGUI>());
            upgradeButtonParent = GameObject.Find("Canvas_Upgrades/PlayerUpgradesScrollView/Viewport/Content").transform;
            playerLevelText = GameObject.Find("PlayerLevelText").GetComponent<TextMeshProUGUI>();

        }
    }

    public IEnumerator CountUpGold(int originalAmount, int newAmount, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // advance the 'timer'
            int currentAmount = (int)Mathf.Lerp(originalAmount, newAmount, elapsed / duration);
            // Set the text on all goldDisplay items
            for (int i = 0; i < goldDisplay.Count; i++)
            {
                goldDisplay[i].text = "$" + currentAmount.ToString();
            }
            yield return null; // wait until next frame
        }
        // Ensure the final value is correct (and not off due to rounding)
        for (int i = 0; i < goldDisplay.Count; i++)
        {
            goldDisplay[i].text = "$" + newAmount.ToString();
        }
    }

}
