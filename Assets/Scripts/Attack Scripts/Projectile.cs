using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack attack;

    public float projectileRange;
    public float knockback;

    public Vector2 spawnPos;

    void Start()
    {
        spawnPos.x = transform.position.x;
        spawnPos.y = transform.position.y;

        knockback = attack.knockback;
    }
    void Update()
    {
        transform.position += transform.up * attack.speed;

    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(spawnPos, transform.position);

        if (distance >= projectileRange)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && attack.owner.GetTransform().name == "Player")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(attack.damage);
            Destroy(gameObject);
        }
        else if (col.gameObject.name == "Player" && attack.owner.GetTransform().tag == "Enemy")
        {
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(attack.damage);
        }
    }
}