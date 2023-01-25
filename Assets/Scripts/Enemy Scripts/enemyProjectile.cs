using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector3 direction;
    public float maxRange;
    private Vector3 startingPosition;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(startingPosition, transform.position);
        if (distance >= maxRange)
        {
            Destroy(gameObject);
        }

        transform.position += direction * speed * Time.deltaTime;

    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == null) return;
        if (col.gameObject.tag == "Player" && Player.GetComponent<StatsHandler>().canDamage == true)
        {
            col.gameObject.GetComponent<StatsHandler>().TakeDamage(damage);
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Player" && Player.GetComponent<StatsHandler>().canDamage == false)
        {
            Destroy(gameObject);
        }
  
     
    }
}
