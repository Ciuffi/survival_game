using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameStatTracker : MonoBehaviour
{
    public GameObject timer, killTracker, goldTracker;

    public string timeSurvived;
    public int enemiesKilled, goldGained, currentGuilt;
    public List<string> weaponNames = new List<string>();
    public List<float> weaponDamage = new List<float>();
    public int stage;

    public GameObject attacks; // reference to the Attacks gameobject

    public PlayerDataManager playerData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        playerData = PlayerDataManager.Instance;
        FindGameObjects();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsGameScene(scene.buildIndex))
        {
            playerData = PlayerDataManager.Instance;
            FindGameObjects();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void EndGameStats()
    {
        Debug.Log("EndGameStats called");
        weaponNames.Clear();
        weaponDamage.Clear();

        CollectStats();

        // Convert the weapon names and damage lists to JSON strings and save them to PlayerPrefs
        WeaponStats weaponStats = new WeaponStats
        {
            weaponNames = weaponNames,
            weaponDamage = weaponDamage
        };
        string weaponStatsJson = JsonUtility.ToJson(weaponStats);
        PlayerPrefs.SetString("weaponStats", weaponStatsJson);

        playerData.ProcessEndGameStats(enemiesKilled, goldGained, currentGuilt, weaponStats, stage);
    }

    private void CollectStats()
    {
        timeSurvived = timer.GetComponent<GameTimer>().time;
        enemiesKilled = killTracker.GetComponent<ComboTracker>().comboCount;
        goldGained = goldTracker.GetComponent<GoldTracker>().goldCount;
        currentGuilt = killTracker.GetComponent<ComboTracker>().GetCurrentGuilt();

        Scene currentScene = SceneManager.GetActiveScene();
        stage = currentScene.buildIndex;

        foreach (Transform child in attacks.transform)
        {
            string weaponName = child.gameObject.name;
            string newName = weaponName.EndsWith("(Clone)") ? weaponName.Substring(0, weaponName.Length - 7) : weaponName;
            weaponNames.Add(newName);

            Attack attackComponent = child.GetComponent<Attack>();
            float totalDamageDealt = attackComponent.totalDamageDealt;
            weaponDamage.Add(totalDamageDealt);
        }
    }

    private bool IsGameScene(int sceneIndex)
    {
        return sceneIndex != 0 && sceneIndex != SceneManager.sceneCountInBuildSettings - 1;
    }

    private void FindGameObjects()
    {
        attacks = FindObjectOfType<AttackHandler>()?.transform.Find("Weapons")?.gameObject;
        timer = FindObjectOfType<GameTimer>()?.gameObject;
        killTracker = FindObjectOfType<ComboTracker>()?.gameObject;
        goldTracker = FindObjectOfType<GoldTracker>()?.gameObject;
    }
}
