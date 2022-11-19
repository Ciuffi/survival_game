using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DamagePopupText : MonoBehaviour
{
    public TextMeshPro textMesh;

    public float moveSpeed;
    public float slowSpeed;
    public float disappearTimer;
    public float disappearSpeed;
    public Color textColor;

    public float scaleUpAmount;
    public float scaleDownAmount;
    const float disappearTimerMax = 0.6f;
    float alphaSpeed;
    Vector3 moveVector;

    static int sortingOrder;

    public void Setup(float damageAmount, bool isCrit)
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        int damageRounded = Mathf.CeilToInt(damageAmount);
        textMesh.SetText(damageRounded.ToString());
        moveVector = new Vector3(0.5f, 1) * moveSpeed;

        if ( isCrit == true ) {

            textMesh.fontSize = 80;
            textColor = new Color32(255, 79, 79, 255);
            textMesh.color = textColor;

        }
        else {
            textMesh.fontSize = 50;
            textColor = new Color32(255, 255, 255, 255);
            textMesh.color = textColor;
        }

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
   
    }

    // Update is called once per frame 
    public void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * slowSpeed * Time.deltaTime;

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
