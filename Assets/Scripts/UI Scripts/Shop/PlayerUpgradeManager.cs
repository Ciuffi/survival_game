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
        foreach (Transform child in upgradeButtonParent)
        {
            Destroy(child.gameObject);
        }
        upgradeButtonDictionary.Clear();

        var allUpgrades = PlayerUpgradesLibrary.getUpgrades();
        var upgradeGroups = allUpgrades.GroupBy(u => u.GetComponent<StatComponent>().stat.GetBaseName());

        foreach (var upgradeGroup in upgradeGroups)
        {
            var filteredUpgrades = upgradeGroup
                .Where(u => u.GetComponent<StatComponent>().stat.unlockLevel <= playerData.playerLevel)
                .ToList();

            if (filteredUpgrades.Count > 0)
            {
                var buttonObj = Instantiate(upgradeButtonPrefab, upgradeButtonParent);
                var button = buttonObj.GetComponent<PlayerUpgradeButton>();
                button.buttonId = upgradeGroup.Key;
                button.upgrades = filteredUpgrades.OrderBy(u => u.GetComponent<StatComponent>().stat.unlockLevel).ToList();

                foreach (var upgrade in button.upgrades)
                {
                    string rootName = upgrade.GetComponent<StatComponent>().stat.GetBaseName();
                    if (!playerData.upgradeIndices.ContainsKey(rootName))
                    {
                        playerData.upgradeIndices[rootName] = 0;
                    }
                }

                upgradeButtonDictionary[button.buttonId] = button;
            }
        }

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
                button.SetUpgrade(0);
            }
        }
    }

    public void RefreshUpgradeButtons()
    {
        LoadUpgradeButtons();
        LoadUpgradeIndices();
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