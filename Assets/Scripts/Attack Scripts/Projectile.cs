using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack attack;

    public float damage;
    public float projectileRange;
    public float knockback;

    public Vector2 spawnPos;

    public GameObject ComboManager;

    public int pierce;
    public bool isBounce;
    public float bounceRange;
    public int bounceTimes;

    public bool isMelee;
    public float meleeTime;
    public float startup;
    public float active;
    public float recovery;
    public float damageTick;


    public float scaleSpeed;
    public float disappearSpeed;

    float critChance;
    float critDmg;
    public bool isCrit;

    void Start()
    {
        spawnPos.x = transform.position.x;
        spawnPos.y = transform.position.y;
        damage = attack.damage;
        knockback = attack.knockback;
        critChance = attack.critChance;
        critDmg = attack.critDmg;

        float critRoll;

        critRoll = Random.value;
        Debug.Log(critRoll);

        if (critChance >= critRoll)
        { //CRITS
            damage = damage * critDmg;
            isCrit = true;

        } else
        {
            //no crit 
            isCrit = false;
        }

    }

  

    void Update()
    {
       
        if (isMelee == false)
        {
            transform.position += transform.up * attack.speed;
        } else
        {
            meleeTime += Time.deltaTime;
            float alphaSpeed;
            Vector3 scaleUp;

            if (meleeTime < startup)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
                GetComponent<Collider2D>().enabled = false;
            }
            else if (meleeTime >= startup && meleeTime < (startup + active))
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                GetComponent<Collider2D>().enabled = true;
            }
            else if (meleeTime >= (startup + active))
            {
                alphaSpeed = disappearSpeed * Time.deltaTime;
                scaleUp = new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, 0);


                GetComponent<SpriteRenderer>().color -= new Color (0,0,0,alphaSpeed);
                transform.localScale -= scaleUp;
                GetComponent<Collider2D>().enabled = false;
            }
        }

        if (transform.localScale.x < 0)
        {
            Destroy(gameObject);
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
            

        }



    }
    
  

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == null || attack.owner == null) return;
        if (col.gameObject.tag == "Enemy" && attack.owner.GetTransform().name == "Player")
        {
            if (isCrit == true)
            {
                col.gameObject.GetComponent<Enemy>().TakeDamage(damage, true);

            }
            else
            {
                col.gameObject.GetComponent<Enemy>().TakeDamage(damage, false);

            }
            col.gameObject.GetComponent<Enemy>().damageTickCounter(damageTick);
            ComboManager.GetComponent<ComboTracker>().IncreaseCount(1);


            if (isMelee == false && pierce <= 0)
            {
               Destroy(gameObject);
            } else if (isMelee == false && pierce > 0)
            {
                pierce -= 1;
            }
        }
        else if (col.gameObject.name == "Player" && attack.owner.GetTransform().tag == "Enemy")
        {
            
        
             float multiplier = col.gameObject.GetComponent<StatsHandler>().damageMultipler;
             col.gameObject.GetComponent<StatsHandler>().TakeDamage(attack.damage * multiplier);
             ComboManager.GetComponent<ComboTracker>().ResetCount();

            if (isMelee == false)
            {
                Destroy(gameObject);
            }
        }
    }
}