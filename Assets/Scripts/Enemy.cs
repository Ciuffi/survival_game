using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;
    public Vector3 directionToPlayer;
    public Vector3 localScale;
    public float moveSpeed = 3f;

    float stopDistance;
    public float stopDistanceMin;
    public float stopDistanceMax;

    public bool isMelee;


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
        directionToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (isMelee == true) //melee just run at u
        {
            MoveEnemy();

        } else //ranged units stop at a distance
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

    // Update is called once per frame
    void Update()
    {

    }
}
