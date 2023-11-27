using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public GameObject player;

    public float pickupDistance;
    private float consumeDistance = 0.5f;
    public float baseConsumeDistance = 0.1f; 

    public float speed;
    public float speedMultiplier;
    public float waitTime;
    public float maxAwayDistance;

    public bool waiting = true;
    private bool movingTowardsPlayer = false;
    private Vector3 startPosition;
    private float currentSpeed;
    private float waitTimer;
    private float elapsedTimeSinceStart = 0f;

    private float distancefromPlayer;
    private bool movingAway;
    private Vector3 capturedPos;
    public bool hasTriggered;
    public bool isGathered = false;
    private List<GameObject> itemsToGather = new List<GameObject>();

    public float bounceHeight,
        bounceSpeed,
        bounceDecay;
    public float rotationAmount = 1f; // adjust this to change the rotation amount
    public float rotationDecayRate = 0.5f; // adjust this to change the rate at which the rotation speed decays
    public GameObject particleSystem;

    // Action options
    public bool recoverHealth;
    public bool gainGold;
    public bool pickupAll;

    // Amounts
    public float recoverHealthAmount;
    public int gainGoldAmount;
    private GoldTracker goldManager;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        currentSpeed = speed;
        player = GameObject.FindWithTag("Player");
        waitTimer = waitTime;
        hasTriggered = false;
        StartBouncing(bounceHeight, bounceSpeed, bounceDecay);
        goldManager = FindObjectOfType<GoldTracker>();
    }


    public IEnumerator Move()
    {
        Debug.Log("huh");
        Debug.Log(waiting);

        while (waiting)
        {
            waitTimer -= Time.deltaTime;
            Debug.Log("waiting");

            if (waitTimer <= 0)
            {
                Debug.Log("done waiting");

                waiting = false;
                movingAway = true;
            }
            Debug.Log("done waiting 2");

            capturedPos = transform.position;
            Debug.Log(capturedPos);

            yield return null;
        }
        Debug.Log("finish waiting");


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

            float effectiveConsumeDistance = consumeDistance;
            if (recoverHealth || pickupAll)
            {
                effectiveConsumeDistance = baseConsumeDistance;
            }

            if (distancefromPlayer <= effectiveConsumeDistance)
            {
                PerformAction();
                Instantiate(particleSystem, transform.position, Quaternion.identity);
                isGathered = true;
                if (!pickupAll)
                {
                    StartCoroutine(FadeOutAndDestroy(0.5f));
                }
                yield break;
            }

            // Check if the GameObject still exists before the next loop iteration
            if (this == null || gameObject == null)
                yield break;

            yield return null;
        }

    }

    public  void PerformAction()
    {
        if (recoverHealth)
        {
            player.GetComponent<StatsHandler>().AddHealth(recoverHealthAmount);
        }
        if (gainGold)
        {
            goldManager.IncreaseCount(gainGoldAmount);
        }
        if (pickupAll)
        {
            PickupAllItems();
            StartCoroutine(CheckAllItemsGathered());
        }
    }

    private void PickupAllItems()
    {
        EXPHandler[] allEXPHandlers = FindObjectsOfType<EXPHandler>();
        foreach (EXPHandler expHandler in allEXPHandlers)
        {
            expHandler.hasTriggered = true;
            StartCoroutine(expHandler.Move());
            itemsToGather.Add(expHandler.gameObject);
        }

        ItemHandler[] allItemHandlers = FindObjectsOfType<ItemHandler>();
        foreach (ItemHandler itemHandler in allItemHandlers)
        {
            if (!itemHandler.pickupAll)
            {
                itemHandler.hasTriggered = true;
                StartCoroutine(itemHandler.Move());
                itemsToGather.Add(itemHandler.gameObject);
            }
        }
    }

    private IEnumerator CheckAllItemsGathered()
    {
        bool allGathered = false;
    while (!allGathered)
    {
        allGathered = true;

        foreach (var item in itemsToGather)
        {
            if (item != null) // Check if the item hasn't been destroyed
            {
                ItemHandler itemHandler = item.GetComponent<ItemHandler>();
                EXPHandler expHandler = item.GetComponent<EXPHandler>();

                // Check if either handler exists and if the item is not gathered
                if ((itemHandler != null && !itemHandler.isGathered) ||
                    (expHandler != null && !expHandler.isGathered))
                {
                    allGathered = false;
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds, adjust as needed
    }
        StartCoroutine(FadeOutAndDestroy(0.5f));
    }


    // Update is called once per frame
    void Update()
    {
        elapsedTimeSinceStart += Time.deltaTime;
        if (elapsedTimeSinceStart > 0.5f) 
        {
            distancefromPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (!hasTriggered)
            {
                pickupDistance = player.GetComponent<StatsHandler>().stats.pickupRange;
                if (distancefromPlayer < pickupDistance && !hasTriggered)
                {
                    StartCoroutine(Move());
                    hasTriggered = true;
                }
            }
        }
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

    private IEnumerator FadeOutAndDestroy(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                yield return null;
            }
        }

        Destroy(gameObject);
    }
}
