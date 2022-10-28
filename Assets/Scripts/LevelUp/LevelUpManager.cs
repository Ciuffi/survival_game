using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public GameObject[] Attacks;

    public float[] toLevelUp;
    public float baseXP;
    public float growthMultiplier;


    private void LevelXPSetUp()
    {
        for (int i = 1; i < toLevelUp.Length; i++)
        {
            toLevelUp[i] = (int)(Mathf.Floor(baseXP * (Mathf.Pow(i, growthMultiplier))));
        }
    }

    public void LevelUp()
    {
        PauseGame();

    }
    

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }





    // Start is called before the first frame update
    void Start()
    {
        LevelXPSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
