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
    private Vector3 startPos;

    public Vector2 spawnPos;

    public int pierce;
    public bool isBounce;
    public float bounceRange;
    public int bounceTimes;

    public bool isMelee;
    private float meleeTime;
    public float startup;
    public float active;

    //public float recovery;


    public float scaleSpeed;
    public float disappearSpeed;

    float critChance;
    float critDmg;
    public bool isCrit;

    public GameObject onHitParticle;

    public float playerShakeTime,
        playerShakeStrength,
        playerShakeRotation;

    public float damageTickDuration;
    private List<GameObject> hitEnemies;
    private Dictionary<GameObject, float> timers;
    float critRoll;

    private float finalDamage;

    public bool isThrown;

    public bool isHover;
    public bool stopOnHit;
    public float hoverTimer;
    private float hoverTime;
    public bool hitFirstEnemy;

    Animator animator;
    public bool isAnimated;

    public bool isMagnet;
    public float magnetStrength;
    public float magnetDuration;
    private Transform magnetTarget;
    private float magnetStartTime;
    private Vector3 magnetStartPos;

    public bool isSlow;
    public float slowPercentage;
    public float slowDuration;

    public bool isStun;
    public float stunDuration;

    public bool hasDeathrattle;
    public GameObject deathSpawn;

    public float wpnProjSizeMultiplier;
    public float wpnMeleeSizeMultiplier;


    float moveSpeed;

    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera");
        ComboManager = GameObject.FindWithTag("ComboManager");
        Player = GameObject.FindWithTag("Player");
        startPos = transform.position;

        if (isAnimated)
        {
            animator = GetComponent<Animator>();
        }

        pierce = attack.stats.pierce;
        damage = attack.stats.damage;
        knockback = attack.stats.knockback;
        critChance = attack.stats.critChance;
        critDmg = attack.stats.critDmg;
        spawnPos.x = transform.position.x;
        spawnPos.y = transform.position.y;
        projectileRange = attack.stats.range;
        moveSpeed = attack.stats.speed;
        wpnProjSizeMultiplier = attack.stats.projectileSize;
        wpnMeleeSizeMultiplier = attack.stats.meleeSize;

        magnetStrength *= attack.stats.effectMultiplier;
        slowPercentage *= attack.stats.effectMultiplier;

        stunDuration += attack.stats.effectDuration;
        magnetDuration += attack.stats.effectDuration;
        slowDuration += attack.stats.effectDuration;

        active = active * attack.stats.activeMultiplier + attack.stats.activeDuration;
        hoverTimer = hoverTimer * attack.stats.activeMultiplier + attack.stats.activeDuration;

        hitEnemies = new List<GameObject>();
        timers = new Dictionary<GameObject, float>();
        hitFirstEnemy = false;
        hoverTime = hoverTimer;

        if (isThrown)
        {
            damage =
                attack.stats.thrownDamage;
            pierce = 5 + attack.stats.pierce;
            projectileRange = 6 + (attack.stats.range / 2);
            knockback = 0.35f + (attack.stats.knockback / 2);
        }
        GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        if (isMelee == false)
        {
            float distance = Vector2.Distance(spawnPos, transform.position);

            if (!isHover) //regular projectile
            {
                transform.position += transform.up * moveSpeed * Time.deltaTime * 60;

                if (distance >= projectileRange)
                {
                    if (hasDeathrattle)
                    {
                        GameObject rattle = Instantiate(
                            deathSpawn,
                            transform.position,
                            Quaternion.identity
                        );
                        rattle.GetComponent<deathRattleAttack>().attack = attack;
                        Vector3 currentScale = rattle.transform.localScale;
                        rattle.transform.localScale = new Vector3(
                            currentScale.x * wpnProjSizeMultiplier,
                            currentScale.y * wpnProjSizeMultiplier,
                            currentScale.z * wpnProjSizeMultiplier
                        );
                    }
                    Destroy(gameObject);
                }
            }
            else //HOVER projectile - moves until end distance or first enemy hit, then stops
            {
                float alphaSpeed;
                Vector3 scaleUp;

                if (!stopOnHit && distance > projectileRange) // doesnt stop on hit - has to reach max range
                {
                    hoverTime -= Time.deltaTime;
                    if (hoverTime <= 0)
                    {
                        alphaSpeed = disappearSpeed * Time.deltaTime;
                        scaleUp = new Vector3(
                            scaleSpeed * Time.deltaTime,
                            scaleSpeed * Time.deltaTime,
                            0
                        );

                        GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, alphaSpeed);
                        GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, alphaSpeed);
                        transform.localScale -= scaleUp;
                        GetComponent<Collider2D>().enabled = false;

                        if (isAnimated)
                        {
                            animator.SetBool("IsRecovery", true);
                        }
                    }

                    if (transform.localScale.x < 0 || GetComponent<SpriteRenderer>().color.a <= 0)
                    {
                        if (hasDeathrattle)
                        {
                            GameObject rattle = Instantiate(
                                deathSpawn,
                                transform.position,
                                Quaternion.identity
                            );
                            rattle.GetComponent<deathRattleAttack>().attack = attack;
                            Vector3 currentScale = rattle.transform.localScale;
                            rattle.transform.localScale = new Vector3(
                                currentScale.x * wpnProjSizeMultiplier,
                                currentScale.y * wpnProjSizeMultiplier,
                                currentScale.z * wpnProjSizeMultiplier
                            );
                        }
                        Destroy(gameObject);
                    }
                }
                else if (stopOnHit && (distance > projectileRange || hitFirstEnemy)) // stops when it hits something
                {
                    hoverTime -= Time.deltaTime;
                    if (hoverTime <= 0)
                    {
                        alphaSpeed = disappearSpeed * Time.deltaTime;
                        scaleUp = new Vector3(
                            scaleSpeed * Time.deltaTime,
                            scaleSpeed * Time.deltaTime,
                            0
                        );

                        GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, alphaSpeed);
                        transform.localScale -= scaleUp;
                        GetComponent<Collider2D>().enabled = false;

                        if (isAnimated)
                        {
                            animator.SetBool("IsRecovery", true);
                        }
                    }

                    if (transform.localScale.x < 0 || GetComponent<SpriteRenderer>().color.a <= 0)
                    {
                        if (hasDeathrattle)
                        {
                            GameObject rattle = Instantiate(
                                deathSpawn,
                                transform.position,
                                Quaternion.identity
                            );
                            rattle.GetComponent<deathRattleAttack>().attack = attack;
                            Vector3 currentScale = rattle.transform.localScale;
                            rattle.transform.localScale = new Vector3(
                                currentScale.x * wpnProjSizeMultiplier,
                                currentScale.y * wpnProjSizeMultiplier,
                                currentScale.z * wpnProjSizeMultiplier
                            );
                        }
                        Destroy(gameObject);
                    }
                }
                else // if neither condition is true, keep moving the projectile
                {
                    transform.position += transform.up * moveSpeed;
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
                }
                GetComponent<Collider2D>().enabled = true;
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

        if (transform.localScale.x < 0 || GetComponent<SpriteRenderer>().color.a <= 0)
        {
            if (hasDeathrattle)
            {
                GameObject rattle = Instantiate(
                    deathSpawn,
                    transform.position,
                    Quaternion.identity
                );
                rattle.GetComponent<deathRattleAttack>().attack = attack;
                Vector3 currentScale = rattle.transform.localScale;
                rattle.transform.localScale = new Vector3(
                    currentScale.x * wpnMeleeSizeMultiplier,
                    currentScale.y * wpnMeleeSizeMultiplier,
                    currentScale.z * wpnMeleeSizeMultiplier
                );
            }
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
        if (pierce < 0)
            return;
        if (col.gameObject == null || attack.owner == null)
        {
            return;
        }

        if (col.gameObject.tag == "Enemy")
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
                    if (!isMelee)
                    {
                        magnetStartPos = transform.position; //set target
                    }
                    else
                    {
                        magnetStartPos = Player.transform.position;
                    }
                    magnetTarget
                        .GetComponent<Enemy>()
                        .StartMagnet(magnetStrength, magnetDuration, magnetStartPos, isMelee);
                }

                if (isCrit == true) //deal damage
                {
                    col.gameObject.GetComponent<Enemy>().TakeDamage(finalDamage, true);
                    Vector3 knockDirection = isMelee
                        ? (col.transform.position - transform.position).normalized
                        : transform.up;
                    col.gameObject.GetComponent<Enemy>().ApplyKnockback(knockback, knockDirection);

                    attack.OnDamageDealt(finalDamage);
                    Camera
                        .GetComponent<ScreenShakeController>()
                        .StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);
                    Instantiate(
                        onHitParticle,
                        col.gameObject.transform.position,
                        Quaternion.identity
                    );
                }
                else
                {
                    col.gameObject.GetComponent<Enemy>().TakeDamage(finalDamage, false);
                    Vector3 knockDirection = isMelee
                        ? (col.transform.position - transform.position).normalized
                        : transform.up;
                    col.gameObject.GetComponent<Enemy>().ApplyKnockback(knockback, knockDirection);
                    attack.OnDamageDealt(finalDamage);
                    Camera
                        .GetComponent<ScreenShakeController>()
                        .StartShake(playerShakeTime, playerShakeStrength, playerShakeRotation);
                    Instantiate(
                        onHitParticle,
                        col.gameObject.transform.position,
                        Quaternion.identity
                    );
                }

                //apply slow effect
                if (isSlow)
                {
                    col.GetComponent<Enemy>().StartSlow(slowPercentage, slowDuration);
                }

                //apply stun effect
                if (isStun)
                {
                    col.GetComponent<Enemy>().StartStun(stunDuration);
                }

                if (isMelee == false)
                {
                    pierce -= 1;
                }
            }
            else
            {
                return;
            }

            if (isMelee == false && pierce < 0)
            {
                if (hasDeathrattle)
                {
                    GameObject rattle = Instantiate(
                        deathSpawn,
                        transform.position,
                        Quaternion.identity
                    );
                    rattle.GetComponent<deathRattleAttack>().attack = attack;
                    Vector3 currentScale = rattle.transform.localScale;
                    rattle.transform.localScale = new Vector3(
                        currentScale.x * wpnProjSizeMultiplier,
                        currentScale.y * wpnProjSizeMultiplier,
                        currentScale.z * wpnProjSizeMultiplier
                    );
                }
                Destroy(gameObject);
            }
        }
    }
}
