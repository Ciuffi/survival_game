using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ComboTracker : MonoBehaviour
{

    public int comboCount;


    // Start is called before the first frame update
    void Start()
    {
        comboCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string comboText = "Kills: " + comboCount;
        GetComponentInChildren<TMP_Text>().text = comboText;
    }

    private void FixedUpdate()
    {
        
    }

    public void IncreaseCount(int amount)
    {
        comboCount += amount;

    }

    public void ResetCount()
    {
        comboCount = 0;
    }

}
