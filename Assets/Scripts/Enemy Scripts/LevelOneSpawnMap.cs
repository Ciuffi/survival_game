using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneSpawnMap : EnemySpawnMap
{
    public LevelOneSpawnMap()
    {
        enemies.LoadEnemies();

        EnemySpawn wave1 = new EnemySpawn()
            .WithDirection(45)
            .WithDistance(1.5f)
            .WithEnemiesPerWave(10)
            .WithSpawnTimer(90)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_flyby_1, 100}
        });

        EnemySpawn wave2 = new EnemySpawn()
            .WithDirection(135)
            .WithDistance(1.5f)
            .WithEnemiesPerWave(10)
            .WithSpawnTimer(90)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_flyby_1, 100}
        });

        EnemySpawn wave3 = new EnemySpawn()
            .WithDirection(225)
            .WithDistance(1.5f)
            .WithEnemiesPerWave(10)
            .WithSpawnTimer(90)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_flyby_1, 100}
        });

        EnemySpawn wave4 = new EnemySpawn()
            .WithDirection(315)
            .WithDistance(1.5f)
            .WithEnemiesPerWave(10)
            .WithSpawnTimer(90)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_flyby_1, 100}
        });


        EnemySpawn wave10 = new EnemySpawn()
            .WithDirection(Random.Range(0, 10))
           .WithDistance(Random.Range(-1, 0))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(0)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.lootBox, 100}
          });


        for (int i = 0; i < 4; i++)
        {
            int roller = Random.Range(1, 4);

            switch (roller)
            {
                case 1:
                    spawnMaps.Add(wave1);
                    break;
                case 2:
                    spawnMaps.Add(wave2);
                    break;
                case 3:
                    spawnMaps.Add(wave3);
                    break;
                case 4:
                    spawnMaps.Add(wave4);
                    break;
            }

        }



    }
}
