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

    public bool waiting = true;
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
    public bool hasTriggered;
    public bool isGathered = false;

    public int xpTier;

    public float bounceHeight,
        bounceSpeed,
        bounceDecay;
    public float rotationAmount = 1f; // adjust this to change the rotation amount
    public float rotationDecayRate = 0.5f; // adjust this to change the rate at which the rotation speed decays

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
        player = GameObject.FindWithTag("Player");
        waitTimer = waitTime;
        hasTriggered = false;
        StartBouncing(bounceHeight, bounceSpeed, bounceDecay);
    }

    public void StartBouncing(float startHeight, float startSpeed, float decayRate)
    {
        StartCoroutine(BounceCoroutine(startHeight, startSpeed, decayRate));
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator BounceCoroutine(float startHeight, float startSpeed, float decayRate)
    {
        float startY = transform.position.y;
        float bounceTimer = 0f;
        float bounceHeight = startHeight;
        float bounceSpeed = startSpeed;

        while (bounceHeight > 0f)
        {
            // update the bounce timer
            bounceTimer += Time.deltaTime * bounceSpeed;

            // calculate the new position based on the timer and height
            Vector3 newPos = transform.position;
            newPos.y = startY + Mathf.Sin(bounceTimer) * bounceHeight;

            // update the position
            transform.position = newPos;

            // reduce the bounce height over time
            bounceHeight -= decayRate * Time.deltaTime;
            if (bounceHeight < 0f)
            {
                bounceHeight = 0f;
            }

            yield return null;
        }
    }

    private IEnumerator RotateCoroutine()
    {
        float currentRotation = 0f;
        float direction = Random.Range(0f, 1f) < 0.5f ? -1f : 1f; // Randomly choose rotation direction

        float randomAmount = Random.Range(0, rotationAmount);

        while (true)
        {
            currentRotation += randomAmount * Time.deltaTime * direction;

            // update the rotation
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // reduce the rotation speed over time
            randomAmount -= rotationDecayRate * Time.deltaTime;
            if (randomAmount < 0f)
            {
                randomAmount = 0f;
            }

            if (randomAmount == 0f) // End the coroutine if rotation speed is zero
            {
                yield break;
            }

            yield return null;
        }
    }

    public void UpdateXpTier()
    {
        xpTier = 0;
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

    public IEnumerator Move()
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
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                -currentSpeed * Time.deltaTime
            );

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
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                currentSpeed * Time.deltaTime
            );

            if (distancefromPlayer <= consumeDistance)
            {
                player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
                Instantiate(particleSystem, transform.position, Quaternion.identity);
                isGathered = true;
                Destroy(gameObject);

                yield break; // Exit the coroutine immediately after destroying the object
            }

            // Check if the GameObject still exists before the next loop iteration
            if (this == null || gameObject == null)
                yield break;

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pickupDistance = player.GetComponent<StatsHandler>().stats.pickupRange;

        distancefromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distancefromPlayer < pickupDistance && !hasTriggered)
        {
            StartCoroutine(Move());
            hasTriggered = true;
        }
    }
}
