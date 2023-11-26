using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootGoldCounter : MonoBehaviour
{
    public int increment = 1;
    public int goldCount = 0;
    public int finalGold;
    public bool finishedCounting = false;

    public float incrementTime = 1f; // time between each increment
    public int baseEffectGoldValue = 0;

    private TMP_Text textComponent;
    private bool resetCount = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponentInChildren<TMP_Text>();

    }
    public void ResetStats()
    {
        finalGold = 0;
        goldCount = 0;
        resetCount = true;
    }

    private void Update()
    {
        if (resetCount)
        {
            textComponent.text = goldCount.ToString();
            resetCount = false;
        }

        if (!finishedCounting && goldCount < finalGold)
        {
            goldCount += (int)increment;
            textComponent.text = goldCount.ToString();
        }
        else if (goldCount >= finalGold)
        {
            finishedCounting = true;
        }
    }

    public float GetExtraIncrementTime(int finalGold)
    {
        int goldDiff = finalGold - baseEffectGoldValue;
        return goldDiff / increment * incrementTime;
    }

}
