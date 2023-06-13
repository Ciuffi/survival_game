using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject selectedImage;
    public PlayerCharacterStats stats;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI infoText;

    //public TextMeshProUGUI weaponsText;

    private CharSelectController characterSelector;
    public StartRun startBtn;
    private bool hasSelected;

    public Image buttonImage; // Image component on the character button.
    public Color lockedColor; // Color when the character is locked.
    public Color defaultColor; // Color when the character is unlocked.

    public Button purchaseButton; // The button to purchase/unlock the character.
    private PlayerDataManager playerDataManager; // Reference to the PlayerDataManager in the scene.
    private TextMeshProUGUI priceText;


    private void Awake()
    {
        // Find the CharacterSelector component in the scene
        characterSelector = FindObjectOfType<CharSelectController>();
        playerDataManager = PlayerDataManager.Instance;
        purchaseButton = GameObject.Find("BuyCharacter").GetComponent<Button>();
        priceText = purchaseButton.GetComponentInChildren<TextMeshProUGUI>();
        GameObject text1 = GameObject.Find("CharacterName");
        nameText = text1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        GameObject text2 = GameObject.Find("InfoBox");
        infoText = text2.transform.Find("Info").GetComponent<TextMeshProUGUI>();
        //GameObject text3 = GameObject.Find("WeaponsName");
        //weaponsText = text3.GetComponent<TextMeshProUGUI>();
        hasSelected = false;
        startBtn = FindObjectOfType<StartRun>();
        buttonImage = GetComponent<Image>();
        defaultColor = GetComponent<Image>().color;

    }

    private void Update()
    {
        if (hasSelected)
        {
            characterSelector.hasSelected = true;
        }

        if (stats != null && stats.isLocked)
        {
            buttonImage.color = lockedColor;
        }
        else
        {
            buttonImage.color = defaultColor;
        }
    }

    private void UpdatePurchaseButton()
    {
        purchaseButton.gameObject.SetActive(stats.isLocked);
       
    }

    private void UpdatePriceText()
    {
        string price = "$" + stats.price.ToString();
        priceText.text = price;
    }

    public void SelectThisCharacter()
    {
        if (!hasSelected)
        {
            hasSelected = true;
        }

        // Deselect the previously selected character, if any
        GameObject previouslySelected = GameObject.FindGameObjectWithTag("SelectedCharacter");
        if (previouslySelected != null)
        {
            CharacterButton previouslySelectedButton =
                previouslySelected.GetComponent<CharacterButton>();
            if (previouslySelectedButton != null)
            {
                previouslySelectedButton.Deselect();
            }
        }

        // Select this character
        selectedImage.SetActive(true);
        gameObject.tag = "SelectedCharacter";

        // Update the selected character in CharacterSelector
        characterSelector.selectedCharacter = stats;

        // Update the selected character in startRun
        startBtn.chosenName = stats.name;

        // Update the text with the selected character's stats
        if (nameText != null)
        {
            // Remove the "(Clone)" suffix from the game object's name and update the name text
            string gameObjectName = gameObject.name;
            string newName = gameObjectName.EndsWith("(Clone)")
                ? gameObjectName.Substring(0, gameObjectName.Length - 7)
                : gameObjectName;
            nameText.text = newName;
        }

        // Update the text with the selected character's stats
        if (infoText != null && stats != null)
        {
            infoText.text = GenerateStatsString(stats);
        }

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);

        UpdatePriceText();
        UpdatePurchaseButton();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!hasSelected)
        {
            hasSelected = true;
        }

        // Deselect the previously selected character, if any
        GameObject previouslySelected = GameObject.FindGameObjectWithTag("SelectedCharacter");
        if (previouslySelected != null)
        {
            CharacterButton previouslySelectedButton =
                previouslySelected.GetComponent<CharacterButton>();
            if (previouslySelectedButton != null)
            {
                previouslySelectedButton.Deselect();
            }
        }

        // Select this character
        selectedImage.SetActive(true);
        gameObject.tag = "SelectedCharacter";


        // Update the selected character in CharacterSelector
        characterSelector.selectedCharacter = stats;

        // Update the selected character in startRun
        startBtn.chosenName = stats.name;

        // Update the text with the selected character's stats
        if (nameText != null)
        {
            // Remove the "(Clone)" suffix from the game object's name and update the name text
            string gameObjectName = gameObject.name;
            string newName = gameObjectName.EndsWith("(Clone)")
                ? gameObjectName.Substring(0, gameObjectName.Length - 7)
                : gameObjectName;
            nameText.text = newName;
        }

        // Update the text with the selected character's stats
        if (infoText != null && stats != null)
        {
            infoText.text = GenerateStatsString(stats);
        }

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        UpdatePriceText();
        UpdatePurchaseButton();
    }

    // New method to handle purchase button click event.
    public void OnPurchaseButtonClicked()
    {
        // Check if the player has enough gold
        if (playerDataManager != null && stats != null && stats.isLocked && playerDataManager.gold >= stats.price)
        {
            // Deduct the price from player's gold
            playerDataManager.gold -= stats.price;
            stats.isLocked = false;

            // Unlock the character in PlayerDataManager
            playerDataManager.UnlockCharacter(gameObject);

            UpdatePriceText();
            UpdatePurchaseButton();
        }
    }

    private string GenerateStatsString(PlayerCharacterStats stats)
    {
        string statsString = "";

        statsString += "Health " + stats.health + "\n";
        statsString += "Speed " + stats.speed + "\n";

        // Check each stat and add it to the string if it meets the criteria

        if (stats.pickupRange != 2)
        {
            statsString += "Pickup Range +" + (stats.pickupRange - 2) + "\n";
        }
        if (stats.defense != 0)
        {
            statsString += "Defense " + stats.defense + "\n";
        }
        if (stats.shield != 0)
        {
            statsString += "Shield " + stats.shield + "\n";
        }
        if (stats.damageMultiplier != 0)
        {
            statsString += "DmgMultiplier% " + stats.damageMultiplier + "\n";
        }
        if (stats.critChance != 0)
        {
            statsString += "Crit% +" + stats.critChance + "\n";
        }
        if (stats.critDmg != 0)
        {
            statsString += "Crit DMG% +" + stats.critDmg + "\n";
        }
        if (stats.castTimeMultiplier != 0)
        {
            statsString += "Cast Time% " + stats.castTimeMultiplier + "\n";
        }
        if (stats.spreadMultiplier != 0)
        {
            statsString += "Rate of Fire% " + stats.spreadMultiplier + "\n";
        }
        if (stats.shotgunSpread > 0)
        {
            statsString += "Shotgun Spread +" + stats.shotgunSpread + "\n";
        }
        if (stats.multicastChance != 0)
        {
            statsString += "Multicast% +" + stats.multicastChance + "\n";
        }
        if (stats.shotsPerAttack != 0 && stats.shotsPerAttack > 0)
        {
            statsString += "Projectiles +" + stats.shotsPerAttack + "\n";
        }
        if (stats.shotsPerAttack != 0 && stats.shotsPerAttack < 0)
        {
            statsString += "Projectiles " + stats.shotsPerAttack + "\n";
        }
        if (stats.projectileSpeedMultiplier != 0)
        {
            statsString += "Proj Spd% " + stats.projectileSpeedMultiplier + "\n";
        }
        if (stats.rangeMultiplier != 0)
        {
            statsString += "Proj Range% " + stats.rangeMultiplier + "\n";
        }
        if (stats.projectileSizeMultiplier != 0)
        {
            statsString += "Proj Size% " + stats.projectileSizeMultiplier + "\n";
        }
        if (stats.comboLength != 0 && stats.comboLength > 0)
        {
            statsString += "Melee Hits +" + stats.comboLength + "\n";
        }
        if (stats.comboLength != 0 && stats.comboLength < 0)
        {
            statsString += "Melee Hits " + stats.comboLength + "\n";
        }
        if (stats.shotsPerAttackMelee != 0 && stats.shotsPerAttackMelee > 0)
        {
            statsString += "Aftershock +" + stats.shotsPerAttackMelee + "\n";
        }
        if (stats.shotsPerAttackMelee != 0 && stats.shotsPerAttackMelee < 0)
        {
            statsString += "Aftershock " + stats.comboLength + "\n";
        }
        if (stats.comboWaitTimeMultiplier != 0)
        {
            statsString += "Melee Speed% " + stats.comboWaitTimeMultiplier + "\n";
        }
        if (stats.meleeSizeMultiplier != 0)
        {
            statsString += "Melee Size% " + stats.meleeSizeMultiplier + "\n";
        }
        if (stats.knockbackMultiplier != 0)
        {
            statsString += "Knockback% " + stats.knockbackMultiplier + "\n";
        }
        if (stats.activeMultiplier != 0)
        {
            statsString += "Attack Duration% " + stats.activeMultiplier + "\n";
        }
        if (stats.activeDuration != 0)
        {
            statsString += "Attack Duration +" + stats.activeDuration + "s" + "\n";
        }
        if (stats.effectMultiplier != 0)
        {
            statsString += "Debuff Power% " + stats.effectMultiplier + "\n";
        }
        if (stats.effectDuration != 0)
        {
            statsString += "Debuff Duration +" + stats.effectDuration + "s" + "\n";
        }
        if (stats.thrownSpeedMultiplier != 0)
        {
            statsString += "Wpn Toss Speed% " + stats.thrownSpeedMultiplier + "\n";
        }
        if (stats.shootOpposideSide != false)
        {
            statsString += "DOUBLE TROUBLE\n";
        }

        return statsString;

    }


    public void Deselect()
    {
        selectedImage.SetActive(false);
        gameObject.tag = "Untagged";
        characterSelector.selectedCharacter = null;
    }

    public PlayerCharacterStats GetStats()
    {
        return stats;
    }
}
