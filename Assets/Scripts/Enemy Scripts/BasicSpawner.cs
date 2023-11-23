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
    public List<float> healthScalingList, xpScalingList;
    public float damageScaling, weightScaling;
    public float stageHealthScaling, stageDamageScaling, stageWeightScaling, stageXpScaling, stageGoldScaling;

    //public float speedScaling; -----Doesn't work properly
    public Camera mainCamera;
    public Camera uiCamera;

    public GameObject comboManager;

    public GameObject bossPrefab;
    public int bossDirection360;
    public int bossDistance;
    private bool bossSpawned;
    private bool isScanning = false; // To avoid scanning when a scan is already underway
    private AstarPath astarPath;

    // Start is called before the first frame update
    void Start()
    {
        astarPath = FindObjectOfType<AstarPath>();
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
        comboManager.GetComponent<ComboTracker>().ColorChange(currentGuilt);

        if (currentGuilt <= 5)
        {
            mainCamera.GetComponent<CameraController>().StartZoom();
            uiCamera.GetComponent<CameraController>().StartZoom();
        }
    }

    IEnumerator ScanWithDelay(float delay)
    {
        isScanning = true; // A scan is underway
        astarPath.Scan();
        yield return new WaitForSeconds(delay); // Wait for delay seconds
        isScanning = false; // Scan completed
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

                float cumulativeHealthScaling = 1.0f;
                for (int j = 0; j < (currentGuilt + 1) && j < healthScalingList.Count; j++)
                {
                    cumulativeHealthScaling += healthScalingList[j];
                }

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
                        // Compute the cumulative health scaling factor up to the current guilt level

                        newSpawn.GetComponent<Enemy>().health *= (cumulativeHealthScaling) + stageHealthScaling; 
                        newSpawn.GetComponent<Enemy>().damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                        newSpawn.GetComponent<Enemy>().weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
                        newSpawn.GetComponent<Enemy>().xpAmount *= (1 + (xpScalingList[currentGuilt])) + stageXpScaling;
                        //newSpawn.GetComponent<Enemy>().calculateSpeed(speedScaling);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the player
        transform.position = player.transform.position;

        //if (!isScanning) // Start scanning only if game is not paused and not currently scanning
        //{
            //StartCoroutine(ScanWithDelay(5.0f));
        //}

        if (currentGuilt == 6 && !bossSpawned)
        {
            bossSpawned = true;
            Vector3 spawnPosition = transform.position + MathUtilities.DegreesToVector3(bossDirection360) * (6 + bossDistance);
            GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

            WaypointManager waypointManager = FindObjectOfType<WaypointManager>();
            waypointManager.AddWaypoint(boss, true);
        }
    }
}
