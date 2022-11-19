using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DamagePopupText : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float moveYspeed;
    public float disappearTimer;
    public float disappearSpeed;
    public Color textColor;

    public float scaleUpAmount;
    public float scaleDownAmount;
    const float disappearTimerMax = 0.6f;


    float alphaSpeed;

    public void Setup(float damageAmount)
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        int damageRounded = Mathf.CeilToInt(damageAmount);
        textMesh.SetText(damageRounded.ToString());
           

        // if ( crit == true ) {
        
            //textMesh.fontSize = increased
        
        //} else { reset fontsize }
   
    }

    // Update is called once per frame 
    public void Update()
    {
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;


        if (disappearTimer > disappearTimerMax * 0.8f) {
            // stretch
            transform.localScale += Vector3.one * scaleUpAmount * Time.deltaTime;
        }
        
        if (disappearTimer <= disappearTimerMax * 0.8f && disappearTimer > disappearTimerMax * 0.7)
        {            
            // rebound
            transform.localScale -= Vector3.one * scaleDownAmount * Time.deltaTime;
        }
        
        
        if (disappearTimer < 0) {
            alphaSpeed = disappearSpeed * Time.deltaTime;
            textMesh.color -= new Color(0, 0, 0, alphaSpeed);
            textColor = textMesh.color;
        }



        if (textColor.a < 0)
        {
            Destroy(gameObject);
        }
        if (transform.localScale.x < 0)
        {
            Destroy(gameObject);
        }
    }
}
