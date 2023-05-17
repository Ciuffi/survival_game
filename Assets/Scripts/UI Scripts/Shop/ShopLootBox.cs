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

        purchaseButton.onClick.AddListener(() =>
        {
            gachaManager.PurchaseLootBox(this);
        });

        // Retrieve the attack names from the AttackLibrary
        possibleWeapons = GetAttackNames();
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

    public string PickRandomWeapon()
    {
        int randomIndex = Random.Range(0, possibleWeapons.Count);
        return possibleWeapons[randomIndex];
    }
}