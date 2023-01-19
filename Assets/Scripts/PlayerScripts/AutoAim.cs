using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AutoAim : MonoBehaviour
{

    public LayerMask enemyLayer;

    GameObject weapon;
    //private Vector3 facingDirection;


    public bool enemyScanned = false;
    public Transform closestEnemy;
    private ArrayList enemies = new ArrayList();
    public float equalDistance = 0.5f; // if enemies are within this distance of each other, pick middle

    public Vector3 autoAimDirection;

    // Start is called before the first frame update
    void Start()
    {
       weapon = GameObject.FindWithTag("Attack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyScanned = true;
            enemies.Add(other.transform);
            closestEnemy = FindClosestEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.transform);
            if (enemies.Count > 0)
            {
                closestEnemy = FindClosestEnemy();
            }
            else
            {
                enemyScanned = false;
                closestEnemy = null;
            }
        }
    }

    private Transform FindClosestEnemy()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        Vector2 center = new Vector2(transform.position.x, transform.position.y);
        foreach (Transform enemy in enemies)
        {
            float distance = Vector2.Distance(center, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }
        // checking if there are multiple enemies at equal distance from center
        ArrayList equalEnemies = new ArrayList();
        equalEnemies.Add(closest);
        foreach (Transform enemy in enemies)
        {
            float distance = Vector2.Distance(center, enemy.position);
            if (distance - closestDistance < equalDistance) // checking if distance is within the range
            {
                equalEnemies.Add(enemy);
            }
        }
        if (equalEnemies.Count > 1)
        {
            closest = (Transform)equalEnemies[0];
            float closestToCenter = Vector2.Distance(center, closest.position);
            for (int i = 1; i < equalEnemies.Count; i++)
            {
                Transform temp = (Transform)equalEnemies[i];
                float tempDistance = Vector2.Distance(center, temp.position);
                if (tempDistance < closestToCenter)
                {
                    closest = temp;
                    closestToCenter = tempDistance;
                }
            }
        }
        return closest;

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 playerDirection = player.GetComponent<PlayerMovement>().GetDirection();
        if (closestEnemy == null) return;
        Vector2 targetDirection = closestEnemy.position - weapon.transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Vector2 angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        //Debug.Log(angleVector);

        autoAimDirection = new Vector3 (angleVector.x, angleVector.y, 0);


        //float dot = Vector2.Dot(playerDirection.normalized, targetDirection.normalized);
        //Vector3 angleVector = new Vector3(dot, dot, 0);
        //Debug.Log(angleVector);

    }

}
