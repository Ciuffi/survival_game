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


    public void ResetGame()
    {
        SceneManager.LoadScene(0);

        //new List<Enemy>(GameManager.FindObjectsOfType<Enemy>()).ForEach((e) => Destroy(e.gameObject));
        //new List<Projectile>(GameManager.FindObjectsOfType<Projectile>()).ForEach((e) => Destroy(e.gameObject));
        //playerMovement.transform.position = playerPosition;
        //playerStats.ResetStats(true);
        //playerAttacks.ResetWeapons();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(2);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        playerMovement = GameManager.FindObjectOfType<PlayerMovement>();
        playerPosition = playerMovement.transform.position;
        playerStats = playerMovement.GetComponent<StatsHandler>();
        playerAttacks = playerMovement.GetComponent<AttackHandler>();
    }
}
