using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GachaOverlayManager : MonoBehaviour
{
    public GameObject outline;
    public GameObject weapon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;
    public float displayDuration = 2f; // The time to wait before allowing the player to close the overlay
    private bool canClose = false;

    public GameObject weaponPrefab;
    public List<Color> outlineColors;

    private void Start()
    {
        outlineColors = weaponPrefab.GetComponent<InventoryItem>().rarityColors;
        //gameObject.SetActive(false);
    }

    public void DisplayWeapon(Weapon weapon)
    {
        //gameObject.SetActive(true);

        // Update the UI elements with the weapon's details

        AttackBuilder attackBuilder = AttackLibrary.GetAttackBuilder(weapon.name);
        this.weapon.GetComponent<Image>().sprite = attackBuilder.GetThrownSprite();

        outline.GetComponent<Image>().sprite = attackBuilder.GetThrownSprite();
        outline.GetComponent<Image>().color = outlineColors[weapon.rarity];

        nameText.text = weapon.name;

        string rarityString = "";

        if (weapon.rarity == 0)
        {
            rarityString = "Common";
        } else if (weapon.rarity == 2)
        {
            rarityString = "Rare";

        }
        else if (weapon.rarity == 4)
        {
            rarityString = "Epic";

        }
        else if (weapon.rarity == 6)
        {
            rarityString = "Legendary";
        }

        rarityText.text = rarityString;
        rarityText.color = outlineColors[weapon.rarity];

        // Set initial scale to 0
        outline.transform.localScale = Vector3.zero;
        this.weapon.transform.localScale = Vector3.zero;
        nameText.transform.localScale = Vector3.zero;
        rarityText.transform.localScale = Vector3.zero;

        // Scale up the child elements one at a time
        Sequence sequence = DOTween.Sequence();
        sequence.Append(outline.transform.DOScale(1, 0.5f).SetEase(Ease.OutElastic));
        sequence.Append(this.weapon.transform.DOScale(1, 0.4f).SetEase(Ease.OutElastic));
        sequence.Append(nameText.transform.DOScale(1, 0.3f).SetEase(Ease.OutElastic));
        sequence.Append(rarityText.transform.DOScale(1, 0.2f).SetEase(Ease.OutElastic));

        StartCoroutine(StartCloseTimer());
    }

    private IEnumerator StartCloseTimer()
    {
        yield return new WaitForSeconds(displayDuration);
        canClose = true;
    }

    private void Update()
    {
        if (canClose && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            canClose = false;
        }
    }
}