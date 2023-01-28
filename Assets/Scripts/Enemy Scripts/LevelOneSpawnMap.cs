using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneSpawnMap : EnemySpawnMap
{
    public LevelOneSpawnMap()
    {
        enemies.LoadEnemies();
        EnemySpawn wave1 = new EnemySpawn()
            .WithDirection(Random.Range(0,360))
            .WithDistance(Random.Range(0,1))
            .WithEnemiesPerWave(1)
            .WithSpawnTimer(0)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 50},
        });

        EnemySpawn wave2 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
            .WithDistance(Random.Range(1, 2))
        .WithEnemiesPerWave(2)
        .WithSpawnTimer(2)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 100}

        });

        EnemySpawn wave3 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 2))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(3)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.rangedEnemy, 100}
        });

        EnemySpawn wave4 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 2))
          .WithEnemiesPerWave(3)
        .WithSpawnTimer(1)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 100}
        });

        EnemySpawn wave5 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 3))
              .WithEnemiesPerWave(1)
            .WithSpawnTimer(2)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                { enemies.meleeEnemy, 50},
                { enemies.rangedEnemy, 50}
    });

        EnemySpawn wave6 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 3))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(5)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.rageEnemy, 100}

        });

        EnemySpawn wave7 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 5))
          .WithEnemiesPerWave(4)
        .WithSpawnTimer(4)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 100},
        });

        EnemySpawn wave8 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 5))
          .WithEnemiesPerWave(4)
        .WithSpawnTimer(4)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.rangedEnemy, 50},
                {enemies.rangedEnemy, 50}
        });

        EnemySpawn wave9 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 5))
          .WithEnemiesPerWave(2)
        .WithSpawnTimer(4)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.rageEnemy, 100}
        });

        EnemySpawn wave10 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(1, 5))
          .WithEnemiesPerWave(6)
        .WithSpawnTimer(6)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 80},
                {enemies.rangedEnemy, 20}
          });

        EnemySpawn wave11 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(0, 0))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(1)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.lootBox, 100}
          });

        EnemySpawn wave12 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(0, 1))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(10)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.bossRage, 100}
          });



        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave6);
        spawnMaps.Add(wave7);
        spawnMaps.Add(wave8);
        spawnMaps.Add(wave9);
        spawnMaps.Add(wave10);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave6);
        spawnMaps.Add(wave7);
        spawnMaps.Add(wave8);
        spawnMaps.Add(wave9);
        spawnMaps.Add(wave10);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave6);
        spawnMaps.Add(wave7);
        spawnMaps.Add(wave8);
        spawnMaps.Add(wave9);
        spawnMaps.Add(wave10);
        spawnMaps.Add(wave8);
        spawnMaps.Add(wave9);
        spawnMaps.Add(wave10);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave9);
        spawnMaps.Add(wave10);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave8);
        spawnMaps.Add(wave10);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave5);

    }
}
