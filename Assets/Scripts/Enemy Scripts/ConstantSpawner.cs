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
    private float OGspawnTimer;

    public float spawnRate = 1f;
    private int spawnRateLimit;
    public int spawnRateLimit_G0 = 2;
    public int spawnRateLimit_G1 = 3;
    public int spawnRateLimit_G3 = 4;
    public int spawnRateLimit_G4 = 5;

    public float scaleRateSeconds = 10f;
    public int spawnRateScaling = 1;

    private bool delayFinished;
    public float elapsedTime = 0f;
    public GameObject basicSpawner;

    private int currentGuilt;
    private float healthScaling, damageScaling, weightScaling, xpScaling;
    private float prevGuiltValue;

    private void Start()
    {
        OGspawnTimer = spawnTimer;    
    }

    // Update is called once per frame
    void Update () {


        // Check if guilt value has increased since last frame
        if (currentGuilt > prevGuiltValue)
        {
            // Reset the timer for scaling the spawn rate
            elapsedTime = 0f;
        }

        // Set the previous guilt value to the current value for the next frame
        prevGuiltValue = currentGuilt;



        // Increment elapsed time if initial delay is complete
        if (delayFinished)
        {
            elapsedTime += Time.deltaTime;

            // Check if it's time to increase spawn rate
            if (elapsedTime >= scaleRateSeconds)
            {
                spawnRate += spawnRateScaling;
                elapsedTime = 0f;
            }

            // Check if it's time to spawn enemies
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnEnemies();
                spawnTimer = OGspawnTimer;
            }
        }
        else
        {
            // Wait for initial delay to complete
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0f)
            {
                delayFinished = true;
                spawnTimer = OGspawnTimer;
            }
        }

        currentGuilt = basicSpawner.GetComponent<BasicSpawner>().currentGuilt;
        healthScaling = basicSpawner.GetComponent<BasicSpawner>().healthScaling;
        weightScaling = basicSpawner.GetComponent<BasicSpawner>().weightScaling;
        damageScaling = basicSpawner.GetComponent<BasicSpawner>().damageScaling;
        xpScaling = basicSpawner.GetComponent<BasicSpawner>().xpScaling;

        if (currentGuilt == 0)
        {
            spawnRateLimit = spawnRateLimit_G0;
        } else if (currentGuilt == 1)
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
        int numEnemies = Mathf.RoundToInt(spawnRate);

        if (numEnemies > 1f)
        {
            // Pick a random point along the edge of the circle
            Vector2 spawnCenter = (Vector2)transform.position;
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = spawnCenter + (spawnDirection * diameter / 2f);

            // Calculate the angle between each enemy
            float angleBetween = 360f / numEnemies;

            // Calculate the range of angles to randomize within
            float angleRange = angleBetween * 0.25f;

            // Spawn enemies evenly around the edge of the circle
            for (int i = 0; i < numEnemies; i++)
            {
                // Randomly adjust the angle within the range
                float angleOffset = Random.Range(-angleRange, angleRange);

                // Calculate the position of the enemy based on the angle
                Vector2 enemyPosition = Quaternion.Euler(0f, 0f, i * angleBetween + angleOffset) * spawnDirection * diameter / 2f;

                // Instantiate the enemy at the spawn position
                GameObject newEnemy = Instantiate(enemyPrefab, spawnCenter + enemyPosition, Quaternion.identity);

                if (currentGuilt > 0)
                {
                    newEnemy.GetComponent<Enemy>().health *= (healthScaling * currentGuilt);
                    newEnemy.GetComponent<Enemy>().projectileDamage *= (damageScaling * currentGuilt);
                    newEnemy.GetComponent<Enemy>().damage *= (damageScaling * currentGuilt);
                    newEnemy.GetComponent<Enemy>().weight *= (weightScaling * currentGuilt);
                    newEnemy.GetComponent<Enemy>().xpAmount *= (xpScaling * currentGuilt);
                }


            }
        }
        else
        {
            // Spawn a single enemy at a random position on the edge of the circle
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * diameter / 2f;
            GameObject newEnemy = Instantiate(enemyPrefab, (Vector2)transform.position + spawnPosition, Quaternion.identity);
            if (currentGuilt > 0)
            {
                newEnemy.GetComponent<Enemy>().health *= (healthScaling * currentGuilt);
                newEnemy.GetComponent<Enemy>().projectileDamage *= (damageScaling * currentGuilt);
                newEnemy.GetComponent<Enemy>().damage *= (damageScaling * currentGuilt);
                newEnemy.GetComponent<Enemy>().weight *= (weightScaling * currentGuilt);
                newEnemy.GetComponent<Enemy>().xpAmount *= (xpScaling * currentGuilt);
            }
        }


    }

    // Draw the circle in the editor for easier debugging
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, diameter / 2f);
    }

}
