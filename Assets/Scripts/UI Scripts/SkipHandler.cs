using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkipHandler : MonoBehaviour, IPointerDownHandler
{
    public bool isLoot;
    public GameObject player;
    private RerollHandler rerollHandler;

    void start()
    {
        rerollHandler = player.GetComponentInChildren<RerollHandler>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isLoot)
        {
            GameObject.FindObjectOfType<LevelUpManager>().SignalItemChosen();
            rerollHandler.gainChances();
        }
        else
        {
            GameObject.FindObjectOfType<LootBoxManager>().SignalItemChosen();
            rerollHandler.gainChances();

        }
    }
}
