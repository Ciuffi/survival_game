using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpawner : MonoBehaviour
{
    // Public variables that can be adjusted in the inspector
    public List<GameObject> enemies;
    public float diameter = 10f;
    public float spawnRate = 1f;
    public float scaleRateSeconds = 10f;
    public int spawnRateScaling = 1;

    public float spawnTimer = 0f;
    private float elapsedTime = 0f;

    // Update is called once per frame
    void Update () {

        // Increase elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to increase spawn rate
        if (elapsedTime >= scaleRateSeconds) {
            spawnRate += spawnRateScaling;
            elapsedTime = 0f;
        }

        // Check if it's time to spawn enemies
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f) {
            SpawnEnemies ();
            spawnTimer = 1f / spawnRate;
        }
    }

    // Spawn enemies along the edge of the circle
    void SpawnEnemies () {

        // Pick a random enemy prefab from the list
        GameObject enemyPrefab = enemies[Random.Range (0, enemies.Count)];

        // Pick a random point along the edge of the circle
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * diameter / 2f;

        // Instantiate the enemy at the spawn position
        Instantiate (enemyPrefab, (Vector2) transform.position + spawnPosition, Quaternion.identity);
    }

    // Draw the circle in the editor for easier debugging
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, diameter / 2f);
    }

}
