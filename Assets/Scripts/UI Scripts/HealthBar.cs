using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color fullHPColor;
    public Color midHPColor;
    public Color lowHPColor;
    private Color currentColor;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        slider = GetComponent<Slider>();
        slider.fillRect.GetComponentInChildren<Image>().color = fullHPColor; // Set the default color of the HP bar
        currentColor = fullHPColor;
    }

    public void FlashRed()
    {
        slider.fillRect.GetComponent<Image>().color = Color.red;
    }
    public void ResetColor()
    {
        slider.fillRect.GetComponent<Image>().color = currentColor;
    }

    public void UpdateHealthBar()
    {
        float fillAmount = player.GetComponent<StatsHandler>().health / player.GetComponent<StatsHandler>().maxHealth;
        Color targetColor;

        if (fillAmount >= 0.7f)
        {
            currentColor = fullHPColor;
            targetColor = fullHPColor;
        }
        else if (fillAmount >= 0.3f)
        {
            currentColor = midHPColor;
            targetColor = Color.Lerp(midHPColor, fullHPColor, (fillAmount - 0.3f) / 0.4f);
        }
        else
        {
            currentColor = lowHPColor;
            targetColor = Color.Lerp(lowHPColor, fullHPColor, fillAmount / 0.3f);
        }

        slider.fillRect.GetComponent<Image>().color = Color.Lerp(slider.fillRect.GetComponent<Image>().color, targetColor, Time.deltaTime);
    }


    private void Update()
    {
        UpdateHealthBar();
    }
}
