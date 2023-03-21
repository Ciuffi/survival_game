using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseScreenButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject confirmationPopupPrefab;
    public UnityEvent onClickEvent;
    public bool isRestart;
    public string popupMessage;

    private void Start()
    {
        if (isRestart)
        {
            popupMessage = "Restart Run?";
        } else
        {
            popupMessage = "Exit to Menu?";
        }
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
}
