using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Image tapAreaImage;
    private Color originalColor;
    public Color highlightColor;
    private InventoryUIManager inventoryUIManager;
    public string weaponName;
    public int rarity;
    public bool isSelected;

    private Image outline;
    public List<Color> rarityColors;

    private void Start()
    {
        inventoryUIManager = FindObjectOfType<InventoryUIManager>();
        tapAreaImage = transform.Find("TapArea").GetComponent<Image>();
        originalColor = tapAreaImage.color;
        isSelected = false;

        outline = transform.Find("WeaponOutline").GetComponent<Image>();
        outline.color = rarityColors[rarity];
    }

    public void SelectItem()
    {
        if (!isSelected)
        {
            inventoryUIManager.DeselectAllItems();
            isSelected = true;
            tapAreaImage.color = highlightColor;
            inventoryUIManager.SetSelectedWeapon(weaponName, rarity);
        }
    }

    public void DeselectItem()
    {
        isSelected = false;
        tapAreaImage.color = originalColor;
    }
}