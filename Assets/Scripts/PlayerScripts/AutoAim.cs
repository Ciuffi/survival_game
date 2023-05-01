using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public float aimRange;
    public SpriteRenderer rangeVisualizerSprite;
    public LayerMask enemyLayer;
    public WpnSpriteRotation weaponSpriteRotation;

    public GameObject currentTarget;
    public bool targetFound;

    void Start()
    {
        UpdateVisualizerSpriteScale();
        weaponSpriteRotation = FindObjectOfType<WpnSpriteRotation>();
    }

    void Update()
    {
        UpdateCurrentTarget();
        weaponSpriteRotation.SetAutoAim(targetFound, currentTarget);
    }


    void UpdateCurrentTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aimRange, enemyLayer);
        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (Collider2D col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();

            if (enemy != null && !enemy.isDead)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.gameObject;
                }
            }
        }

        currentTarget = closestEnemy;
        targetFound = currentTarget != null;
    }

    void UpdateVisualizerSpriteScale()
    {
        if (rangeVisualizerSprite != null)
        {
            rangeVisualizerSprite.transform.localScale = new Vector3(aimRange * 1.5f, aimRange * 1.5f, 1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aimRange);
    }
}