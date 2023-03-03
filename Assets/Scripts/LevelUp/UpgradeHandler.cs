using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour, IPointerDownHandler
{
    public Upgrade upgrade;
    private AttackHandler playerAttacks;
    private StatsHandler playerStats;
    private LevelUpManager levelUpManager;

    public float uiDelay;
    public bool startDelay;
    public bool delayFinished;
    private float timer; // make timer a class member variable

    public void OnPointerDown(PointerEventData eventData)
    {
        if (delayFinished)
        {
            if (upgrade.GetUpgradeType() == UpgradeType.StatBoost)
            {

                GameObject newStat = Instantiate(upgrade.GetTransform().gameObject, playerStats.transform.position, Quaternion.identity);
                playerStats.AddStat(newStat);
                levelUpManager.SignalItemChosen();


            }
            else
            {
                if (playerAttacks.attacks.Count < 6)
                {
                    GameObject newWeapon = Instantiate(upgrade.GetTransform().gameObject, playerAttacks.transform.position, Quaternion.identity);
                    playerAttacks.AddWeapon(newWeapon);
                    levelUpManager.SignalItemChosen();

                }
                else
                {
                    return;
                }
            }
        }
    }



    // Start is called before the first frame update
    void Start()
        {
            GameObject player = GameObject.Find("Player");
            playerAttacks = player.GetComponent<AttackHandler>();
            playerStats = player.GetComponent<StatsHandler>();
            levelUpManager = GameObject.FindObjectOfType<LevelUpManager>();
        }


    // Update is called once per frame
    void Update()
    {
        if (startDelay)
        {
            timer += Time.unscaledDeltaTime; // increment timer each frame
            if (timer >= uiDelay)
            {
                startDelay = false;
                delayFinished = true;
            }
        }
    }

    public void setActive()
    {
        timer = 0f; // reset timer when panel is set active
        startDelay = true;
        delayFinished = false;
    }

}

