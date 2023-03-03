using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneSpawnMap : EnemySpawnMap
{
    public LevelOneSpawnMap()
    {
        enemies.LoadEnemies();

        EnemySpawn wave1 = new EnemySpawn()
            .WithDirection(Random.Range(0,45))
            .WithDistance(Random.Range(0.1f,0.3f))
            .WithEnemiesPerWave(2)
            .WithSpawnTimer(0)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 100}
        });
        EnemySpawn wave2 = new EnemySpawn()
           .WithDirection(Random.Range(135, 180))
            .WithDistance(Random.Range(0.1f, 0.3f))
           .WithEnemiesPerWave(1)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 100}
       });

        EnemySpawn wave3 = new EnemySpawn()
           .WithDirection(Random.Range(185, 230))
            .WithDistance(Random.Range(0.1f, 0.3f))
           .WithEnemiesPerWave(1)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 100}
       });

        EnemySpawn wave4 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
            .WithDistance(Random.Range(0.1f, 0.3f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 100}
       });

        EnemySpawn wave5 = new EnemySpawn()
                    .WithDirection(Random.Range(0, 45))
            .WithDistance(Random.Range(0.1f, 0.5f))
                    .WithEnemiesPerWave(2)
                    .WithSpawnTimer(3)
                    .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_fast_1, 100}

                });
        EnemySpawn wave6 = new EnemySpawn()
           .WithDirection(Random.Range(135, 180))
            .WithDistance(Random.Range(0.1f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(3)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_fast_1, 100}
       });

        EnemySpawn wave7 = new EnemySpawn()
           .WithDirection(Random.Range(185, 230))
            .WithDistance(Random.Range(0.1f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(3)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_fast_1, 100}
       });

        EnemySpawn wave8 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
            .WithDistance(Random.Range(0.1f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(3)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_fast_1, 100}
       });


        EnemySpawn wave9 = new EnemySpawn()
            .WithDirection(Random.Range(0, 45))
           .WithDistance(Random.Range(-1, 0))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(35)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.lootBox, 100}
          });

        EnemySpawn wave10 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
          .WithDistance(Random.Range(-1, 0))
         .WithEnemiesPerWave(1)
       .WithSpawnTimer(120)
       .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.ranged_proj_2, 100}
         });

        EnemySpawn wave11 = new EnemySpawn()
            .WithDirection(Random.Range(0, 45))
            .WithDistance(Random.Range(0f, 0.5f))
            .WithEnemiesPerWave(1)
            .WithSpawnTimer(0)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.ranged_proj_2, 100}
        });

        EnemySpawn wave12 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
            .WithDistance(Random.Range(0f, 0.5f))
           .WithEnemiesPerWave(1)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.ranged_proj_2, 100}
       });


        spawnMaps.Add(wave5);
        spawnMaps.Add(wave6);
        spawnMaps.Add(wave7);
        spawnMaps.Add(wave8);

        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);

        spawnMaps.Add(wave9);
        spawnMaps.Add(wave11);

        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);

        spawnMaps.Add(wave5);
        spawnMaps.Add(wave6);
        spawnMaps.Add(wave7);
        spawnMaps.Add(wave8);

        spawnMaps.Add(wave10);
        spawnMaps.Add(wave12);

        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);
    }
}
