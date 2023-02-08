using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    public float zoomSpeed;
    public float targetPercentIncrease;
    private float originalSize;
    private float originalUiSize;
    private float currentSize;
    private float currentUiSize;
    private bool zoomStarted;
    public Camera UICamera;
    public bool isUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        originalSize = Camera.main.orthographicSize;
        originalUiSize = UICamera.orthographicSize;
        currentSize = originalSize;
        currentUiSize = originalUiSize;
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
        if(isUI){
            UICamera.orthographicSize += zoomSpeed * Time.unscaledDeltaTime;
        }

        float percentIncrease = (Camera.main.orthographicSize - currentSize) / currentSize;

        if (percentIncrease >= targetPercentIncrease)
        {
            zoomStarted = false;
            currentUiSize = UICamera.orthographicSize;
            currentSize = Camera.main.orthographicSize;
        }
    }
    public void StartZoom()
    {
        zoomStarted = true;
    }
}
