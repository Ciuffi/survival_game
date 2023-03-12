using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnemyTracker : MonoBehaviour
{
    public List<int> enemyLimit;
    public int currentLimit;
    public int enemyAmount = 0;
    public int availableAmount;

    public Queue<ConstantSpawner> spawnerQueue;
    public GameObject spawner;
    public int currentGuilt;

    private void Start()
    {
        spawnerQueue = new Queue<ConstantSpawner>();
        currentLimit = enemyLimit[0];
        availableAmount = currentLimit - enemyAmount;
    }

    public void IncreaseCount(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            enemyAmount++;
            availableAmount = currentLimit - enemyAmount;
        }
    }
    public void DecreaseCount()
    {
        enemyAmount--;
        availableAmount = currentLimit - enemyAmount;

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

        currentGuilt = spawner.GetComponent<BasicSpawner>().currentGuilt;

        switch (currentGuilt)
        {
            case 0:
                currentLimit = enemyLimit[0];
                break;
            case 1:
                currentLimit = enemyLimit[1];
                break;
            case 2:
                currentLimit = enemyLimit[2];
                break;
            case 3:
                currentLimit = enemyLimit[3];
                break;
            case 4:
                currentLimit = enemyLimit[4];
                break;
            case 5:
                currentLimit = enemyLimit[5];
                break;
        }
    }
}
