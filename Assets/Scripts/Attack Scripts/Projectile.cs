using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack attack;

    GameObject Camera;
    GameObject ComboManager;

    public float damage;
    public float projectileRange;
    public float knockback;

    public Vector2 spawnPos;

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

    public float playerShakeTime, playerShakeStrength, playerShakeRotation;

    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera");
        ComboManager = GameObject.FindWithTag("ComboManager");

        spawnPos.x = transform.position.x;
        spawnPos.y = transform.position.y;
        damage = attack.damage;
        knockback = attack.knockback;
        critChance = attack.critChance;
        critDmg = attack.critDmg;

        float critRoll;

        critRoll = Random.value;

        if (critChance >= critRoll)
        { //CRITS
            damage = damage * critDmg;
            isCrit = true;

        }
        else
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
        }
        else
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


                GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, alphaSpeed);
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
        }
        else
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
                col.gameObject.GetComponent<LootBox>().TakeDamage(damage, false);

            }
            col.gameObject.GetComponent<Enemy>().damageTickCounter(damageTick);
            ComboManager.GetComponent<ComboTracker>().IncreaseCount(1);
            ComboManager.GetComponent<ScreenShakeController>().StartShake(0.25f, 0.2f, 5f);

            if (isMelee == false && pierce <= 0)
            {
                Destroy(gameObject);
            }
            else if (isMelee == false && pierce > 0)
            {
                pierce -= 1;
            }
        }
        else if (col.gameObject.name == "Player" && attack.owner.GetTransform().tag == "Enemy")
        {


            float multiplier = col.gameObject.GetComponent<StatsHandler>().damageMultipler;
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(attack.damage * multiplier);
            ComboManager.GetComponent<ComboTracker>().ResetCount();
            Camera.GetComponent<ScreenShakeController>().StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);

            if (isMelee == false)
            {
                Destroy(gameObject);
            }
        }
        else if (col.gameObject.tag == "Loot" && attack.owner.GetTransform().name == "Player")
        {
            if (isCrit == true)
            {
                col.gameObject.GetComponent<LootBox>().TakeDamage(damage, true);
            }
            else
            {
                col.gameObject.GetComponent<LootBox>().TakeDamage(damage, false);

            }
            ComboManager.GetComponent<ComboTracker>().IncreaseCount(1);
            ComboManager.GetComponent<ScreenShakeController>().StartShake(0.25f, 0.2f, 5f);

            if (isMelee == false && pierce <= 0)
            {
                Destroy(gameObject);
            }
            else if (isMelee == false && pierce > 0)
            {
                pierce -= 1;
            }

        }

    }
}