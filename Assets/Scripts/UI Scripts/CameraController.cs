using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    public float zoomSpeed;
    public float targetPercentIncrease;
    private float originalSize;
    private float currentSize;
    private bool zoomStarted;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        originalSize = Camera.main.orthographicSize;
        currentSize = originalSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position;
        transform.position = new Vector3(player.position.x, player.position.y, -10);

        if (!zoomStarted)
        {
            return;
        }

        Camera.main.orthographicSize += zoomSpeed * Time.unscaledDeltaTime;
        float percentIncrease = (Camera.main.orthographicSize - currentSize) / currentSize;

        if (percentIncrease >= targetPercentIncrease)
        {
            zoomStarted = false;
            currentSize = Camera.main.orthographicSize;
        }
    }
    public void StartZoom()
    {
        zoomStarted = true;
    }
}
