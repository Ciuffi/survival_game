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
            .WithEnemiesPerWave(1)
            .WithSpawnTimer(2)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
            {enemies.meleeEnemy, 100}
        });

        EnemySpawn wave2 = new EnemySpawn()
        .WithDirection(270)
        .WithDistance(5)
        .WithEnemiesPerWave(1)
        .WithSpawnTimer(3)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
            {enemies.meleeEnemy, 25},
            {enemies.rangedEnemy, 75}

        });

        EnemySpawn wave3 = new EnemySpawn()
        .WithDirection(180)
        .WithDistance(0)
        .WithEnemiesPerWave(1)
        .WithSpawnTimer(4)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 100}
        });

        EnemySpawn wave4 = new EnemySpawn()
        .WithDirection(90)
        .WithDistance(5)
        .WithEnemiesPerWave(1)
        .WithSpawnTimer(5)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 25},
                {enemies.rangedEnemy, 75}
        });

        EnemySpawn wave5 = new EnemySpawn()
            .WithDirection(180)
            .WithDistance(5)
            .WithEnemiesPerWave(2)
            .WithSpawnTimer(2)
            .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 40},
                {enemies.rangedEnemy, 40},
                {enemies.lootBox, 10}
        });

        EnemySpawn wave6 = new EnemySpawn()
        .WithDirection(270)
        .WithDistance(0)
        .WithEnemiesPerWave(1)
        .WithSpawnTimer(4)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.rageEnemy, 100}

        });

        EnemySpawn wave7 = new EnemySpawn()
        .WithDirection(180)
        .WithDistance(10)
        .WithEnemiesPerWave(2)
        .WithSpawnTimer(5)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 50},
                {enemies.rangedEnemy, 50}
        });

        EnemySpawn wave8 = new EnemySpawn()
        .WithDirection(90)
        .WithDistance(0)
        .WithEnemiesPerWave(3)
        .WithSpawnTimer(6)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.rageEnemy, 100}
        });

        EnemySpawn wave9 = new EnemySpawn()
        .WithDirection(0)
        .WithDistance(5)
        .WithEnemiesPerWave(4)
        .WithSpawnTimer(10)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 50},
                {enemies.rangedEnemy, 15},
                {enemies.rageEnemy, 15},
                {enemies.lootBox, 20}
        });

        EnemySpawn wave10 = new EnemySpawn()
        .WithDirection(180)
        .WithDistance(15)
        .WithEnemiesPerWave(4)
        .WithSpawnTimer(10)
        .WithEnemyMap(new Dictionary<GameObject, int>(){
                {enemies.meleeEnemy, 50},
                {enemies.rangedEnemy, 20},
                {enemies.rageEnemy, 20},
                {enemies.lootBox, 10}
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


    }
}
