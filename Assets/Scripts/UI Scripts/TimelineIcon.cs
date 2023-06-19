using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimelineIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Attack AssociatedAttack { get; set; }
    public AttackHandler attackHandler;

    private Vector3 originalPosition;
    private static TimelineIcon draggedIcon = null;
    private GameObject ghostIcon = null;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private TimelineUI timeline;
    List<Color> rarityColors;

    void Start()
    {
        attackHandler = GameObject.FindObjectOfType<AttackHandler>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
        timeline = FindObjectOfType<TimelineUI>();
        rarityColors = timeline.rarityColors;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (attackHandler.attacks.Count <= 1)
            return;

        originalPosition = transform.localPosition;
        draggedIcon = GetComponent<TimelineIcon>();

        ghostIcon = Instantiate(gameObject);
        ghostIcon.transform.SetParent(canvas.transform, false); // set the parent to the Canvas
        ghostIcon.transform.localPosition = transform.localPosition;

        Image ghostImageComponent = ghostIcon.GetComponent<Image>();
        if (ghostImageComponent != null)
        {
            ghostImageComponent.raycastTarget = false;
        }

        Image[] ghostImages = ghostIcon.GetComponentsInChildren<Image>();
        foreach (Image img in ghostImages)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
        }

        TimelineIcon ghostDrag = ghostIcon.GetComponent<TimelineIcon>();
        if (ghostDrag != null)
            Destroy(ghostDrag);

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ghostIcon != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint);
            ghostIcon.transform.localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Add this null check here
        if (AssociatedAttack == null)
        {
            Debug.Log(name + ": AssociatedAttack is null");
            return;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (ghostIcon != null)
            Destroy(ghostIcon);
        ghostIcon = null;

        TimelineIcon targetIcon = null;

        Debug.Log("Hovered Objects Count: " + eventData.hovered.Count);
        foreach (var result in eventData.hovered)
        {
            Debug.Log("Hovered: " + result.name);
        }

        foreach (var result in eventData.hovered)
        {
            targetIcon = result.GetComponent<TimelineIcon>();
            if (targetIcon == null)
            {
                // Continue search up through the parents
                var parent = result.transform.parent;
                while (parent != null && targetIcon == null)
                {
                    targetIcon = parent.GetComponent<TimelineIcon>();
                    parent = parent.parent;
                }
            }
            if (targetIcon != null)
                break;
        }

        if (targetIcon != null && targetIcon != draggedIcon)
        {

            Debug.Log("Swapping attacks...");
            int draggedIndex = draggedIcon.transform.parent.GetSiblingIndex();
            int targetIndex = targetIcon.transform.parent.GetSiblingIndex();

            // Swap attacks in the attacks list
            Attack temp = attackHandler.attacks[draggedIndex];
            attackHandler.attacks[draggedIndex] = attackHandler.attacks[targetIndex];
            attackHandler.attacks[targetIndex] = temp;

            // Update UI elements directly
            UpdateUIElements(draggedIcon, attackHandler.attacks[draggedIndex]);
            UpdateUIElements(targetIcon, attackHandler.attacks[targetIndex]);

            Canvas.ForceUpdateCanvases();

            //attackHandler.attacks = GetAttackOrder();

            Debug.Log("Swapping done...");

        }
        else
        {
            Debug.Log("Reset position...");
            transform.localPosition = originalPosition;
        }

        draggedIcon = null;
    }

    private void UpdateUIElements(TimelineIcon icon, Attack attack)
    {
        // Get the sibling TextMeshProUGUI component
        var textComponent = icon.transform.parent
            .GetComponentsInChildren<TextMeshProUGUI>()
            .FirstOrDefault(t => t.gameObject != icon.gameObject);

        if (textComponent != null)
        {
            textComponent.text = attack.weaponSetType.ToString();
        }

        // Update background color based on rarity
        var bgImage = icon.transform.parent
            .GetComponentsInChildren<Image>()
            .FirstOrDefault(i => i.gameObject != icon.gameObject && i.name == "BG");

        if (bgImage != null)
        {
            bgImage.color = rarityColors[(int)attack.stats.GetRarity()];
        }

        icon.GetComponent<Image>().sprite = attack.GetUpgradeIcon();
    }


}