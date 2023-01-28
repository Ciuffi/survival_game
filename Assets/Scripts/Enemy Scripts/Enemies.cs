using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies
{
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject lootBox;
    public GameObject bossRage;
    public GameObject rangedAOE;
    public GameObject rageEnemy;

    public void LoadEnemies()
    {
        meleeEnemy = Resources.Load("Enemy/EnemyMelee", typeof(GameObject)) as GameObject;
        rangedEnemy = Resources.Load("Enemy/EnemyRanged", typeof(GameObject)) as GameObject;
        rageEnemy = Resources.Load("Enemy/EnemyRage", typeof(GameObject)) as GameObject;
        lootBox = Resources.Load("Enemy/LootBox", typeof(GameObject)) as GameObject;
        bossRage = Resources.Load("Enemy/BossRage", typeof(GameObject)) as GameObject;
        rangedAOE = Resources.Load("Enemy/EnemyRanged_AOE", typeof(GameObject)) as GameObject;
    }
}
