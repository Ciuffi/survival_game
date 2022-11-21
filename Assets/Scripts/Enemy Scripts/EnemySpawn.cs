using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn
{
    public float Direction;
    public float Distance;
    public float TimeToSpawn;
    public float EnemiesPerWave;
    public Dictionary<GameObject, int> EnemiesToSpawn;

    public EnemySpawn WithSpawnTimer(float timer)
    {
        this.TimeToSpawn = timer;
        return this;
    }

    public EnemySpawn WithDirection(float direction)
    {
        this.Direction = direction + 90;
        return this;
    }

    public EnemySpawn WithDistance(float distance)
    {
        this.Distance = distance;
        return this;
    }

    public EnemySpawn WithEnemiesPerWave(float enemiesPerWave)
    {
        EnemiesPerWave = enemiesPerWave;
        return this;
    }

    public EnemySpawn WithEnemy(GameObject enemy, int weight)
    {
        this.EnemiesToSpawn.Add(enemy, weight);
        return this;
    }
    public EnemySpawn WithEnemyMap(Dictionary<GameObject, int> enemes)
    {
        this.EnemiesToSpawn = enemes;
        return this;
    }
}
