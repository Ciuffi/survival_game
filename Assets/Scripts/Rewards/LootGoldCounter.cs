using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootGoldCounter : MonoBehaviour
{
    public int increment = 1;
    private int goldCount = 0;
    public int finalGold;
    public bool finishedCounting = false;

    private TMP_Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponentInChildren<TMP_Text>();
    }
    void ResetStats()
    {
        finalGold = 0;
        goldCount = 0;
    }

    private void Update()
    {
        if (!finishedCounting && goldCount < finalGold)
        {
            goldCount += (int)increment;
            textComponent.text = goldCount.ToString();
        }
        else if (goldCount >= finalGold)
        {
            finishedCounting = true;
            ResetStats();
        }
    }
}
