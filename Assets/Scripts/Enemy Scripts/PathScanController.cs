using UnityEngine;
using System.Collections;

public class PathScanController : MonoBehaviour
{
    public static PathScanController Instance;

    public float scanInterval = 2.0f;
    private float lastScanTime = 0f;
    private AstarPath astarPath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            astarPath = FindObjectOfType<AstarPath>();
            StartCoroutine(ScanAtIntervals());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ScanAtIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(scanInterval);
            if (astarPath != null && Time.time >= lastScanTime + scanInterval)
            {
                astarPath.Scan();
                lastScanTime = Time.time;
            }
        }
    }

    public void RequestScan()
    {
        if (Time.time >= lastScanTime + scanInterval)
        {
            astarPath.Scan();
            lastScanTime = Time.time;
        }
    }
}