using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpawner : MonoBehaviour
{
    // Public variables that can be adjusted in the inspector
    public List<GameObject> enemies;
    public float diameter = 10f;
    public float initialDelay;
    public float spawnTimer = 0f;

    public float spawnRate = 1f;
    private int spawnRateLimit;
    public int spawnRateLimit_G1 = 3;
    public int spawnRateLimit_G3 = 4;
    public int spawnRateLimit_G4 = 5;

    public float scaleRateSeconds = 10f;
    public int spawnRateScaling = 1;

    private bool delayFinished;
    private float elapsedTime = 0f;
    public GameObject basicSpawner;

    private int currentGuilt;
    private float healthScaling, damageScaling, weightScaling, xpScaling;

    // Update is called once per frame
    void Update () {

        // Increment elapsed time if initial delay is complete
        if (delayFinished)
        {
            elapsedTime += Time.deltaTime;

            // Check if it's time to increase spawn rate
            if (elapsedTime >= scaleRateSeconds)
            {
                if (spawnRate < spawnRateLimit)
                {
                    spawnRate += spawnRateScaling;
                }
                elapsedTime = 0f;
            }

            // Check if it's time to spawn enemies
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnEnemies();
                spawnTimer = 1f / spawnRate;
            }
        }
        else
        {
            // Wait for initial delay to complete
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0f)
            {
                delayFinished = true;
                spawnTimer = 1f / spawnRate;
            }
        }

        currentGuilt = basicSpawner.GetComponent<BasicSpawner>().currentGuilt;
        healthScaling = basicSpawner.GetComponent<BasicSpawner>().healthScaling;
        weightScaling = basicSpawner.GetComponent<BasicSpawner>().weightScaling;
        damageScaling = basicSpawner.GetComponent<BasicSpawner>().damageScaling;
        xpScaling = basicSpawner.GetComponent<BasicSpawner>().xpScaling;

        if (currentGuilt == 1)
        {
            spawnRateLimit = spawnRateLimit_G1;
        } else if (currentGuilt == 3)
        {
            spawnRateLimit = spawnRateLimit_G3;
        } else if (currentGuilt == 4)
        {
            spawnRateLimit = spawnRateLimit_G4;
        }


    }

    // Spawn enemies along the edge of the circle
    void SpawnEnemies () {

        // Pick a random enemy prefab from the list
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Count)];

        // Pick a random point along the edge of the circle
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * diameter / 2f;

        // Instantiate the enemy at the spawn position
        Instantiate(enemyPrefab, (Vector2)transform.position + spawnPosition, Quaternion.identity);
        if (currentGuilt > 0)
        {
            enemyPrefab.GetComponent<Enemy>().health *= (healthScaling * currentGuilt);
            enemyPrefab.GetComponent<Enemy>().projectileDamage *= (damageScaling * currentGuilt);
            enemyPrefab.GetComponent<Enemy>().damage *= (damageScaling * currentGuilt);
            enemyPrefab.GetComponent<Enemy>().weight *= (weightScaling * currentGuilt);
            enemyPrefab.GetComponent<Enemy>().xpAmount *= (xpScaling * currentGuilt);
        }

    }

    // Draw the circle in the editor for easier debugging
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, diameter / 2f);
    }

}
