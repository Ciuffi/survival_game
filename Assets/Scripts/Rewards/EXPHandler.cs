using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPHandler : MonoBehaviour
{
    public GameObject player;

    public GameObject particleSystem;
    public bool isAutoCollect;

    public float speed;
    public float speedMultiplier;
    public float waitTime;

    private bool waiting = true;
    private bool movingTowardsPlayer = false;
    private Vector3 startPosition;
    private float currentSpeed;
    private float waitTimer;

    public float xpAmount;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
        player = GameObject.FindWithTag("Player");
        waitTimer = waitTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (isAutoCollect)
        {

            if (waiting)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    waiting = false;
                    movingTowardsPlayer = true;
                }
            }
            else if (movingTowardsPlayer)
            {
                currentSpeed += speedMultiplier * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);

                if (transform.position == player.transform.position)
                {
                    player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (isAutoCollect)
        {
            return;
        }

        if (col.gameObject.tag == "Player" && !isAutoCollect)
        {
            player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
            Instantiate(particleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
