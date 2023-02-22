using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GoldTracker : MonoBehaviour
{
    public int goldCount;

    // Start is called before the first frame update
    void Start()
    {
        goldCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string comboText;

        if (goldCount < 10)
        {
            comboText = "00" + goldCount;
        } else if (goldCount >= 10 && goldCount < 100)
        {
            comboText = "0" + goldCount;
        } else
        {
            comboText = "" + goldCount;
        }

        GetComponentInChildren<TMP_Text>().text = comboText;
    }

    public void IncreaseCount(int amount)
    {
        goldCount += amount;

    }
}
