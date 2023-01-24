using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPHandler : MonoBehaviour
{
    public GameObject player;
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
        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                waiting = false;
                movingTowardsPlayer = true;
            }
        } else if (movingTowardsPlayer)
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
