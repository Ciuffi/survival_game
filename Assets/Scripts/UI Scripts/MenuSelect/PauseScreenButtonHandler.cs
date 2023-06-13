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

    EventTrigger eventTrigger;
    GameManager gameManager;

    private void Start()
    {
        if (isRestart)
        {
            popupMessage = "Restart Run?";
        } else
        {
            popupMessage = "Exit to Menu?";
        }

        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        // Get the EventTrigger component from the button
        eventTrigger = GetComponent<EventTrigger>();

        if (gameManager != null && eventTrigger != null)
        {
            // Create a new trigger entry
            EventTrigger.Entry entry = new EventTrigger.Entry();
            // Set the event type
            entry.eventID = EventTriggerType.PointerUp;
            // Set the method to be called when the event triggers
            entry.callback.AddListener((eventData) => { gameManager.MenuReset(); });

            // Add the trigger entry to the event trigger
            eventTrigger.triggers.Add(entry);
        }
        else
        {
            Debug.LogError("GameManager or EventTrigger not found!");
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
        popupController.Setup(onClickEvent, ClosePopup, popupMessage, gameManager);
    }

    void ClosePopup(GameObject popup)
    {
        Destroy(popup);
    }
}
