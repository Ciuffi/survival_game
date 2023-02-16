using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack attack;

    GameObject Camera;
    GameObject ComboManager;
    GameObject Player;

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
    //public float recovery;


    public float scaleSpeed;
    public float disappearSpeed;

    float critChance;
    float critDmg;
    public bool isCrit;

    public GameObject onHitParticle;

    public float playerShakeTime, playerShakeStrength, playerShakeRotation;

    public float damageTickDuration;
    private List<GameObject> hitEnemies;
    private Dictionary<GameObject, float> timers;
    float critRoll;

    private float finalDamage;
    public Vector3 startPos;

    public bool isThrown;

    public bool isHover;
    public float hoverTimer;
    private float hoverTime;
    public bool hitFirstEnemy;

    Animator animator;
    public bool isAnimated;

    public bool isMagnet;
    public float magnetStrength;
    public float magnetDuration;

    public bool isSlow;
    public float slowActiveSpeed;
    public float slowPercentage;
    public float slowDuration;

    private bool isMagnetizing = false;
    private Transform magnetTarget;
    private float magnetStartTime;
    private Vector3 magnetStartPos;


    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera");
        ComboManager = GameObject.FindWithTag("ComboManager");
        Player = GameObject.FindWithTag("Player");
        if (isAnimated)
        {
            animator = GetComponent<Animator>();
        }

        pierce = attack.pierce;
        spawnPos.x = transform.position.x;  
        spawnPos.y = transform.position.y;
        damage = attack.damage * Player.GetComponent<StatsHandler>().damageMultipler;
        knockback = attack.knockback;
        critChance = attack.critChance + Player.GetComponent<StatsHandler>().critChance;
        critDmg = attack.critDmg + Player.GetComponent<StatsHandler>().critDmg;
        projectileRange = attack.range;

        hitEnemies = new List<GameObject>();
        timers = new Dictionary<GameObject, float>();
        startPos = transform.position;
        hitFirstEnemy = false;
        hoverTime = hoverTimer;

        if (isThrown)
        {
            damage = damage * (attack.shotsPerAttack / 2 + 2) / 3;
            pierce = 0;
            projectileRange = 8;
            //knockback = 20;
        }

    }



    void Update()
    {

        if (isMelee == false)
        {
            float distance = Vector2.Distance(spawnPos, transform.position);

            if (!isHover) //regular projectile
            {
                transform.position += transform.up * attack.speed;

                if (distance >= projectileRange)
                {
                    Destroy(gameObject);
                }

            } else //HOVER projectile - moves until end distance or first enemy hit, then stops
            {
                float alphaSpeed;
                Vector3 scaleUp;

                if (distance < projectileRange && !hitFirstEnemy)
                {
                    transform.position += transform.up * attack.speed;

                } else if (distance > projectileRange || hitFirstEnemy)
                {                  
                    hoverTime -= Time.deltaTime;
                    if (hoverTime <= 0)
                    {
                        alphaSpeed = disappearSpeed * Time.deltaTime;
                        scaleUp = new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, 0);

                        GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, alphaSpeed);
                        transform.localScale -= scaleUp;
                        GetComponent<Collider2D>().enabled = false;
                    }

                    if (transform.localScale.x < 0)
                    {
                        Destroy(gameObject);
                    }

                }
            }
        }
        else
        {
            meleeTime += Time.deltaTime;
            float alphaSpeed;
            Vector3 scaleUp;

            if (meleeTime < startup)
            {
                if (isAnimated)
                {
                    animator.SetBool("IsActive", false);
                    animator.SetBool("IsRecovery", false);
                }
                GetComponent<Collider2D>().enabled = false;
            }
            else if (meleeTime >= startup && meleeTime < (startup + active))
            {
                if (isAnimated)
                {
                    animator.SetBool("IsActive", true);
                    animator.SetBool("IsRecovery", false);
                    GetComponent<Collider2D>().enabled = true;
                }
            }
            else if (meleeTime >= (startup + active))
            {
                if (isAnimated)
                {
                    animator.SetBool("IsActive", false);
                    animator.SetBool("IsRecovery", true);
                }

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


        // Check the timers for each object in the list
        if (hitEnemies.Count > 0)
        {
            List<GameObject> hitEnemiesCopy = hitEnemies.ToList();
            foreach (GameObject enemy in hitEnemiesCopy)
            {
                if (timers.ContainsKey(enemy))
                {
                    timers[enemy] -= Time.deltaTime;

                    if (timers[enemy] <= 0)
                    {
                        hitEnemies.Remove(enemy);
                        timers.Remove(enemy);
                    }
                }

                else
                {
                    return;
                }
            }
        }

    }



    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }
        if (pierce < 0) return;
        if (col.gameObject == null || attack.owner == null)
        {
            return;
        }

        if (col.gameObject.tag == "Enemy" && attack.owner.GetTransform().name == "Player")
        {
            hitFirstEnemy = true;

            GameObject enemy = col.gameObject;

            if (!hitEnemies.Contains(enemy)) //if enemy is not within hitDetection List
            {
                critRoll = Random.value; //roll for crit
                if (critChance >= critRoll)
                { //CRITS
                    finalDamage = damage * critDmg;
                    isCrit = true;

                }
                else
                {
                    //no crit 
                    finalDamage = damage;
                    isCrit = false;
                }

                hitEnemies.Add(enemy); //add enemy to hitList
                timers[enemy] = damageTickDuration;

                //apply magnetizing effect
                if (isMagnet)
                {
                    magnetTarget = col.transform; //set collider
                    magnetStartTime = Time.time;
                    magnetStartPos = transform.position; //set target
                    magnetTarget.GetComponent<Enemy>().StartMagnet(magnetStrength, magnetDuration, magnetStartPos);
                }

                //apply slow effect
                if (isSlow)
                {
                    col.GetComponent<Enemy>().StartSlow(slowActiveSpeed, slowPercentage, slowDuration);

                }


                if (isCrit == true) //deal damage
                {
                    col.gameObject.GetComponent<Enemy>().TakeDamage(finalDamage, true);
                    Camera.GetComponent<ScreenShakeController>().StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);
                    Instantiate(onHitParticle, col.gameObject.transform.position, Quaternion.identity);


                }
                else
                {
                    col.gameObject.GetComponent<Enemy>().TakeDamage(finalDamage, false);
                    Camera.GetComponent<ScreenShakeController>().StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);
                    Instantiate(onHitParticle, col.gameObject.transform.position, Quaternion.identity);

                }

                if (isMelee == false)
                {
                    pierce -= 1;
                }
               

            } else
            {
                return;
            }
      
            if (isMelee == false && pierce < 0)
            {
                Destroy(gameObject);
            }   
        }
    }


}