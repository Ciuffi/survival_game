using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkipHandler : MonoBehaviour, IPointerDownHandler
{
    public bool isLoot;
    public GameObject player;
    public GameObject rerollHandler;

    void start()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isLoot)
        {
            GameObject.FindObjectOfType<LevelUpManager>().SignalItemChosen();
            rerollHandler.GetComponent<RerollHandler>().gainChances();
        }
        else
        {
            GameObject.FindObjectOfType<LootBoxManager>().SignalItemChosen();
            rerollHandler.GetComponent<RerollHandler>().gainChances();

        }
    }
}
