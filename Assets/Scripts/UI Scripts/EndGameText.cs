using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class EndGameText : MonoBehaviour
{
    public TextMeshProUGUI totalDamage;
    public TextMeshProUGUI wpnDamage;
    public TextMeshProUGUI timeSurvivedText, goldGainedText, enemiesKilledText;
    WeaponStats weaponStats;
    List<int> finalDmg;

    public float countUpSpeed = 0f;
    public float displaySpeed = 0.5f;
    public float defaultScaleDuration = 0.22f;

    public float scaleDuration = 0.5f;
    public float scaleAmount = 0.15f;

    private Color totalDmgColor, wpnDmgColor, runStatsColor, timeSurvivedColor, goldGainedColor, enemiesKilledColor;
    private bool skipToEnd = false; // Added to allow skip

    public TextMeshProUGUI outcomeText;
    public List<string> winMessages;
    public List<string> loseMessages;


    private void Update() // Listen for a screen tap
    {
        if (Input.GetMouseButtonDown(0))
        {
            skipToEnd = true;
        }
    }

    public void Start()
    {
        // Load the information from PlayerPrefs
        string timeSurvived = PlayerPrefs.GetString("timeSurvived", "");
        int enemiesKilled = PlayerPrefs.GetInt("enemiesKilled", 0);
        int goldGained = PlayerPrefs.GetInt("goldGained", 0);

        string weaponStatsJson = PlayerPrefs.GetString("weaponStats", "");
        weaponStats = JsonUtility.FromJson<WeaponStats>(weaponStatsJson);

        finalDmg = new List<int>();
        foreach (float f in weaponStats.weaponDamage)
        {
            finalDmg.Add(Mathf.RoundToInt(f));
        }

        totalDmgColor = totalDamage.color;
        wpnDmgColor = wpnDamage.color;
        timeSurvivedColor = timeSurvivedText.color;
        goldGainedColor = goldGainedText.color;
        enemiesKilledColor = enemiesKilledText.color;

        HideTextElements();
        StartCoroutine(DisplayOutcomeMessage());
        StartCoroutine(StartCounting(finalDmg, timeSurvived, goldGained, enemiesKilled, weaponStats));
    }

    private IEnumerator DisplayOutcomeMessage()
    {
        bool isPlayerVictory = PlayerPrefs.GetInt("isPlayerVictory", 0) == 1; // Retrieves and converts the int to a bool
        string message = isPlayerVictory ? winMessages[Random.Range(0, winMessages.Count)] : loseMessages[Random.Range(0, loseMessages.Count)];

        outcomeText.text = message;
        outcomeText.color = new Color(outcomeText.color.r, outcomeText.color.g, outcomeText.color.b, 0); // Set alpha to 0

        // Fade in
        yield return outcomeText.DOFade(1f, 3f).WaitForCompletion(); // Fade in over 1 second
    }

    private void HideTextElements()
    {
        Color hiddenColor = new Color (0,0,0,0);
        totalDamage.color = hiddenColor;
        wpnDamage.color = hiddenColor;
        timeSurvivedText.color = hiddenColor;
        goldGainedText.color = hiddenColor;
        enemiesKilledText.color = hiddenColor;
    }

    private IEnumerator StartCounting(List<int> finalDmg, string timeSurvived, int goldGained, int enemiesKilled, WeaponStats weaponStats)
    {
        yield return StartCoroutine(UpdateTotalDamage(finalDmg));
        yield return new WaitForSeconds(displaySpeed);
        yield return StartCoroutine(UpdateWeaponDamage(finalDmg, weaponStats));
        if (!skipToEnd)
        {
            yield return new WaitForSeconds(displaySpeed);
        }
        yield return StartCoroutine(UpdateTimeSurvived(timeSurvived));
        if (!skipToEnd)
        {
            yield return new WaitForSeconds(displaySpeed);
        }
        yield return StartCoroutine(UpdateGoldGained(goldGained));
        if (!skipToEnd)
        {
            yield return new WaitForSeconds(displaySpeed);
        }
        yield return StartCoroutine(UpdateEnemiesKilled(enemiesKilled));
    }



    private IEnumerator UpdateTotalDamage(List<int> finalDmg)
    {

        int totalDmg = 0;
        int currentTotalDmg = 0;

        foreach (int damage in finalDmg)
        {
            totalDmg += damage;
        }

        float targetTime = 0.75f; // Set your target time here

        // Apply multiplier based on thresholds
        if (totalDmg > 499999)
        {
            targetTime *= 1.5f; // Adjust this value as needed
        }
        else if (totalDmg > 99999)
        {
            targetTime *= 1.3f; // Adjust this value as needed
        }
        else if (totalDmg > 9999)
        {
            targetTime *= 1.1f; // Adjust this value as needed
        }

        int incrementValue = (int)(totalDmg / (targetTime / countUpSpeed));

        ScaleTextEffect(totalDamage, scaleAmount * 1.1f, defaultScaleDuration);
        totalDamage.color = totalDmgColor;

        while (currentTotalDmg < totalDmg)
        {
            if (skipToEnd)
            {
                currentTotalDmg = totalDmg;
                totalDamage.text = "FINAL SCORE:\n" + currentTotalDmg;
                break;
            }

            currentTotalDmg += incrementValue;
            if (currentTotalDmg > totalDmg)
            {
                currentTotalDmg = totalDmg;
            }
            totalDamage.text = "FINAL SCORE:\n" + currentTotalDmg;
            yield return new WaitForSeconds(countUpSpeed);
        }
    }

    private IEnumerator UpdateWeaponDamage(List<int> finalDmg, WeaponStats weaponStats)
    {
        for (int i = 0; i < finalDmg.Count; i++)
        {
            yield return StartCoroutine(UpdateSingleWeaponDamage(i, finalDmg[i], weaponStats.weaponNames[i]));
            if (i < finalDmg.Count - 1)
            {
                yield return new WaitForSeconds(displaySpeed);
            }
        }
    }

    private IEnumerator UpdateSingleWeaponDamage(int weaponIndex, int finalDmg, string weaponName)
    {
        if (finalDmg == 0)
        {
            ScaleTextEffect(wpnDamage, scaleAmount, defaultScaleDuration);
            string currentLine = weaponName + " - " + finalDmg + "\n";
            wpnDamage.text = GetWeaponDamageText(weaponIndex, currentLine);
            wpnDamage.color = wpnDmgColor;
        }
        else
        {
            //float countDuration = (finalDmg / incrementValueFast) * 0.02f;
            ScaleTextEffect(wpnDamage, scaleAmount, defaultScaleDuration);

            wpnDamage.color = wpnDmgColor;

            int currentDmg = 0;
            float targetTime = 0.3f; // Set your target time here

            // Apply multiplier based on thresholds
            if (finalDmg > 499999)
            {
                targetTime *= 1.5f; // Adjust this value as needed
            }
            else if (finalDmg > 99999)
            {
                targetTime *= 1.3f; // Adjust this value as needed
            }
            else if (finalDmg > 9999)
            {
                targetTime *= 1.1f; // Adjust this value as needed
            }

            int incrementValue = (int)(finalDmg / (targetTime / countUpSpeed));

            ScaleTextEffect(wpnDamage, scaleAmount * 1.1f, defaultScaleDuration);
            wpnDamage.color = wpnDmgColor;

            while (currentDmg < finalDmg)
            {
                if (skipToEnd)
                {
                    currentDmg = finalDmg;
                    string finalLine = weaponName + " - " + currentDmg + "\n";
                    wpnDamage.text = GetWeaponDamageText(weaponIndex, finalLine);
                    break;
                }

                currentDmg += incrementValue;
                if (currentDmg > finalDmg)
                {
                    currentDmg = finalDmg;
                }
                string currentLine = weaponName + " - " + currentDmg + "\n";
                wpnDamage.text = GetWeaponDamageText(weaponIndex, currentLine);
                yield return new WaitForSeconds(countUpSpeed);
            }
        }
    }

    private string GetWeaponDamageText(int weaponIndex, string currentLine)
    {
        string weaponInfo = "";
        for (int i = 0; i < weaponIndex; i++)
        {
            weaponInfo += weaponStats.weaponNames[i] + " - " + finalDmg[i] + "\n";
        }
        weaponInfo += currentLine;
        return weaponInfo;
    }

    private IEnumerator UpdateTimeSurvived(string timeSurvived)
    {
        ScaleTextEffect(timeSurvivedText, scaleAmount, defaultScaleDuration);

        timeSurvivedText.text = "Time Survived: \n" + timeSurvived;
        timeSurvivedText.color = timeSurvivedColor;

        yield return new WaitForSeconds(countUpSpeed);
    }

    private IEnumerator UpdateGoldGained(int goldGained)
    {
        if (goldGained == 0)
        {
            ScaleTextEffect(goldGainedText, scaleAmount, defaultScaleDuration);
            goldGainedText.text = "Blood Money: \n" + goldGained;
            goldGainedText.color = goldGainedColor;
        }
        else
        {
            //float countDuration = (goldGained / incrementValueSlow) * 0.02f;
            ScaleTextEffect(goldGainedText, scaleAmount, defaultScaleDuration);

            int currentGold = 0;
            float targetTime = 0.25f; // Set your target time here
            int incrementValue = (int)(goldGained / (targetTime / countUpSpeed));

            while (currentGold < goldGained)
            {
                if (skipToEnd)
                {
                    currentGold = goldGained;
                    goldGainedText.text = "Blood Money: \n" + currentGold;
                    goldGainedText.color = goldGainedColor;
                    break;
                }

                currentGold += incrementValue;
                if (currentGold > goldGained)
                {
                    currentGold = goldGained;
                }
                goldGainedText.text = "Blood Money: \n" + currentGold;
                goldGainedText.color = goldGainedColor;
                yield return new WaitForSeconds(countUpSpeed);
            }    
        }
    }

    private IEnumerator UpdateEnemiesKilled(int enemiesKilled)
    { 
        if (enemiesKilled == 0)
        {
            ScaleTextEffect(enemiesKilledText, scaleAmount, defaultScaleDuration);
            enemiesKilledText.text = "Murders: \n" + enemiesKilled;
            enemiesKilledText.color = enemiesKilledColor;
        }
        else
        {
            //float countDuration = (enemiesKilled / incrementValueSlow) * 0.02f;
            ScaleTextEffect(enemiesKilledText, scaleAmount, defaultScaleDuration);

            int currentKills = 0;
            float targetTime = 0.25f; // Set your target time here
            int incrementValue = (int)(enemiesKilled / (targetTime / countUpSpeed));

            while (currentKills < enemiesKilled)
            {
                if (skipToEnd)
                {
                    currentKills = enemiesKilled;
                    enemiesKilledText.text = "Murders: \n" + currentKills;
                    enemiesKilledText.color = enemiesKilledColor;
                    break;
                }

                currentKills += incrementValue;
                if (currentKills > enemiesKilled)
                {
                    currentKills = enemiesKilled;
                }
                enemiesKilledText.text = "Murders: \n" + currentKills;
                enemiesKilledText.color = enemiesKilledColor;
                yield return new WaitForSeconds(countUpSpeed);
            }
        }
    }


    private void ScaleTextEffect(TextMeshProUGUI textElement, float scaleFactor, float duration)
    {
        if (duration < defaultScaleDuration)
        {
            duration = defaultScaleDuration;
        }

        Vector3 originalScale = textElement.transform.localScale;

        textElement.transform.DOScale(scaleFactor, duration / 2f)
            .SetEase(Ease.InElastic)
            .OnComplete(() =>
            {
                StartCoroutine(WaitAndScaleDown(textElement, originalScale, duration));
            });
    }

    private IEnumerator WaitAndScaleDown(TextMeshProUGUI textElement, Vector3 originalScale, float duration)
    {
        yield return new WaitForSeconds(duration / 2f);
        textElement.transform.DOScale(originalScale, duration / 2f).SetEase(Ease.OutElastic);
    }
}
