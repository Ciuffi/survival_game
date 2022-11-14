using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
       
    }

    private void FixedUpdate()
    {
        string comboText = "x" + comboCount;
        GetComponentInChildren<TMP_Text>().text = comboText;
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
