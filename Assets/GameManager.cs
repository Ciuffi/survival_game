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
    public GameObject deathTransition;
    public GameObject pauseMenu;

    private PlayerDataManager playerData;
    private PlayerInventory playerInv;

    public void ShowPauseScreen()
    {
        if (playerStats.currentHealth <= 0)
        {
            return;
        }
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
        Time.timeScale = 1;
    }

    public void MenuReset()
    {
        Time.timeScale = 1;
        playerInv.DecrementWeaponDurability();
        SceneManager.LoadScene(0);

        //new List<Enemy>(GameManager.FindObjectsOfType<Enemy>()).ForEach((e) => Destroy(e.gameObject));
        //new List<Projectile>(GameManager.FindObjectsOfType<Projectile>()).ForEach((e) => Destroy(e.gameObject));
        //playerMovement.transform.position = playerPosition;
        //playerStats.ResetStats(true);
        //playerAttacks.ResetWeapons();
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void WinGame()
    {
        playerInv.DecrementWeaponDurability();

        deathTransition.GetComponent<DeathTransition>().StartTransition();
        int currentStageID = SceneManager.GetActiveScene().buildIndex;
        playerData.UnlockNextStage(currentStageID);
    }

    public void EndGame()
    {
        playerInv.DecrementWeaponDurability();

        deathTransition.GetComponent<DeathTransition>().StartTransition();
    }

    public void playerDeathScreen()
    {
        GameObject.FindObjectOfType<EndgameStatTracker>().EndGameStats();
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
        pauseMenu.SetActive(false);

    }
}
