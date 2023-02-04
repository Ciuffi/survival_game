using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWpnAttack : MonoBehaviour
{
    GameObject Player;
    GameObject Camera;

    public float damage;
    public float knockback;
    public float critChance;
    public float critDmg;

    public GameObject onHitParticle;
    public float playerShakeTime, playerShakeStrength, playerShakeRotation;

    public float damageTickDuration;
    private List<GameObject> hitEnemies;
    private Dictionary<GameObject, float> timers;
    float critRoll;
    float finalDamage;
    bool isCrit;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        critChance = critChance + Player.GetComponent<StatsHandler>().critChance;
        critDmg = critDmg + Player.GetComponent<StatsHandler>().critDmg;

        hitEnemies = new List<GameObject>();
        timers = new Dictionary<GameObject, float>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hitEnemies.Count > 0)
        {
            foreach (GameObject enemy in hitEnemies)
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
            }
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

                if (isCrit == true)
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


            }
            else
            {
                return;
            }
        }
    }
}
