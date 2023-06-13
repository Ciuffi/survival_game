using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DeleteAttackButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject confirmationPopupPrefab;
    public UnityEvent onClickEvent;
    public bool isRestart;
    public string popupMessage;
    private GameManager gameManager; // Add this line

    // Start is called before the first frame update
    void Start()
    {
        popupMessage = "Delete Weapon?";
        onClickEvent.AddListener(RemoveAssociatedAttack);
        gameManager = FindObjectOfType<GameManager>(); // Initialize it here
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowConfirmationPopup();
    }

    void ShowConfirmationPopup()
    {
        GameObject popup = Instantiate(confirmationPopupPrefab);
        popup.transform.SetParent(transform.root, false);

        ConfirmationPopupController popupController = popup.GetComponent<ConfirmationPopupController>();
        popupController.Setup(onClickEvent, ClosePopup, popupMessage, gameManager);
    }

    void ClosePopup(GameObject popup)
    {
        Destroy(popup);
    }

    void RemoveAssociatedAttack()
    {
        // Fetch the TimelineIcon from the parent GameObject
        TimelineIcon timelineIcon = transform.parent.Find("WpnImage").gameObject.GetComponent<TimelineIcon>();
        AttackHandler attackCycle = FindObjectOfType<AttackHandler>();

        if (timelineIcon != null && timelineIcon.AssociatedAttack != null)
        {
            // Call the RemoveAttack method on the AttackHandler
            timelineIcon.attackHandler.RemoveAttack(timelineIcon.AssociatedAttack);
            attackCycle.ResetAttackCycle();
        }
    }
}
