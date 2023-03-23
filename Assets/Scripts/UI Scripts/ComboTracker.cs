using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ComboTracker : MonoBehaviour
{

    public int comboCount;
    private Color baseColor = new Color (0,0,0,255);

    public GameObject spawner;
    public int guiltThreshold1, guiltThreshold2, guiltThreshold3, guiltThreshold4, guiltThreshold5, guiltThreshold6;
    private bool threshold1Reached, threshold2Reached, threshold3Reached, threshold4Reached, threshold5Reached, threshold6Reached;

    public Color guiltColor1, guiltColor2, guiltColor3, guiltColor4, guiltColor5;

    // Start is called before the first frame update
    void Start()
    {
    
        GetComponentInChildren<TMP_Text>().color = baseColor;
        comboCount = 0;
    }

    public void ColorChange(int currentGuilt)
    {
        switch (currentGuilt)
        {
            case 0:
                GetComponentInChildren<TMP_Text>().color = baseColor;
                break;

            case 1:
                GetComponentInChildren<TMP_Text>().color = guiltColor1;
                break;

            case 2:
                GetComponentInChildren<TMP_Text>().color = guiltColor2;
                break;

            case 3:
                GetComponentInChildren<TMP_Text>().color = guiltColor3;
                break;

            case 4:
                GetComponentInChildren<TMP_Text>().color = guiltColor4;
                break;

            case 5:
                GetComponentInChildren<TMP_Text>().color = guiltColor5;
                break;
            case 6:
                GetComponentInChildren<TMP_Text>().color = guiltColor5;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        string comboText;

        if (comboCount < 10)
        {
            comboText = "00" + comboCount;
        }
        else if (comboCount >= 10 && comboCount < 100)
        {
            comboText = "0" + comboCount;
        }
        else
        {
            comboText = "" + comboCount;
        }

        GetComponentInChildren<TMP_Text>().text = comboText;


        if (comboCount >= guiltThreshold1 && comboCount < guiltThreshold2 && !threshold1Reached)
        {
            spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
            threshold1Reached = true;
        }
        else if (comboCount >= guiltThreshold2 && comboCount < guiltThreshold3 && !threshold2Reached)
        {
            spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
            threshold2Reached = true;
        }
        else if (comboCount >= guiltThreshold3 && comboCount < guiltThreshold4 && !threshold3Reached)
        {
            spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
            threshold3Reached = true;
        }
        else if (comboCount >= guiltThreshold4 && comboCount < guiltThreshold5 && !threshold4Reached)
        {
            spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
            threshold4Reached = true;
        }
        else if (comboCount >= guiltThreshold5 && !threshold5Reached)
        {
            spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
            threshold5Reached = true;
        }
        else if (comboCount >= guiltThreshold6 && !threshold6Reached)
        {
            spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
            threshold6Reached = true;
        }


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
