using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BasicSpawner : MonoBehaviour
{
    public bool isSpawning;
    public StatsHandler player;
    public Queue<EnemySpawn> enemySpawns;
    public EnemySpawnMap spawnMap;

    public int currentGuilt;
    public float healthScaling, damageScaling, weightScaling, xpScaling; //percentile - start with base of 1.0f
    public float stageHealthScaling, stageDamageScaling, stageWeightScaling, stageXpScaling;

    //public float speedScaling; -----Doesn't work properly
    public Camera mainCamera;
    public Camera uiCamera;

    public GameObject comboManager;

    // Start is called before the first frame update
    void Start()
    {
        currentGuilt = 0;
        player = GameObject.Find("Player").GetComponent<StatsHandler>();
        isSpawning = true;
        spawnMap = new LevelOneSpawnMap();
        StartCoroutine(StartSpawner());
    }

    public void StopSpawn()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void IncreaseGuilt()
    {
        currentGuilt += 1;
        if (currentGuilt <= 4)
        {
            mainCamera.GetComponent<CameraController>().StartZoom();
            uiCamera.GetComponent<CameraController>().StartZoom();
        }
    }

    IEnumerator StartSpawner()
    {
        while (true)
        {
            foreach (EnemySpawn enemy in spawnMap.spawnMaps)
            {
                if (!isSpawning) yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(enemy.TimeToSpawn);
                if (!isSpawning) yield return new WaitForEndOfFrame();
                for (int i = 0; i < enemy.EnemiesPerWave; i++)
                {
                    int spawnIndex = MathUtilities.GetWeightedResult(enemy.EnemiesToSpawn.Values.ToArray<int>());
                    
                    GameObject spawn = enemy.EnemiesToSpawn.Keys.ToArray<GameObject>()[spawnIndex];
                    Vector3 spawnPosition = transform.position + MathUtilities.DegreesToVector3(enemy.Direction) * (6 + enemy.Distance);
                    GameObject newSpawn = Instantiate(spawn, spawnPosition, Quaternion.identity);

                    //scaling with Guilt + rescan map for pathing
                    if (newSpawn != null && newSpawn.tag == "Enemy")
                    {
                        //Debug.Log(newSpawn.name.ToString());
                        newSpawn.GetComponent<Enemy>().health *= (1 + (healthScaling * currentGuilt)) + stageHealthScaling;
                        newSpawn.GetComponent<Enemy>().projectileDamage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                        newSpawn.GetComponent<Enemy>().damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                        newSpawn.GetComponent<Enemy>().weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
                        newSpawn.GetComponent<Enemy>().xpAmount *= (1 + (xpScaling * currentGuilt)) + stageXpScaling;
                        //newSpawn.GetComponent<Enemy>().calculateSpeed(speedScaling);
                        comboManager.GetComponent<ComboTracker>().ColorChange(currentGuilt);
                    }
                    AstarPath.active.Scan();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the player
        transform.position = player.transform.position;
    }
}
