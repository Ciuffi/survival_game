using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EndgameUnlockPopup : MonoBehaviour
{
    public GameObject unlockImageSquare;
    public GameObject unlockImageRect;
    public GameObject unlockBackgroundSquare;
    public GameObject unlockBackgroundRect;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;
    public List<Color> backgroundColors;

    private List<Unlockable> unlockables;
    private int currentUnlockIndex = 0;
    private float lastTapTime;
    private float tapDelay = 0.75f; // Delay to prevent accidental taps

    public GameObject vfxExplosion;

    private void Awake()
    {
        CheckAndShowUnlocks();
    }

    private void CheckAndShowUnlocks()
    {
        var playerDataManager = PlayerDataManager.Instance;
        Debug.Log(playerDataManager.HasLeveledUp);

        if (playerDataManager.HasLeveledUp)
        {
            unlockables = new List<Unlockable>(playerDataManager.GetUnlocksAtLevel(playerDataManager.playerLevel));
            if (unlockables.Count > 0)
            {
                Debug.Log("has unlockables: " + unlockables.Count);
                ShowCurrentUnlock();
                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(false); // No level-up, keep popup hidden
        }
    }

    private void ShowCurrentUnlock()
    {
        if (currentUnlockIndex < unlockables.Count)
        {
            var unlock = unlockables[currentUnlockIndex];
            ConfigurePopup(unlock);
            currentUnlockIndex++;
        }
        else
        {
            ClosePopup();
        }
    }

    private void ConfigurePopup(Unlockable unlock)
    {
        if (unlock.Type == UnlockableType.PlayerStat || unlock.Type == UnlockableType.AttackStat || unlock.Type == UnlockableType.PlayerUpgrade)
        {
            if (unlock.Name.Length > 2)
            {
                unlock.Name = unlock.Name.Substring(0, unlock.Name.Length - 2);
            }
        }

        nameText.text = unlock.Name;

        string typeString = "";
        if (unlock.Type == UnlockableType.PlayerStat)
        {
            typeString = "Relic";
        }
        else if (unlock.Type == UnlockableType.AttackStat)
        {
            typeString = "Weapon Upgrade";
        }
        else if (unlock.Type == UnlockableType.PlayerCharacter)
        {
            typeString = "Character";
        }
        else if (unlock.Type == UnlockableType.PlayerUpgrade)
        {
            typeString = "Talent";
        }
        else //Weapon
        {
            typeString = "Weapon";
        }
      
        typeText.text = typeString;
        descriptionText.text = unlock.Description;

        if (unlock.Type == UnlockableType.Weapon)
        {
            unlockImageRect.SetActive(true);
            unlockImageSquare.SetActive(false);
            unlockImageRect.GetComponent<Image>().sprite = unlock.Image;

            unlockBackgroundRect.SetActive(true);
            unlockBackgroundSquare.SetActive(false);
            unlockBackgroundRect.GetComponent<Image>().sprite = unlock.Image; // Use the same image as the weapon
        }
        else
        {
            // For other unlock types
            unlockImageRect.SetActive(false);
            unlockImageSquare.SetActive(true);
            unlockImageSquare.GetComponent<Image>().sprite = unlock.Image;

            unlockBackgroundRect.SetActive(false);
            unlockBackgroundSquare.SetActive(true);
            unlockBackgroundSquare.GetComponent<Image>().sprite = unlock.Image;
            unlockBackgroundSquare.GetComponent<Image>().color = GetBackgroundColor(unlock);
        }

        // Reset the scales to 0 for animation
        unlockImageSquare.transform.localScale = Vector3.zero;
        unlockImageRect.transform.localScale = Vector3.zero;
        unlockBackgroundSquare.transform.localScale = Vector3.zero;
        unlockBackgroundRect.transform.localScale = Vector3.zero;
        nameText.transform.localScale = Vector3.zero;
        typeText.transform.localScale = Vector3.zero;
        descriptionText.transform.localScale = Vector3.zero;

        // Determine which image and background to use based on unlock type
        GameObject imageToAnimate = unlock.Type == UnlockableType.Weapon ? unlockImageRect : unlockImageSquare;
        GameObject backgroundToAnimate = unlock.Type == UnlockableType.Weapon ? unlockBackgroundRect : unlockBackgroundSquare;

        // Create a DOTween sequence
        Sequence sequence = DOTween.Sequence();

        // Animate the image and background
        sequence.Append(imageToAnimate.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        sequence.Join(backgroundToAnimate.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack));
        GameObject particle = Instantiate(vfxExplosion, nameText.transform.position, Quaternion.identity, transform);
        particle.transform.localScale *= 30f;

        // Animate the name and type text together
        sequence.Append(nameText.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack));
        sequence.Join(typeText.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack));

        // Animate the description text
        sequence.Append(descriptionText.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack));
    }

    public void ShowNextUnlock()
    {
        if (currentUnlockIndex < unlockables.Count)
        {
            ConfigurePopup(unlockables[currentUnlockIndex]);
            currentUnlockIndex++;
        }
        else
        {
            ClosePopup();
        }
    }

    public void OnTap()
    {
        if (Time.time - lastTapTime > tapDelay)
        {
            lastTapTime = Time.time;
            ShowNextUnlock();
        }
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
        currentUnlockIndex = 0; // Reset for the next use
        // Additional cleanup if needed
    }

    private Color GetBackgroundColor(Unlockable unlock)
    {
        int colorIndex = 0;

        if (unlock.Type == UnlockableType.PlayerStat)
        {
            colorIndex = 1;
        }
        else if (unlock.Type == UnlockableType.AttackStat)
        {
            colorIndex = 2;
        }
        else if (unlock.Type == UnlockableType.PlayerCharacter)
        {
            colorIndex = 3;
        }
        else if (unlock.Type == UnlockableType.PlayerUpgrade)
        {
            colorIndex = 4;
        }
        else //Weapon
        {
            colorIndex = 0;
        }

        return backgroundColors[colorIndex];
    }
}
