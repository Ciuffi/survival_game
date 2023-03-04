using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnemyTracker : MonoBehaviour
{
    public int enemyLimit = 200;
    public int enemyAmount = 0;
    public int availableAmount;

    public Queue<ConstantSpawner> spawnerQueue;

    private void Start()
    {
        spawnerQueue = new Queue<ConstantSpawner>();
        availableAmount = enemyLimit - enemyAmount;
    }

    public void IncreaseCount(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            enemyAmount++;
            availableAmount = enemyLimit - enemyAmount;
        }
    }
    public void DecreaseCount()
    {
        enemyAmount--;
        availableAmount = enemyLimit - enemyAmount;

        if (spawnerQueue.Count > 0)
        {
            ConstantSpawner nextSpawn = spawnerQueue.Peek();
            int spawnRate = nextSpawn.spawnRate;

            if (spawnRate <= availableAmount)
            {
                ConstantSpawner nextSpawner = spawnerQueue.Dequeue();
                nextSpawner.SpawnEnemies();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (availableAmount < 0)
        {
            availableAmount = 0;
        }
    }
}
