using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHitVFX : MonoBehaviour
{
    public float deathTimer;
    public float disappearTimer;
    public float disappearSpeed;

    public float scaleUpAmount;
    public float scaleDownAmount;
    const float disappearTimerMax = 0.8f;
    float alphaSpeed;
    Vector3 moveVector;

    static int sortingOrder;

    SpriteRenderer sprite;

    public bool noSpriteRend;


    // Start is called before the first frame update
    void Start()
    {
        if (!noSpriteRend)
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();
            sortingOrder++;
            sprite.sortingOrder = sortingOrder;
        }
        
   
    }

    // Update is called once per frame
    void Update()
    {
    
        disappearTimer -= Time.deltaTime;
        deathTimer -= Time.deltaTime;

        if (disappearTimer > disappearTimerMax * 0.8f)
        {
            // stretch
            transform.localScale += Vector3.one * scaleUpAmount * Time.deltaTime;
        }

        if (disappearTimer <= disappearTimerMax * 0.8f && disappearTimer > disappearTimerMax * 0.7)
        {
            // rebound
            transform.localScale -= Vector3.one * scaleDownAmount * Time.deltaTime;
        }


        if (disappearTimer < 0 && !noSpriteRend)
        {
            alphaSpeed = disappearSpeed * Time.deltaTime;
            sprite.color -= new Color(0, 0, 0, alphaSpeed);
            
        }



        if (deathTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
