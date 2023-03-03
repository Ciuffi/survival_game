using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkipHandler : MonoBehaviour, IPointerDownHandler
{
    public bool isLoot;
    public GameObject player;
    public GameObject rerollHandler;

    public float uiDelay;
    public bool startDelay;
    public bool delayFinished;
    private float timer; // make timer a class member variable

    void start()
    {
    }

    private void Update()
    {
        if (startDelay)
        {
            timer += Time.unscaledDeltaTime; // increment timer each frame
            if (timer >= uiDelay)
            {
                startDelay = false;
                delayFinished = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (delayFinished)
        {
            if (!isLoot)
            {
                GameObject.FindObjectOfType<LevelUpManager>().SignalItemChosen();
                rerollHandler.GetComponent<RerollHandler>().gainRerolls();
            }
            else
            {
                GameObject.FindObjectOfType<LootBoxManager>().SignalItemChosen();
                rerollHandler.GetComponent<RerollHandler>().gainRerolls();

            }
        }
    }


    public void setActive()
    {
        timer = 0f; // reset timer when panel is set active
        startDelay = true;
        delayFinished = false;
    }
}
