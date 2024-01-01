using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Vector3 playerPosition;
    PlayerMovement playerMovement;
    StatsHandler playerStats;
    AttackHandler playerAttacks;
    public GameObject pauseMenu;

    private PlayerDataManager playerData;
    private PlayerInventory playerInv;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to sceneLoaded event
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe from sceneLoaded when this GameObject is destroyed
    }

    public void ShowPauseScreen()
    {
        if (playerStats.currentHealth <= 0)
        {
            return;
        }
        GameTime.instance.Pause();
        Time.timeScale = 0;
        GameObject.FindObjectOfType<PlayerMovement>().StopMoving();
        GameObject.FindObjectOfType<VirtualJoystick>().enabled = false;
        pauseMenu.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseMenu.SetActive(false);
        GameObject.FindObjectOfType<VirtualJoystick>().enabled = true;
        GameObject.FindObjectOfType<PlayerMovement>().StartMoving();
        GameTime.instance.Unpause();
        Time.timeScale = 1;
    }

    public void MenuReset()
    {
        GameTime.instance.resetTime();
        GameTime.instance.Unpause();
        Time.timeScale = 1;
        playerData.SaveData();  // Save data before changing scene
        SceneManager.LoadScene(0);

    }

    public void ReloadScene()
    {
        GameTime.instance.resetTime();
        GameTime.instance.Unpause();

        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void WinGame()
    {
        DeathTransition.Instance.StartTransition();
        int currentStageID = SceneManager.GetActiveScene().buildIndex;
        playerData.UnlockNextStage(currentStageID);

        PlayerPrefs.SetInt("isPlayerVictory", 1); // 1 for victory
        Debug.Log("Victory");
    }

    public void EndGame()
    {
        DeathTransition.Instance.StartTransition();
        PlayerPrefs.SetInt("isPlayerVictory", 0); // 0 for loss
        Debug.Log("Defeat");

    }

    public void playerDeathScreen()
    {
        SceneManager.LoadScene("DeathResults");
    }


    void Start()
    {
        Application.targetFrameRate = 60;
        playerMovement = GameManager.FindObjectOfType<PlayerMovement>();
        playerPosition = playerMovement.transform.position;
        playerStats = playerMovement.GetComponent<StatsHandler>();
        playerAttacks = playerMovement.GetComponent<AttackHandler>();
        playerData = PlayerDataManager.Instance;
        playerInv = PlayerInventory.Instance;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            pauseMenu = GameObject.Find("PauseMenu");
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
        } 
        
    }
}
