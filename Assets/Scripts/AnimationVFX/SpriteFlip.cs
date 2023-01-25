using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{

    public bool facingLeft;
    public GameObject player;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;

        if (enemyPosition.x < playerPosition.x)
        {
            if (facingLeft)
            {
                renderer.flipX = true;

            } else
            {
                renderer.flipX = false;

            }
        }
        else
        {
            if (facingLeft)
            {
                renderer.flipX = false;

            }
            else
            {
                renderer.flipX = true;

            }
        }
    }
}
