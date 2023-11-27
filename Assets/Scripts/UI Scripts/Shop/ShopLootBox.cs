using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopLootBox : MonoBehaviour
{
    public int rarity; // The rarity level of this loot box
    public List<string> possibleWeapons; // The weapons that can be dropped from this loot box
    public Button purchaseButton; // The button to purchase this loot box
    public TextMeshProUGUI costText; // The text component to display the cost of the lootbox

    private GachaManager gachaManager;

    private void Start()
    {
        gachaManager = FindObjectOfType<GachaManager>();
        RarityData rarityData = gachaManager.GetRarityData(rarity);
        costText.text = "$" + rarityData.cost.ToString();
        purchaseButton.onClick.AddListener(() => gachaManager.PurchaseLootBox(this));
        RefreshLootboxPool();
    }

    public void RefreshLootboxPool()
    {
        possibleWeapons = GetAttackNamesBasedOnLevel(PlayerDataManager.Instance.playerLevel);
    }

    private List<string> GetAttackNamesBasedOnLevel(int playerLevel)
    {
        var attackBuilders = AttackLibrary.getAttackBuilders();
        var attackNames = new List<string>();

        foreach (var attackBuilder in attackBuilders)
        {
            if (attackBuilder.GetUnlockLevel() <= playerLevel)
            {
                attackNames.Add(attackBuilder.GetAttackName());
            }
        }

        return attackNames;
    }

    public bool AllWeaponsOwned(PlayerInventory inventory)
    {
        // Check if there's any weapon that the player doesn't own
        foreach (string weaponName in possibleWeapons)
        {
            if (!inventory.WeaponExists(weaponName))
            {
                return false;
            }
        }

        return true;
    }

    private List<string> GetAttackNames()
    {
        var attackBuilders = AttackLibrary.getAttackBuilders();
        var attackNames = new List<string>();

        foreach (var attackBuilder in attackBuilders)
        {
            attackNames.Add(attackBuilder.GetAttackName());
        }

        return attackNames;
    }

    public string PickRandomWeapon(PlayerInventory inventory)
    {
        List<string> availableWeapons = new List<string>();

        // Filter out weapons that are already owned by the player
        foreach (string weaponName in possibleWeapons)
        {
            if (!inventory.WeaponExists(weaponName))
            {
                availableWeapons.Add(weaponName);
            }
        }

        if (availableWeapons.Count == 0)
        {
            // Player already owns all weapons of this rarity,
            // so choose a random weapon from the original list
            int random = Random.Range(0, possibleWeapons.Count);
            return possibleWeapons[random];
        }

        int randomIndex = Random.Range(0, availableWeapons.Count);
        return availableWeapons[randomIndex];
    }
}