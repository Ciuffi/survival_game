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

    private float uiDelay = 1f;
    public bool startDelay;
    public bool delayFinished;
    private float timer; // make timer a class member variable
    float pressTimer;

    void Start()
    {
        startDelay = true;
        delayFinished = false;
        currentReroll = LevelUp.GetComponent<RerollHandler>().currentReroll;
        currentSwap = LevelUp.GetComponent<RerollHandler>().currentSwap;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (delayFinished)
        {
            startDelay = true;
            delayFinished = false;

            if (isRoll)
            {
                if (!isLoot)
                {
                    LevelUp.GetComponent<RerollHandler>().usedReroll();
                    GameObject.FindObjectOfType<LevelUpManager>().reroll();

                }
                else
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
                }
                else
                {
                    LevelUp.GetComponent<RerollHandler>().usedSwap();
                    GameObject.FindObjectOfType<LootBoxManager>().swap();
                }
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
        else
        {
            if (currentSwap <= 0)
            {
                gameObject.SetActive(false);
            }
        }

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


    public void setActive()
    {
        //LevelUp.GetComponent<RerollHandler>().resetSwap();
        timer = 0f; // reset timer when panel is set active
        startDelay = true;
        delayFinished = false;
        gameObject.SetActive(true);
    }

}
