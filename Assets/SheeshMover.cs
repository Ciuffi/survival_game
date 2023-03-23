using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SheeshMover : MonoBehaviour
{
    public string[] textList;
    public int spawnRate = 1;
    public float spawnFrequency = 1f;
    public float despawnDelay = 5.0f;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minUpwardForce = 5f;
    public float maxUpwardForce = 20f;
    public float minRotationSpeed = 0f;
    public float maxRotationSpeed = 100f;

    public GameObject text;
    private TextMeshProUGUI textPrefab;

    void Start()
    {
        textPrefab = text.GetComponent<TextMeshProUGUI>();
        StartCoroutine(SpawnTexts());

    }

    IEnumerator SpawnTexts()
    {
        while (true)
        {
            
            for (int i = 0; i < spawnRate; i++)
            {
                int randomIndex = Random.Range(0, textList.Length);
                string selectedText = textList[randomIndex];

                // Instantiate new text object
                TextMeshProUGUI textInstance = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);
                textInstance.text = selectedText;

                // Randomize size, rotation speed, and upward force
                float randomSize = Random.Range(minSize, maxSize);
                textInstance.fontSize *= randomSize;

                float randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                float randomRotation = Random.Range(0f, 360f);
                textInstance.GetComponent<Rigidbody2D>().angularVelocity = randomRotation * Mathf.Deg2Rad * randomRotationSpeed;

                float randomUpwardForce = Random.Range(minUpwardForce, maxUpwardForce);
                Vector2 randomForceDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                textInstance.GetComponent<Rigidbody2D>().AddForce(randomForceDirection * randomUpwardForce, ForceMode2D.Impulse);

                // Destroy text object after delay
                Destroy(textInstance.gameObject, despawnDelay);

            }

            // Wait for spawnFrequency seconds before spawning next text
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
