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

    public bool isHoming;
    public float rotateSpeed;
    private float turnSpeed;

    public bool isDoT;
    public float dotDuration;
    public float dotDamage;
    public float dotTickRate;

    public bool isSplit;
    public int splitAmount;
    public float splitStatPercentage; //determines percent of stats carried over into split projectiles

    public bool isChain;
    public int chainTimes;
    public float chainStatDecayPercent; //determines percent of stats removed after each chain
    public float chainRange;
    public float chainSpeed;
    private GameObject chainPrefab;

    public bool isSplitProjectile; //to be turned on after splitting to acquire scaled stats

    public bool hasDeathrattle;
    public GameObject deathSpawn;

    public float wpnProjSizeMultiplier;
    public float wpnMeleeSizeMultiplier;
    private List<SpriteRenderer> spriteRenderers;

    float moveSpeed;
    private Vector2 targetDirection = Vector2.zero;

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
        spawnPos.x = transform.position.x;
        spawnPos.y = transform.position.y;
        critChance = attack.stats.critChance;
        critDmg = attack.stats.critDmg;

        if (isSplitProjectile) // scale down stats if it's a split projectile
        {
            pierce = Mathf.RoundToInt(attack.stats.pierce * splitStatPercentage);
            if (!isMelee)
            {
                damage = attack.stats.damage * splitStatPercentage;

            }
            else //we pass damage in Attack.cs to Melee for combo scaling
            {
                damage *= splitStatPercentage;
            }
            knockback = attack.stats.knockback * splitStatPercentage;
            projectileRange = attack.stats.range * splitStatPercentage;
            moveSpeed = attack.stats.speed * splitStatPercentage;
            wpnProjSizeMultiplier = attack.stats.projectileSize * splitStatPercentage;
            wpnMeleeSizeMultiplier = attack.stats.meleeSize * splitStatPercentage;
            active = active * attack.stats.activeMultiplier + attack.stats.activeDuration;
            hoverTimer = hoverTimer * attack.stats.activeMultiplier + attack.stats.activeDuration;
        }
        else
        {
            pierce = attack.stats.pierce;
            if (!isMelee)
            {
                damage = attack.stats.damage;
            }
            knockback = attack.stats.knockback;
            projectileRange = attack.stats.range;
            moveSpeed = attack.stats.speed;
            wpnProjSizeMultiplier = attack.stats.projectileSize;
            wpnMeleeSizeMultiplier = attack.stats.meleeSize;
            active = active * attack.stats.activeMultiplier + attack.stats.activeDuration;
            hoverTimer = hoverTimer * attack.stats.activeMultiplier + attack.stats.activeDuration;
        }


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

        isHoming = attack.stats.isHoming;

        isSplit = attack.stats.isSplit;
        splitAmount = attack.stats.splitAmount;
        splitStatPercentage = attack.stats.splitStatPercentage;

        isChain = attack.stats.isChain;
        chainTimes = attack.stats.chainTimes;
        chainStatDecayPercent = attack.stats.chainStatDecayPercent;
        chainRange = attack.stats.chainRange;
        chainSpeed = attack.stats.chainSpeed;
        if (isChain) { chainPrefab = Resources.Load<GameObject>("Projectiles/ChainProjectile"); }

        hitEnemies = new List<GameObject>();
        timers = new Dictionary<GameObject, float>();
        hitFirstEnemy = false;
        hoverTime = hoverTimer;

        if (isThrown)
        {
            damage =
                attack.stats.thrownDamage;
            pierce = 10 + attack.stats.pierce;
            projectileRange = 6 + (attack.stats.range / 2);
            knockback = 0.25f + (attack.stats.knockback / 2);
        }

        GetComponent<Collider2D>().enabled = true;


        // Instantiate your list
        spriteRenderers = new List<SpriteRenderer>();

        // Get SpriteRenderer from this gameObject, if exists
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (sr != null) spriteRenderers.Add(sr);

        // Get SpriteRenderers from all child objects
        SpriteRenderer[] childSR = this.GetComponentsInChildren<SpriteRenderer>();
        spriteRenderers.AddRange(childSR);

        turnSpeed = Random.Range(rotateSpeed / 3, rotateSpeed);
    }

    void Update()
    {
        if (isMelee == false)
        {
            float distance = Vector2.Distance(spawnPos, transform.position);

            if (!isHover) //regular projectile
            {
                if (isHoming)
                {
                    // If we don't have a target direction or the target has been hit, find a new target
                    if (targetDirection == Vector2.zero || Vector2.Dot(transform.up, targetDirection) > 0.999f)
                    {
                        GameObject target = FindNearestEnemy(transform.position, projectileRange);
                        if (target != null)
                        {
                            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
                            targetDirection = dirToTarget;
                        }
                    }

                    // If we have a target direction, rotate towards it and move
                    if (targetDirection != Vector2.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                    }
                }

                // Always move regardless of whether it's homing or if there's a target
                transform.position += transform.up * moveSpeed * Time.deltaTime * 60;


                float alphaSpeed;
                Vector3 scaleUp;

                if (distance >= projectileRange)
                {
                    alphaSpeed = disappearSpeed * Time.deltaTime;
                    scaleUp = new Vector3(
                        scaleSpeed * Time.deltaTime,
                        scaleSpeed * Time.deltaTime,
                        0
                    );

                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color c = sr.color;
                        c.a -= alphaSpeed;
                        sr.color = c;
                    }

                    transform.localScale -= scaleUp;
                    GetComponent<Collider2D>().enabled = false;

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

                }

                if (transform.localScale.x < 0 || GetComponent<SpriteRenderer>().color.a <= 0)
                {
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

                        foreach (SpriteRenderer sr in spriteRenderers)
                        {
                            Color c = sr.color;
                            c.a -= alphaSpeed;
                            sr.color = c;
                        }

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

                        foreach (SpriteRenderer sr in spriteRenderers)
                        {
                            Color c = sr.color;
                            c.a -= alphaSpeed;
                            sr.color = c;
                        }

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
                    if (isHoming)
                    {
                        // If we don't have a target direction or the target has been hit, find a new target
                        if (targetDirection == Vector2.zero || Vector2.Dot(transform.up, targetDirection) > 0.999f)
                        {
                            GameObject target = FindNearestEnemy(transform.position, projectileRange);
                            if (target != null)
                            {
                                Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
                                targetDirection = dirToTarget;
                            }
                        }

                        // If we have a target direction, rotate towards it and move
                        if (targetDirection != Vector2.zero)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                        }
                    }

                    transform.position += transform.up * moveSpeed * Time.deltaTime * 60;
                }
            }
        }
        else //isMelee
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

                foreach (SpriteRenderer sr in spriteRenderers)
                {
                    Color c = sr.color;
                    c.a -= alphaSpeed;
                    sr.color = c;
                }

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
                        StartCoroutine(RemoveEnemyAfterDelay(enemy));
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

    private IEnumerator RemoveEnemyAfterDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(0.1f);
        hitEnemies.Remove(enemy);
    }

    private GameObject FindNearestEnemy(Vector3 center, float range, GameObject exclude = null)
    {
        Collider2D[] results = new Collider2D[100];
        int numResults = Physics2D.OverlapCircleNonAlloc(center, range, results);

        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < numResults; i++)
        {
            GameObject potentialTarget = results[i].gameObject;

            // Skip if the potential target is the excluded GameObject
            if (potentialTarget == exclude)
                continue;

            if (potentialTarget.tag == "Enemy")
            {
                float distanceSqr = (potentialTarget.transform.position - center).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closestEnemy = potentialTarget;
                }
            }
        }

        return closestEnemy;
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
            float finalDotDamage;
            GameObject enemy = col.gameObject;

            if (!hitEnemies.Contains(enemy)) //if enemy is not within hitDetection List
            {
                critRoll = Random.value; //roll for crit
                if (critChance >= critRoll)
                { //CRITS
                    finalDamage = damage * critDmg;
                    finalDotDamage = dotDamage * critDmg;
                    isCrit = true;
                }
                else
                {
                    //no crit
                    finalDamage = damage;
                    finalDotDamage = dotDamage;
                    isCrit = false;
                }

                hitEnemies.Add(enemy); //add enemy to hitList
                timers[enemy] = damageTickDuration;

                if (isDoT)
                {
                    col.gameObject.GetComponent<Enemy>().StartDoT(finalDotDamage, dotTickRate, dotDuration, isCrit);
                }

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

                if (isSplit && !isSplitProjectile) // we only split on the first collision with an enemy
                {
                    float randomOffsetAngle = Random.Range(0, 360) * Mathf.Deg2Rad; // Random offset angle in radians

                    for (int i = 0; i < splitAmount; i++)
                    {
                        float angle = i * 360f / splitAmount * Mathf.Deg2Rad + randomOffsetAngle;
                        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

                        GameObject newProjectile = Instantiate(gameObject, transform.position, rotation);
                        Projectile newProjectileScript = newProjectile.GetComponent<Projectile>();

                        // Pass the necessary variables to the new projectile
                        newProjectileScript.isSplitProjectile = true;
                        if (splitStatPercentage < 0.4)
                        {
                            newProjectile.transform.localScale *= 0.4f;
                        }
                        else
                        {
                            newProjectile.transform.localScale *= splitStatPercentage;
                        }

                        // If it's a melee attack, move the object
                        if (isMelee)
                        {
                            newProjectile.transform.position += direction * attack.stats.meleeSpacerGap;
                        }



                    }

                    // Make sure we don't split again
                    isSplit = false;
                }


                if (isChain)
                {
                    GameObject chainTarget = FindNearestEnemy(transform.position, chainRange, col.gameObject);

                    GameObject chainProjectileGO = Instantiate(chainPrefab, transform.position, Quaternion.identity);
                    ChainProjectile chainProjectile = chainProjectileGO.GetComponent<ChainProjectile>();

                    chainProjectile.Initialize(chainTimes, chainStatDecayPercent, chainRange, chainTarget, damage, chainSpeed);

                    if (isMelee)
                    {
                        chainProjectileGO.transform.localScale *= 5f;
                    }

                    isChain = false;
                }
            }
            else
            {
                return;
            }

            if (!isMelee && pierce < 0)
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
