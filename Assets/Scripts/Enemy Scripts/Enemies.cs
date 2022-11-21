using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies
{
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject EliteMeleeEnemy;
    public GameObject EliteRangedEnemy;


    public void LoadEnemies()
    {
        meleeEnemy = Resources.Load("Enemy/EnemyMelee", typeof(GameObject)) as GameObject;
        rangedEnemy = Resources.Load("Enemy/EnemyRanged", typeof(GameObject)) as GameObject;
        EliteMeleeEnemy = Resources.Load("Enemy/ELITE_Melee", typeof(GameObject)) as GameObject;
        EliteRangedEnemy = Resources.Load("Enemy/ELITE_Ranged", typeof(GameObject)) as GameObject;
    }
}
