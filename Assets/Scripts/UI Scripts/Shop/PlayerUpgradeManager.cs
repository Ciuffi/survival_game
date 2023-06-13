using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;
    public Transform upgradeButtonParent;

    private void Start()
    {
        upgradeButtonParent = transform.Find("Viewport/Content").transform;
        var allUpgrades = PlayerUpgradesLibrary.getUpgrades();

        // Group upgrades by base name
        var upgradeGroups = allUpgrades.GroupBy(u => u.GetBaseName());

        // For each group, create an upgrade button and assign upgrades
        foreach (var upgradeGroup in upgradeGroups)
        {
            var buttonObj = Instantiate(upgradeButtonPrefab, upgradeButtonParent);
            var button = buttonObj.GetComponent<PlayerUpgradeButton>();

            // Order upgrades by level and convert back to list
            button.upgrades = upgradeGroup.OrderBy(u => u.level).ToList();
            button.SetUpgrade();
        }
    }
}