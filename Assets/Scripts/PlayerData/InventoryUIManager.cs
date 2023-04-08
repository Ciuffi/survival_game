using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public GameObject contentPanel;
    public GameObject inventoryItemPrefab;

    public string selectedWeapon;
    public int selectedWeaponRarity;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        PopulateInventoryUI();
    }

    public void SetSelectedWeapon(string weaponName, int weaponRarity)
    {
        selectedWeapon = weaponName;
        selectedWeaponRarity = weaponRarity;
    }

    public void DeselectAllItems()
    {
        InventoryItem[] items = contentPanel.GetComponentsInChildren<InventoryItem>();
        foreach (InventoryItem item in items)
        {
            if (item.isSelected)
            {
                item.DeselectItem();
            }
        }
    }

    private void PopulateInventoryUI()
    {
        for (int i = 0; i < playerInventory.weaponInventory.Count; i++)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, contentPanel.transform);
            Image weaponImage = newItem.transform.Find("WeaponImage").GetComponent<Image>();
            Image outlineImage = newItem.transform.Find("WeaponOutline").GetComponent<Image>();
            TextMeshProUGUI weaponDurability = newItem.transform.Find("Durability").GetComponent<TextMeshProUGUI>();

            string weaponName = playerInventory.weaponInventory[i].name;

            AttackBuilder attackBuilder = AttackLibrary.GetAttackBuilder(weaponName);
            weaponImage.sprite = attackBuilder.GetThrownSprite();
            outlineImage.sprite = attackBuilder.GetThrownSprite();

            if (playerInventory.weaponInventory[i].isPermanent) //hide durability if permanent item
            {
                weaponDurability.text = "";
            }
            else
            {
                weaponDurability.text = playerInventory.weaponInventory[i].durability.ToString(); //show durability count
            }

            InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
            inventoryItem.weaponName = weaponName; //set name of item
            inventoryItem.rarity = playerInventory.weaponInventory[i].rarity; //set rarity of item
            Button button = newItem.GetComponent<Button>();
            button.onClick.AddListener(inventoryItem.SelectItem);

            // Optionally, set the scale of the sprite if needed
            // weaponImage.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }

    public string GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public int GetSelectedWeaponRarity()
    {
        return selectedWeaponRarity;
    }

}