using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class deathRattleAttack : MonoBehaviour
{
    public Attack attack;
    GameObject Player;
    GameObject Camera;
    private Vector3 startPos;

    public float damage;
    public float critChance;
    public float critDmg;
    public float knockback;

    public float startup;
    public float active;

    //public float recovery;

    public float scaleSpeed;
    public float disappearSpeed;

    public GameObject onHitParticle;
    public float playerShakeTime,
        playerShakeStrength,
        playerShakeRotation;

    public float damageTickDuration;
    private List<GameObject> hitEnemies;
    private Dictionary<GameObject, float> timers;

    float critRoll;
    float finalDamage;
    bool isCrit;

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

    public bool isDoT;
    public float dotDuration;
    public float dotDamage;
    public float dotTickRate;

    private float meleeTime;

    private List<SpriteRenderer> spriteRenderers;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        startPos = transform.position;

        damage = attack.stats.damage;
        knockback = attack.stats.knockback;
        critChance = attack.stats.critChance;
        critDmg = attack.stats.critDmg;

        isMagnet = attack.stats.isMagnet;
        magnetStrength = attack.stats.magnetStrength;
        magnetDuration = attack.stats.magnetDuration + attack.stats.effectDuration;

        isSlow = attack.stats.isSlow;
        slowPercentage = attack.stats.slowPercentage;
        slowDuration = attack.stats.slowDuration + attack.stats.effectDuration;

        isStun = attack.stats.isStun;
        stunDuration = attack.stats.stunDuration + attack.stats.effectDuration;

        isDoT = attack.stats.isDoT;
        dotDuration = attack.stats.dotDuration += attack.stats.effectDuration;
        dotDamage = attack.stats.dotDamage;
        dotTickRate = attack.stats.dotTickRate;

        active = active * attack.stats.activeMultiplier + attack.stats.activeDuration;


        hitEnemies = new List<GameObject>();
        timers = new Dictionary<GameObject, float>();
        if (isAnimated)
        {
            animator = GetComponent<Animator>();
        }

        // Instantiate your list
        spriteRenderers = new List<SpriteRenderer>();

        // Get SpriteRenderer from this gameObject, if exists
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (sr != null) spriteRenderers.Add(sr);

        // Get SpriteRenderers from all child objects
        SpriteRenderer[] childSR = this.GetComponentsInChildren<SpriteRenderer>();
        spriteRenderers.AddRange(childSR);
    }

    // Update is called once per frame
    void Update()
    {
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

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                Color c = sr.color;
                c.a -= alphaSpeed;
                sr.color = c;
            }

            transform.localScale -= scaleUp;
            GetComponent<Collider2D>().enabled = false;
        }

        if (transform.localScale.x < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        if (col.gameObject.tag == "Enemy")
        {
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
                    magnetTarget
                        .GetComponent<Enemy>()
                        .StartMagnet(magnetStrength, magnetDuration, magnetStartPos, false);
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

                if (isCrit == true)
                {
                    col.gameObject.GetComponent<Enemy>().TakeDamage(finalDamage, true);
                    Vector3 knockDirection = (col.transform.position - startPos).normalized;
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
                    Vector3 knockDirection = (col.transform.position - startPos).normalized;
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
            }
            else
            {
                return;
            }
        }
    }
}
