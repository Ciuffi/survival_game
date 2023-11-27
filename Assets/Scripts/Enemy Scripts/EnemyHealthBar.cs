using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Transform healthFill;
    private Transform parentTransform;
    private Vector3 originalScale;

    void Start()
    {
        healthFill = transform.GetChild(0);
        parentTransform = transform.parent;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (parentTransform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    public void SetHealth(float healthPercent)
    {
        StartCoroutine(AnimateHealthChange(healthPercent));
    }

    private IEnumerator AnimateHealthChange(float targetHealthPercent)
    {
        float duration = 0.25f; // Duration of the animation
        float elapsed = 0; // Time elapsed
        float startScale = healthFill.localScale.x; // Starting scale

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentScale = Mathf.Lerp(startScale, targetHealthPercent, elapsed / duration);
            healthFill.localScale = new Vector3(currentScale, 1f, 1f);
            yield return null;
        }

        // Ensure it ends exactly at the target scale
        healthFill.localScale = new Vector3(targetHealthPercent, 1f, 1f);
    }
}
