using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeLootHandler : MonoBehaviour, IPointerDownHandler
{
    public Upgrade upgrade;
    private AttackHandler playerAttacks;
    private StatsHandler playerStats;
    private LootBoxManager lootManager;
    public float uiDelay;
    public bool startDelay;
    public bool delayFinished;
    private float timer; // make timer a class member variable

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerAttacks = player.GetComponent<AttackHandler>();
        playerStats = player.GetComponent<StatsHandler>();
        lootManager = GameObject.FindObjectOfType<LootBoxManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (delayFinished)
        {
            if (upgrade.GetUpgradeType() == UpgradeType.Player)
            {
                playerStats.AddStat((PlayerCharacterStats)upgrade);
                lootManager.SignalItemChosen();
            }
            else
            {
                if (playerAttacks.attacks.Count < 6)
                {
                    playerAttacks.AddWeapon((Attack)upgrade);
                    lootManager.SignalItemChosen();
                }
                else
                {
                    return;
                }
            }
        }
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
