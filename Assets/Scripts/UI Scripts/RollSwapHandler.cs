using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RollSwapHandler : MonoBehaviour, IPointerDownHandler
{
    public bool isLoot;

    public GameObject player;
    private RerollHandler rerollHandler;
    public bool isRoll;
    public bool isSwap;
    public int currentReroll;
    public int currentSwap;

    void start()
    {
        rerollHandler = player.GetComponentInChildren<RerollHandler>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isRoll)
        {
            if (!isLoot)
            {
                rerollHandler.usedReroll();
                GameObject.FindObjectOfType<LevelUpManager>().reroll();

            }else
            {
                rerollHandler.usedReroll();
                GameObject.FindObjectOfType<LootBoxManager>().reroll();
            }

        }

        if (isSwap)
        {
            if (!isLoot)
            {
                rerollHandler.usedSwap();
                GameObject.FindObjectOfType<LevelUpManager>().swap();
            }else
            {
                rerollHandler.usedSwap();
                GameObject.FindObjectOfType<LootBoxManager>().swap();
            }
        }
    }

    void Update()
    {
        rerollHandler = player.GetComponentInChildren<RerollHandler>();
        currentReroll = player.GetComponentInChildren<RerollHandler>().currentReroll;
        currentSwap = player.GetComponentInChildren<RerollHandler>().currentSwap;

        if (isRoll)
        {
            if (currentReroll <= 0)
            {
                gameObject.SetActive(false);
            }
        }


        if (!isRoll)
        {
            if (currentSwap <= 0)
            {
                gameObject.SetActive(false);
            }
        }

    }


    public void setActive()
    {
        currentSwap = 1;
        gameObject.SetActive(true);
    }

}
