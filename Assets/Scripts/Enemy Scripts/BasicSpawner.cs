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


    // Start is called before the first frame update
    void Start()
    {
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
                    Instantiate(spawn, spawnPosition, Quaternion.identity);
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
