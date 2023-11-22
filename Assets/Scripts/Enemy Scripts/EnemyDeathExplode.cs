using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathExplode : MonoBehaviour
{
    public float damage; //passed down from enemy
    public float knockback, activeTime; //need to be set
    private float timer;

    public GameObject onHitParticle;
    private bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!hasExploded)
        {
            if (col.CompareTag("Player"))
            {
                Instantiate(onHitParticle, col.transform.position, Quaternion.identity);
                col.gameObject.GetComponent<StatsHandler>().TakeDamage(damage, gameObject);
            }

            if (col.CompareTag("Enemy"))
            {
                Instantiate(onHitParticle, col.transform.position, Quaternion.identity);

                float enemyDmg = damage * 10;
                col.gameObject.GetComponent<Enemy>().TakeDamage(enemyDmg, true);

                Vector3 knockDirection = (col.transform.position - transform.position).normalized;
                col.gameObject.GetComponent<Enemy>().ApplyKnockback(knockback, knockDirection);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (hasExploded)
        {
            Destroy(gameObject);
        } else
        {
            timer += Time.deltaTime;
            if (timer >= activeTime)
            {
                hasExploded = true;
            }
        }

    }
}
