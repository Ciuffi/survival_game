using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimelineIcon : MonoBehaviour, IPointerDownHandler
{
    public GameObject attack;
    public AttackHandler handler;

    public void OnPointerDown(PointerEventData eventData)
    {
        handler.RemoveWeapon(attack);
    }
}
