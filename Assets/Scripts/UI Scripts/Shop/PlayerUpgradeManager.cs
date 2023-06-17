using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;
    public Transform upgradeButtonParent;
    private PlayerUpgradeButton selectedButton;
    private PlayerDataManager playerData;
    public Dictionary<string, PlayerUpgradeButton> upgradeButtonDictionary = new Dictionary<string, PlayerUpgradeButton>();


    private void Awake()
    {
        playerData = FindObjectOfType<PlayerDataManager>();
    }

    private void OnEnable()
    {
        upgradeButtonParent = transform.Find("Viewport/Content").transform;
        PlayerUpgradesLibrary.ResetLibrary();
        LoadUpgradeButtons();
        LoadUpgradeIndices();
    }

    private void LoadUpgradeButtons()
    {
        // Clear the existing buttons and the dictionary
        foreach (Transform child in upgradeButtonParent)
        {
            Destroy(child.gameObject);
        }
        upgradeButtonDictionary.Clear();

        // Get all upgrades
        var allUpgrades = PlayerUpgradesLibrary.getUpgrades();

        // Group upgrades by base name
        var upgradeGroups = allUpgrades.GroupBy(u => u.GetComponent<StatComponent>().stat.GetBaseName());

        // For each group, create an upgrade button and assign upgrades
        foreach (var upgradeGroup in upgradeGroups)
        {
            var buttonObj = Instantiate(upgradeButtonPrefab, upgradeButtonParent);
            var button = buttonObj.GetComponent<PlayerUpgradeButton>();
            button.buttonId = upgradeGroup.Key;

            // Order upgrades by level and convert back to list
            button.upgrades = upgradeGroup.OrderBy(u => u.GetComponent<StatComponent>().stat.level).ToList();

            // Initialize the upgradeIndices for each upgrade in this button
            foreach (var upgrade in button.upgrades)
            {
                string rootName = upgrade.GetComponent<StatComponent>().stat.GetBaseName(); // Get the root name without the level number
                if (!playerData.upgradeIndices.ContainsKey(rootName))
                {
                    playerData.upgradeIndices[rootName] = 0;
                }
            }

            upgradeButtonDictionary[button.buttonId] = button;
        }

        // Call LoadUpgrade() for each button after all upgrades have been assigned
        foreach (var button in upgradeButtonDictionary.Values)
        {
            button.LoadUpgrade();
        }
    }

    private void LoadUpgradeIndices()
    {
        foreach (var button in upgradeButtonDictionary.Values)
        {
            string rootName = button.buttonId;
            if (playerData.upgradeIndices.ContainsKey(rootName))
            {
                int savedIndex = playerData.upgradeIndices[rootName];
                button.SetUpgrade(savedIndex);
            }
            else
            {
                button.SetUpgrade(0); // Set a default value if the index is not found
            }
        }
    }


    public void OnButtonClicked(PlayerUpgradeButton button)
    {
        if (selectedButton != null)
        {
            selectedButton.Deselect();
        }

        button.Select();
        selectedButton = button;
    }
}