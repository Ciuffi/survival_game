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

    // Start is called before the first frame update
    void Start()
    {
        popupMessage = "Delete Weapon?";
        onClickEvent.AddListener(RemoveAssociatedAttack);
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
        popupController.Setup(onClickEvent, ClosePopup, popupMessage);
    }

    void ClosePopup(GameObject popup)
    {
        Destroy(popup);
    }

    void RemoveAssociatedAttack()
    {
        // Fetch the TimelineIcon from the parent GameObject
        TimelineIcon timelineIcon = GetComponentInParent<TimelineIcon>();
        AttackHandler attackCycle = FindObjectOfType<AttackHandler>();

        if (timelineIcon != null && timelineIcon.AssociatedAttack != null)
        {
            // Call the RemoveAttack method on the AttackHandler
            timelineIcon.attackHandler.RemoveAttack(timelineIcon.AssociatedAttack);
            attackCycle.ResetAttackCycle();
        }
    }
}
