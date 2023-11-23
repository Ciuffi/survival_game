using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ComboTracker : MonoBehaviour
{

    public int comboCount;

    public GameObject spawner;
    public List<int> guiltThresholds;
    public List<Color> guiltColors;
    private List<bool> thresholdsReached;

    public GameObject bannerPrefab;
    private Canvas parentCanvas;

    public int yOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the thresholdsReached list with all values set to false
        thresholdsReached = new List<bool>();
        for (int i = 0; i < guiltThresholds.Count; i++)
        {
            thresholdsReached.Add(false);
        }
        parentCanvas = GetComponentInParent<Canvas>();
        comboCount = 0;
        ColorChange(0);
    }

    public void ColorChange(int currentGuilt)
    {
        if (currentGuilt == 0)
        {
            GetComponentInChildren<TMP_Text>().color = guiltColors[0];
        }
        else if (currentGuilt <= guiltColors.Count) // Ensure we don't exceed the list's boundaries
        {
            GetComponentInChildren<TMP_Text>().color = guiltColors[currentGuilt - 1];
        }
    }

    void Update()
    {
        int previousThreshold = GetPreviousThreshold();
        int nextThreshold = GetNextThreshold();

        bool isFinalThresholdReached = comboCount >= guiltThresholds[guiltThresholds.Count - 1];

        string comboText;
        if (isFinalThresholdReached)
        {
            comboText = "SLAY BOSS"; 
        }
        else
        {
            // The difference in kills since the last threshold
            int killsSinceLastThreshold = comboCount - previousThreshold;

            // The difference between the next and last threshold
            int differenceBetweenThresholds = nextThreshold - previousThreshold;

            comboText = killsSinceLastThreshold + "/" + differenceBetweenThresholds;
        }

        GetComponentInChildren<TMP_Text>().text = comboText;


        for (int i = 0; i < guiltThresholds.Count; i++)
        {
            if (comboCount >= guiltThresholds[i] && !thresholdsReached[i])
            {
                if (i + 1 < guiltThresholds.Count && comboCount < guiltThresholds[i + 1])
                {
                    spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
                    thresholdsReached[i] = true;

                    // Call ColorChange with the new guilt level
                    ColorChange(i + 1);
                        GameObject bannerInstance = Instantiate(bannerPrefab, parentCanvas.transform);
                        bannerInstance.GetComponent<BannerController>().DisplayBannerWithText("! SWARM INCOMING !");

                    // Get the RectTransform
                    RectTransform rectTransform = bannerInstance.GetComponent<RectTransform>();

                    // Move it upwards by a certain amount (e.g., 100 units)
                    rectTransform.anchoredPosition += new Vector2(0, yOffset);
                }
                else if (i + 1 >= guiltThresholds.Count)
                {
                    spawner.GetComponent<BasicSpawner>().IncreaseGuilt();
                    thresholdsReached[i] = true;

                    // Call ColorChange with the new guilt level
                    ColorChange(i + 1);
                }
            }
        }
    }

    int GetNextThreshold()
    {
        foreach (int threshold in guiltThresholds)
        {
            if (comboCount < threshold)
            {
                return threshold;
            }
        }
        return guiltThresholds[guiltThresholds.Count - 1];
    }

    int GetPreviousThreshold()
    {
        int lastThreshold = 0;
        foreach (int threshold in guiltThresholds)
        {
            if (comboCount >= threshold)
            {
                lastThreshold = threshold;
            }
            else
            {
                break;  // Exit once we reach a threshold greater than comboCount
            }
        }
        return lastThreshold;
    }

    public void IncreaseCount(int amount)
    {
        comboCount += amount;
    }

    public void ResetCount()
    {
        comboCount = 0;

        // Reset all threshold reached flags
        for (int i = 0; i < thresholdsReached.Count; i++)
        {
            thresholdsReached[i] = false;
        }
    }

}
