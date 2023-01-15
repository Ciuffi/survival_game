using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneSpawnMap : EnemySpawnMap
{
    public LevelOneSpawnMap()
    {
        enemies.LoadEnemies();
        EnemySpawn wave1 = new EnemySpawn()
            .WithDirection(0)
            .WithDistance(0)
            .WithEnemiesPerWave(2)
            .WithSpawnTimer(2)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
            {enemies.lootBox, 100},
            {enemies.meleeEnemy, 100}
        });

        EnemySpawn wave2 = new EnemySpawn()
        .WithDirection(270)
        .WithDistance(0)
        .WithEnemiesPerWave(2)
        .WithSpawnTimer(5)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
            {enemies.meleeEnemy, 100}
        });

        EnemySpawn wave3 = new EnemySpawn()
        .WithDirection(180)
        .WithDistance(0)
        .WithEnemiesPerWave(2)
        .WithSpawnTimer(10)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 50},
                {enemies.rangedEnemy, 50}
        });
        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);

    }
}
