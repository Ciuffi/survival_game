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
    public int incrementValueFast = 9;
    public int incrementValueSlow = 3;
    public float displaySpeed = 0.5f;
    public float defaultScaleDuration = 0.22f;

    public float scaleDuration = 0.5f;
    public float scaleAmount = 0.15f;

    private Color totalDmgColor, wpnDmgColor, runStatsColor, timeSurvivedColor, goldGainedColor, enemiesKilledColor;

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
        StartCoroutine(StartCounting(finalDmg, timeSurvived, goldGained, enemiesKilled, weaponStats));
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
        yield return new WaitForSeconds(displaySpeed *2);
        yield return StartCoroutine(UpdateWeaponDamage(finalDmg, weaponStats));
        yield return new WaitForSeconds(displaySpeed);
        yield return StartCoroutine(UpdateTimeSurvived(timeSurvived));
        yield return new WaitForSeconds(displaySpeed);
        yield return StartCoroutine(UpdateGoldGained(goldGained));
        yield return new WaitForSeconds(displaySpeed);
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

        // Remove the if (totalDmg == 0) condition
        ScaleTextEffect(totalDamage, scaleAmount * 1.1f, defaultScaleDuration);
        totalDamage.color = totalDmgColor;

        if (totalDmg > 499999)
        {
            incrementValueFast *= 15;
        }
        else if (totalDmg > 99999)
        {
            incrementValueFast *= 10;
        }
        else if (totalDmg > 49999)
        {
            incrementValueFast *= 5;
        }
        else if (totalDmg > 9999)
        {
            incrementValueFast *= 3;
        }
        else if (totalDmg > 999)
        {
            incrementValueFast *= 2;
        }
        else
        {
            incrementValueFast *= 20;
        }

        while (currentTotalDmg < totalDmg)
        {
            currentTotalDmg += incrementValueFast;
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

            if (finalDmg > 499999)
            {
                incrementValueFast *= 15;
            }
            else if (finalDmg > 99999)
            {
                incrementValueFast *= 10;
            }
            else if (finalDmg > 49999)
            {
                incrementValueFast *= 5;
            }
            else if (finalDmg > 9999)
            {
                incrementValueFast *= 3;
            }
            else if (finalDmg > 999)
            {
                incrementValueFast *= 2;
            }
            else
            {
                incrementValueFast *= 20;
            }

            int currentDmg = 0;
            while (currentDmg < finalDmg)
            {
                currentDmg += incrementValueFast;
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

            while (currentGold < goldGained)
            {
                currentGold += incrementValueSlow;
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

            if (enemiesKilled > 199)
            {
                incrementValueSlow *= 2;
            }
            else if (enemiesKilled > 999)
            {
                incrementValueSlow *= 5;
            }

            int currentKills = 0;

            while (currentKills < enemiesKilled)
            {
                currentKills += incrementValueSlow;
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
