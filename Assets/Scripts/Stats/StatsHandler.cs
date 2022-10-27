using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public float speed;
    public float health;
    public float damageMultipler;
    public float defense;
    public List<StatBoost> stats;


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
