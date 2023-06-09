using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScan : MonoBehaviour
{
    private AstarPath astarPath;
    private bool isScanning = false; // To avoid scanning when a scan is already underway

    // Start is called before the first frame update
    void Start()
    {
        astarPath = FindObjectOfType<AstarPath>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (astarPath != null && !isScanning)
            {
                StartCoroutine(ScanWithDelay(2.0f)); // Delay scanning for 2 seconds
            }
        }
    }

    IEnumerator ScanWithDelay(float delay)
    {
        isScanning = true; // A scan is underway
        yield return new WaitForSeconds(delay); // Wait for delay seconds
        astarPath.Scan();
        isScanning = false; // Scan completed
    }
}
