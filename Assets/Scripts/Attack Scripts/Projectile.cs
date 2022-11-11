using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack attack;

    public float projectileRange;
    public float knockback;

    public Vector2 spawnPos;

    public GameObject self;
    public Vector3 scaleRate;

    public bool isBounce;
    public float bounceRange;
    public int bounceTimes;

    public bool isMelee;
    public float meleeTime;
    public float startup;
    public float active;
    public float recovery;
    public float damageTick;

    


    void Start()
    {
        spawnPos.x = transform.position.x;
        spawnPos.y = transform.position.y;

        knockback = attack.knockback;
        
    }

  

    void Update()
    {
       
        if (isMelee == false)
        {
            transform.position += transform.up * attack.speed;
        } else
        {
            meleeTime += Time.deltaTime;
            if (meleeTime < startup)
            {
                self.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
                self.GetComponent<Collider2D>().enabled = false;
            }
            else if (meleeTime >= startup && meleeTime < (startup + active))
            {
                self.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                self.GetComponent<Collider2D>().enabled = true;
            }
            else if (meleeTime >= (startup + active))
            {
                self.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
                self.GetComponent<Collider2D>().enabled = false;
            }
        }


    }

  

    private void FixedUpdate()
    {
        if (isMelee == false)
        {
            float distance = Vector2.Distance(spawnPos, transform.position);

            if (distance >= projectileRange)
            {
                Destroy(gameObject);
            }
        } else
        {
            if (meleeTime >= startup + active + recovery)
            {
                Destroy(gameObject);
            }

        }



    }
    
  

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == null || attack.owner == null) return;
        if (col.gameObject.tag == "Enemy" && attack.owner.GetTransform().name == "Player")
        {
            
            col.gameObject.GetComponent<Enemy>().TakeDamage(attack.damage);
            col.gameObject.GetComponent<Enemy>().damageTickCounter(damageTick);


            if (isMelee == false)
            {
               Destroy(gameObject);
            } 
        }
        else if (col.gameObject.name == "Player" && attack.owner.GetTransform().tag == "Enemy")
        {
            
        
             float multiplier = col.gameObject.GetComponent<StatsHandler>().damageMultipler;
             col.gameObject.GetComponent<StatsHandler>().TakeDamage(attack.damage * multiplier);
          
            if (isMelee == false)
            {
                Destroy(gameObject);
            }
        }
    }
}