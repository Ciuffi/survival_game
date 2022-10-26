using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;
    public Vector3 directionToPlayer;
    public Vector3 localScale;
    public Vector3 knockDirection;
    public float moveSpeed = 3f;

    float stopDistance;
    public float stopDistanceMin;
    public float stopDistanceMax;

    public bool isMelee;

    public float health;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(Player)) as Player;
        localScale = transform.localScale;

        stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);

    }


    private void FixedUpdate()
    {
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        
        directionToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (isMelee == true) //melee 
        {
            if (distance <= stopDistance)
            {
                StopMoving();
            }
            else
            {
                MoveEnemy();

            }

        } else //ranged 
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
        if (col.gameObject.tag == "Attack")
        {
            knockDirection = rb.transform.position - col.gameObject.transform.position;
     
            rb.AddForce(knockDirection.normalized * col.gameObject.GetComponent<Projectile>().knockback);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
