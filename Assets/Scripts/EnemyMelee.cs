using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;
    public Vector3 directionToPlayer;
    public Vector3 localScale;
    public float moveSpeed = 3f;
    public float stopDistance = 10f;

    public bool isMelee;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(Player)) as Player;
        localScale = transform.localScale;

    }


    private void FixedUpdate()
    {
       
        MoveEnemy();
      
    }

    private void MoveEnemy()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
    }


    private void LateUpdate()
    {
         float Distance()
            {
                return Vector3.Distance(transform.position, player.transform.position);
            }
         if (Distance() <= stopDistance) 
                    {
                        //close enough. do nothing or shoot or whatever

                    } else
                     { //too far! move closer
                        
                        if (rb.velocity.x > 0)
                            {
                                transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
                         }
                            else if (rb.velocity.x < 0)
                            {
                                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                            }
                     }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
