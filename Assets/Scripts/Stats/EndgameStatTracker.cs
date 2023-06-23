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
        DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator Start()
    {
        // Wait for a short amount of time to ensure that all objects are fully loaded
        yield return new WaitForSeconds(0.1f);

        playerData = PlayerDataManager.Instance;

        // Try finding and assigning the objects again
        attacks = FindObjectOfType<AttackHandler>().transform.Find("Weapons").gameObject;
        timer = FindObjectOfType<GameTimer>().gameObject;
        killTracker = FindObjectOfType<ComboTracker>().gameObject;
        goldTracker = FindObjectOfType<GoldTracker>().gameObject;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // only reassign if the loaded scene is a game scene
        if (scene.buildIndex != 0 && scene.buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            // PlayerDataManager singleton is not destroyed when switching scenes
            playerData = PlayerDataManager.Instance;

            attacks = FindObjectOfType<AttackHandler>().transform.Find("Weapons").gameObject;
            timer = FindObjectOfType<GameTimer>().gameObject;
            killTracker = FindObjectOfType<ComboTracker>().gameObject;
            goldTracker = FindObjectOfType<GoldTracker>().gameObject;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded when this GameObject is enabled
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from sceneLoaded when this GameObject is disabled
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
