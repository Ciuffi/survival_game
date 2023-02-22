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

    public List<Sprite> spriteList; // List of sprites
    public List<int> xpTierThresholds; // List of XP thresholds

    public float pickupDistance;
    public float distancefromPlayer;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
        player = GameObject.FindWithTag("Player");
        waitTimer = waitTime;
    }

    public void UpdateXpTier()
    {
        int xpTier = 0;
        for (int i = 0; i < xpTierThresholds.Count; i++)
        {
            if (xpAmount >= xpTierThresholds[i])
            {
                xpTier = i + 1;
            }
            else
            {
                break;
            }
        }
        // Set the sprite based on the xpTier
        if (xpTier > 0 && xpTier <= spriteList.Count)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[xpTier - 1];
        }
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
        } else //doesnt auto collect
        {
            distancefromPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distancefromPlayer < pickupDistance)
            {
                player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
                Instantiate(particleSystem, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }


        }
    }

}
