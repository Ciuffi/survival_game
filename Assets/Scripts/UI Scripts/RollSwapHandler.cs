using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RollSwapHandler : MonoBehaviour, IPointerDownHandler
{
    public bool isRoll;
    public bool isSwap;
    public int useChances;
    private int uses;

    void start()
    {
        uses = useChances;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isRoll)
        {
            GameObject.FindObjectOfType<LevelUpManager>().reroll();
            uses -= 1;
        }

        if (isSwap)
        {
            GameObject.FindObjectOfType<LevelUpManager>().swap();
            uses -= 1;
        }
    }

    void Update()
    {
        if (uses <= 0)
        {
            gameObject.SetActive(false);
        } 
    }


    public void resetChances()
    {
        gameObject.SetActive(true);
        uses = useChances;
    }

}
