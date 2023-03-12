using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject selectedImage;
    private PlayerCharacterStats stats;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI weaponsText;

    private CharSelectController characterSelector;
    public GameObject startBtn;
    private bool hasSelected;

    private void Start()
    {
        // Find the CharacterSelector component in the scene
        characterSelector = FindObjectOfType<CharSelectController>();
        GameObject text1 = GameObject.Find("CharacterName");
        nameText= text1.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        GameObject text2 = GameObject.Find("InfoBox");
        infoText = text2.transform.Find("Info").GetComponent<TextMeshProUGUI>();
        GameObject text3 = GameObject.Find("WeaponsName");
        weaponsText = text3.GetComponent<TextMeshProUGUI>();
        hasSelected = false;
        startBtn = GameObject.Find("StartBtn");
    }

    private void Update()
    {
        if (hasSelected)
        {
            characterSelector.GetComponent<CharSelectController>().hasSelected = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (!hasSelected)
        {
            startBtn.GetComponent<Image>().enabled = true;
            hasSelected = true;
        }


        // Deselect the previously selected character, if any
        GameObject previouslySelected = GameObject.FindGameObjectWithTag("SelectedCharacter");
        if (previouslySelected != null)
        {
            CharacterButton previouslySelectedButton = previouslySelected.GetComponent<CharacterButton>();
            if (previouslySelectedButton != null)
            {
                previouslySelectedButton.Deselect();
            }
        }

        // Select this character
        selectedImage.SetActive(true);
        gameObject.tag = "SelectedCharacter";

        // Save the character stats
        stats = GetComponent<PlayerCharacterStats>();

        // Update the selected character in CharacterSelector
        CharSelectController characterSelector = FindObjectOfType<CharSelectController>();
        if (characterSelector != null)
        {
            characterSelector.selectedCharacter = stats;
        }
        // Update the selected character in startRun
        startBtn.GetComponent<StartRun>().chosenName = stats.name;
        


        // Update the text with the selected character's stats
        if (nameText != null)
        {
            // Remove the "(Clone)" suffix from the game object's name and update the name text
            string gameObjectName = gameObject.name;
            string newName = gameObjectName.EndsWith("(Clone)") ? gameObjectName.Substring(0, gameObjectName.Length - 7) : gameObjectName;
            nameText.text = newName;
        }

        // Update the text with the selected character's stats
        if (weaponsText != null)
        {
            string weaponsString = "";

            foreach (GameObject weapons in stats.startingWeapons)
            {
                weaponsString += weapons.name + "\n";
            }

            weaponsText.text = weaponsString;
        }


        // Update the text with the selected character's stats
        if (infoText != null && stats != null)
        {
            string statsString = "";

            statsString += "Health " + stats.health + "\n";
            statsString += "Speed " + stats.speed + "\n";

            // Check each stat and add it to the string if it meets the criteria

            if (stats.pickupRange != 1.5)
            {
                statsString += "Pickup Range +" + stats.pickupRange + "\n";
            }
            if (stats.defense != 0)
            {
                statsString += "Defense " + stats.defense + "\n";
            }
            if (stats.shield != 0)
            {
                statsString += "Shield " + stats.shield + "\n";
            }
            if (stats.damageMultiplier != 1)
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
            if (stats.castTimeMultiplier != 1)
            {
                statsString += "Cast Time% " + stats.castTimeMultiplier + "\n";
            }
            if (stats.spreadMultiplier != 1)
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
            if (stats.projectileSpeedMultiplier != 1)
            {
                statsString += "Proj Spd% " + stats.projectileSpeedMultiplier + "\n";
            }
            if (stats.rangeMultiplier != 1)
            {
                statsString += "Proj Range% " + stats.rangeMultiplier + "\n";
            }
            if (stats.projectileSizeMultiplier != 1)
            {
                statsString += "Proj Size% " + stats.projectileSizeMultiplier + "\n";
            }
            if (stats.meleeComboLength != 0 && stats.meleeComboLength > 0)
            {
                statsString += "Melee Hits +" + stats.meleeComboLength + "\n";
            }
            if (stats.meleeComboLength != 0 && stats.meleeComboLength < 0)
            {
                statsString += "Melee Hits " + stats.meleeComboLength + "\n";
            }
            if (stats.shotsPerAttackMelee != 0 && stats.shotsPerAttackMelee > 0)
            {
                statsString += "Aftershock +" + stats.shotsPerAttackMelee + "\n";
            }
            if (stats.shotsPerAttackMelee != 0 && stats.shotsPerAttackMelee < 0)
            {
                statsString += "Aftershock " + stats.meleeComboLength + "\n";
            }
            if (stats.meleeWaitTimeMultiplier != 1)
            {
                statsString += "Melee Speed% " + stats.meleeWaitTimeMultiplier + "\n";
            }
            if (stats.meleeSizeMultiplier != 1)
            {
                statsString += "Melee Size% " + stats.meleeSizeMultiplier + "\n";
            }         
            if (stats.knockbackMultiplier != 1)
            {
                statsString += "Knockback% " + stats.knockbackMultiplier + "\n";
            }                    
            if (stats.thrownDamageMultiplier != 1)
            {
                statsString += "Wpn Toss Dmg% " + stats.thrownDamageMultiplier + "\n";
            }
            if (stats.thrownSpeedMultiplier != 1)
            {
                statsString += "Wpn Toss Speed% " + stats.thrownSpeedMultiplier + "\n";
            }           
            if (stats.shootOpposideSide != false)
            {
                statsString += "DOUBLE TROUBLE\n";
            }

            infoText.text = statsString;
        }


    }

    public void Deselect()
    {
        selectedImage.SetActive(false);
        gameObject.tag = "Untagged";
        stats = null;

        if (nameText != null)
        {
            nameText.text = "";
        }

        if (infoText != null)
        {
            infoText.text = "Select a Class.";
        }

    }

    public PlayerCharacterStats GetStats()
    {
        return stats;
    }
}
