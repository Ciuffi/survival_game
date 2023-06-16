using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddMoney : MonoBehaviour
{
    PlayerDataManager playerDataManager;
    public int goldToAdd = 500; // Amount of gold to add when the button is clicked

    public float cooldown = 12 * 60 * 60; // Cooldown in seconds
    public TextMeshProUGUI cooldownText; // Reference to a Text component to display the cooldown

    private float lastClickTime;
    private Button button;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        button = GetComponent<Button>();
        // If the last click time has not been set in PlayerPrefs, initialize it to allow immediate button press
        if (!PlayerPrefs.HasKey("LastClickTime"))
        {
            lastClickTime = Time.time - cooldown;
            PlayerPrefs.SetFloat("LastClickTime", lastClickTime);
        }
        else
        {
            lastClickTime = PlayerPrefs.GetFloat("LastClickTime", 0);
        }

        cooldownText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // Add an on-click listener to the button
        button.onClick.AddListener(AddGold);
        // Start the coroutine to update the cooldown text
        StartCoroutine(UpdateCooldownText());
    }

    private void AddGold()
    {
        // If the cooldown has elapsed, add gold and start the cooldown
        if (Time.time >= lastClickTime + cooldown)
        {
            playerDataManager.AddGold(goldToAdd);
            lastClickTime = Time.time;
            PlayerPrefs.SetFloat("LastClickTime", lastClickTime);
            button.interactable = false;

            GameObject ps = Instantiate(particle, transform.position, Quaternion.identity, transform);
            ps.transform.localScale *= 0.25f;
        }
    }

    private IEnumerator UpdateCooldownText()
    {
        while (true)
        {
            float remainingCooldown = lastClickTime + cooldown - Time.time;
            if (remainingCooldown > 0)
            {
                // Convert the remaining cooldown to hours, minutes, and seconds
                int hours = (int)(remainingCooldown / 3600);
                remainingCooldown -= hours * 3600;
                int minutes = (int)(remainingCooldown / 60);
                remainingCooldown -= minutes * 60;
                int seconds = (int)remainingCooldown;

                // Update the cooldown text
                cooldownText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
            }
            else
            {
                cooldownText.text = "Ready!";
            }

            // Wait for the next frame
            yield return null;
        }
    }

    public void ResetCooldown()
    {
        // Reset the last click time to allow immediate button press
        lastClickTime = Time.time - cooldown;
        PlayerPrefs.SetFloat("LastClickTime", lastClickTime);
        button.interactable = true;
    }

    private void Update()
    {
        // If the cooldown has elapsed, enable the button
        if (Time.time >= lastClickTime + cooldown)
        {
            button.interactable = true;
        }
    }
}
