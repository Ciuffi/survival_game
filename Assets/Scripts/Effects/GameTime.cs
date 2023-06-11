using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static GameTime instance;

    private float _time;

    private bool _isPaused;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!_isPaused)
        {
            _time += Time.deltaTime;
        }
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Unpause()
    {
        _isPaused = false;
    }

    public void resetTime()
    {
        _time = 0;
    }

    public float GetTime()
    {
        return _time;
    }
}
