using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, Attacker
{
    Rigidbody2D rb;
    GameObject player;
    public Vector3 directionToPlayer;
    public Vector3 localScale;
    public Vector3 knockDirection;

    public bool isElite;
    private Animator eliteBorder;
    public float damage;
    public float health;
    public float maxHealth;
    public float xpAmount;
    public float weight; //for knockback
    private float magnetWeightThreshold = 20f;
    private float magnetWeightEffect = 0.6f;
    private float newSpeed;

    float stopDistance;
    public float stopDistanceMin;
    public float stopDistanceMax;
    public float moveDistanceCheck;
    public bool canMove;
    public float timeCheck;
    float elapsedTime;

    public bool isMelee;
    public bool isAOE;
    public bool isArmor;
    private float armorTimer;
    public float armorTime;
    public float armorPercent; //percentile damage reduction between 0 (no reduction) and 1 (full block)
    private bool armorOn;
    public float weightIncrease; //increased knockback resistance during armor
    private float newWeight;
    private float oldWeight;

    private float currentForce;
    private bool duringKnockback;
    private float knockbackWeight, newKnockbackWeight, oldKnockbackWeight;


    public bool canDamage;
    public GameObject DamagePopup;
    public GameObject HitEffect;
    public GameObject DeathEffect;
    public GameObject EXPdrop;

    public float Iframes;
    public bool isInvuln;

    GameObject ComboManager;

    public Animator animator;
    public GameObject Sprite;
    private SpriteRenderer spriteRend;
    public Color rageColor;
    public float disappearSpeed;
    // public float shinySpeed;
    public bool isDead;
    Color color;
    Color OGcolor;

    private AIPath aiPath;
    public float speed;
    public float originalSpeed;
    private Coroutine slowCoroutine;

    public Color slowColor;

    public bool isRage;
    public float rageTriggerPercent;
    private bool rageTriggered = false;
    public float rageSpeedMod = 1.6f;
    public float rageDmgMod = 2.5f;
    private float rageSpeed;

    public bool isDash;
    public float chargeTime = 1f;
    public float dashSpeed = 20f;
    public bool canDash = true;
    private Vector3 savedPlayerPosition;
    private bool isCharging;
    private bool isDashing;
    public float dashCD;
    public float waitTime;

    public float flashDuration;
    private Material defaultMaterial;
    public Material newMaterial;
    private bool resetMaterial = false;

    private Vector3 center;

    public GameObject projectilePrefab; //projectile attack
    public float attackRange;
    public float shootChargeTime;
    public float projectileSpeed;
    public float projectileRange;
    public float projectileDamage;
    private bool canAttack = true;
    private bool attacking = false;
    private bool recovering = false;
    float shootRecoveryTimer;
    public float shootRecovery;

    public GameObject AOEPrefab; //AOE attack
    public GameObject dangerSign;
    SpriteRenderer dangerRenderer;

    public bool isDeathrattle;
    public bool isDeathExplode;
    public List<GameObject> deathRattle;
    public bool isSpawn;
    private bool spawnFinished;
    public float spawnTimer = 0.5f;

    public bool isFlyby;
    private Vector3 startingPosition; // starting position of enemy
    private Vector3 endPosition;
    public float flybyDistance = 15f; // distance to move from starting position

    private Vector3 magnetTarget;
    private float magnetStrength;
    private float magnetDuration;
    private bool magnetIsMelee;
    private float magnetStartTime;

    private float animSpeed;
    private bool isSlowing = false;
    private float currentSlowPercentage;
    private float magnetMinDistance = 1.7f;

    public bool isStunned = false;
    private float stunTimer;
    public Color stunColor;
    public GameObject shadow;
    public GameObject eliteOutline;
    
    Vector3 deathPos;
    Vector3 stunPos;
    private GameObject maxEnemiesTracker;

    public float xpSizeScaling = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        ComboManager = GameObject.FindWithTag("ComboManager");
        localScale = transform.localScale;
        canMove = true;
        maxHealth = health;
        oldWeight = weight;
        newWeight = weight + weightIncrease;
        knockbackWeight = weight / 100; //scale weight down for knockback (2 decimal places)
        oldKnockbackWeight = knockbackWeight;
        newKnockbackWeight = newWeight / 100;

        stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);

        canDamage = true;
        isDead = false;
        maxEnemiesTracker = GameObject.Find("EnemyTracker");

        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;
        color = Sprite.GetComponent<SpriteRenderer>().color;

        aiPath = GetComponent<AIPath>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        if (isElite)
        {
            eliteBorder = transform.GetChild(1).GetComponent<Animator>();
        }

        defaultMaterial = spriteRend.material;
        dangerRenderer = dangerSign.GetComponent<SpriteRenderer>();

        speed = aiPath.maxSpeed;
        originalSpeed = speed;
        animSpeed = animator.GetFloat("speed");
        rageSpeed = originalSpeed + rageSpeedMod;

        if (isFlyby)
        {
            startingPosition = transform.position; // capture starting position
            Vector3 directionToPlayer = (player.transform.position - startingPosition).normalized; // calculate direction to player
            endPosition = startingPosition + directionToPlayer * flybyDistance; // calculate end position based on direction and distance
            GetComponent<AIDestinationSetter>().target = null; // clear previous target
            GetComponent<AIDestinationSetter>().target = new GameObject().transform; // create a new empty game object as target
            GetComponent<AIDestinationSetter>().target.position = endPosition; // set target position to end position
        }

    }

    public void StartMagnet(float strength, float duration, Vector3 target, bool isMelee)
    {
        magnetStrength = strength;
        magnetDuration = duration;
        magnetIsMelee = isMelee;
        magnetTarget = target;
        magnetStartTime = Time.time;
    }

    public void StartStun(float duration)
    {
        stunTimer = duration;
        isStunned = true;
        spriteRend.color = stunColor;

        stunPos = transform.position;
    }

    private float EaseInOutCubic(float t)
    {
        if (t < 0.5f)
        {
            return 4 * t * t * t;
        }
        else
        {
            float f = ((2 * t) - 2);
            return 0.5f * f * f * f + 1;
        }
    }


    void Update()
    {

        if (isSpawn)
        {
            if (spawnTimer > 0f)
            {
                spawnTimer -= Time.deltaTime; // count down the timer
            }
            else
            {
                spawnFinished = true;
            }
        } else
        {
            spawnFinished = true;
        }

        if (isFlyby)
        {
            if (Vector3.Distance(transform.position, endPosition) < 1f)
            {
                Destroy(gameObject);
            }
        }


        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (health <= 0 && !isDead)
        {
            isDead = true;
            canMove = false;
            deathPos = transform.position;
            maxEnemiesTracker.GetComponent<MaxEnemyTracker>().DecreaseCount();

            if (xpAmount > 0)
            {
                GameObject xpDrop = Instantiate(EXPdrop, transform.position, Quaternion.identity);
                xpDrop.GetComponent<EXPHandler>().xpAmount = xpAmount;
                xpDrop.GetComponent<EXPHandler>().UpdateXpTier();
                if (xpDrop.GetComponent<EXPHandler>().xpTier > 0)
                {
                    xpDrop.transform.localScale *= 1 + (xpSizeScaling * xpDrop.GetComponent<EXPHandler>().xpTier);
                }

            }

            Vector3 deathSpawnPos = new Vector3(Random.Range(transform.position.x - 0.1f, transform.position.x + 0.1f), Random.Range(transform.position.y - 0.1f, transform.position.y + 0.1f), transform.position.z);
            Instantiate(DeathEffect, deathSpawnPos, Quaternion.identity);
            ComboManager.GetComponent<ComboTracker>().IncreaseCount(1);
            ComboManager.GetComponent<ScreenShakeController>().StartShake(0.1f, 0.1f, 0.1f);


            if (isDeathrattle)
            {
                foreach (GameObject rattle in deathRattle)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.x += Random.Range(-0.3f, 0.3f);
                    newPosition.y += Random.Range(-0.3f, 0.3f);
                    GameObject dr = Instantiate(rattle, newPosition, Quaternion.identity);
                    if (isDeathExplode)
                    {
                        dr.GetComponent<EnemyDeathExplode>().damage = damage;
                    }
                }
            }
        }

        if (health <= 0)
        {
            transform.position = deathPos;
            animator.SetBool("IsDead", true);
            if (isElite)
            {
                eliteBorder.SetBool("IsDead", true);
            }
            SetAllCollidersStatus(false);

            color = Sprite.GetComponent<SpriteRenderer>().color;
            Sprite.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -disappearSpeed * Time.deltaTime);

            if (shadow != null)
            {
                shadow.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -disappearSpeed * Time.deltaTime);
            }
            if (eliteOutline != null)
            {
                eliteOutline.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -disappearSpeed * Time.deltaTime);
            }
        }

        if (color.a <= 0)
        {

            Destroy(gameObject);
        }

        //magnetizing effect
        if (magnetDuration > 0 && Time.time < magnetStartTime + magnetDuration)
        {
            float magDistance = Vector3.Distance(transform.position, player.transform.position);
            if (magnetIsMelee) //if is meleeAttack, check distance and stop if too close to player
            {
                if (magDistance > magnetMinDistance)
                {
                    float t = (Time.time - magnetStartTime) / magnetDuration;
                    t = EaseInOutCubic(t);

                    float effectiveMagnetStrength = magnetStrength;
                    if (weight > 10f)
                    {
                        float weightReduction = magnetWeightEffect / (1 + Mathf.Exp((weight - magnetWeightThreshold) / 2));
                        effectiveMagnetStrength = Mathf.Max(effectiveMagnetStrength - weightReduction, 0f);
                    }

                    transform.position = Vector3.Lerp(transform.position, magnetTarget, t * effectiveMagnetStrength * Time.deltaTime);
                }
                else
                {
                    magnetDuration = 0;
                }
            }
            else //not melee, no need to check
            {
                float t = (Time.time - magnetStartTime) / magnetDuration;
                t = EaseInOutCubic(t);

                float effectiveMagnetStrength = magnetStrength;
                if (weight > 10f)
                {
                    float weightReduction = (((weight - 10f) / magnetWeightThreshold) * magnetWeightEffect);
                    effectiveMagnetStrength -= weightReduction;
                }

                transform.position = Vector3.Lerp(transform.position, magnetTarget, t * effectiveMagnetStrength * Time.deltaTime);
            }
        }
        else
        {
            magnetDuration = 0;
        }


        directionToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        //rage enemies
        if (isRage)
        {
            if (health / maxHealth <= rageTriggerPercent && !rageTriggered)
            {
                aiPath.maxSpeed += rageSpeedMod;
                animator.speed *= 1.5f;
                damage *= rageDmgMod;
                animator.SetBool("IsRage", true);

                if (isElite)
                {
                    eliteBorder.speed *= 1.5f;
                    eliteBorder.SetBool("IsRage", true);
                }

                rageTriggered = true;
            }

            if (rageTriggered && !isStunned && isDead == false) //back to red
            {
                spriteRend.color = new Color(rageColor.r, rageColor.g, rageColor.b);
            }

        }

        if (armorOn) //currently armored
        {
            armorTimer += Time.deltaTime;
            //Debug.Log(armorTime);

            animator.SetBool("IsArmored", true);
            if (isElite)
            {
                eliteBorder.SetBool("IsArmored", true);
            }
            StopMoving();
            weight = newWeight;
            knockbackWeight = newKnockbackWeight;

            if (armorTimer > armorTime)
            {
                animator.SetBool("IsArmored", false);
                if (isElite)
                {
                    eliteBorder.SetBool("IsArmored", false);
                }
                StartMoving();
                weight = oldWeight;
                knockbackWeight = oldKnockbackWeight;
                armorOn = false;
                armorTimer = 0;
            }
        }

            //knockback or stunned - stop enemy actions
            if (duringKnockback || isStunned)
        {
            StopMoving();
            animator.speed = 0f;
            if (isElite)
            {
                eliteBorder.speed = 0f;
            }

            
            //stun
            if (isStunned)
            {
                stunTimer -= Time.deltaTime;
                transform.position = stunPos;
                if (stunTimer <= 0)
                {
                    spriteRend.color = OGcolor;
                    isStunned = false;
                    stunTimer = 0;
                    animator.speed = 1f;
                    if (isElite)
                    {
                        eliteBorder.speed = 1f;
                    }
                    StartMoving();
                }

            } else
            {
                //knockback            
                currentForce = EasingFunction.EaseOutExpo(currentForce, 0, Time.deltaTime);

                float forceToApply = Mathf.Max(currentForce - knockbackWeight, 0);
                currentForce = (currentForce > knockbackWeight) ? forceToApply : 0f;
                transform.position += knockDirection * forceToApply;
                //Debug.Log(forceToApply);

                if (currentForce < 0.1) //knockback over
                {
                    duringKnockback = false;
                    if (!isStunned)
                    {
                        animator.speed = 1f;
                        if (isElite)
                        {
                            eliteBorder.speed = 1f;
                        }
                        StartMoving();
                    }
                }

            }          
        }
        else //perform enemy actions
        {

            if (isMelee == true) //melee 
            {
                //animator.SetFloat("Distance", distance);

                if (!isDash && !isArmor)
                {
                    if (distance <= stopDistance)
                    {
                        StopMoving();
                        transform.LookAt(player.transform);
                        transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                    }
                    else
                    {
                        StartMoving();
                    }

                }
                else if (!isDash && isArmor) //armored enemies
                {
                        if (distance <= stopDistance)
                        {
                            StopMoving();
                            transform.LookAt(player.transform);
                            transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                        }
                        else
                        {
                            StartMoving();
                        }
                }

                else if (isDash && !isArmor) // does dash
                {
                    if (distance <= stopDistance && canDash)
                    {
                        canDash = false;
                        savedPlayerPosition = player.transform.position;
                        isCharging = true;

                        StartCoroutine(Charge());
                    }

                    if (isCharging)
                    {
                        StopMoving();
                        //stop movement or animation during charging
                    }
                    if (isDashing)
                    {
                        //move towards player position
                        transform.position = Vector3.MoveTowards(transform.position, savedPlayerPosition, dashSpeed * Time.deltaTime);
                    }

                    if (Vector3.Distance(this.transform.position, savedPlayerPosition) < 1f) //arrives at location
                    {
                        isDashing = false;
                        StartMoving();
                        dashCD -= Time.deltaTime;
                    }

                    else if (dashCD <= 0)
                    {
                        canDash = true;
                        StartMoving();
                    }

                }


            }
            else //ranged 
            {
                if (!isAOE)
                {
                    //projectile attack
                    if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && canAttack)
                    {
                        attacking = true;
                        canAttack = false;
                        StopMoving();
                        StartCoroutine(ProjectileAttack());
                    }

                    if (recovering && !canAttack)
                    {
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("FollowThrough", false);

                        if (isElite)
                        {
                            eliteBorder.SetBool("IsAttacking", false);
                            eliteBorder.SetBool("FollowThrough", false);
                        }
                        shootRecoveryTimer -= Time.deltaTime;
                        if (shootRecoveryTimer <= 0)
                        {
                            recovering = false;
                            attacking = false;
                            StartMoving();
                            canAttack = true;

                        }
                    }

                }
                else
                {

                    //AOE attack 
                    if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && canAttack)
                    {
                        attacking = true;
                        canAttack = false;
                        StopMoving();
                        StartCoroutine(RangedAOEAttack(gameObject));
                    }

                    if (recovering && !canAttack)
                    {
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("FollowThrough", false);
                        if (isElite)
                        {
                            eliteBorder.SetBool("IsAttacking", false);
                            eliteBorder.SetBool("FollowThrough", false);
                        }
                        shootRecoveryTimer -= Time.deltaTime;
                        if (shootRecoveryTimer <= 0)
                        {
                            recovering = false;
                            attacking = false;
                            StartMoving();
                            canAttack = true;

                        }
                    }
                }
            }
        }

    }


    IEnumerator Charge()
    {    
        yield return new WaitForSeconds(chargeTime);
        isCharging = false;
        isDashing = true;
  
    }

    IEnumerator ProjectileAttack()
    {
        StopMoving();
        Sprite.GetComponent<SpriteChargeUpAnim>().ChargeUpColorChange(shootChargeTime);
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsAttacking", true);
        if (isElite)
        {
            eliteBorder.SetBool("IsMoving", false);
            eliteBorder.SetBool("IsAttacking", true);
        }
        dangerRenderer.enabled = true;
        
        yield return new WaitForSeconds(shootChargeTime - 0.09f);
        animator.SetBool("FollowThrough", true);
        if (isElite)
        {
            eliteBorder.SetBool("FollowThrough", true);
        }
        yield return new WaitForSeconds(0.09f);

        dangerRenderer.enabled = false;
        if (!isDead)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            // Calculate the angle of rotation using the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Rotate the projectile to face the correct direction
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            projectile.GetComponent<enemyProjectile>().direction = direction;
            projectile.GetComponent<enemyProjectile>().speed = projectileSpeed;
            projectile.GetComponent<enemyProjectile>().maxRange = projectileRange;
            projectile.GetComponent<enemyProjectile>().damage = projectileDamage;
            shootRecoveryTimer = shootRecovery;
            recovering = true;
        }

    }

    IEnumerator RangedAOEAttack(GameObject caster)
    {
        StopMoving();
        Sprite.GetComponent<SpriteChargeUpAnim>().ChargeUpColorChange(shootChargeTime);
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsAttacking", true);
        if (isElite)
        {
            eliteBorder.SetBool("IsMoving", false);
            eliteBorder.SetBool("IsAttacking", true);
        }
        dangerRenderer.enabled = true;
        Vector3 pos = player.transform.position;
        GameObject AOE = Instantiate(AOEPrefab, pos, Quaternion.identity);
      
        AOE.GetComponent<EnemyAOEProjectile>().damage = projectileDamage;
        AOE.GetComponent<EnemyAOEProjectile>().AOEChargeTime = shootChargeTime;
        AOE.GetComponent<EnemyAOEProjectile>().caster = caster;

        yield return new WaitForSeconds(shootChargeTime - 0.09f);
        animator.SetBool("FollowThrough", true);
        if (isElite)
        {
            eliteBorder.SetBool("FollowThrough", true);
        }
        yield return new WaitForSeconds(0.09f);

        shootRecoveryTimer = shootRecovery;
        recovering = true;

        dangerRenderer.enabled = false;

    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);
        if (canDamage)
        {
            spriteRend.material = defaultMaterial;
            resetMaterial = false;
        }
        else
        {
            StartCoroutine(ResetMaterial());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
    }

        public void StopMoving()
    {
        animator.SetBool("IsMoving", false);
        if (isElite)
        {
            eliteBorder.SetBool("IsMoving", false);
        }
        aiPath.canMove = false;
    }

    public void StartMoving()
    {
        animator.SetBool("IsHurt", false);
        animator.SetBool("IsMoving", true);
        if (isElite)
        {
            eliteBorder.SetBool("IsHurt", false);
            eliteBorder.SetBool("IsMoving", true);
        }
        aiPath.canMove = true;
    }

    public void TakeDamage(float damageAmount, bool isCrit)
    {
        if (health <= 0 || !spawnFinished) return;
        if (canDamage == true)
        {
            animator.SetBool("IsHurt", true);
            if (isElite)
            {
                eliteBorder.SetBool("IsHurt", true);
            }
            Vector3 popupPosition = rb.position;
            popupPosition.x = Random.Range(rb.position.x - 0.075f, rb.position.x + 0.075f);
            popupPosition.y = Random.Range(rb.position.y, rb.position.y + 0.1f);
            Vector3 modifier = transform.position;
            modifier.x = Random.Range(-0.1f, 0.1f);
            modifier.y = Random.Range(-0.1f, 0.1f);
            
            spriteRend.material = newMaterial;
            //Debug.Log(spriteRend.material);
            if (!resetMaterial)
            {
                StartCoroutine(ResetMaterial());
                resetMaterial = true;
            }

            DamagePopupText damagePopup = Instantiate(DamagePopup, popupPosition, Quaternion.identity).GetComponent<DamagePopupText>();
            Instantiate(HitEffect, transform.position + modifier, Quaternion.identity);

            //roll for DPS fudging
            float finalDamage;
            float randomValue = Random.value; // Generate a random number between 0 and 1
            if (randomValue < 0.5f) // 50% chance for case 1
            {
                finalDamage = damageAmount;
            }
            else if (randomValue < 0.75f) // 25% chance for case 2
            {
                if (damageAmount > 1)
                {
                    finalDamage = damageAmount + 1;
                }
                else
                {
                    finalDamage = damageAmount;
                }
            }
            else // 25% chance for case 3
            {
                if (damageAmount > 1)
                {
                    finalDamage = damageAmount - 1;
                }
                else
                {
                    finalDamage = damageAmount;
                }
            }


            if (!armorOn) // first hit
            {
                if (isArmor)
                {
                    armorOn = true;
                }

                if (isCrit == true)
                {
                    health -= finalDamage;
                    damagePopup.GetComponent<DamagePopupText>().Setup(finalDamage, true);
                }
                else
                {
                    health -= finalDamage;
                    damagePopup.GetComponent<DamagePopupText>().Setup(finalDamage, false);
                }

            } else //armor is now active
            {
                float armoredDamage = finalDamage - (finalDamage * armorPercent);
                health -= armoredDamage;
                damagePopup.GetComponent<DamagePopupText>().Setup(armoredDamage, false);

            }

        }

    }

    public void StartSlow(float slowPercentage, float slowDuration)
    {
        float currentPercentage = (speed / originalSpeed);
        
        if (slowPercentage > currentPercentage)
        {
            return;
        }
        else
        {
            if (slowCoroutine != null)
            {
                StopCoroutine(slowCoroutine);
            }
            slowCoroutine = StartCoroutine(SlowCoroutine(slowPercentage, slowDuration));
            if (isStunned == false)
            {
                spriteRend.color = slowColor;
            }
        }

    }

    private IEnumerator SlowCoroutine(float slowPercentage, float slowDuration)
    {
        float slowTargetSpeed;
        float slowStartTime = Time.time;
        float slowEndTime = slowStartTime + slowDuration;
        
        if (!rageTriggered)
        {
             slowTargetSpeed = originalSpeed * slowPercentage;
        } else{ 
            //rage mod being added repeatedly - fix!
             slowTargetSpeed = rageSpeed * slowPercentage;
        }
        //Debug.Log(rageSpeed);

        while (Time.time < slowEndTime)
        {
            speed = slowTargetSpeed;
            aiPath.maxSpeed = speed;
            yield return null;
        }

        if (!rageTriggered)
        {
            speed = originalSpeed;
            aiPath.maxSpeed = speed;
            spriteRend.color = OGcolor;
        }
        else
        {
            speed = rageSpeed;
            aiPath.maxSpeed = speed;
            spriteRend.color = rageColor;
            //Debug.Log(rageSpeed);
        }
        StopCoroutine(slowCoroutine);  
    }

    

    public static float EaseInQuint(float t)
    {
        return t * t * t * t * t;
    }

    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = active;
        }
    }




        private void LateUpdate()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }
        

        if (col.gameObject.name == "Player" && isDead == false && player.GetComponent<StatsHandler>().canDamage == true)
        {
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(damage);
        }
    }

    public void ApplyKnockback(float knockback, Vector3 direction)
    {
        currentForce = knockback;
        knockDirection = direction;
        duringKnockback = true;
    }


    public Vector3 GetDirection()
    {
        return directionToPlayer;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
