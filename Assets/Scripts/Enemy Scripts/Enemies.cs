using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies
{
    public GameObject lootBox;

    public GameObject melee_slow_1, melee_slow_2;
    public GameObject melee_fast_1, melee_fast_2;
    public GameObject melee_flyby_1;

    public GameObject ranged_proj_1, ranged_proj_2;


    public void LoadEnemies()
    {
        melee_slow_1 = Resources.Load("Enemy/Melee_basic/EnemyMelee_slow_1", typeof(GameObject)) as GameObject;
        melee_slow_2 = Resources.Load("Enemy/Melee_basic/EnemyMelee_slow_2", typeof(GameObject)) as GameObject;
        melee_fast_1 = Resources.Load("Enemy/Melee_basic/EnemyMelee_fast_1", typeof(GameObject)) as GameObject;
        melee_fast_2 = Resources.Load("Enemy/Melee_basic/EnemyMelee_fast_2", typeof(GameObject)) as GameObject;
        melee_flyby_1 = Resources.Load("Enemy/Melee_basic/EnemyMelee_flyby_1", typeof(GameObject)) as GameObject;

        ranged_proj_1 = Resources.Load("Enemy/Ranged_Projectile/EnemyRanged_1", typeof(GameObject)) as GameObject;
        ranged_proj_2 = Resources.Load("Enemy/Ranged_Projectile/EnemyRanged_2", typeof(GameObject)) as GameObject;

        lootBox = Resources.Load("Enemy/LootBox", typeof(GameObject)) as GameObject;
    }
}
