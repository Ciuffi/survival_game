using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAOEProjectile : MonoBehaviour
{

    public float damage;

    //public float speed;
    //public Vector3 direction;
    //public float maxRange;

    private Vector3 startingPosition;
    public GameObject Player;
    public GameObject caster;

    public float AOEChargeTime;
    public float activeTimer;
    public float recoveryTimer;

    private float currentTimer;
    private bool startupPhase = true;
    private bool activePhase = false;
    private bool recoveryPhase = false;
    public GameObject spriteFill;
    private GameObject newSpriteObject;
    private Vector3 originalScale;




    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Player = GameObject.FindWithTag("Player");

        currentTimer = AOEChargeTime;
        originalScale = spriteFill.transform.localScale;

        newSpriteObject = Instantiate(spriteFill, transform.position, Quaternion.identity, transform);
        newSpriteObject.transform.parent = transform;
        newSpriteObject.transform.localScale = new Vector3(0, 0, 0);
        newSpriteObject.GetComponent<SpriteRenderer>().color *= new Color(1, 0, 0.2f, 0.30f);
        newSpriteObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }


    // Update is called once per frame
    void Update()
    {
        if (caster.GetComponent<Enemy>().isDead == true)
        {
            Destroy(gameObject);
        }

        if (startupPhase)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = activeTimer;
                startupPhase = false;
                activePhase = true;
            }

            // Perform startup animation actions here
            newSpriteObject.transform.localScale = Vector3.Lerp(newSpriteObject.transform.localScale, originalScale, Time.deltaTime * (1 / currentTimer));

        }
        else if (activePhase)
        {
            //can deal damage
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = recoveryTimer;
                activePhase = false;
                recoveryPhase = true;
            }
        }
        else if (recoveryPhase)
        {
            // Perform recovery actions here
            float alpha = 0f;
            newSpriteObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);

            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                Destroy(gameObject);
                Destroy(newSpriteObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == ("Player") && activePhase)
        {
            //Debug.Log("woo");
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(damage);
        }
    }

}



