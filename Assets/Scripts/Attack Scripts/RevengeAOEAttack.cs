using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengeAOEAttack : MonoBehaviour
{
    public float colliderActiveDuration = 0.1f; 
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();
    private CircleCollider2D colliderComponent;

    public delegate void OnHitEnemy(GameObject enemy);
    public event OnHitEnemy onHitEnemy;

    public float sizeMultiplier;
    private PlayerCharacterStats playerStats;

    public float knockbackForce;

    void Start()
    {
        colliderComponent = GetComponent<CircleCollider2D>();
        StartCoroutine(DisableColliderAfterDuration(colliderActiveDuration));
        playerStats = GameObject.FindWithTag("Player").GetComponent<StatsHandler>().stats;

        transform.localScale *= 1 + (playerStats.projectileSizeMultiplier + playerStats.meleeSizeMultiplier) / 2;
    }

    IEnumerator DisableColliderAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        colliderComponent.enabled = false; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !hitEnemies.Contains(other.gameObject))
        {
            hitEnemies.Add(other.gameObject); 
            onHitEnemy?.Invoke(other.gameObject);
        }
    }
}
