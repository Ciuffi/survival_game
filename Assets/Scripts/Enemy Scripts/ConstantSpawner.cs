using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpawner : MonoBehaviour
{
    // Public variables that can be adjusted in the inspector
    public List<GameObject> enemies;
    public bool isEliteSpawner = false;
    public bool isWallSpawner;
    private int eliteEnemyIndex = 0; // Index of the enemy to spawn

    public bool alwaysSpawn;
    public bool isCircleSpawn;
    public bool isClumpSpawn;
    public float diameter = 10f;
    public float diameterMultiplier;
    public float initialDelay;
    public float spawnTimer = 0f;
    private float OGspawnTimer;

    public int spawnRate = 1;
    private int spawnRateLimit;
    public int spawnRateLimit_G0 = 2;
    public int spawnRateLimit_G1 = 3;
    public int spawnRateLimit_G2 = 3;
    public int spawnRateLimit_G3 = 4;
    public int spawnRateLimit_G4 = 5;
    public int spawnRateLimit_G5 = 6;
    public int spawnRateLimit_G6 = 1;

    public float scaleRateSeconds = 10f;
    public int spawnRateScaling = 1;

    private bool delayFinished;
    public float elapsedTime = 0f;
    public GameObject basicSpawner;
    private GameObject maxEnemiesTracker;
    private int availableEnemyAmount;


    private int currentGuilt;
    public List<float> healthScalingList, xpScalingList;
    private float damageScaling, weightScaling;
    private float stageHealthScaling, stageDamageScaling, stageWeightScaling, stageXpScaling;
    private float prevGuiltValue;


    private void Start()
    {
        OGspawnTimer = spawnTimer;
        maxEnemiesTracker = GameObject.Find("EnemyTracker");
        availableEnemyAmount = maxEnemiesTracker.GetComponent<MaxEnemyTracker>().availableAmount;

        stageHealthScaling = basicSpawner.GetComponent<BasicSpawner>().stageHealthScaling;
        stageDamageScaling = basicSpawner.GetComponent<BasicSpawner>().stageDamageScaling;
        stageWeightScaling = basicSpawner.GetComponent<BasicSpawner>().stageWeightScaling;
        stageXpScaling = basicSpawner.GetComponent<BasicSpawner>().stageXpScaling;
        healthScalingList = basicSpawner.GetComponent<BasicSpawner>().healthScalingList;
    }

    // Update is called once per frame
    void Update () {

        availableEnemyAmount = maxEnemiesTracker.GetComponent<MaxEnemyTracker>().availableAmount;

        // Check if guilt value has increased since last frame
        if (currentGuilt > prevGuiltValue)
        {
            diameter *= (1 + (diameterMultiplier * currentGuilt));
            elapsedTime = 0f;

            if (isEliteSpawner)
            {
                SpawnEliteEnemy();
            }
            if (isWallSpawner)
            {
                QueueSpawn();
            }
        }

        // Set the previous guilt value to the current value for the next frame
        prevGuiltValue = currentGuilt;

        if (currentGuilt == 0)
        {
            spawnRateLimit = spawnRateLimit_G0;
        }
        else if (currentGuilt == 1)
        {
            spawnRateLimit = spawnRateLimit_G1;
        }
        else if (currentGuilt == 2)
        {
            spawnRateLimit = spawnRateLimit_G2;
        }
        else if (currentGuilt == 3)
        {
            spawnRateLimit = spawnRateLimit_G3;
        }
        else if (currentGuilt == 4)
        {
            spawnRateLimit = spawnRateLimit_G4;
        }
        else if (currentGuilt == 5)
        {
            spawnRateLimit = spawnRateLimit_G5;
        }
        else if (currentGuilt == 6)
        {
            spawnRateLimit = spawnRateLimit_G6;
        }

        if (spawnRate > spawnRateLimit) //if current rate is greater than the max
        {
            spawnRate = spawnRateLimit;
        }

        // Increment elapsed time if initial delay is complete
        if (delayFinished)
        {
            elapsedTime += Time.deltaTime;

            // Check if it's time to increase spawn rate
            if (elapsedTime >= scaleRateSeconds)
            {
                if (spawnRate < spawnRateLimit) //if current rate is less than max spawnRate
                {
                    spawnRate += spawnRateScaling; 
                }

                elapsedTime = 0f;
            }

            
            // Check if it's time to spawn enemies
            if (!isWallSpawner)
            {
                spawnTimer -= Time.deltaTime;
            }
            if (spawnTimer <= 0f)
            {
                QueueSpawn();
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
        weightScaling = basicSpawner.GetComponent<BasicSpawner>().weightScaling;
        damageScaling = basicSpawner.GetComponent<BasicSpawner>().damageScaling;
        xpScalingList = basicSpawner.GetComponent<BasicSpawner>().xpScalingList;
    }



    private void QueueSpawn()
    {
        if (alwaysSpawn)
        {
            SpawnEnemies();
        }
        else
        {
            if (spawnRate <= availableEnemyAmount)
            {
                SpawnEnemies();
                maxEnemiesTracker.GetComponent<MaxEnemyTracker>().IncreaseCount(spawnRate);
            }
            else
            {
                maxEnemiesTracker.GetComponent<MaxEnemyTracker>().spawnerQueue.Enqueue(this);
            }
        }
    }


    // Spawn enemies along the edge of the circle
    public void SpawnEnemies () {

        if (isEliteSpawner)
        {
            return;
        }

        // Pick a random enemy prefab from the list
        GameObject enemyPrefab;

        // Pick a random enemy prefab from the list
        enemyPrefab = enemies[Random.Range(0, enemies.Count)];


        float cumulativeHealthScaling = 1.0f;
        for (int j = 0; j < (currentGuilt + 1) && j < healthScalingList.Count; j++)
        {
            cumulativeHealthScaling += healthScalingList[j];
        }

        if (isCircleSpawn)
        {
            if (spawnRate > 1)
            {
                // Pick a random point along the edge of the circle
                Vector2 spawnCenter = (Vector2)transform.position;
                Vector2 spawnDirection = Random.insideUnitCircle.normalized;
                Vector2 spawnPosition = spawnCenter + (spawnDirection * diameter / 2f);

                // Calculate the angle between each enemy
                float angleBetween = 360f / spawnRate;

                // Calculate the range of angles to randomize within
                float angleRange = angleBetween * 0.25f;

                // Spawn enemies evenly around the edge of the circle
                for (int i = 0; i < spawnRate; i++)
                {
                    // Randomly adjust the angle within the range
                    float angleOffset = Random.Range(-angleRange, angleRange);

                    // Calculate the position of the enemy based on the angle
                    Vector2 enemyPosition = Quaternion.Euler(0f, 0f, i * angleBetween + angleOffset) * spawnDirection * diameter / 2f;

                    // Instantiate the enemy at the spawn position
                    GameObject newEnemy = Instantiate(enemyPrefab, spawnCenter + enemyPosition, Quaternion.identity);
                    Enemy enemy = newEnemy.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.health *= (cumulativeHealthScaling) + stageHealthScaling;
                        enemy.projectileDamage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                        enemy.damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                        enemy.weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
                        enemy.xpAmount *= (1 + (xpScalingList[currentGuilt])) + stageXpScaling;
                    }

                }
            }
            else if (spawnRate == 1)
            {
                // Spawn a single enemy at a random position on the edge of the circle
                Vector2 spawnPosition = Random.insideUnitCircle.normalized * diameter / 2f;
                GameObject newEnemy = Instantiate(enemyPrefab, (Vector2)transform.position + spawnPosition, Quaternion.identity);
                Enemy enemy = newEnemy.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.health *= (cumulativeHealthScaling) + stageHealthScaling;
                    enemy.projectileDamage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                    enemy.damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                    enemy.weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
                    enemy.xpAmount *= (1 + (xpScalingList[currentGuilt])) + stageXpScaling;
                }
            }
        }
        else if (isClumpSpawn)
        {
            Vector2 clumpCenter = Random.insideUnitCircle.normalized * diameter / 2f;

            for (int i = 0; i < spawnRate; i++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, clumpCenter, Quaternion.identity);
                Enemy enemy = newEnemy.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.health *= (cumulativeHealthScaling) + stageHealthScaling;
                    enemy.projectileDamage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                    enemy.damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                    enemy.weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
                    enemy.xpAmount *= (1 + (xpScalingList[currentGuilt])) + stageXpScaling;
                }
            }
        }
        else //random spawning
        {
            for (int i = 0; i < spawnRate; i++)
            {
                // Calculate a random angle for enemy placement
                float randomAngle = Random.Range(0f, 360f);

                // Convert the angle to radians
                float angleInRadians = randomAngle * Mathf.Deg2Rad;

                // Calculate the position of the enemy based on the random angle
                Vector2 spawnPosition = (Vector2)transform.position +
                    new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * diameter / 2f;

                // Instantiate the enemy at the spawn position
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                Enemy enemy = newEnemy.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.health *= (cumulativeHealthScaling) + stageHealthScaling;
                    enemy.projectileDamage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                    enemy.damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
                    enemy.weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
                    enemy.xpAmount *= (1 + (xpScalingList[currentGuilt])) + stageXpScaling;
                }
            }

        }

    }

    private void SpawnEliteEnemy()
    {
        // Choose the next enemy in the list instead of picking randomly.
        GameObject enemyPrefab = enemies[eliteEnemyIndex];
        eliteEnemyIndex++;
        if (eliteEnemyIndex >= enemies.Count)
            eliteEnemyIndex = enemies.Count - 1;

        float cumulativeHealthScaling = 1.0f;
        for (int j = 0; j < (currentGuilt + 1) && j < healthScalingList.Count; j++)
        {
            cumulativeHealthScaling += healthScalingList[j];
        }

        // Spawn a single elite enemy at a random position on the edge of the circle
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * diameter / 2f;
        GameObject newEnemy = Instantiate(enemyPrefab, (Vector2)transform.position + spawnPosition, Quaternion.identity);

        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.health *= (cumulativeHealthScaling) + stageHealthScaling;
            enemy.projectileDamage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
            enemy.damage *= (1 + (damageScaling * currentGuilt)) + stageDamageScaling;
            enemy.weight *= (1 + (weightScaling * currentGuilt)) + stageWeightScaling;
            enemy.xpAmount *= (1 + (xpScalingList[currentGuilt])) + stageXpScaling;
        }

        WaypointManager waypointManager = FindObjectOfType<WaypointManager>();
        waypointManager.AddWaypoint(newEnemy, true);
    }

    // Draw the circle in the editor for easier debugging
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, diameter / 2f);
    }

}
