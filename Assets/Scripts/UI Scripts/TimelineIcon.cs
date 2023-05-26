using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TimelineIcon : MonoBehaviour
{
    public Attack AssociatedAttack { get; set; }
    public AttackHandler attackHandler; // add this line

    // Start is called before the first frame update
    void Start()
    {
        attackHandler = GameObject.FindObjectOfType<AttackHandler>(); // Find the AttackHandler in the scene
        Debug.Log("AttackHandler instance: " + attackHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
