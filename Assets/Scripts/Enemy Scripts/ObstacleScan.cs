using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScan : MonoBehaviour
{
    public float health;
    Rigidbody2D rb;

    public GameObject deathParticles;
    public float fadeOutSpeed;

    public float shakeIntensity = 0.5f; // Maximum distance to shake
    public float shakeSpeed = 1.0f; // Speed of shaking
    public float shakeDuration = 0.5f; // How long the shake lasts
    public float maxShakeIntensity = 2.0f; // Maximum allowed shake intensity
    private bool isShaking = false;
    private Vector3 originalPosition;

    public GameObject damageText;
    public float damageTick = 1f;
    private float lastDamageTime = -Mathf.Infinity;

    public Material defaultMaterial;
    public Material newMaterial;
    private bool resetMaterial = false;
    public SpriteRenderer spriteRend;
    public SpriteRenderer shadowSprite;
    public float flashDuration = 0.15f;

    public DropItem[] dropItems; 
    public float xpAmount; 

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultMaterial = spriteRend.material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //PathScanController.Instance.RequestScan();
            TakeEnemyDamage(other.GetComponent<Enemy>().damage, false);
        } 
    }

    private void TakeEnemyDamage(float amount, bool isCrit)
    {
        if (Time.time - lastDamageTime < damageTick)
        {
            return;
        }

        TakeDamage(amount, isCrit); 
        lastDamageTime = Time.time;
    }

    public void TakeDamage(float amount, bool isCrit)
    {
        if (health <= 0) return;
        spriteRend.material = newMaterial;
        if (!resetMaterial)
        {
            StartCoroutine(ResetMaterial());
            resetMaterial = true;
        }

        Vector3 popupPosition = rb.position;
        popupPosition.x = Random.Range(rb.position.x - 0.075f, rb.position.x + 0.075f);
        popupPosition.y = Random.Range(rb.position.y, rb.position.y + 0.1f);
        DamagePopupText damagePopup = Instantiate(damageText, popupPosition, Quaternion.identity).GetComponent<DamagePopupText>();

        if (isCrit == true)
        {
            damagePopup.GetComponent<DamagePopupText>().Setup(amount, true, false);
        }
        else
        {
            damagePopup.GetComponent<DamagePopupText>().Setup(amount, false, false);
        }

        health -= amount;
        shakeIntensity = Mathf.Min(shakeIntensity + amount * 0.1f, maxShakeIntensity); // Increase intensity
        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    private void HandleDrops()
    {
        foreach (var dropItem in dropItems)
        {
            if (dropItem.doesDrop && Random.value <= dropItem.dropChance)
            {
                int dropCount = Random.Range(dropItem.minDrop, dropItem.maxDrop + 1);
                for (int i = 0; i < dropCount; i++)
                {
                    Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 0.8f; // 0.5f is the variance range
                    GameObject spawnedItem = Instantiate(dropItem.itemPrefab, spawnPosition, Quaternion.identity);

                    if (spawnedItem.GetComponent<EXPHandler>() != null)
                    {
                        spawnedItem.GetComponent<EXPHandler>().xpAmount = xpAmount;
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            HandleDrops(); 
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            StartCoroutine(FadeOutAndDestroy()); 
            enabled = false;
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            float alpha = originalColor.a;
            Color shadowColor = shadowSprite.color;
            float shadowAlpha = shadowSprite.color.a;

            while (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeOutSpeed;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                shadowSprite.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, shadowAlpha);
                yield return null;
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator Shake()
    {
        if (!isShaking)
        {
            isShaking = true;
            originalPosition = transform.position;
            float endTime = Time.time + shakeDuration;

            while (Time.time < endTime)
            {
                Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeIntensity;
                transform.position = new Vector3(randomPoint.x, randomPoint.y, originalPosition.z);
                yield return new WaitForSeconds(1f / shakeSpeed);
            }

            transform.position = originalPosition; // Reset position
            isShaking = false;
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);
        spriteRend.material = defaultMaterial;
        resetMaterial = false;
    }

    [System.Serializable] // Make it visible in the inspector
    public class DropItem
    {
        public GameObject itemPrefab; // The prefab to drop
        public bool doesDrop; // Whether the item drops or not
        public float dropChance; // Drop chance (0-1 range, where 1 is 100% chance)
        public int minDrop; // Minimum number of items to drop
        public int maxDrop; // Maximum number of items to drop
    }
}
