using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class PlayerUpgradeButton : MonoBehaviour
{
    public string buttonId;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;

    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button purchaseButton;

    public Image iconImage;
    public Transform rarityImageParent; // Parent object for rarity images

    public Sprite[] rarityImages; // Array of images corresponding to rarity levels

    private PlayerDataManager playerDataManager;
    public List<GameObject> upgrades;
    private PlayerCharacterStats currentUpgrade;
    public GameObject selectedImage; // Reference to the 'Selected' image

    public float offsetAmount; // Offset distance between each image
    private PlayerUpgradeManager playerUpgradeManager;
    public int currentUpgradeIndex;
    private int initialUpgradesCount;

    public GameObject particleEffectPrefab;

    public GameObject invWpn;
    private List<Color> rarityColors;
    
    private void Awake()
    {
        playerDataManager = FindObjectOfType<PlayerDataManager>();
        playerUpgradeManager = FindObjectOfType<PlayerUpgradeManager>();

        nameText = transform.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        rarityText = transform.Find("Text_Rarity").GetComponent<TextMeshProUGUI>();
        iconImage = transform.Find("Image").GetComponent<Image>();

        descriptionText = GameObject.Find("Canvas_Upgrades/PlayerUpgradesScrollView/Textbox/Text").GetComponent<TextMeshProUGUI>();
        priceText = GameObject.Find("Canvas_Upgrades/PlayerUpgradesScrollView/BuyUpgrade/Text").GetComponent<TextMeshProUGUI>();
        purchaseButton = GameObject.Find("Canvas_Upgrades/PlayerUpgradesScrollView/BuyUpgrade").GetComponent<Button>();

        // Get the parent object for rarity images
        rarityImageParent = transform.Find("RarityImageParent").transform;
        rarityColors = invWpn.GetComponent<InventoryItem>().rarityColors;

    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => playerUpgradeManager.OnButtonClicked(this));
        selectedImage.SetActive(false);       
    }

    public void LoadUpgrade()
    {
        initialUpgradesCount = upgrades.Count;
        Debug.Log($"Initial upgrade count: {initialUpgradesCount}");

        InstantiateRarityImages();

        currentUpgradeIndex = playerDataManager.upgradeIndices[buttonId];
        // Make sure the currentUpgradeIndex is within the valid range
        if (currentUpgradeIndex >= 0 && currentUpgradeIndex < upgrades.Count)
        {
            currentUpgrade = upgrades[currentUpgradeIndex].GetComponent<StatComponent>().stat;
        }
        else if (upgrades.Count > 0)
        {
            currentUpgradeIndex = upgrades.Count - 1;
            currentUpgrade = upgrades[currentUpgradeIndex].GetComponent<StatComponent>().stat;
        }
        else
        {
            currentUpgrade = null;
        }
        Debug.Log($"Current upgrade: {currentUpgrade}");
        UpdateUI();
    }

    public void SetUpgrade(int targetIndex)
    {
        string rootName = currentUpgrade.GetBaseName();

        // Update the upgrade index
        playerDataManager.upgradeIndices[rootName] = targetIndex;

        // Make sure the targetIndex is within the valid range
        if (targetIndex >= 0 && targetIndex < upgrades.Count)
        {
            currentUpgrade = upgrades[targetIndex].GetComponent<StatComponent>().stat;
            currentUpgradeIndex = targetIndex;
        }
        else
        {
            currentUpgradeIndex = upgrades.Count - 1;
            currentUpgrade = upgrades[currentUpgradeIndex].GetComponent<StatComponent>().stat;
        }

        UpdateUI();
        UpdateRarityImages(null);
    }


    private void InstantiateRarityImages()
    {
        float totalWidth = (initialUpgradesCount - 1) * offsetAmount;
        float currentOffset = -totalWidth / 2;

        for (int i = 0; i < initialUpgradesCount; i++)
        {
            PlayerCharacterStats upgrade = upgrades[i].GetComponent<StatComponent>().stat;

            GameObject newImageObject = new GameObject("RarityImage");
            Image newImage = newImageObject.AddComponent<Image>();
            newImage.sprite = rarityImages[(int)upgrade.rarity];

            // Set color to gray by default
            newImage.color = Color.gray;

            newImageObject.transform.localScale *= 0.8f;
            newImageObject.transform.SetParent(rarityImageParent, false);
            newImageObject.transform.localPosition = new Vector3(currentOffset, 0, 0);

            // If the upgrade is already purchased, color it white
            if (IsUpgradePurchased(upgrade))
            {
                newImage.color = Color.white;
            }

            currentOffset += offsetAmount;
        }
    }

    public void PurchaseUpgrade()
    {
        if (!IsUpgradePurchased(currentUpgrade) && playerDataManager.gold >= currentUpgrade.price)
        {
            if (particleEffectPrefab != null)
            {
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                Instantiate(particleEffectPrefab, newPos, Quaternion.identity, transform);
            }

            // Spawn a smaller particle effect on the purchase button
            if (purchaseButton != null && particleEffectPrefab != null)
            {
                // Calculate the position at the center of the purchase button
                Vector3 particlePosition = purchaseButton.transform.position;

                // Spawn a smaller particle effect at the calculated position
                GameObject smallerParticleEffect = Instantiate(particleEffectPrefab, particlePosition, Quaternion.identity, purchaseButton.transform);
                smallerParticleEffect.transform.localScale *= 0.3f;
            }

            string upgradeName = currentUpgrade.GetUpgradeName();
            playerDataManager.PurchaseUpgrade(upgradeName, currentUpgrade.price, currentUpgradeIndex);

            Image imageToUpdate = rarityImageParent.GetChild(currentUpgradeIndex).GetComponent<Image>();
            if (imageToUpdate != null)
            {
                imageToUpdate.color = Color.white;
            }

            // Save the currently purchased upgrade
            PlayerCharacterStats purchasedUpgrade = currentUpgrade;

            // Update the upgrade index for the specific upgrade group
            string rootName = currentUpgrade.GetBaseName();
            playerDataManager.upgradeIndices[rootName]++;

            // Update currentUpgrade to the next upgrade
            currentUpgradeIndex++;
            if (currentUpgradeIndex < upgrades.Count)
            {
                currentUpgrade = upgrades[currentUpgradeIndex].GetComponent<StatComponent>().stat;
                UpdateUI();
            }
            else
            {
                UpdateUI();
                purchaseButton.interactable = false;
            }

            // Update rarity images after purchasing an upgrade
            // Pass the purchasedUpgrade into UpdateRarityImages
            UpdateRarityImages(purchasedUpgrade);
        }
        else
        {
            Debug.Log("No more upgrades of type: " + buttonId);
        }
    }

    public void UpdateUI()
    {
        if (currentUpgrade == null)
        {
            Debug.Log("Current upgrade is null");
            ResetUI();
            return;
        }

        string upgradeName = currentUpgrade.GetUpgradeName();
        string editedName = Regex.Replace(upgradeName, @"\s\d$", "");
        nameText.text = editedName;
        rarityText.text = currentUpgrade.GetRarity().ToString();
        rarityText.color = rarityColors[(int)currentUpgrade.GetRarity()];
        descriptionText.text = currentUpgrade.GetUpgradeDescription();
        iconImage.sprite = currentUpgrade.GetUpgradeIcon();

        bool isUpgradePurchased = IsUpgradePurchased(currentUpgrade);
        purchaseButton.interactable = !isUpgradePurchased;
        iconImage.color = isUpgradePurchased ? Color.gray : Color.white;

        string price;
        if (!isUpgradePurchased)
        {
            price = "$" + currentUpgrade.price.ToString();
            nameText.color = Color.white;

        } else
        {
            price = "---";
            nameText.color = Color.gray;
        }

        priceText.text = price;

    }

    private bool IsUpgradePurchased(PlayerCharacterStats upgrade)
    {
        return playerDataManager.purchasedUpgrades.Contains(upgrade.GetUpgradeName());
    }

    public void Select()
    {
        selectedImage.SetActive(true);
        UpdateUI();

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => PurchaseUpgrade());
    }

    public void Deselect()
    {
        selectedImage.SetActive(false);
        ResetUI();
    }

    public void ResetUI()
    {
        descriptionText.text = "Select an Upgrade.";
        priceText.text = "$000";
    }

    public void ResetUpgradeUI()
    {
        ResetUI();

        playerDataManager.upgradeIndices[buttonId] = 0;

        upgrades.Clear();
        upgrades = PlayerUpgradesLibrary.getUpgrades().Where(u => u.GetComponent<StatComponent>().stat.GetBaseName() == buttonId).OrderBy(u => u.GetComponent<StatComponent>().stat.level).ToList();

        currentUpgradeIndex = 0;
        if (upgrades.Count > 0)
        {
            currentUpgrade = upgrades.ElementAt(0).GetComponent<StatComponent>().stat;
        }
        else
        {
            currentUpgrade = null;
        }

        iconImage.color = Color.white;
        purchaseButton.interactable = true;

        // Clear the old rarity images
        ClearRarityImages();

        // Instantiate new rarity images
        InstantiateRarityImages();

        // Reset the color of the rarity images
        UpdateRarityImages(null);
    }

    private void UpdateRarityImages(PlayerCharacterStats purchasedUpgrade)
    {
        int childCount = rarityImageParent.childCount;
        int unlocksCount = playerDataManager.upgradeIndices[buttonId];

        for (int i = 0; i < childCount; i++)
        {
            Image image = rarityImageParent.GetChild(i).GetComponent<Image>();
            image.color = i < unlocksCount ? Color.white : Color.gray;
        }
    }

    public void ClearRarityImages()
    {
        foreach (Transform child in rarityImageParent)
        {
            Destroy(child.gameObject);
        }
    }
}
