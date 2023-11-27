using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{

    public bool facingLeft;
    public GameObject player;
    private SpriteRenderer renderer;
    private Enemy enemyScript; 

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        enemyScript = transform.parent.GetComponent<Enemy>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.isMagnetizing)
        {
            // Use the initial side for flipping during magnetizing
            renderer.flipX = enemyScript.initialPlayerSideLeft == facingLeft;
        }
        else
        {
            // Regular flipping logic
            Vector3 directionToPlayer = player.transform.position - transform.position;
            if (directionToPlayer.x >= 0)
            {
                renderer.flipX = facingLeft ? true : false;
            }
            else
            {
                renderer.flipX = facingLeft ? false : true;
            }
        }
    }
}
