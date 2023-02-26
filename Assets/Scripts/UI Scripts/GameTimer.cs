using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public string time;
    public TextMeshProUGUI timerText; // Reference to the UI Text component to display the timer

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.timeSinceLevelLoad;
        UpdateTimerUI(timeElapsed);
    }

    void UpdateTimerUI(float timeElapsed)
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        time = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = time;
    }
}
