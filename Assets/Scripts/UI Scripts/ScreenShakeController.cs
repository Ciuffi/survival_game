using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public float maxShakePower;

    private float shakeTimeLeft;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;
    private float rotationMultiplier;

    public bool isCombo;
    Vector3 originalPos;
    private Transform parentTransform;
    private Vector3 localPosition;
    private Quaternion localRotation;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        if (isCombo)
        {
            parentTransform = transform.parent.transform;
            localPosition = transform.localPosition;
            localRotation = transform.localRotation;
        }
    }

    // Update is called once per frame

    private void LateUpdate()
    { 

        if (shakeTimeLeft > 0)
        {
            shakeTimeLeft -= Time.unscaledDeltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.unscaledDeltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.unscaledDeltaTime);
        }

        if (shakeTimeLeft <= 0 && isCombo == true)
        {
            transform.position = parentTransform.TransformPoint(localPosition);
            transform.rotation = parentTransform.rotation * localRotation;
        }
    }
    
    public void StartShake (float length, float power, float rotation)
    {
        if (shakeTimeLeft <= 0)
        {
            // check if the new shake power will exceed the maximum threshold
            if (shakePower + power > maxShakePower)
            {
                // cap the shake power at the maximum value
                shakePower = maxShakePower;
            }
            else
            {
                // add the new power value to the existing shake power
                shakePower += power;
            }

            shakeTimeLeft = length;
            shakeFadeTime = shakePower / length;

            shakeRotation = shakePower * rotation;
            rotationMultiplier = rotation;
        }
        else
        {
            // reset
            shakeTimeLeft = 0;
            shakePower = 0;
            shakeFadeTime = 0;
            shakeRotation = 0;
            rotationMultiplier = 0;

            // call StartShake again with the new parameters
            StartShake(length, power, rotation);
        }
    }
}
