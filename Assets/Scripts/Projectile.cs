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
        print(transform.position);

        knockback = attack.knockback;
    }
    void Update()
    {
        //Debug.Log(transform.up);
        transform.position += transform.up * attack.speed;

    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(spawnPos, transform.position);
        print("Distance: " + distance);

        if (distance >= projectileRange)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(attack.damage);
            Destroy(gameObject);
        }
    }
}