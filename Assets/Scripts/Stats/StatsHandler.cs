using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    public int level;
    public float xp;
    public float nextXp;

    public float health;
    public float maxHealth = 20;
    public float speed;
    public float damageMultipler;
    public float defense;
    public List<StatBoost> stats;

    public LevelUpManager LevelManager;
    private Slider healthBar;
    private CoroutineQueue healthBarQueue;



    public void TakeDamage(float damageAmount)
    {
        healthBarQueue.AddToQueue(BarHelper.RemoveFromBar(healthBar, health, health - damageAmount, maxHealth, 0.5f));
        health -= damageAmount;
    }


    public void GainXP(float xpAmount)
    {
        LevelManager.AddXP(xp, xpAmount + xp, nextXp);
        xp += xpAmount;
        if (xp >= nextXp)
        {
            nextXp = LevelManager.GetXpToNextLevel(level);
            LevelUp();
        }
    }


    public void LevelUp()
    {
        xp = 0;
        level = level + 1;
        LevelManager.LevelUp(level);
    }

    void Start()
    {
        level = 1;
        xp = 0;
        LevelManager = GameObject.FindObjectOfType<LevelUpManager>();
        nextXp = LevelManager.GetXpToNextLevel(level);
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBarQueue = gameObject.AddComponent<CoroutineQueue>();
        healthBarQueue.StartQueue();
        health = maxHealth;
    }
}

