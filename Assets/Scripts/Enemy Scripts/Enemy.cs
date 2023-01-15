using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float xpAmount;

    public bool canDamage;
    public GameObject DamagePopup;

    public float Iframes;
    float timer = 0f;
    public bool isInvuln;

    GameObject ComboManager;

    public Animator animator;
    public GameObject Sprite;
    public float disappearSpeed;
    // public float shinySpeed;
    public bool isDead;
    Color color;
    Color OGcolor;



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
        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;
        color = Sprite.GetComponent<SpriteRenderer>().color;
        Sprite.GetComponent<SpriteRenderer>().color = OGcolor;

    }


    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (health <= 0)
        {

            isDead = true;
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

        if (isMelee == true) //melee 
        {
            animator.SetFloat("Distance", distance);
            if (distance <= stopDistance)
            {
                StopMoving();
                transform.LookAt(player.transform);
                transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                animator.SetBool("IsMoving", false);
            }
            else
            {
                StartMoving();
                MoveEnemy();
                animator.SetBool("IsMoving", true);
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
                MoveEnemy();
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

    private void MoveEnemy()
    {
        if (canMove == true)
        {
            //rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed


        }
    }

    public void StopMoving()
    {
        //rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * 0;
        canMove = false;
        //Vector3 position = player.transform.position - transform.position;
       // position.z = 0;
    }

    public void StartMoving()
    {
        canMove = true;
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
 
            DamagePopupText damagePopup = Instantiate(DamagePopup, popupPosition, Quaternion.identity).GetComponent<DamagePopupText>();
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
