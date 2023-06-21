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
    public TextMeshProUGUI descText;

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
    private Image characterPreviewSprite;
    private Animator characterPreviewAnimator;


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
        descText = GameObject.Find("Canvas/InfoBox_Desc/Info").GetComponent<TextMeshProUGUI>();
        //GameObject text3 = GameObject.Find("WeaponsName");
        //weaponsText = text3.GetComponent<TextMeshProUGUI>();
        hasSelected = false;
        startBtn = FindObjectOfType<StartRun>();
        buttonImage = GetComponent<Image>();
        defaultColor = GetComponent<Image>().color;

        characterPreviewSprite = GameObject.Find("Canvas/CharacterPreview").GetComponent<Image>();
        characterPreviewAnimator = GameObject.Find("Canvas/CharacterPreview").GetComponent<Animator>();
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

    public void UpdateCharacterPreview(PlayerCharacterStats newCharacter)
    {
        Debug.Log(newCharacter.name);
        characterPreviewSprite.sprite = newCharacter.characterSprite;
        characterPreviewAnimator.runtimeAnimatorController = newCharacter.characterAnimationController;
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

        descText.text = stats.description;

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
        descText.text = stats.description;

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        UpdatePriceText();
        UpdatePurchaseButton();
        UpdateCharacterPreview(stats);
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
        statsString += "Pickup Range " + stats.pickupRange + "\n";

        // Check each stat and add it to the string if it meets the criteria 
        if (stats.rerollTimes != 0)
        {
            statsString += "Reroll +" + stats.rerollTimes + "\n";
        }
        if (stats.defense != 0)
        {
            statsString += "Defense " + stats.defense + "\n";
        }
        if (stats.shield != 0)
        {
            statsString += "Shield " + stats.shield + "\n";
        }
        if (stats.damageMultiplier > 0)
        {
            statsString += "Damage +" + stats.damageMultiplier * 100 + "%" + "\n";
        } else if (stats.damageMultiplier < 0)
        {
            statsString += "Damage " + stats.damageMultiplier * 100 + "%" + "\n";
        }
        if (stats.critChance > 0)
        {
            statsString += "Crit Chance +" + stats.critChance * 100 + "%" + "\n";
        } else if (stats.critChance < 0)
        {
            statsString += "Crit Chance " + stats.critChance * 100 + "%" + "\n";
        }
        if (stats.critDmg > 0)
        {
            statsString += "Crit Damage +" + stats.critDmg * 100 + "%" + "\n";
        } else if (stats.critDmg < 0)
        {
            statsString += "Crit Damage " + stats.critDmg * 100 + "%" + "\n";
        }
        if (stats.castTimeMultiplier > 0)
        {
            statsString += "Cast Time +" + stats.castTimeMultiplier * 100 + "%" + "\n";
        }
        else if (stats.castTimeMultiplier < 0){
            statsString += "Cast Time " + stats.castTimeMultiplier * 100 + "%" + "\n";
        }
        if (stats.spreadMultiplier > 0)
        {
            statsString += "Rate of Fire +" + stats.spreadMultiplier * 100 + "%" + "\n";
        }
        else if (stats.spreadMultiplier < 0)
        {
            statsString += "Rate of Fire " + stats.spreadMultiplier * 100 + "%" + "\n";
        }
        if (stats.shotgunSpread > 0)
        {
            statsString += "Shotgun Spread +" + stats.shotgunSpread + "degrees" + "\n";
        } else if (stats.shotgunSpread < 0)
        {
            statsString += "Shotgun Spread " + stats.shotgunSpread + "degrees" + "\n";
        }
        if (stats.multicastChance > 0)
        {
            statsString += "Multicast Chance +" + stats.multicastChance * 100 + "%" + "\n";
        } else if (stats.multicastChance < 0)
        {
            statsString += "Multicast Chance " + stats.multicastChance * 100 + "%" + "\n";
        }
        if (stats.shotsPerAttack != 0 && stats.shotsPerAttack > 0)
        {
            statsString += "Projectiles +" + stats.shotsPerAttack + "\n";
        }
        if (stats.shotsPerAttack != 0 && stats.shotsPerAttack < 0)
        {
            statsString += "Projectiles " + stats.shotsPerAttack + "\n";
        }
        if (stats.projectileSpeedMultiplier > 0)
        {
            statsString += "Projectile Speed +" + stats.projectileSpeedMultiplier * 100 + "%" + "\n";
        } else if (stats.projectileSpeedMultiplier < 0)
        {
            statsString += "Projectile Speed " + stats.projectileSpeedMultiplier * 100 + "%" + "\n";
        }
        if (stats.rangeMultiplier > 0)
        {
            statsString += "Projectile Range +" + stats.rangeMultiplier * 100 + "%" + "\n";
        }
        else if (stats.rangeMultiplier < 0)
        {
            statsString += "Projectile Range " + stats.rangeMultiplier * 100 + "%" + "\n";
        }
        if (stats.projectileSizeMultiplier > 0)
        {
            statsString += "Projectile Size +" + stats.projectileSizeMultiplier * 100 + "%" + "\n";
        } else if (stats.projectileSizeMultiplier < 0)
        {
            statsString += "Projectile Size " + stats.projectileSizeMultiplier * 100 + "%" + "\n";
        }
        if (stats.comboLength > 0)
        {
            statsString += "Melee, Nova cast +" + stats.comboLength + "\n";
        }
        else if (stats.comboLength < 0)
        {
            statsString += "Melee, Nova cast " + stats.comboLength + "\n";
        }
        if (stats.shotsPerAttackMelee > 0)
        {
            statsString += "Aftershocks +" + stats.shotsPerAttackMelee + "\n";
        }
        else if (stats.shotsPerAttackMelee < 0)
        {
            statsString += "Aftershocks " + stats.comboLength + "\n";
        }
        if (stats.comboWaitTimeMultiplier > 0)
        {
            statsString += "Melee, Nova Speed +" + stats.comboWaitTimeMultiplier * 100 + "%" + "\n";
        }
        else if (stats.comboWaitTimeMultiplier < 0)
        {
            statsString += "Melee, Nova Speed " + stats.comboWaitTimeMultiplier * 100 + "%" + "\n";
        }
        if (stats.meleeSizeMultiplier > 0)
        {
            statsString += "Melee, Nova Size +" + stats.meleeSizeMultiplier * 100 + "%" + "\n";
        } else if (stats.meleeSizeMultiplier < 0)
        {
            statsString += "Melee, Nova Size " + stats.meleeSizeMultiplier * 100 + "%" + "\n";
        }
        if (stats.knockbackMultiplier > 0)
        {
            statsString += "Knockback +" + stats.knockbackMultiplier * 100 + "%" + "\n";
        } else if (stats.knockbackMultiplier < 0)
        {
            statsString += "Knockback " + stats.knockbackMultiplier * 100 + "%" + "\n";
        }
        if (stats.activeMultiplier > 0)
        {
            statsString += "Attack Duration +" + stats.activeMultiplier * 100 + "%" + "\n";
        } else if (stats.activeMultiplier < 0)
        {
            statsString += "Attack Duration " + stats.activeMultiplier * 100 + "%" + "\n";
        }
        if (stats.activeDuration > 0)
        {
            statsString += "Attack Duration +" + stats.activeDuration + "s" + "\n";
        }
        else if (stats.activeDuration < 0)
        {
            statsString += "Attack Duration " + stats.activeDuration + "s" + "\n";
        }
        if (stats.effectMultiplier > 0)
        {
            statsString += "Effect Power +" + stats.effectMultiplier * 100 + "%" + "\n";
        } else if (stats.effectMultiplier < 0)
        {
            statsString += "Effect Power " + stats.effectMultiplier * 100 + "%" + "\n";
        }
        if (stats.effectDuration > 0)
        {
            statsString += "Effect Duration +" + stats.effectDuration + "s" + "\n";
        }
        else if (stats.effectDuration < 0)
        {
            statsString += "Effect Duration " + stats.effectDuration + "s" + "\n";
        }
        if (stats.thrownDamageMultiplier > 0)
        {
            statsString += "Wpn Toss Damage +" + stats.thrownSpeedMultiplier * 100 + "%" + "\n";
        } else if (stats.thrownDamageMultiplier < 0)
        {
            statsString += "Wpn Toss Damage " + stats.thrownSpeedMultiplier * 100 + "%" + "\n";
        }
        if (stats.thrownSpeedMultiplier > 0)
        {
            statsString += "Wpn Toss Speed +" + stats.thrownSpeedMultiplier * 100 + "%" + "\n";
        }
        else if (stats.thrownSpeedMultiplier < 0)
        {
            statsString += "Wpn Toss Speed " + stats.thrownSpeedMultiplier * 100 + "%" + "\n";
        }
        if (stats.isHoming != false)
        {
            statsString += "Homing Projectiles!\n";
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
