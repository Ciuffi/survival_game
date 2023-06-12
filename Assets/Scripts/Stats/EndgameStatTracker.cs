using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameStatTracker : MonoBehaviour
{
    public GameObject timer, killTracker, goldTracker;

    public string timeSurvived;
    public int enemiesKilled, goldGained;
    public List<string> weaponNames = new List<string>();
    public List<float> weaponDamage = new List<float>();

    public GameObject attacks; // reference to the Attacks gameobject

    public PlayerDataManager playerData;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to sceneLoaded event

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerData = FindObjectOfType<PlayerDataManager>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe from sceneLoaded when this GameObject is destroyed
    }

    public void EndGameStats()
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
        PlayerPrefs.SetInt("incrementGold", goldGained);

        playerData.IncrementGold();

        // Convert the weapon names and damage lists to JSON strings and save them to PlayerPrefs
        WeaponStats weaponStats = new WeaponStats();
        weaponStats.weaponNames = weaponNames;
        weaponStats.weaponDamage = weaponDamage;
        string weaponStatsJson = JsonUtility.ToJson(weaponStats);
        PlayerPrefs.SetString("weaponStats", weaponStatsJson);
    }
}
