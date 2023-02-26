using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameStatTracker : MonoBehaviour
{
    public GameObject timer, killTracker, goldTracker;

    public string timeSurvived;
    public int enemiesKilled, goldGained;
    public List<string> weaponNames = new List<string>();
    public List<float> weaponDamage = new List<float>();

    public GameObject attacks; // reference to the Attacks gameobject

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPlayerDeath()
    {
        weaponNames.Clear();
        weaponDamage.Clear();

        timeSurvived = timer.GetComponent<GameTimer>().time;
        enemiesKilled = killTracker.GetComponent<ComboTracker>().comboCount;
        goldGained = goldTracker.GetComponent<GoldTracker>().goldCount;

        foreach (Transform child in attacks.transform)
        {
            string weaponName = child.gameObject.name;
            string newName = weaponName.EndsWith("(Clone)") ? weaponName.Substring(0, weaponName.Length - 7) : weaponName;
            weaponNames.Add(newName);

            Attack attackComponent = child.GetComponent<Attack>();
            float totalDamageDealt = attackComponent.totalDamageDealt;
            weaponDamage.Add(totalDamageDealt);
        }

        // Save the information to PlayerPrefs
        PlayerPrefs.SetString("timeSurvived", timeSurvived);
        PlayerPrefs.SetInt("enemiesKilled", enemiesKilled);
        PlayerPrefs.SetInt("goldGained", goldGained);

        // Convert the weapon names and damage lists to JSON strings and save them to PlayerPrefs
        WeaponStats weaponStats = new WeaponStats();
        weaponStats.weaponNames = weaponNames;
        weaponStats.weaponDamage = weaponDamage;
        string weaponStatsJson = JsonUtility.ToJson(weaponStats);
        PlayerPrefs.SetString("weaponStats", weaponStatsJson);
    }
}
