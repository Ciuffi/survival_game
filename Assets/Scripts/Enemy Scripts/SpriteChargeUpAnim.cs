using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChargeUpAnim : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    private Color startColor;
    public Color newColor;


    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void ChargeUpColorChange(float castTime)
    {
        StartCoroutine(LerpSpriteColorCoroutine(castTime));
    }

    private IEnumerator LerpSpriteColorCoroutine(float castTime)
    {
        // Save the start time and color
        float startTime = Time.time;
        Color startColor = spriteRend.color;
        Debug.Log(castTime);
        while (Time.time - startTime < castTime)
        {
            // Calculate the lerp value based on the elapsed time
            float t = (Time.time - startTime) / castTime;
            t = Mathf.Clamp01(t); // Clamp the value between 0 and 1

            // Lerp the color from the start color to red
            Color lerpedColor = Color.Lerp(startColor, Color.red, t);
            spriteRend.color = lerpedColor;

            yield return null;
        }

        // Set the final color to red
        spriteRend.color = startColor;
    }
}
