using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheeshMover : MonoBehaviour
{
    public float waitTime;
    public float speed;
    public float deathTime;

    private bool startedMoving = false;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!startedMoving)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                startedMoving = true;
                timer = 0.0f;
            }
        }
        else
        {
            timer += Time.deltaTime;

            if (timer <= deathTime)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
            }
        }
    }
}
