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
                    .WithSpawnTimer(0)
                    .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}

                });
        EnemySpawn wave6 = new EnemySpawn()
           .WithDirection(Random.Range(135, 180))
            .WithDistance(Random.Range(0.1f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave7 = new EnemySpawn()
           .WithDirection(Random.Range(185, 230))
            .WithDistance(Random.Range(0.1f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave8 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
            .WithDistance(Random.Range(0.1f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });


        EnemySpawn wave9 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(-1, 0))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(5)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.lootBox, 100}
          });
        
        EnemySpawn wave10 = new EnemySpawn()
            .WithDirection(Random.Range(0, 360))
           .WithDistance(Random.Range(-1, 0))
          .WithEnemiesPerWave(1)
        .WithSpawnTimer(10)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.lootBox, 100}
          });

        EnemySpawn wave11 = new EnemySpawn()
           .WithDirection(Random.Range(0, 45))
           .WithDistance(Random.Range(0, 1))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(1)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });
        EnemySpawn wave12 = new EnemySpawn()
           .WithDirection(Random.Range(135, 180))
           .WithDistance(Random.Range(0, 1))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(1)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave13 = new EnemySpawn()
           .WithDirection(Random.Range(185, 230))
           .WithDistance(Random.Range(0, 1))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(1)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave14 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
           .WithDistance(Random.Range(0, 1))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(1)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave15 = new EnemySpawn()
           .WithDirection(Random.Range(0, 45))
           .WithDistance(Random.Range(1, 2))
           .WithEnemiesPerWave(3)
           .WithSpawnTimer(5)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });
        EnemySpawn wave16 = new EnemySpawn()
           .WithDirection(Random.Range(135, 180))
           .WithDistance(Random.Range(1, 2))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(5)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave17 = new EnemySpawn()
           .WithDirection(Random.Range(185, 230))
           .WithDistance(Random.Range(1, 2))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(5)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave18 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
           .WithDistance(Random.Range(1, 2))
           .WithEnemiesPerWave(3)
           .WithSpawnTimer(5)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_fast_1, 50}
       });

        EnemySpawn wave19 = new EnemySpawn()
           .WithDirection(Random.Range(0, 45))
           .WithDistance(Random.Range(0.1f, 0.3f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_slow_2, 50}

        });

        EnemySpawn wave20 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
            .WithDistance(Random.Range(0.1f, 0.3f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(0)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_slow_2, 50}
       });

       EnemySpawn wave21 = new EnemySpawn()
           .WithDirection(Random.Range(0, 45))
           .WithDistance(Random.Range(0.2f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(2)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_slow_2, 50}

        });

        EnemySpawn wave22 = new EnemySpawn()
           .WithDirection(Random.Range(315, 355))
            .WithDistance(Random.Range(0.2f, 0.5f))
           .WithEnemiesPerWave(2)
           .WithSpawnTimer(2)
           .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.melee_slow_1, 50},
                {enemies.melee_slow_2, 50}
       });

        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave11);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave13);
        spawnMaps.Add(wave14);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);
        spawnMaps.Add(wave9);

        spawnMaps.Add(wave1);
        spawnMaps.Add(wave2);
        spawnMaps.Add(wave3);
        spawnMaps.Add(wave4);
        spawnMaps.Add(wave5);
        spawnMaps.Add(wave6);
        spawnMaps.Add(wave7);
        spawnMaps.Add(wave8);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave11);
        spawnMaps.Add(wave12);
        spawnMaps.Add(wave13);
        spawnMaps.Add(wave14);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);

        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);
        spawnMaps.Add(wave15);
        spawnMaps.Add(wave16);
        spawnMaps.Add(wave17);
        spawnMaps.Add(wave18);
        spawnMaps.Add(wave19);
        spawnMaps.Add(wave20);
        spawnMaps.Add(wave21);
        spawnMaps.Add(wave22);
        spawnMaps.Add(wave10);

    }
}
