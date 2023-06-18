using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RollSwapHandler : MonoBehaviour
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
    private Button button; // Reference to the Button component

    void Start()
    {
        startDelay = true;
        delayFinished = false;
        currentReroll = LevelUp.GetComponent<RerollHandler>().currentReroll;
        currentSwap = LevelUp.GetComponent<RerollHandler>().currentSwap;
        button = GetComponent<Button>(); // Get the reference to the Button component

        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
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
                button.interactable = false; // Disable button interaction
            }
        }
        else
        {
            if (currentSwap <= 0)
            {
                button.interactable = false; // Disable button interaction
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
        button.interactable = true; // Enable button interaction
        gameObject.SetActive(true);
    }

}
