using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RollSwapHandler : MonoBehaviour, IPointerDownHandler
{
    public bool isLoot;

    public GameObject LevelUp;
    public bool isRoll;
    public bool isSwap;
    public int currentReroll;
    public int currentSwap;

    void start()
    {
        currentReroll = LevelUp.GetComponent<RerollHandler>().currentReroll;
        currentSwap = LevelUp.GetComponent<RerollHandler>().currentSwap;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isRoll)
        {
            if (!isLoot)
            {
                LevelUp.GetComponent<RerollHandler>().usedReroll();
                GameObject.FindObjectOfType<LevelUpManager>().reroll();

            }else
            {
                LevelUp.GetComponent<RerollHandler>().usedReroll();
                GameObject.FindObjectOfType<LootBoxManager>().reroll();
            }

        }

        if (isSwap)
        {
            if (!isLoot)
            {
                LevelUp.GetComponent<RerollHandler>().usedSwap();
                GameObject.FindObjectOfType<LevelUpManager>().swap();
            }else
            {
                LevelUp.GetComponent<RerollHandler>().usedSwap();
                GameObject.FindObjectOfType<LootBoxManager>().swap();
            }
        }
    }

    void Update()
    {
        currentReroll = LevelUp.GetComponent<RerollHandler>().currentReroll;
        currentSwap = LevelUp.GetComponent<RerollHandler>().currentSwap;

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
        LevelUp.GetComponent<RerollHandler>().resetSwap();
        gameObject.SetActive(true);
    }

}
