using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;

    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button purchaseButton;

    public Image iconImage;
    public Transform rarityImageParent; // Parent object for rarity images

    public Sprite[] rarityImages; // Array of images corresponding to rarity levels

    private PlayerDataManager playerDataManager;
    public List<PlayerCharacterStats> upgrades;
    private PlayerCharacterStats currentUpgrade;

    private void Awake()
    {
        playerDataManager = FindObjectOfType<PlayerDataManager>();
    }

    private void Start()
    {
        nameText = transform.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        rarityText = transform.Find("Text_Rarity").GetComponent<TextMeshProUGUI>();
        iconImage = transform.Find("Image").GetComponent<Image>();

        descriptionText = GameObject.Find("Canvas_Shop/PlayerUpgradesScrollView/Textbox/Text").GetComponent<TextMeshProUGUI>();
        priceText = GameObject.Find("Canvas_Shop/PlayerUpgradesScrollView/BuyUpgrade/Text").GetComponent<TextMeshProUGUI>();
        purchaseButton = GameObject.Find("Canvas_Shop/PlayerUpgradesScrollView/BuyUpgrade").GetComponent<Button>();

        // Get the parent object for rarity images
        rarityImageParent = transform.Find("RarityImageParent").transform;

        SetUpgrade();
    }

    public void SetUpgrade()
    {
        // Clear previous rarity images
        foreach (Transform child in rarityImageParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var upgrade in upgrades.OrderBy(u => u.level))
        {
            if (!playerDataManager.purchasedUpgrades.Contains(upgrade.GetUpgradeName()))
            {
                currentUpgrade = upgrade;
                break;
            }
        }

        // Spawn rarity images based on upgrade list
        foreach (var upgrade in upgrades.OrderBy(u => u.level))
        {
            GameObject newImageObject = new GameObject("RarityImage");
            Image newImage = newImageObject.AddComponent<Image>();
            newImage.sprite = rarityImages[(int)upgrade.rarity];
            newImageObject.transform.SetParent(rarityImageParent, false);
        }

        if (currentUpgrade == null)
        {
            purchaseButton.interactable = false;
            iconImage.color = Color.gray;
            return;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        nameText.text = currentUpgrade.GetUpgradeName();
        descriptionText.text = currentUpgrade.GetUpgradeDescription();
        priceText.text = currentUpgrade.price.ToString();
        iconImage.sprite = currentUpgrade.GetUpgradeIcon();

        if (playerDataManager.purchasedUpgrades.Contains(currentUpgrade.GetUpgradeName()))
        {
            purchaseButton.interactable = false;
            iconImage.color = Color.gray;
        }
        else
        {
            purchaseButton.interactable = true;
            iconImage.color = Color.white;
        }
    }

    public void PurchaseUpgrade()
    {
        if (playerDataManager.PurchaseUpgrade(currentUpgrade.GetUpgradeName(), currentUpgrade.price))
        {
            SetUpgrade();
        }
    }
}