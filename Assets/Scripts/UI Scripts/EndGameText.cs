using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class EndGameText : MonoBehaviour
{

    public TextMeshProUGUI totalDamage;
    public TextMeshProUGUI wpnDamage;
    public TextMeshProUGUI runStats;

    // Start is called before the first frame update
    public void Start()
    {
        // Load the information from PlayerPrefs
        string timeSurvived = PlayerPrefs.GetString("timeSurvived", "");
        int enemiesKilled = PlayerPrefs.GetInt("enemiesKilled", 0);
        int goldGained = PlayerPrefs.GetInt("goldGained", 0);

        string weaponStatsJson = PlayerPrefs.GetString("weaponStats", "");
        WeaponStats weaponStats = JsonUtility.FromJson<WeaponStats>(weaponStatsJson);

        List<int> finalDmg = new List<int>();
        foreach (float f in weaponStats.weaponDamage)
        {
            finalDmg.Add(Mathf.RoundToInt(f));
        }



        string weaponInfo = "";
        for (int i = 0; i < weaponStats.weaponNames.Count; i++)
        {
            weaponInfo += weaponStats.weaponNames[i] + " - " + finalDmg[i] + "\n";
        }
        wpnDamage.text = weaponInfo;



        int totalDmg = 0;
        foreach (int damage in finalDmg)
        {
            totalDmg += damage;
        }

        //total damage
        string dmg = "TOTAL DAMAGE:" + "\n" + totalDmg;
        totalDamage.text = dmg;

        //run stats
        string statsInfo = "";
        statsInfo += "Time Survived: " + "\n" + timeSurvived + "\n";
        statsInfo += "Blood Money: " + "\n" + goldGained + "\n";
        statsInfo += "Murders: " + "\n" + enemiesKilled + "\n";
        runStats.text = statsInfo;



        // Clear the PlayerPrefs values so they don't persist across scenes
        PlayerPrefs.DeleteKey("timeSurvived");
        PlayerPrefs.DeleteKey("enemiesKilled");
        PlayerPrefs.DeleteKey("goldGained");
        PlayerPrefs.DeleteKey("weaponNames");
        PlayerPrefs.DeleteKey("weaponDamage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
