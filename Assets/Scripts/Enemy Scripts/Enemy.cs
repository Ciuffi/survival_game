using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Attacker
{
    Rigidbody2D rb;
    PlayerMovement player;
    public Vector3 directionToPlayer;
    public Vector3 localScale;
    public Vector3 knockDirection;
    public float moveSpeed = 3f;

    float stopDistance;
    public float stopDistanceMin;
    public float stopDistanceMax;

    public bool isMelee;

    public float health;
    public float xpAmount;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        localScale = transform.localScale;

        stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);

        health = 10;

    }


    private void FixedUpdate()
    {

        if (health <= 0)
        {
            player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
            Destroy(gameObject);
        }


        directionToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (isMelee == true) //melee 
        {
            if (distance <= stopDistance)
            {
                StopMoving();
                transform.LookAt(player.transform);
            }
            else
            {
                MoveEnemy();

            }

        }
        else //ranged 
        {
            if (distance <= stopDistance)
            {
                StopMoving();
            }
            else
            {
                MoveEnemy();

            }
        }

    }

    private void MoveEnemy()
    {
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
    }

    private void StopMoving()
    {
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * 0;
        Vector3 position = player.transform.position - transform.position;
        position.z = 0;
        transform.up = position;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        StopMoving();
    }


    private void LateUpdate()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack" && col.GetComponent<Projectile>().attack.owner.GetTransform().name == "Player")
        {
            knockDirection = rb.transform.position - col.gameObject.transform.position;

            rb.AddForce(knockDirection.normalized * col.gameObject.GetComponent<Projectile>().knockback);
        }
        if (col.gameObject.name == "Player")
        {
            col.GetComponent<StatsHandler>().TakeDamage(isMelee ? 2 : 1);
        }
    }

    public Vector3 GetDirection()
    {
        return directionToPlayer;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
