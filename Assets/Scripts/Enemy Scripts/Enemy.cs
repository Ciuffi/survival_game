using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour, Attacker
{
    Rigidbody2D rb;
    GameObject player;
    public Vector3 directionToPlayer;
    public Vector3 localScale;
    public Vector3 knockDirection;
    public float damage;
    public float health;
    public float maxHealth;
    public float xpAmount;
    public float weight; //for knockback
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
    public float armorTimer;
    private float armorTime;
    public float armorPercent; //percentile damage reduction between 0 (no reduction) and 1 (full block)
    private bool armorOn;
    public float weightIncrease; //increased knockback resistance during armor
    public float newWeight;
    public float oldWeight;

    private float currentForce;
    private bool duringKnockback;


    public bool canDamage;
    public GameObject DamagePopup;
    public GameObject HitEffect;
    public GameObject DeathEffect;
    public GameObject EXPdrop;

    public float Iframes;
    float timer = 0f;
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
    private float speed;
    private float originalSpeed;
    private Coroutine slowCoroutine;

    public bool isRage;
    public float rageTriggerPercent;
    private bool rageTriggered = false;
    public float rageSpeedMod = 1.6f;
    public float rageDmgMod = 2.5f;

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

    public GameObject projectilePrefab;
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

    public GameObject AOEPrefab;


    public GameObject dangerSign;
    SpriteRenderer dangerRenderer;

    private Vector3 magnetTarget;
    private float magnetStrength;
    private float magnetDuration;
    private float magnetStartTime;

    private float animSpeed;
    private bool isSlowing = false;
    private float currentSlowPercentage;
    private float magnetMinDistance = 2f;


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

        stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);

        canDamage = true;
        isDead = false;

        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;
        color = Sprite.GetComponent<SpriteRenderer>().color;
        Sprite.GetComponent<SpriteRenderer>().color = OGcolor;

        aiPath = GetComponent<AIPath>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();

        defaultMaterial = spriteRend.material;
        dangerRenderer = dangerSign.GetComponent<SpriteRenderer>();
        armorTime = armorTimer;

        speed = aiPath.maxSpeed;
        originalSpeed = speed;
        animSpeed = animator.GetFloat("speed");

    }

    public void StartMagnet(float strength, float duration, Vector3 targetPos)
    {
        magnetStrength = strength;
        magnetDuration = duration;
        magnetTarget = targetPos;
        magnetStartTime = Time.time;
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

    private IEnumerator SlowCoroutine(float slowActiveSpeed, float slowPercentage, float slowDuration)
    {
        float slowStartSpeed = speed;
        float slowTargetSpeed = originalSpeed * slowPercentage;
        float slowStartTime = Time.time;
        float slowEndTime = slowStartTime + slowDuration;

        while (Time.time < slowEndTime)
        {
            float t = (Time.time - slowStartTime) / slowDuration;
            t = EaseInOutCubic(t);
            speed = Mathf.Lerp(slowStartSpeed, slowTargetSpeed, t * slowActiveSpeed);
            aiPath.maxSpeed = speed;
            yield return null;
        }


        speed = originalSpeed;
        aiPath.maxSpeed = speed;
        StopCoroutine(slowCoroutine);
    }

    public void StartSlow(float slowActiveSpeed, float slowPercentage, float slowDuration)
    {
        if (slowCoroutine != null)
        {
            float currentPercentage = (speed / originalSpeed);
            if (slowPercentage < currentPercentage)
            {
                return;
            }
            else
            {
                StopCoroutine(slowCoroutine);
                slowCoroutine = StartCoroutine(SlowCoroutine(slowActiveSpeed, slowPercentage, slowDuration));

            }
        }
    }


    void Update()
    {
        //magnetizing effect
        if (magnetDuration > 0 && Time.time < magnetStartTime + magnetDuration)
        {
            float magDistance = Vector3.Distance(transform.position, magnetTarget);
            if (magDistance > magnetMinDistance)
            {
                float t = (Time.time - magnetStartTime) / magnetDuration;
                t = EaseInOutCubic(t);
                transform.position = Vector3.Lerp(transform.position, magnetTarget, t * magnetStrength * Time.deltaTime);
            }
            else
            {
                magnetDuration = 0;
            }
        }
        else
        {
            magnetDuration = 0;
        }


        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        center = GetComponent<SpriteRenderer>().bounds.center;

        if (health <= 0 && !isDead)
        {

            isDead = true;
            GameObject xpDrop = Instantiate(EXPdrop, center, Quaternion.identity);
            xpDrop.GetComponent<EXPHandler>().xpAmount = xpAmount;

            Instantiate(DeathEffect, center, Quaternion.identity);

            ComboManager.GetComponent<ComboTracker>().IncreaseCount(1);
            ComboManager.GetComponent<ScreenShakeController>().StartShake(0.25f, 0.2f, 5f);
        }

        if (health <= 0)
        {
            animator.SetBool("IsDead", true);
            SetAllCollidersStatus(false);
            color = Sprite.GetComponent<SpriteRenderer>().color;
            Sprite.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -disappearSpeed * Time.deltaTime);


        }

        if (color.a <= 0)
        {
            Destroy(gameObject);
        }


        directionToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);
   

        //rage enemies
        if (isRage == true)
        {
            if (health / maxHealth <= rageTriggerPercent && !rageTriggered)
            {
                aiPath.maxSpeed *= rageSpeedMod;
                animator.speed *= 1.5f;
                damage *= rageDmgMod;
                animator.SetBool("IsRage", true);
                spriteRend.color = new Color (rageColor.r,rageColor.g,rageColor.b);
                rageTriggered = true;
            }

        } 


        //active actions - melee or ranged
        if (duringKnockback)
        {
            StopMoving();
            currentForce = Mathf.Lerp(currentForce, 0, weight * Time.deltaTime);
            transform.position += knockDirection * currentForce * Time.deltaTime;
            if (currentForce <= 1)
            {
                duringKnockback = false;
                StartMoving();

            }
        }else{

            if (isMelee == true) //melee 
            {
                animator.SetFloat("Distance", distance);

                if (!isDash && !isArmor)
                {
                    if (distance <= stopDistance)
                    {
                        StopMoving();
                        animator.SetBool("IsMoving", false);
                        transform.LookAt(player.transform);
                        transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                    }
                    else
                    {
                        StartMoving();
                        animator.SetBool("IsMoving", true);
                    }

                } else if (!isDash && isArmor) //armored enemies
                {

                    if (armorOn) //currently armored
                    {
                        animator.SetBool("IsArmored", true);
                        StopMoving();
                        armorTime -= Time.deltaTime;
                        weight = newWeight;
                        
                        if (armorTime <= 0)
                        {
                            animator.SetBool("IsArmored", false);
                            StartMoving();
                            weight = oldWeight;
                            armorOn = false;
                            armorTime = armorTimer;
                        }
                    }else //walk to enemy
                    {
                        if (distance <= stopDistance)
                        {
                            StopMoving();
                            animator.SetBool("IsMoving", false);
                            transform.LookAt(player.transform);
                            transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                        }
                        else
                        {
                            StartMoving();
                            animator.SetBool("IsMoving", true);
                        }
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
                        animator.SetBool("IsMoving", false);
                        StartCoroutine(ProjectileAttack());
                    }

                    if (recovering && !canAttack)
                    {
                    animator.SetBool("IsAttacking", false);
                    animator.SetBool("FollowThrough", false);
                        shootRecoveryTimer -= Time.deltaTime;
                        if (shootRecoveryTimer <= 0)
                        {
                            recovering = false;
                            attacking = false;
                            StartMoving();
                            animator.SetBool("IsMoving", true);
                            canAttack = true;

                        }
                    }

                }else{

                    //AOE attack 
                    if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && canAttack)
                    {
                        attacking = true;
                        canAttack = false;
                        StopMoving();
                        animator.SetBool("IsMoving", false);
                        StartCoroutine(RangedAOEAttack());
                    }

                    if (recovering && !canAttack)
                    {
                        animator.SetBool("IsAttacking", false);
                        animator.SetBool("FollowThrough", false);
                        shootRecoveryTimer -= Time.deltaTime;
                        if (shootRecoveryTimer <= 0)
                        {
                            recovering = false;
                            attacking = false;
                            StartMoving();
                            animator.SetBool("IsMoving", true);
                            canAttack = true;

                        }
                    }
                }
            }
        }
       


        //Iframes
        if (isInvuln == true)
        {
            timer += Time.deltaTime;
            if (timer <= Iframes)
            {
                canDamage = false;
                
            }
            else
            {
                canDamage = true;
                timer = 0f;
                isInvuln = false;
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
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsAttacking", true);
        dangerRenderer.enabled = true;
        
        yield return new WaitForSeconds(shootChargeTime - 0.09f);
        animator.SetBool("FollowThrough", true);
        yield return new WaitForSeconds(0.09f);

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        projectile.GetComponent<enemyProjectile>().direction = direction;
        projectile.GetComponent<enemyProjectile>().speed = projectileSpeed;
        projectile.GetComponent<enemyProjectile>().maxRange = projectileRange;
        projectile.GetComponent<enemyProjectile>().damage = projectileDamage;
        shootRecoveryTimer = shootRecovery;
        recovering = true;

        dangerRenderer.enabled = false;

    }

    IEnumerator RangedAOEAttack()
    {
        StopMoving();
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsAttacking", true);
        dangerRenderer.enabled = true;
        Vector3 pos = player.transform.position;
        GameObject AOE = Instantiate(AOEPrefab, pos, Quaternion.identity);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        AOE.GetComponent<EnemyAOEProjectile>().damage = projectileDamage;
        AOE.GetComponent<EnemyAOEProjectile>().AOEChargeTime = shootChargeTime;


        yield return new WaitForSeconds(shootChargeTime - 0.09f);
        animator.SetBool("FollowThrough", true);
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
        aiPath.canMove = false;
    }

    public void StartMoving()
    {
        animator.SetBool("IsHurt", false);
        aiPath.canMove = true;
    }


    public void TakeDamage(float damageAmount, bool isCrit)
    {
        if (health <= 0) return;
        if (canDamage == true)
        {
            animator.SetBool("IsHurt", true);
            Vector3 popupPosition = rb.position;
            popupPosition.x = Random.Range(rb.position.x - 0.075f, rb.position.x + 0.075f);
            popupPosition.y = Random.Range(rb.position.y, rb.position.y + 0.1f);
            Vector3 modifier = transform.position;
            modifier.x = Random.Range(-0.1f, 0.1f);
            modifier.y = Random.Range(-0.1f, 0.1f);

            spriteRend.material = newMaterial;
            if (!resetMaterial)
            {
                StartCoroutine(ResetMaterial());
                resetMaterial = true;
            }

            DamagePopupText damagePopup = Instantiate(DamagePopup, popupPosition, Quaternion.identity).GetComponent<DamagePopupText>();
            Instantiate(HitEffect, transform.position + modifier, Quaternion.identity);

            if (!armorOn) // first hit
            {
                if (isArmor)
                {
                    armorOn = true;
                }

                if (isCrit == true)
                {
                    health -= damageAmount;
                    damagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, true);
                }
                else
                {
                    health -= damageAmount;
                    damagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, false);
                }

            } else //armor is now active
            {
                float armoredDamage = damageAmount - (damageAmount * armorPercent);
                health -= armoredDamage;
                damagePopup.GetComponent<DamagePopupText>().Setup(armoredDamage, false);

            }

        }

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
        if (col.gameObject.tag == "Attack" && col.GetComponent<Projectile>().attack.owner.GetTransform().name == "Player")
        {
            Vector3 colCenter = col.GetComponent<Projectile>().startPos;

            knockDirection = center - colCenter;

            ApplyKnockback(col.gameObject.GetComponent<Projectile>().knockback, knockDirection);
            //rb.AddForce(knockDirection.normalized * col.gameObject.GetComponent<Projectile>().knockback);
        }

        if (col.gameObject.name == "Player" && isDead == false && player.GetComponent<StatsHandler>().canDamage == true)
        {
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(damage);
        }
    }

    public void ApplyKnockback(float knockback, Vector3 direction)
    {
        currentForce = knockback;
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
