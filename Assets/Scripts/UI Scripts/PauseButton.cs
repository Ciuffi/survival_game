using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour
{
    EventTrigger eventTrigger;
    GameManager gameManager;

    public bool isResume;

    void Start()
    {
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

            if (!isResume)
            {
                // Set the method to be called when the event triggers
                entry.callback.AddListener((eventData) => { gameManager.ShowPauseScreen(); });
            } else
            {
                // Set the method to be called when the event triggers
                entry.callback.AddListener((eventData) => { gameManager.HidePauseScreen(); });
            }
           

            // Add the trigger entry to the event trigger
            eventTrigger.triggers.Add(entry);
        }
        else
        {
            Debug.LogError("GameManager or EventTrigger not found!");
        }
    }
}