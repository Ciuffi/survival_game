using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color defaultColor;
    public Color midHPColor;
    public Color lowHPColor;
    public float midHPLimitPercent = 0.5f;
    public float lowHPLimitPercent = 0.25f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        slider = GetComponent<Slider>();
        slider.fillRect.GetComponentInChildren<Image>().color = defaultColor; // Set the default color of the HP bar
    }

    private void Update()
    {
        float currentHpPercent = player.GetComponent<StatsHandler>().GetPlayerHpPercent(); // Replace GetPlayerHpPercent() with your own method for retrieving the player's current HP percentage

        if (currentHpPercent <= lowHPLimitPercent)
        {
            slider.fillRect.GetComponentInChildren<Image>().color = lowHPColor; // Set the HP bar color to the low HP color
        }
        else if (currentHpPercent <= midHPLimitPercent)
        {
            float t = Mathf.InverseLerp(lowHPLimitPercent, midHPLimitPercent, currentHpPercent); // Calculate the transition value between the low HP color and the mid HP color
            slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowHPColor, midHPColor, t); // Set the HP bar color to the interpolated color
        }
        else
        {
            slider.fillRect.GetComponentInChildren<Image>().color = defaultColor; // Set the HP bar color back to the default color
        }
    }
}
