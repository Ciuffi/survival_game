using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public List<ShopLootBox> shopLootBoxes; // List of all ShopLootBox instances
    public PlayerDataManager playerDataManager; // Reference to the player's data manager
    public PlayerInventory inventory;
    public GachaOverlayManager overlay;

    // Define the cost and drop rates for each rarity
    public List<RarityData> rarities;

    public void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        inventory = FindObjectOfType<PlayerInventory>();
        overlay = FindObjectOfType<GachaOverlayManager>();
        overlay.gameObject.SetActive(false);

    }


    public void PurchaseLootBox(ShopLootBox shopLootBox)
    {
        RarityData rarityData = rarities.Find(r => r.rarity == shopLootBox.rarity);

        // Check if the player has enough currency
        if (playerDataManager.gold >= rarityData.cost)
        {
            // Subtract the cost of the loot box from the player's currency
            playerDataManager.gold -= rarityData.cost;

            // Roll to determine the weapon's name and rarity
            string weaponName = shopLootBox.PickRandomWeapon();
            int weaponRarity = rarityData.RollRarity();

            // Add the weapon to the player's inventory
            Weapon weapon = new Weapon(weaponName, weaponRarity, false, 1);
            inventory.AddWeapon(weapon);

            // Add the weapon to the UI
            FindObjectOfType<InventoryUIManager>().AddWeaponToUI(weapon);

            overlay.gameObject.SetActive(true);
            overlay.DisplayWeapon(weapon);

            // Debug line to ensure weapon is being added
            Debug.Log("Added weapon: " + weapon.name + ", rarity: " + weapon.rarity);
        }
        else
        {
            // TODO: Notify the player that they don't have enough currency
            Debug.Log("NOT ENOUGH MONEY!!");
        }

        playerDataManager.SaveData();
    }


    public RarityData GetRarityData(int rarity)
    {
        return rarities.Find(r => r.rarity == rarity);
    }

}

[System.Serializable]
public class RarityData
{
    public int rarity; // The rarity level
    public int cost; // The cost to purchase a loot box of this rarity
    public List<float> dropChances; // The drop chances for each rarity level

    public int RollRarity()
    {
        float roll = Random.Range(0f, 100f);
        float cumulativeChance = 0;

        for (int i = 0; i < dropChances.Count; i++)
        {
            cumulativeChance += dropChances[i];
            if (roll <= cumulativeChance)
            {
                // Multiply the index by 2 to get the desired rarity
                return i * 2;
            }
        }

        // Default to the highest rarity if the roll is above all defined chances
        return (dropChances.Count - 1) * 2;
    }
}