using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    public int level;
    public float xp;
    public float nextXp;

    public float health;
    public float speed;
    public float damageMultipler;
    public float defense;
    public List<StatBoost> stats;

    public GameObject LevelManager;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }


    public void GainXP(float xpAmount)
    {
        xp += xpAmount;
    }


    public void LevelUp()
    {
        xp = 1;
        level = level + 1;
        LevelManager.GetComponent<LevelUpManager>().LevelUp();
    }

    void Start()
    {
        level = 0;
        xp = 1;
        nextXp = LevelManager.GetComponent<LevelUpManager>().toLevelUp[level];
    }


    private void FixedUpdate()
    {
        nextXp = LevelManager.GetComponent<LevelUpManager>().toLevelUp[level];
        if (xp >= nextXp)
        {
            LevelUp();
        }
    }
}

