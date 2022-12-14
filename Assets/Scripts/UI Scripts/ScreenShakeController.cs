using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{

    private float shakeTimeLeft;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;
    private float rotationMultiplier;

    public bool isCombo;
    Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        if (shakeTimeLeft > 0)
        {
            shakeTimeLeft -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));

        if (shakeTimeLeft <= 0 && isCombo == true)
        {
            transform.position = originalPos;
        }
    }
    
    public void StartShake (float length, float power, float rotation)
    {
        if (shakeTimeLeft <= 0)
        {
            shakeTimeLeft = length;
            shakePower = power;

            shakeFadeTime = power / length;

            shakeRotation = power * rotation;
            rotationMultiplier = rotation;
        } else
        {
            //reset
            shakeTimeLeft = 0;
            shakePower = 0;
            shakeFadeTime = 0;
            shakeRotation = 0;
            rotationMultiplier = 0;

            //again
            shakeTimeLeft = length;
            shakePower = power;
            shakeFadeTime = power / length;
            shakeRotation = power * rotation;
            rotationMultiplier = rotation;
        }
        
    }
}
