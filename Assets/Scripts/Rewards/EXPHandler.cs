using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPHandler : MonoBehaviour
{
    public GameObject player;

    public GameObject particleSystem;

    public float pickupDistance;
    private float consumeDistance = 0.5f;

    public float speed;
    public float speedMultiplier;
    public float waitTime;
    public float maxAwayDistance;


    private bool waiting = true;
    private bool movingTowardsPlayer = false;
    private Vector3 startPosition;
    private float currentSpeed;
    private float waitTimer;

    public List<Sprite> spriteList; // List of sprites
    public List<int> xpTierThresholds; // List of XP thresholds
    public float xpAmount;

    private float distancefromPlayer;
    private bool movingAway;
    private Vector3 capturedPos;
    private bool hasTriggered;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
        player = GameObject.FindWithTag("Player");
        waitTimer = waitTime;
        hasTriggered = false;
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

    IEnumerator Move()
    {
        while (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                waiting = false;
                movingAway = true;
            }
            capturedPos = transform.position;
            yield return null;
        }

        while (movingAway)
        {
            currentSpeed += speedMultiplier * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -currentSpeed * Time.deltaTime);

            float distanceFromSpot = Vector3.Distance(transform.position, capturedPos);

            if (distanceFromSpot > maxAwayDistance)
            {
                movingAway = false;
                movingTowardsPlayer = true;
            }
            yield return null;
        }

        while (movingTowardsPlayer)
        {
            currentSpeed += speedMultiplier * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);

            if (distancefromPlayer <= consumeDistance)
            {
                player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
                Instantiate(particleSystem, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pickupDistance = player.GetComponent<StatsHandler>().pickupRange;

        distancefromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distancefromPlayer < pickupDistance && !hasTriggered)
        {
            StartCoroutine(Move());
            hasTriggered = true;
        }

    }
}
