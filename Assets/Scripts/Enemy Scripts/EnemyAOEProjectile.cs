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

    public float AOEChargeTime;
    public float activeTimer;
    public float recoveryTimer;

    private float currentTimer;
    private bool startupPhase = true;
    private bool activePhase = false;
    private bool recoveryPhase = false;
    private bool damagePlayer;
    private GameObject spriteObject;
    private GameObject newSpriteObject;
    private Vector3 originalScale;




    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Player = GameObject.FindWithTag("Player");

        currentTimer = AOEChargeTime;
        spriteObject = transform.GetChild(0).gameObject;
        originalScale = spriteObject.transform.localScale;

        newSpriteObject = Instantiate(spriteObject, transform.position, Quaternion.identity, transform);
        newSpriteObject.transform.parent = transform;
        newSpriteObject.transform.localScale = new Vector3(0, 0, 0);
        newSpriteObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.2f, 0.8f);
        newSpriteObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }


    // Update is called once per frame
    void Update()
    {
        if (startupPhase)
        {

            // Perform startup animation actions here
            newSpriteObject.transform.localScale = Vector3.Lerp(newSpriteObject.transform.localScale, originalScale, Time.deltaTime * (1 / currentTimer));

            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = activeTimer;
                startupPhase = false;
                activePhase = true;
            }
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
            float alpha = 0.25f;
            newSpriteObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);

            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                Destroy(gameObject);
                Destroy(newSpriteObject);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && activePhase && Player.GetComponent<StatsHandler>().canDamage == true)
        {
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(damage);
        }
    }

}


