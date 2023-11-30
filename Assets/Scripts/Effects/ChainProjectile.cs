using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainProjectile : MonoBehaviour
{
    public float chainSpeed; // Chain speed - set this as needed

    private int remainingChains;
    private float chainStatDecayPercent;
    private float chainRange;
    private GameObject currentTarget;
    private float currentDamage;

    public void Initialize(int chainTimes, float chainStatDecayPercent, float chainRange, GameObject initialTarget, float initialDamage, float chainSpeed)
    {
        this.remainingChains = chainTimes;
        this.chainStatDecayPercent = chainStatDecayPercent;
        this.chainRange = chainRange;
        this.currentTarget = initialTarget;
        this.currentDamage = initialDamage;
        this.chainSpeed = chainSpeed;
    }

    private void Start()
    {
        Chain();
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            Destroy(gameObject);

        }
    }

    private void Chain()
    {
        if (currentTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        // Find next target
        GameObject nextTarget = FindNearestEnemy(currentTarget.transform.position, chainRange, currentTarget);

        if (nextTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        // move chain
        StartCoroutine(MoveChain(this.gameObject, transform.position, nextTarget.transform.position, chainSpeed, () =>
        {
            if (nextTarget != null && remainingChains > 0)
            {
                // Update target and damage for next chain
                currentTarget = nextTarget;
                currentDamage *= 1 - chainStatDecayPercent;
                remainingChains--;
                Chain();
            }
            else
            {
                Destroy(gameObject);
            }
        }));
    }

    private GameObject FindNearestEnemy(Vector3 center, float range, GameObject exclude = null)
    {
        Collider2D[] results = new Collider2D[100];
        int numResults = Physics2D.OverlapCircleNonAlloc(center, range, results);

        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < numResults; i++)
        {
            GameObject potentialTarget = results[i].gameObject;

            // Skip if the potential target is the excluded GameObject
            if (potentialTarget == exclude)
                continue;

            if (potentialTarget.tag == "Enemy")
            {
                float distanceSqr = (potentialTarget.transform.position - center).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closestEnemy = potentialTarget;
                }
            }
        }

        return closestEnemy;
    }

    IEnumerator MoveChain(GameObject chain, Vector3 startPos, Vector3 endPos, float chainSpeed, Action callback)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPos, endPos);
        while (true)
        {
            float distCovered = (Time.time - startTime) * chainSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            chain.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            if (fractionOfJourney >= 1) break;
            yield return null;
        }
        // Deal damage
        currentTarget.GetComponent<Enemy>().TakeDamage(Mathf.Max(currentDamage, 1), false);
        callback?.Invoke();
    }
}