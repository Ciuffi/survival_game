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
    public float moveSpeed = 3f;
    public float damage;

    float stopDistance;
    public float stopDistanceMin;
    public float stopDistanceMax;
    public float moveDistanceCheck;
    public bool canMove;
    public float timeCheck;
    float elapsedTime;

    public bool isMelee;

    public float health;
    public float maxHealth;
    public float xpAmount;

    public bool canDamage;
    public GameObject DamagePopup;
    public GameObject HitEffect;

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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        ComboManager = GameObject.FindWithTag("ComboManager");

        localScale = transform.localScale;
        canMove = true;

        stopDistance = Random.Range(stopDistanceMin, stopDistanceMax);

        canDamage = true;
        isDead = false;
        maxHealth = health;

        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;
        color = Sprite.GetComponent<SpriteRenderer>().color;
        Sprite.GetComponent<SpriteRenderer>().color = OGcolor;

        aiPath = this.GetComponent<AIPath>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();

        defaultMaterial = spriteRend.material;
    }


    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (health <= 0)
        {

            isDead = true;
            animator.SetBool("IsDead", true);
            SetAllCollidersStatus(false);
            Sprite.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -disappearSpeed * Time.deltaTime);
            color = Sprite.GetComponent<SpriteRenderer>().color;

        }

        if (color.a <= 0)
        {
            player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
            Destroy(gameObject);
        }


        directionToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);


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

        if (isMelee == true) //melee 
        {
            animator.SetFloat("Distance", distance);

            if (isDash == false)
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
            } else if (isDash == true) // does dash
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
            if (distance <= stopDistance)
            {
                StopMoving();
                animator.SetBool("IsMoving", false);
            }
            else
            {
                StartMoving();
                animator.SetBool("IsMoving", true);
            }
        }
       


        //Iframes
        if (isInvuln == true)
        {
            timer += Time.deltaTime;
            if (timer <= Iframes)
            {
                canDamage = false;
                animator.SetBool("IsHurt", true);
            }
            else
            {
                canDamage = true;
                timer = 0f;
                isInvuln = false;
                animator.SetBool("IsHurt", false);
            }
        }



    }
    IEnumerator Charge()
    {
        yield return new WaitForSeconds(chargeTime);
        isCharging = false;
        isDashing = true;
  
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
        aiPath.canMove = true;
    }

    public void TakeDamage(float damageAmount, bool isCrit)
    {
        if (health <= 0) return;
        if (canDamage == true)
        {
            health -= damageAmount;
            StopMoving();
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

            if (isCrit == true)
            {
                damagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, true);
            }
            else
            {
                damagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, false);
            }

        }
        else
        {

        }

    }


    public void damageTickCounter(float damageTickCD)
    {
        //turn timer on
        isInvuln = true;
        //damageTickCD =  

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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack" && col.GetComponent<Projectile>().attack.owner.GetTransform().name == "Player")
        {
            knockDirection = rb.transform.position - col.gameObject.transform.position;

            rb.AddForce(knockDirection.normalized * col.gameObject.GetComponent<Projectile>().knockback);
        }
        if (col.gameObject.name == "Player" && isDead == false && player.GetComponent<StatsHandler>().canDamage == true)
        {
            col.GetComponent<StatsHandler>().TakeDamage(damage);
        }
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
